using System;
using Characters.Movement;
using Combat;
using Events.ScriptableObjects;
using Events.ScriptableObjects.FieldOfView;
using Events.ScriptableObjects.Pathfinding;
using FieldOfView;
using Level.Grid;
using System.Collections.Generic;
using System.Linq;
using Characters.Types;
using GDP01._Gameplay.Provider;
using GDP01.Characters.Component;
using GDP01.World.Components;
using UnityEngine;
using Util;
using GDP01.TileEffects;
using Ability;

namespace Characters.EnemyCharacter
{
		public class AIController : MonoBehaviour
		{
				[Header("Sending Events On:")]
				[SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
				[SerializeField] private FOVCrossQueryEventChannelSO fieldOfViewCrossQueryEvent;
				[SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEvent;
				[SerializeField] private FOVQueryEventChannelSO fieldOfViewQueryEvent;

				public Targetable aiTarget; // player, they want to attack
				public PathNode closestNodeToTarget;

				// these could just be saved in the respective controllers, 
				// but for debug reasons I think they can stay here, 
				// so we can see what the AI tries to do
				public PathNode movementTarget; // position they want to reach
				public int selectedAbility;
				
				[SerializeField] private List<Tuple<AbilitySO, List<Vector3Int>>> validAbilitiesWithRange;

				private AbilitySO currentAbility; // used to determine what ability a view query is for 

				private EnemyBehaviorSO behavior;
				[Header("References to other Components of Enemy Character: ")]
				[SerializeField] private GridTransform _gridTransform;
				[SerializeField] private Statistics _statistics;
				[SerializeField] private MovementController _movementController;
				[SerializeField] private AbilityController _abilityController;
				[SerializeField] private Attacker _attacker;

				public void SetBehavior(EnemyBehaviorSO behavior) { this.behavior = behavior; }
				public EnemyBehaviorSO GetBehavior() { return behavior; }

				private void Awake()
				{
						ClearFullCache();
				}

				public void ClearPartsOfCache()
				{
						selectedAbility = -1;
						
						validAbilitiesWithRange = new List<Tuple<AbilitySO, List<Vector3Int>>>();
				}

				public void ClearFullCache()
				{
						aiTarget = null;
						// enemyTarget = null;
						movementTarget = null;
						selectedAbility = -1;

						validAbilitiesWithRange = new List<Tuple<AbilitySO, List<Vector3Int>>>();
				}

				public void TargetClosestVisiblePlayer(List<PathNode> visibleTiles) {
					pathfindingQueryEvent.RaiseEvent(_gridTransform.gridPosition, behavior.rangeOfInterestMovement,
						list => {
							var visibleTargetsInRange = list.FindAll(node => visibleTiles.Any(pathNode => pathNode.pos == node.pos));
							SaveClosestPlayerAsTarget(visibleTargetsInRange);
						});
				}

				private void SaveClosestPlayerAsTarget(List<PathNode> pathNodes) {
					aiTarget = null;

					for ( int i = 0; !aiTarget && i < pathNodes.Count; i++ ) {
						List<PlayerCharacterSC> playerCharacters = 
							GameplayProvider.Current.CharacterManager.GetPlayerCharacters();
						
						foreach ( var player in playerCharacters ) {
							Targetable playerTargetable = player.GetComponent<Targetable>();

							float distanceBetweenPlayerAndTile = !playerTargetable
								? Int32.MaxValue
								: ( pathNodes[i].pos - playerTargetable.GetGridPosition() ).magnitude;

							if ( distanceBetweenPlayerAndTile < 1.001 ) {
								aiTarget = playerTargetable;
								closestNodeToTarget = pathNodes[i];
								// set also in attacker component
								_attacker.SetTarget(aiTarget);
							}
						}
					}
				}

				public void TargetEnemyWithLowestHealth(List<PathNode> visibleTiles) {
						Targetable lowestHealthTarget = null;
						float lowestHealth = 1.0f;

						var enemys = GameplayProvider.Current.CharacterManager.GetEnemyCahracters();
						
						foreach(var enemy in enemys) {
								Vector3Int pos = enemy.GridPosition;

								if(visibleTiles.Any(node => node.pos.Equals(pos))) {
										Statistics stats = enemy.GetComponent<Statistics>();
										float health = stats.StatusValues.HitPoints.InPercent;
										if (health < lowestHealth) {
												Debug.Log($"Enemy in sight with only {health * 100}% health. ");
												lowestHealth = health;
												lowestHealthTarget = enemy.GetComponent<Targetable>();
										}
								}
						}
						
						if(lowestHealthTarget) {
								PathNode closest = null;
								float minDistance = (_gridTransform.gridPosition - lowestHealthTarget.GetGridPosition()).magnitude;

								foreach(PathNode reachableNode in _movementController.reachableTiles) {
										float distance = (reachableNode.pos - lowestHealthTarget.GetGridPosition()).magnitude;
										if(distance < minDistance) {
												minDistance = distance;
												closest = reachableNode;
										}
								}

								if ( closest != null ) {
										closestNodeToTarget = closest;
										aiTarget = lowestHealthTarget;
								}
						}
				}

				// targets the nearest reachable tile towards the targeted player
				// the tile the enemy is standing on will not be recognized as reachable, it will be skipped, 
				// so if the enemy can't move another tile, movement target will be set to null
				public void TargetNearestTileToTarget()
				{
						if(aiTarget)
								pathfindingPathQueryEvent.RaiseEvent(_gridTransform.gridPosition, closestNodeToTarget.pos, SaveClosestTileToTargetAsMovementTarget);
				}

				private void SaveClosestTileToTargetAsMovementTarget(List<PathNode> pathNodes)
				{
						if ( pathNodes == null || pathNodes.Count <= 1 )
								return;

						int lastAffordableStep = 0;

						while ( lastAffordableStep < pathNodes.Count - 1 &&
								pathNodes[lastAffordableStep + 1].dist <= _movementController.GetMaxMoveDistance() &&
								Vector3.Distance(pathNodes[lastAffordableStep + 1].pos, aiTarget.GetGridPosition()) > behavior.keepDistance)
								lastAffordableStep++;

						// if the only affordable step is the enemies position, set target to null
						// also if the path to target is empty or if empty is next to enemy, set target to null
						movementTarget = (lastAffordableStep >= pathNodes.Count || lastAffordableStep < 1) ? null : pathNodes[lastAffordableStep];
						_movementController.movementTarget = movementTarget;
				}

				#region New Functionality

				/// <summary>
				/// Initializes the list of abilities that can be used, regardless of targets in range. 
				/// Also saves the range of each valid ability. 
				/// </summary>
				public void RefreshValidAbilities() {
						validAbilitiesWithRange.Clear();

						foreach (AbilitySO ability in _abilityController.Abilities) {
								if ( _abilityController.IsAbilityAvailable(ability) ) { 
										// save ability in list with empty range 
										validAbilitiesWithRange.Add(new Tuple<AbilitySO, List<Vector3Int>>(ability, new List<Vector3Int>()));

										currentAbility = ability;

										if(ability.targetableTilesAreCross) {
												fieldOfViewCrossQueryEvent.RaiseEvent(_gridTransform.gridPosition, ability.conditions, SaveRangeForValidAbility);
										}
										else {
												fieldOfViewQueryEvent.RaiseEvent(_gridTransform.gridPosition, ability.range, ability.conditions, SaveRangeForValidAbility);
										}
								}
						}
				}

				/// <summary>
				/// Saves fieldOfViewQuery tiles to current ability in validAbilitiesWithRange
				/// </summary>
				/// <param name="tilesInRange">Tiles in range </param>
				public void SaveRangeForValidAbility(bool[,] tilesInRange) {
						foreach ( Tuple<AbilitySO, List<Vector3Int>> abilityRangePair in validAbilitiesWithRange ) {
								if ( abilityRangePair.Item1.Equals(currentAbility) ) {
										abilityRangePair.Item2.AddRange(
												PathNode.ConvertPathNodeListToVector3IntList(FieldOfViewController.VisibleTilesToPathNodeList(tilesInRange)));
								}
						}
						currentAbility = null;
				}

				/*
				private bool IsPlayerVisible() {
						return CombatUtils.FindAllTargets(PathNode.ConvertPathNodeListToVector3IntList(_attacker.visibleTiles), _attacker, TargetRelationship.Enemy).Count > 0;
				}
				*/

				/// <summary>
				/// Checks, whether a player is in viewing range. 
				/// </summary>
				/// <returns>True, if viewing range is greater or equal than the distance to a player </returns>
				public bool OutOfSight() {
						List <PlayerCharacterSC> players = GameplayProvider.Current.CharacterManager.GetPlayerCharacters();
						bool playerInRange = players.Exists(player => ( player.transform.position - transform.position ).magnitude <= _statistics.StatusValues.ViewDistance.Value);
						if ( !playerInRange )
								Debug.Log("No player in range. ");
						return !playerInRange;
				}
				

				/// <summary>
				/// Goes through all valid abilities, calculates the damage output for every 
				/// possible target and if any has more damage than 0, chooses the ability. 
				/// Returns whether or not an ability has been choosen. 
				/// </summary>
				/// <returns>True if ability has been choosen </returns>
				public bool ChooseAbilityWithHighestDamageOutput() {
						return ChooseAbilityWithHighestOutput(true);
				}

				/// <summary>
				/// Goes through all valid abilities, calculates the healing output for every 
				/// possible target and if any has more healing than 0, chooses the ability. 
				/// Returns whether or not an ability has been choosen. 
				/// </summary>
				/// <returns>True if ability has been choosen </returns>
				public bool ChooseAbilityWithHighestHealingOutput() {
						return ChooseAbilityWithHighestOutput(false);
				}

				public bool ChooseAbilityWithHighestOutput(bool outputIsDamage) {
						float bestScoring = 0; // defines how good an ability use is according to the ai 
						float scoringThreshold = 1f; // defines how good an ability has to be to be used 
						AbilitySO bestAbility = null;
						Vector3Int bestTargetPos = Vector3Int.zero;
						Targetable bestTarget = null;

						foreach(Tuple<AbilitySO, List<Vector3Int>> abilityRangePair in validAbilitiesWithRange) {
								foreach(Vector3Int possibleTarget in abilityRangePair.Item2) {
										AbilitySO ability = abilityRangePair.Item1;
										Targetable target = Targetable.GetTargetsWithPosition(possibleTarget);
										
										if (ability.targets.HasFlag(TargetRelationship.Ground) ||
												AbilityController.HasRightRelationshipForAbility(ability, _attacker, target) ) {

												// if damage is supposed to be maximized, calculate virtual damage
												// if healing is supposed to be maximized, calculate *actual* healing
												float scoring;
												if (outputIsDamage)
														scoring = CombatUtils.GetCumulatedDamageOnFactionInArea(possibleTarget, ability, _attacker, Faction.Player, false, true, _attacker.visibleTiles);
												else
														scoring = -CombatUtils.GetCumulatedDamageOnFaction(possibleTarget, ability, _attacker, Faction.Enemy, true);

												// consider tile effects
												scoring += CalculateWorthOfTileEffect(ability, possibleTarget, outputIsDamage);

												// damage should be maximized, healing should be minimized (most negative healing is best)
												if (scoring > bestScoring) {
														bestScoring = scoring;
														bestAbility = ability;
														bestTargetPos = possibleTarget;
														bestTarget = target;
														
														Debug.Log($"Ability {ability.name} on {possibleTarget} would deal {scoring} {(outputIsDamage ? "damage" : "healing")} (effective). ");
												}
										}
								}
						}

						if(bestScoring >= scoringThreshold) {
								_abilityController.SelectedAbilityID = bestAbility.id;
								_abilityController.abilitySelected = true;
								_abilityController.abilityConfirmed = true;

								_attacker.SetGroundTarget(bestTargetPos);
								_attacker.SetTarget(bestTarget);

								return true;
						}
						else
								return false;
				}

				private float CalculateWorthOfTileEffect(AbilitySO ability, Vector3Int target, bool offensive) {
						float worth = 0;
						float distanceBias = 0.01f; // this much is substracted from the scoring 
																				// for each unit of distance between the targeted tile and the targeted character (1% substracted for each unit of distance) 
						
						foreach(TargetedEffect effect in ability.targetedEffects) {
								if(effect.tileEffect) {
										TileEffectController tileEffect = effect.tileEffect.GetComponent<TileEffectController>();

										if (tileEffect) {
												List<Vector3Int> affectedTiles = effect.area.GetTargetedTiles(target, _attacker.GetRotationsToTarget(target));
												List<Targetable> targetsInArea = offensive ? CombatUtils.FindAllTargets(affectedTiles, _attacker, TargetRelationship.Enemy) :
																																		 CombatUtils.FindAllTargets(affectedTiles, _attacker, TargetRelationship.Ally);

												// if offensive, ignore the ones that aren't in sight
												if ( offensive )
														targetsInArea.RemoveAll(targetable => !_attacker.visibleTiles.Exists(node => node.pos.Equals(targetable.GetGridPosition())));

												int worthPerTarget = offensive ? tileEffect.worthAgainstPlayerPerTarget : tileEffect.worthForEnemyPerTarget;

												// for each target in the area, add something to the score
												foreach(Targetable targetCharacter in targetsInArea) {
														float worthForTarget = worthPerTarget * ( 1 - distanceBias * Vector3Int.Distance(targetCharacter.GetGridPosition(), target) );

														// addition to score should not be negative 
														worth += Mathf.Max(0, worthForTarget);
												}
										}
								}
						}

						return worth;
				}

				#endregion

				#region Utils/Helpers that belong elsewhere

				private bool IsAffordable(AbilitySO ability)
				{
						return _statistics.StatusValues.Energy.Value >= ability.costs;
				}

				// copied from HasValidTargetSO
				private bool HasRightRelationshipForAbility(AbilitySO ability)
				{
						if ( !aiTarget )
								return false;

						bool isValid = false;

						Faction attackerFaction = _statistics.GetFaction();
						Faction targetFaction = aiTarget.gameObject.GetComponent<Statistics>().GetFaction();

						// Debug.Log("Target Faction: " + targetFaction.ToString());
						// Debug.Log("Attacker Faction: " + attackerFaction.ToString());

						if ( ability.targets.HasFlag( TargetRelationship.Self) )
						{
								if ( _statistics.gameObject == aiTarget.gameObject )
										isValid = true;
						}
						if ( ability.targets.HasFlag( TargetRelationship.Ally) )
						{
								if ( attackerFaction.Equals(targetFaction) &&
										 _statistics.gameObject != aiTarget.gameObject )
										isValid = true;
						}
						if ( ability.targets.HasFlag( TargetRelationship.Enemy) )
						{
								// only valid if the attacker is enemy and target is player
								// of if attacker is player and target is enemy
								if ( ( attackerFaction.Equals(Faction.Enemy) && targetFaction.Equals(Faction.Player) ) ||
										( attackerFaction.Equals(Faction.Player) && targetFaction.Equals(Faction.Enemy) ) )
										isValid = true;
						}

						return isValid;
				}

				#endregion
		}
}