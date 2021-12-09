using Characters.Ability;
using Characters.Movement;
using Combat;
using Events.ScriptableObjects;
using Events.ScriptableObjects.Pathfinding;
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

				public Targetable playerTarget; // player, they want to attack
				public Targetable enemyTarget; // in case they want to heal each other
				public PathNode movementTarget; // position they want to reach

				private EnemyBehaviorSO behavior;
				[SerializeField] private GridTransform _gridTransform;
				[SerializeField] private MovementController _movementController;

				public void SetBehavior(EnemyBehaviorSO behavior) { this.behavior = behavior; }

				private void Awake()
				{
						ClearTargetCache();
				}

				public void ClearTargetCache()
				{
						playerTarget = null;
						enemyTarget = null;
						movementTarget = null;
				}

				// uses pathfinding to find closest player
				public void TargetClosestPlayer()
				{
						pathfindingQueryEvent.RaiseEvent(_gridTransform.gridPosition, behavior.rangeOfInterestMovement, SaveClosestPlayerAsTarget);
				}

				private void SaveClosestPlayerAsTarget(List<PathNode> pathNodes)
				{
						playerTarget = null;

						GameObject characterListObj = GameObject.Find("Characters");
						CharacterList characterList = null;
						if ( characterListObj )
								characterList = characterListObj.GetComponent<CharacterList>();
						if ( characterList )
						{
								for(int i = 0; !playerTarget && i < pathNodes.Count; i++)
								{
										foreach(GameObject player in characterList.playerContainer)
										{
												Targetable playerTargetable = player.GetComponent<Targetable>();
												if ( playerTargetable && playerTargetable.GetGridPosition().Equals(pathNodes[i].pos) )
														playerTarget = playerTargetable;
										}
								}
						}
				}

				// targets the nearest reachable tile towards the targeted player
				// the tile the enemy is standing on will not be recognized as reachable, it will be skipped, 
				// so if the enemy can't move another tile, movement target will be set to null
				public void TargetNearestTileToPlayerTarget()
				{
						pathfindingPathQueryEvent.RaiseEvent(_gridTransform.gridPosition, playerTarget.GetGridPosition(), SaveClosestTileToPlayerAsMovementTarget);
				}

				private void SaveClosestTileToPlayerAsMovementTarget(List<PathNode> pathNodes)
				{
						int lastAffordableStep = 1; // 1 because first tile will be skipped
						while ( lastAffordableStep < pathNodes.Count - 1 && pathNodes[lastAffordableStep].dist < _movementController.GetMaxMoveDistance() )
								lastAffordableStep++;

						movementTarget = lastAffordableStep >= pathNodes.Count ? null : pathNodes[lastAffordableStep];
						_movementController.movementTarget = movementTarget;
				}
		}
}