using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Pathfinding;

[CreateAssetMenu(fileName = "ClearReachableTiles", menuName = "State Machines/Actions/Player/ClearReachableTiles")]
public class ClearReachableTilesSO : StateActionSO
{
    [SerializeField] private GameObject pathfindingController;
    public override StateAction CreateAction() => new ClearReachableTiles(pathfindingController);
}

public class ClearReachableTiles : StateAction
{
    private PlayerCharacterSC playerStateContainer;
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
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
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
