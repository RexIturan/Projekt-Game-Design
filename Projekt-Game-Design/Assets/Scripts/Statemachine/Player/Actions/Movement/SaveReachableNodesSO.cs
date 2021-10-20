using System.Collections.Generic;
using Events.ScriptableObjects.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "SaveReachableNodes", menuName = "State Machines/Actions/Player/SaveReachableNodes")]
public class SaveReachableNodesSO : StateActionSO
{
    [SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
    public override StateAction CreateAction() => new SaveReachableNodes(pathfindingQueryEvent);
}

public class SaveReachableNodes : StateAction
{
    private PlayerCharacterSC _playerStateContainer;
    private PathfindingQueryEventChannelSO _pathfindingQueryEvent;

    public SaveReachableNodes(PathfindingQueryEventChannelSO pathfindingQueryEvent)
    {
        this._pathfindingQueryEvent = pathfindingQueryEvent;
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnStateEnter()
    {
        // Debug.Log("Calculate new reachable tiles... max distance = " + playerStateContainer.GetMaxMoveDistance());
        _pathfindingQueryEvent.RaiseEvent(_playerStateContainer.gridPosition, _playerStateContainer.GetMaxMoveDistance(), SaveToStateContainer);
    }

    public void SaveToStateContainer(List<PathNode> reachableTiles)
    {
        _playerStateContainer.reachableTiles = reachableTiles;
    }
}
