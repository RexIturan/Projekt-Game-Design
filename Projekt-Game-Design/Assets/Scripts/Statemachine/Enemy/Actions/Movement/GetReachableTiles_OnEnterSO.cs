using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;

namespace Statemachine.Enemy.Actions.Movement {
    [CreateAssetMenu(fileName = "GetReachableTiles_OnEnter",
        menuName = "State Machines/Actions/Get Reachable Tiles_On Enter")]
    public class GetReachableTiles_OnEnterSO : StateActionSO {
        [SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
        public override StateAction CreateAction() => new GetReachableTiles_OnEnter(pathfindingQueryEvent);
    }

    public class GetReachableTiles_OnEnter : StateAction {
        protected new GetReachableTiles_OnEnterSO OriginSO => (GetReachableTiles_OnEnterSO) base.OriginSO;

        private EnemyCharacterSC enemyCharacterSC;
        private PathfindingQueryEventChannelSO pathfindingQueryEvent;

        public GetReachableTiles_OnEnter(PathfindingQueryEventChannelSO pathfindingQueryEvent) {
            this.pathfindingQueryEvent = pathfindingQueryEvent;
        }

        public override void Awake(StateMachine stateMachine) {
            enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate() { }

        public override void OnStateExit() { }

        public override void OnStateEnter() {
            // Debug.Log("Calculate new reachable tiles... max distance = " + enemyCharacterSC.GetMaxMoveDistance());
            pathfindingQueryEvent.RaiseEvent(enemyCharacterSC.gridPosition, enemyCharacterSC.GetMaxMoveDistance(),
                saveToStateContainer);
        }

        public void saveToStateContainer(List<PathNode> reachableTiles) {
            enemyCharacterSC.reachableNodes = reachableTiles;
        }
    }
}