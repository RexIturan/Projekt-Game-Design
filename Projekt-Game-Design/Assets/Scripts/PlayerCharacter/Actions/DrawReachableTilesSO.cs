using Events.ScriptableObjects;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;
using Pathfinding;

[CreateAssetMenu(fileName = "DrawReachableTiles", menuName = "State Machines/Actions/Player/DrawReachableTiles")]
public class DrawReachableTilesSO : StateActionSO
{
    [SerializeField] private GameObject pathfindingController;
    protected override StateAction CreateAction() => new DrawReachableTiles(pathfindingController);
}

public class DrawReachableTiles : StateAction
{
    private PlayerCharacterCO playerStateContainer;
    private PathfindingDrawer pathfindingDrawer;

    public DrawReachableTiles(GameObject pathfindingController)
    {
        this.pathfindingDrawer = pathfindingController.GetComponent<PathfindingDrawer>();
    }

    public override void OnUpdate()
    {
        pathfindingDrawer.DrawPreview(playerStateContainer.reachableTiles);
    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterCO>();
    }

    public override void OnStateEnter()
    {
        pathfindingDrawer.ClearPreviewTilemap();
        Debug.Log("Zeichne reachable tiles von Player aus");
        pathfindingDrawer.DrawPreview(playerStateContainer.reachableTiles);
    }

    public override void OnStateExit()
    {

    }
}
