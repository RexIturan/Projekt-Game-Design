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
        [SerializeField] private FindPathBatch_EventChannel_SO findPathBatchEC;
        public override StateAction CreateAction() => new GetNewTarget(phase, findPathBatchEC);
    }

    public class GetNewTarget : StateAction {
        protected new GetNewTargetSO OriginSO => (GetNewTargetSO) base.OriginSO;

        private EnemyCharacterSC enemyCharacterSC;
        private CharacterList characterList;
        private FindPathBatch_EventChannel_SO findPathBatchEC;
        private StateAction.SpecificMoment phase;

        public GetNewTarget(StateAction.SpecificMoment phase, FindPathBatch_EventChannel_SO findPathBatchEc) {
            this.phase = phase;
            findPathBatchEC = findPathBatchEc;
        }

        private void Callback(List<List<PathNode>> paths) {
            // the character with the shortest path gets set to be the nem target

            Debug.Log("e_getNewTarget:  get target??");
            
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

            foreach (var player in characterList.playerContainer) {
                if (shortest.Count > 0) {
                    var node = shortest[shortest.Count - 1];
                    var pos = new Vector2Int(node.x, node.y);
                    // todo check height as well;
                    var playerCharacterSc = player.GetComponent<PlayerCharacterSC>();
                    var playerPos = new Vector2Int(playerCharacterSc.gridPosition.x, playerCharacterSc.gridPosition.z);
                    if (playerPos == pos) {
                        enemyCharacterSC.target = playerCharacterSc;
                        Debug.Log("target found");
                    }
                }
                else {
                    Debug.Log("no Player found");
                    enemyCharacterSC.isDone = true;
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
            Debug.Log($"new target on enter {phase}");
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
            characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
            List<Tuple<Vector3Int, Vector3Int>> coords = new List<Tuple<Vector3Int, Vector3Int>>();
            // for each character get path to them
            foreach (var player in characterList.playerContainer) {
                coords.Add(new Tuple<Vector3Int, Vector3Int>(enemyCharacterSC.gridPosition, player.GetComponent<PlayerCharacterSC>().gridPosition));
            }

            findPathBatchEC.RaiseEvent(coords, Callback);
        }
    }
}