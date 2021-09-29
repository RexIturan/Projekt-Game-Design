using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Pathfinding;

[CreateAssetMenu(fileName = "DrawReachableTiles", menuName = "State Machines/Actions/Player/DrawReachableTiles")]
public class DrawReachableTilesSO : StateActionSO {
    [Header("Sending Events On")]
    [SerializeField] private NodeListEventChannelSO drawReachableTileEC;
    public override StateAction CreateAction() => new DrawReachableTiles(drawReachableTileEC);
}

public class DrawReachableTiles : StateAction
{
    private NodeListEventChannelSO drawReachableTileEC;
    private PlayerCharacterSC playerStateContainer;
    
    public DrawReachableTiles(NodeListEventChannelSO drawReachableTileEC) {
        this.drawReachableTileEC = drawReachableTileEC;
    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }
    
    public override void OnUpdate()
    {
        // pathfindingDrawer.DrawPreview(playerStateContainer.reachableTiles);
    }

    public override void OnStateEnter()
    {
        // pathfindingDrawer.ClearPreviewTilemap();
        Debug.Log("Zeichne reachable tiles von Player aus");
        drawReachableTileEC.RaiseEvent(playerStateContainer.reachableTiles);
    }

    public override void OnStateExit()
    {

    }
}
