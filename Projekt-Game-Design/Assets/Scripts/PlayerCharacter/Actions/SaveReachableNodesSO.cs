using Events.ScriptableObjects;
using System.Collections.Generic;
using UnityEditorInternal;
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
    private PlayerCharacterSC playerStateContainer;
    private PathfindingQueryEventChannelSO pathfindingQueryEvent;

    public SaveReachableNodes(PathfindingQueryEventChannelSO pathfindingQueryEvent)
    {
        this.pathfindingQueryEvent = pathfindingQueryEvent;
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnStateEnter()
    {
        pathfindingQueryEvent.RaiseEvent(playerStateContainer.position, playerStateContainer.GetMaxMoveDistance(), saveToStateContainer);
    }

    public void saveToStateContainer(List<PathNode> reachableTiles)
    {
        playerStateContainer.reachableTiles = reachableTiles;
    }
}
