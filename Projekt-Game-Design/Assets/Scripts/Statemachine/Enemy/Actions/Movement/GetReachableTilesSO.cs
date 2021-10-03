using System.Collections.Generic;
using Events.ScriptableObjects;
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
        private StateAction.SpecificMoment phase;
        
        private EnemyCharacterSC enemyCharacterSC;
        private PathfindingQueryEventChannelSO pathfindingQueryEvent;

        public GetReachableTiles(StateAction.SpecificMoment phase, PathfindingQueryEventChannelSO pathfindingQueryEvent) {
            this.phase = phase;
            this.pathfindingQueryEvent = pathfindingQueryEvent;
        }

        public override void Awake(StateMachine stateMachine) {
            enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate() {
            if (phase == SpecificMoment.OnUpdate) {
                // if (enemyCharacterSC.target != null) {
                    RaisePathfindingQuery();    
                // }
            }
        }

        public override void OnStateEnter() {
            if (phase == SpecificMoment.OnStateEnter) {
                // if (enemyCharacterSC.target != null) {
                    RaisePathfindingQuery();    
                // }           
            }
        }

        public override void OnStateExit() {
            if (phase == SpecificMoment.OnStateExit) {
                // if (enemyCharacterSC.target != null) {
                    RaisePathfindingQuery();
                // }
            }
        }
        
        private void RaisePathfindingQuery() {
            // Debug.Log("Calculate new reachable tiles... max distance = " + enemyCharacterSC.GetMaxMoveDistance());
            pathfindingQueryEvent.RaiseEvent(enemyCharacterSC.gridPosition, enemyCharacterSC.GetMaxMoveDistance(),
                saveToStateContainer);
        }

        public void saveToStateContainer(List<PathNode> reachableTiles) {
            enemyCharacterSC.reachableNodes = reachableTiles;
        }
    }
}