using System.Collections.Generic;
using Events.ScriptableObjects.FieldOfView;
using Level.Grid;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Statemachine.Enemy.Actions {
    [CreateAssetMenu(fileName = "e_GetTilesWithinRange",
        menuName = "State Machines/Actions/Enemy/Get Tiles Within Range")]
    public class GetTilesWithinRangeSO : StateActionSO {
        [SerializeField] private StateAction.SpecificMoment phase;
        [SerializeField] private FOVQueryEventChannelSO fieldOfViewQueryEC;
        public override StateAction CreateAction() => new GetTilesWithinRange(phase,fieldOfViewQueryEC);
    }

    public class GetTilesWithinRange : StateAction {
        // protected new GetTilesWithinRangeSO originSo => (GetTilesWithinRangeSO) base.OriginSO;

        private readonly SpecificMoment _phase;
        private readonly FOVQueryEventChannelSO _fieldOfViewQueryEC;
        private EnemyCharacterSC _enemyCharacterSC;

        public GetTilesWithinRange(SpecificMoment phase, FOVQueryEventChannelSO fieldOfViewQueryEc) {
            _phase = phase;
            _fieldOfViewQueryEC = fieldOfViewQueryEc;
        }

        public override void Awake(StateMachine stateMachine) {
            _enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public void SetTilesWithinRange(bool[,] visible) {
            var level = _enemyCharacterSC.gridPosition.y;
            var pos = _enemyCharacterSC.target.GetGridPosition();

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
            
            _enemyCharacterSC.tileInRangeOfTarget.Clear();
            _enemyCharacterSC.tileInRangeOfTarget = inRange;
            _enemyCharacterSC.rangeChecked = true;
        }

        private void RaiseGetTargetInRangeTiles() {
            
            Debug.Log("get tiles within range");
            
            // get target
            var target = _enemyCharacterSC.target;
            var range = (int) _enemyCharacterSC.attackRange;
            var blocking = TileProperties.Solid;

            _fieldOfViewQueryEC.RaiseEvent(target.GetGridPosition(), range, blocking, SetTilesWithinRange);
        }
        
        public override void OnUpdate() {
            if (_phase == SpecificMoment.OnUpdate) {
                // if (enemyCharacterSC.target != null) {
                    RaiseGetTargetInRangeTiles();    
                // }
            }
        }

        public override void OnStateEnter() {
            if (_phase == SpecificMoment.OnStateEnter) {
                // if (enemyCharacterSC.target != null) {
                    RaiseGetTargetInRangeTiles();    
                // }
            }
        }

        public override void OnStateExit() {
            if (_phase == SpecificMoment.OnStateExit) {
                // if (enemyCharacterSC.target != null) {
                    RaiseGetTargetInRangeTiles();    
                // }
            }
        }
    }
}