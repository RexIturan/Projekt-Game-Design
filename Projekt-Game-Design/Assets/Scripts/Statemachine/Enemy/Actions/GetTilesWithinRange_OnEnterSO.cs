using System.Collections.Generic;
using Events.ScriptableObjects.FieldOfView;
using Grid;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;

namespace Statemachine.Enemy.Actions {
    [CreateAssetMenu(fileName = "e_GetTilesWithinRange",
        menuName = "State Machines/Actions/Enemy/Get Tiles Within Range")]
    public class GetTilesWithinRange_OnEnterSO : StateActionSO {
        [SerializeField] private FieldOfViewQueryEventChannelSO fieldOfViewQueryEC;
        public override StateAction CreateAction() => new GetTilesWithinRange_OnEnter(fieldOfViewQueryEC);
    }

    public class GetTilesWithinRange_OnEnter : StateAction {
        protected new GetTilesWithinRange_OnEnterSO originOnEnterSo => (GetTilesWithinRange_OnEnterSO) base.OriginSO;

        private FieldOfViewQueryEventChannelSO fieldOfViewQueryEC;
        private EnemyCharacterSC enemyCharacterSC;

        public GetTilesWithinRange_OnEnter(FieldOfViewQueryEventChannelSO fieldOfViewQueryEc) {
            fieldOfViewQueryEC = fieldOfViewQueryEc;
        }

        public override void Awake(StateMachine stateMachine) {
            enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public void setTilesWithinRange(bool[,] visible) {
            var level = enemyCharacterSC.gridPosition.y;
            var pos = enemyCharacterSC.target.gridPosition;

            var inRange = new List<Vector3Int>();

            for (int y = 0; y < visible.GetLength(1); y++) {
                for (int x = 0; x < visible.GetLength(0); x++) {
                    // Debug.Log($"visible [{x},{y}] {visible[x,y]}");
                    if(pos.x == x && pos.y == y) break;
                    if (visible[x, y]) {
                        var tilePos = new Vector3Int(x, level, y);
                        inRange.Add(tilePos);
                    }
                }
            }
            enemyCharacterSC.tileInRangeOfTarget.Clear();
            enemyCharacterSC.tileInRangeOfTarget = inRange;
        }

        public override void OnUpdate() { }

        public override void OnStateEnter() {
            // get target
            var target = enemyCharacterSC.target;
            var range = (int) enemyCharacterSC.attackRange;
            var blocking = ETileFlags.solid;

            fieldOfViewQueryEC.RaiseEvent(target.gridPosition, range, blocking, setTilesWithinRange);
        }

        public override void OnStateExit() { }
    }
}