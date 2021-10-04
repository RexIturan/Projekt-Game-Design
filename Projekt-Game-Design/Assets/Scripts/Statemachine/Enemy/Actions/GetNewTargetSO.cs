using System;
using System.Collections.Generic;
using Characters.ScriptableObjects;
using Events.ScriptableObjects;
using Events.ScriptableObjects.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;

namespace Statemachine.Enemy.Actions {
    [CreateAssetMenu(fileName = "e_GetNewTarget",
        menuName = "State Machines/Actions/Enemy/Get New Target")]
    public class GetNewTargetSO : StateActionSO {
        [SerializeField] private StateAction.SpecificMoment phase;
        [SerializeField] private CharacterContainerSO characterContainerSo;
        [SerializeField] private FindPathBatch_EventChannel_SO findPathBatchEC;
        public override StateAction CreateAction() => new GetNewTarget(phase, characterContainerSo, findPathBatchEC);
    }

    public class GetNewTarget : StateAction {
        protected new GetNewTargetSO OriginSO => (GetNewTargetSO) base.OriginSO;

        private EnemyCharacterSC enemyCharacterSC;
        private CharacterContainerSO characterContainerSo;
        private FindPathBatch_EventChannel_SO findPathBatchEC;
        private StateAction.SpecificMoment phase;

        public GetNewTarget(StateAction.SpecificMoment phase, CharacterContainerSO characterContainerSo,
            FindPathBatch_EventChannel_SO findPathBatchEc) {
            this.phase = phase;
            this.characterContainerSo = characterContainerSo;
            findPathBatchEC = findPathBatchEc;
        }

        private void Callback(List<List<PathNode>> paths) {
            // the character with the shortest path gets set to be the nem target

            // Debug.Log("target??");
            
            List<PathNode> shortest = paths[0];
            foreach (var path in paths) {
                if (path.Count == 0) {
                    break;
                }

                if (shortest.Count == 0) {
                    shortest = path;
                    break;
                }

                if (path[path.Count - 1].gCost < shortest[shortest.Count - 1].gCost) {
                    shortest = path;
                }
            }

            foreach (var player in characterContainerSo.playerContainer) {
                if (shortest.Count > 0) {
                    var node = shortest[shortest.Count - 1];
                    var pos = new Vector2Int(node.x, node.y);
                    // todo check height as well;
                    var playerPos = new Vector2Int(player.gridPosition.x, player.gridPosition.z);
                    if (playerPos == pos) {
                        enemyCharacterSC.target = player;
                        // Debug.Log("target found");
                    }
                }
                else {
                    Debug.Log("no Player found");
                    //enemyCharacterSC.noTargetFound = true;
                }
            }

            // todo verarbeitung des gefundenen weges
        }

        public override void Awake(StateMachine stateMachine) {
            enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate() {
            if (phase == SpecificMoment.OnUpdate) {
                RaiseFindPathBatchEvent();    
            }
        }

        public override void OnStateEnter() {
            if (phase == SpecificMoment.OnStateEnter) {
                RaiseFindPathBatchEvent();    
            }
        }

        public override void OnStateExit() {
            if (phase == SpecificMoment.OnStateExit) {
                RaiseFindPathBatchEvent();    
            }
        }

        private void RaiseFindPathBatchEvent() {
            List<Tuple<Vector3Int, Vector3Int>> coords = new List<Tuple<Vector3Int, Vector3Int>>();
            // for each character get path to them
            foreach (var player in characterContainerSo.playerContainer) {
                coords.Add(new Tuple<Vector3Int, Vector3Int>(enemyCharacterSC.gridPosition, player.gridPosition));
            }

            findPathBatchEC.RaiseEvent(coords, Callback);
        }
    }
}