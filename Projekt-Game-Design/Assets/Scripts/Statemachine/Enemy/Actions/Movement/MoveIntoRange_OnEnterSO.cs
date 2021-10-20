using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;

namespace Statemachine.Enemy.Actions.Movement {
    [CreateAssetMenu(fileName = "e_MoveIntoRange_OnEnter", menuName = "State Machines/Actions/Enemy/Move Into Range On Enter")]
    public class MoveIntoRange_OnEnterSO : StateActionSO {
        [SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEC;
        
        public override StateAction CreateAction() => new MoveIntoRange_OnEnter(pathfindingPathQueryEC);
    }

    public class MoveIntoRange_OnEnter : StateAction {
        protected new MoveIntoRange_OnEnterSO OriginSO => (MoveIntoRange_OnEnterSO) base.OriginSO;
        private EnemyCharacterSC _enemySC;
        private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEC;
        
        public MoveIntoRange_OnEnter(PathFindingPathQueryEventChannelSO pathfindingPathQueryEc) {
            this._pathfindingPathQueryEC = pathfindingPathQueryEc;
        }

        public override void Awake(StateMachine stateMachine) {
            _enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate() { }

        public override void OnStateEnter() {
            var inRangeTiles = _enemySC.tileInRangeOfTarget;
            var reachableTiles = _enemySC.reachableNodes;
            var level = _enemySC.gridPosition.y;

            List<PathNode> reachableTilesInRange = new List<PathNode>();

            foreach (var node in reachableTiles) {
                var pos = new Vector3Int(node.x, level, node.y);
                foreach (var tilePos in inRangeTiles) {
                    if (pos == tilePos) {
                        reachableTilesInRange.Add(node);
                    }
                }   
            }

            PathNode nearestNode = null; 
            foreach (var node in reachableTilesInRange) {
                if (nearestNode is null || node.dist < nearestNode.dist) {
                    nearestNode = node;
                }
            }

            if (nearestNode is null) {
                var targetPlayer = _enemySC.target;
                
                if (targetPlayer is null) {
                    _enemySC.isDone = true;
                }
                else {
                    PlayerCharacterSC targetContainer = targetPlayer.GetComponent<PlayerCharacterSC>();

                    Vector3Int startNode = new Vector3Int(_enemySC.gridPosition.x,
                        _enemySC.gridPosition.z,
                        0);
                    Vector3Int endNode = new Vector3Int(targetContainer.gridPosition.x,
                        targetContainer.gridPosition.z,
                        0);

                    _pathfindingPathQueryEC.RaiseEvent(startNode, endNode, SaveClosestToPlayer);                    
                }
            }
            else {
                _enemySC.movementTarget = nearestNode;
                var targetPos = new Vector3Int(nearestNode.x, 1, nearestNode.y);
                _enemySC.gridPosition = targetPos;
                _enemySC.MoveToGridPosition();

                // reduce costs
                _enemySC.energy -= _enemySC.GetEnergyUseUpFromMovement();
                _enemySC.abilityExecuted = true;
            }
        }

        public override void OnStateExit() { }
        
        private void SaveClosestToPlayer(List<PathNode> path) {
            int index = 0;
            for (int i = 1; i < path.Count; i++) {
                // TODO: distance instead of GCost? 
                if (path[i].gCost <= _enemySC.movementPointsPerEnergy * _enemySC.energy)
                    index = i;
                else
                    break;
            }

            if (index == 0) {
                // Debug.Log("Gegner kann sich nicht bewegen");
                _enemySC.isDone = true;
            }
            else {
                var nearestNode = path[index];
                nearestNode.dist = nearestNode.gCost;
                _enemySC.movementTarget = nearestNode;
                var targetPos = new Vector3Int(nearestNode.x, 1, nearestNode.y);
                _enemySC.gridPosition = targetPos;
                _enemySC.MoveToGridPosition();

                // reduce costs
                _enemySC.energy -= _enemySC.GetEnergyUseUpFromMovement();
                _enemySC.abilityExecuted = true;
            }
        }
    }
}