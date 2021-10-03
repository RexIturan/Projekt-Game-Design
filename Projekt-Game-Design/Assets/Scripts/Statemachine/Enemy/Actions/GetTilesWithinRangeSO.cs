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
    public class GetTilesWithinRangeSO : StateActionSO {
        [SerializeField] private StateAction.SpecificMoment phase;
        [SerializeField] private FieldOfViewQueryEventChannelSO fieldOfViewQueryEC;
        public override StateAction CreateAction() => new GetTilesWithinRange(phase,fieldOfViewQueryEC);
    }

    public class GetTilesWithinRange : StateAction {
        protected new GetTilesWithinRangeSO originSo => (GetTilesWithinRangeSO) base.OriginSO;

        private StateAction.SpecificMoment phase;
        private FieldOfViewQueryEventChannelSO fieldOfViewQueryEC;
        private EnemyCharacterSC enemyCharacterSC;

        public GetTilesWithinRange(StateAction.SpecificMoment phase, FieldOfViewQueryEventChannelSO fieldOfViewQueryEc) {
            this.phase = phase;
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
            
            Debug.Log("got tiles within range");
            
            enemyCharacterSC.tileInRangeOfTarget.Clear();
            enemyCharacterSC.tileInRangeOfTarget = inRange;
            enemyCharacterSC.rangeChecked = true;
        }

        private void RaiseGetTargetInRangeTiles() {
            
            Debug.Log("get tiles within range");
            
            // get target
            var target = enemyCharacterSC.target;
            var range = (int) enemyCharacterSC.attackRange;
            var blocking = ETileFlags.solid;

            fieldOfViewQueryEC.RaiseEvent(target.gridPosition, range, blocking, setTilesWithinRange);
        }
        
        public override void OnUpdate() {
            if (phase == SpecificMoment.OnUpdate) {
                // if (enemyCharacterSC.target != null) {
                    RaiseGetTargetInRangeTiles();    
                // }
            }
        }

        public override void OnStateEnter() {
            if (phase == SpecificMoment.OnStateEnter) {
                // if (enemyCharacterSC.target != null) {
                    RaiseGetTargetInRangeTiles();    
                // }
            }
        }

        public override void OnStateExit() {
            if (phase == SpecificMoment.OnStateExit) {
                // if (enemyCharacterSC.target != null) {
                    RaiseGetTargetInRangeTiles();    
                // }
            }
        }
    }
}