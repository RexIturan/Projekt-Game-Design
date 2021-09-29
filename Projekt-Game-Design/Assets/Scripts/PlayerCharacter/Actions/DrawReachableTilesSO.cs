using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Pathfinding;

[CreateAssetMenu(fileName = "DrawReachableTiles", menuName = "State Machines/Actions/Player/DrawReachableTiles")]
public class DrawReachableTilesSO : StateActionSO
{
    [SerializeField] private GameObject pathfindingController;
    public override StateAction CreateAction() => new DrawReachableTiles(pathfindingController);
}

public class DrawReachableTiles : StateAction
{
    private PlayerCharacterSC playerStateContainer;
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
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
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
