using System.Collections.Generic;
using Events.ScriptableObjects.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;

namespace Statemachine.Enemy.Actions.Movement {
    [CreateAssetMenu(fileName = "e_GetReachableTiles",
        menuName = "State Machines/Actions/Enemy/Get Reachable Tiles")]
    public class GetReachableTilesSO : StateActionSO {
        [SerializeField] private StateAction.SpecificMoment phase;
        [SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
        public override StateAction CreateAction() => new GetReachableTiles(phase, pathfindingQueryEvent);
    }

    public class GetReachableTiles : StateAction {
        protected new GetReachableTilesSO OriginSO => (GetReachableTilesSO) base.OriginSO;
        private readonly SpecificMoment _phase;
        
        private EnemyCharacterSC _enemyCharacterSC;
        private readonly PathfindingQueryEventChannelSO _pathfindingQueryEvent;

        public GetReachableTiles(SpecificMoment phase, PathfindingQueryEventChannelSO pathfindingQueryEvent) {
            _phase = phase;
            _pathfindingQueryEvent = pathfindingQueryEvent;
        }

        public override void Awake(StateMachine stateMachine) {
            _enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate() {
            if (_phase == SpecificMoment.OnUpdate) {
                // if (enemyCharacterSC.target != null) {
                    RaisePathfindingQuery();    
                // }
            }
        }

        public override void OnStateEnter() {
            if (_phase == SpecificMoment.OnStateEnter) {
                // if (enemyCharacterSC.target != null) {
                    RaisePathfindingQuery();    
                // }           
            }
        }

        public override void OnStateExit() {
            if (_phase == SpecificMoment.OnStateExit) {
                // if (enemyCharacterSC.target != null) {
                    RaisePathfindingQuery();
                // }
            }
        }
        
        private void RaisePathfindingQuery() {
            // Debug.Log("Calculate new reachable tiles... max distance = " + enemyCharacterSC.GetMaxMoveDistance());
            _pathfindingQueryEvent.RaiseEvent(_enemyCharacterSC.gridPosition, _enemyCharacterSC.GetMaxMoveDistance(),
                SaveToStateContainer);
        }

        public void SaveToStateContainer(List<PathNode> reachableTiles) {
            _enemyCharacterSC.reachableNodes = reachableTiles;
        }
    }
}