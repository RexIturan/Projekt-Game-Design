using Events.ScriptableObjects;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_MakeMoveOnEnter", menuName = "State Machines/Actions/Enemy/e_MakeMoveOnEnter")]
public class E_MakeMoveOnEnterSO : StateActionSO {
    [Header("Sending events on")] [SerializeField]
    private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

    public override StateAction CreateAction() => new E_MakeMoveOnEnter(pathfindingPathQueryEventChannel);
}

public class E_MakeMoveOnEnter : StateAction {
    private EnemyCharacterSC _enemySC;
    private EnemyBehaviorSO _behavior;

    private GameObject _targetPlayer;
    private PathNode _closesTileToPlayer;

    private bool _canMove;

    private PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;

    public E_MakeMoveOnEnter(PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel) {
        this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
    }

    public override void OnUpdate() { }

    public override void Awake(StateMachine stateMachine) {
        _enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
        _behavior = _enemySC.behavior;
    }

    public override void OnStateEnter() {
				Skip();
				// TODO: Make AI later on
				/*
        if (_behavior.alwaysSkip)
            Skip();
        else {
            _canMove = true;

            // TODO: find nearest player instead
            _targetPlayer = _enemySC.characterList.playerContainer[0].gameObject;
            var targetContainer = _targetPlayer.GetComponent<GridTransform>();

            Vector3Int startNode = new Vector3Int(_enemySC.gridPosition.x,
                _enemySC.gridPosition.z,
                0);
            Vector3Int endNode = new Vector3Int(targetContainer.gridPosition.x,
                targetContainer.gridPosition.z,
                0);

            _pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, SaveClosestToPlayer);

            if (_canMove) {
                _enemySC.movementTarget = _closesTileToPlayer;
                _enemySC.abilityID = 0;
                _enemySC.abilitySelected = true;
            }
            else
                Skip();
        }
				*/
    }

    private void Skip() {
        _enemySC.isDone = true;
    }

		/*
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
            Debug.Log("Gegner kann sich nicht bewegen");
            _closesTileToPlayer = null;
            _canMove = false;
        }
        else {
            path[index].dist = path[index].gCost;
            _closesTileToPlayer = path[index];
        }
    }
		*/
}