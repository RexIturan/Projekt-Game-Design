using System;
using System.Collections.Generic;
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
        [SerializeField] private FindPathBatchEventChannelSO findPathBatchEC;
        public override StateAction CreateAction() => new GetNewTarget(phase, findPathBatchEC);
    }

    public class GetNewTarget : StateAction {
        protected new GetNewTargetSO OriginSO => (GetNewTargetSO) base.OriginSO;

        private EnemyCharacterSC _enemyCharacterSC;
        private CharacterList _characterList;
        private readonly FindPathBatchEventChannelSO _findPathBatchEC;
        private readonly SpecificMoment _phase;

        public GetNewTarget(SpecificMoment phase, FindPathBatchEventChannelSO findPathBatchEc) {
            _phase = phase;
            _findPathBatchEC = findPathBatchEc;
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

            foreach (var player in _characterList.playerContainer) {
                if (shortest.Count > 0) {
                    var node = shortest[shortest.Count - 1];
                    var pos = new Vector2Int(node.x, node.y);
                    // todo check height as well;
                    var playerCharacterSc = player.GetComponent<PlayerCharacterSC>();
                    var playerPos = new Vector2Int(playerCharacterSc.gridPosition.x, playerCharacterSc.gridPosition.z);
                    if (playerPos == pos) {
                        _enemyCharacterSC.target = playerCharacterSc;
                        Debug.Log("target found");
                    }
                }
                else {
                    Debug.Log("no Player found");
                    _enemyCharacterSC.isDone = true;
                }
            }

            // todo verarbeitung des gefundenen weges
        }

        public override void Awake(StateMachine stateMachine) {
            _enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        }

        public override void OnUpdate() {
            if (_phase == SpecificMoment.OnUpdate) {
                RaiseFindPathBatchEvent();    
            }
        }

        public override void OnStateEnter() {
            Debug.Log($"new target on enter {_phase}");
            if (_phase == SpecificMoment.OnStateEnter) {
                RaiseFindPathBatchEvent();    
            }
        }

        public override void OnStateExit() {
            if (_phase == SpecificMoment.OnStateExit) {
                RaiseFindPathBatchEvent();    
            }
        }

        private void RaiseFindPathBatchEvent() {
            _characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
            List<Tuple<Vector3Int, Vector3Int>> coords = new List<Tuple<Vector3Int, Vector3Int>>();
            // for each character get path to them
            foreach (var player in _characterList.playerContainer) {
                coords.Add(new Tuple<Vector3Int, Vector3Int>(_enemyCharacterSC.gridPosition, player.GetComponent<PlayerCharacterSC>().gridPosition));
            }

            _findPathBatchEC.RaiseEvent(coords, Callback);
        }
    }
}