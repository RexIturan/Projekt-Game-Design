using Events.ScriptableObjects;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;
using Pathfinding;

[CreateAssetMenu(fileName = "ClearReachableTiles", menuName = "State Machines/Actions/Player/ClearReachableTiles")]
public class ClearReachableTilesSO : StateActionSO
{
    [SerializeField] private GameObject pathfindingController;
    protected override StateAction CreateAction() => new ClearReachableTiles(pathfindingController);
}

public class ClearReachableTiles : StateAction
{
    private PlayerCharacterCO playerStateContainer;
    private PathfindingDrawer pathfindingDrawer;

    public ClearReachableTiles(GameObject pathfindingController)
    {
        this.pathfindingDrawer = pathfindingController.GetComponent<PathfindingDrawer>();
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterCO>();
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
        Debug.Log("Clearing reachable tiles.");
        pathfindingDrawer.ClearPreviewTilemap();
    }
}
