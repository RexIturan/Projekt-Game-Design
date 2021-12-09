using Ability;
using Characters.Ability;
using Characters.Movement;
using Combat;
using Events.ScriptableObjects;
using Events.ScriptableObjects.FieldOfView;
using Events.ScriptableObjects.Pathfinding;
using Level.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
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
				// public Targetable enemyTarget; // in case they want to heal each other ... better use just one target

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
												if ( playerTargetable && playerTargetable.GetGridPosition().Equals(pathNodes[i].pos) )
												{
														aiTarget = playerTargetable;
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
						pathfindingPathQueryEvent.RaiseEvent(_gridTransform.gridPosition, aiTarget.GetGridPosition(), SaveClosestTileToPlayerAsMovementTarget);
				}

				private void SaveClosestTileToPlayerAsMovementTarget(List<PathNode> pathNodes)
				{
						int lastAffordableStep = 1; // 1 because first tile will be skipped
						while ( lastAffordableStep < pathNodes.Count - 1 && pathNodes[lastAffordableStep].dist < _movementController.GetMaxMoveDistance() )
								lastAffordableStep++;

						movementTarget = lastAffordableStep >= pathNodes.Count ? null : pathNodes[lastAffordableStep];
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
						tilesInRangePerAbility.Add(C_SaveTilesInRange_OnEnter.VisibleTilesToPathNodeList(visibleTilesInRange));
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
						return _statistics.StatusValues.Energy.value >= ability.costs;
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

						if ( C_HasValidTarget.HasFlag(ability.targets, AbilityTarget.Self) )
						{
								if ( _statistics.gameObject == aiTarget.gameObject )
										isValid = true;
						}
						if ( C_HasValidTarget.HasFlag(ability.targets, AbilityTarget.Ally) )
						{
								if ( attackerFaction.Equals(targetFaction) &&
										 _statistics.gameObject != aiTarget.gameObject )
										isValid = true;
						}
						if ( C_HasValidTarget.HasFlag(ability.targets, AbilityTarget.Enemy) )
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