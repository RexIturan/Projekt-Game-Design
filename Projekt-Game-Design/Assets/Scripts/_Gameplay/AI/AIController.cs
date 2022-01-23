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
using GDP01.Characters.Component;
using GDP01.World.Components;
using UnityEngine;
using Util;

namespace Characters.EnemyCharacter
{
		public class AIController : MonoBehaviour
		{
				[Header("Sending Events On:")]
				[SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
				[SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEvent;
				[SerializeField] private FOVQueryEventChannelSO fieldOfViewQueryEvent;

				public Targetable aiTarget; // player, they want to attack
				public PathNode closestNodeToTarget;

				// these could just be saved in the respective controllers, 
				// but for debug reasons I think they can stay here, 
				// so we can see what the AI tries to do
				public PathNode movementTarget; // position they want to reach
				public int selectedAbility;

				public List<AbilitySO> validAbilities;

				[SerializeField] private List<List<PathNode>> tilesInRangePerAbility;
				[SerializeField] private List<AbilitySO> abilityPerTilesInRange;


				private EnemyBehaviorSO behavior;
				[Header("References to other Components of Enemy Character: ")]
				[SerializeField] private GridTransform _gridTransform;
				[SerializeField] private Statistics _statistics;
				[SerializeField] private MovementController _movementController;
				[SerializeField] private AbilityController _abilityController;
				[SerializeField] private Attacker _attacker;

				public void SetBehavior(EnemyBehaviorSO behavior) { this.behavior = behavior; }

				private void Awake()
				{
						ClearFullCache();
				}

				public void ClearPartsOfCache()
				{
						selectedAbility = -1;

						validAbilities = new List<AbilitySO>();
						tilesInRangePerAbility = new List<List<PathNode>>();
						abilityPerTilesInRange = new List<AbilitySO>();
				}

				public void ClearFullCache()
				{
						aiTarget = null;
						// enemyTarget = null;
						movementTarget = null;
						selectedAbility = -1;

						validAbilities = new List<AbilitySO>();
						tilesInRangePerAbility = new List<List<PathNode>>();
						abilityPerTilesInRange= new List<AbilitySO>();
				}

				// uses pathfinding to find closest player
				public void TargetClosestPlayer()
				{
						pathfindingQueryEvent.RaiseEvent(_gridTransform.gridPosition, behavior.rangeOfInterestMovement, SaveClosestPlayerAsTarget);
				}

				public void TargetClosestVisiblePlayer(List<PathNode> visibleTiles) {
					pathfindingQueryEvent.RaiseEvent(_gridTransform.gridPosition, behavior.rangeOfInterestMovement,
						list => {
							var visibleTargetsInRange = list.FindAll(node => visibleTiles.Any(pathNode => pathNode.pos == node.pos));
							SaveClosestPlayerAsTarget(visibleTargetsInRange);
						});
				}

				private void SaveClosestPlayerAsTarget(List<PathNode> pathNodes)
				{
						aiTarget = null;

						GameObject characterListObj = GameObject.Find("Characters");
						CharacterList characterList = null;
						if ( characterListObj )
								characterList = characterListObj.GetComponent<CharacterList>();

						if ( characterList )
						{
								for(int i = 0; !aiTarget && i < pathNodes.Count; i++)
								{
										foreach(GameObject player in characterList.playerContainer)
										{
												Targetable playerTargetable = player.GetComponent<Targetable>();

												float distanceBetweenPlayerAndTile = !playerTargetable ? Int32.MaxValue : 
														(pathNodes[i].pos - playerTargetable.GetGridPosition()).magnitude;

												if ( distanceBetweenPlayerAndTile < 1.001 )
												{
														aiTarget = playerTargetable;
														closestNodeToTarget = pathNodes[i];
														// set also in attacker component
														_attacker.SetTarget(aiTarget);
												}
										}
								}
						}
				}

				// targets the nearest reachable tile towards the targeted player
				// the tile the enemy is standing on will not be recognized as reachable, it will be skipped, 
				// so if the enemy can't move another tile, movement target will be set to null
				public void TargetNearestTileToPlayerTarget()
				{
						pathfindingPathQueryEvent.RaiseEvent(_gridTransform.gridPosition, closestNodeToTarget.pos, SaveClosestTileToPlayerAsMovementTarget);
				}

				private void SaveClosestTileToPlayerAsMovementTarget(List<PathNode> pathNodes)
				{
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

				public void SaveValidAbilities()
				{
						validAbilities.Clear();
						tilesInRangePerAbility.Clear();
						abilityPerTilesInRange.Clear();

						// should be refreshed on entering the Search state in StateMachine
						foreach (AbilitySO ability in _abilityController.Abilities)
						{
								// only valid if affordable
								if(IsAffordable(ability))
								{
										// only if target has valid relationship for ability
										if(HasRightRelationshipForAbility(ability))
										{
												fieldOfViewQueryEvent.RaiseEvent(_gridTransform.gridPosition, ability.range, 
														TileProperties.ShootTrough, AddAbilitiesInRangeToValidAbilities);
												abilityPerTilesInRange.Add(ability);
										}
								}
						}

						AddTilesInRangeToValidAbilities();
				}

				private void AddAbilitiesInRangeToValidAbilities(bool[,] visibleTilesInRange)
				{
						tilesInRangePerAbility.Add(FieldOfViewController.VisibleTilesToPathNodeList(visibleTilesInRange));
				}

				private void AddTilesInRangeToValidAbilities()
				{
						for ( int i = 0; i < tilesInRangePerAbility.Count; i++ )
						{
								bool targetIsInRange = false;
								foreach ( PathNode node in tilesInRangePerAbility[i] )
								{
										if ( node.pos.Equals(aiTarget.GetGridPosition()) )
												targetIsInRange = true;
								}
								if ( targetIsInRange )
										validAbilities.Add(abilityPerTilesInRange[i]);
						}
				}

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