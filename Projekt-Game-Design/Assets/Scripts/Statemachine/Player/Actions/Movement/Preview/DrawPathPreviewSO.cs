using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Pathfinding;
using System.Collections.Generic;
using Util;
using Grid;

[CreateAssetMenu(fileName = "DrawPathPreview", menuName = "State Machines/Actions/Player/DrawPathPreview")]
public class DrawPathPreviewSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private NodeListEventChannelSO drawPathEvent;
    [SerializeField] private VoidEventChannelSO clearPathEvent;
    [Header("Receiving Events On")]
    [SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

    [SerializeField] private GridDataSO globalGridData;

    public override StateAction CreateAction() => new DrawPathPreview(drawPathEvent, clearPathEvent, 
                                                                      pathfindingPathQueryEventChannel, globalGridData);
}

public class DrawPathPreview : StateAction
{
    private const int STANDARD_Y_VALUE = 1;

    private readonly NodeListEventChannelSO drawPathEvent;
    private readonly VoidEventChannelSO clearPathEvent;
    private readonly PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;
    private readonly GridDataSO globalGridData;
    private PlayerCharacterSC playerStateContainer;

    public DrawPathPreview(NodeListEventChannelSO drawPathEvent, VoidEventChannelSO clearPathEvent, 
                           PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel, GridDataSO globalGridData)
    {
        this.drawPathEvent = drawPathEvent;
        this.clearPathEvent = clearPathEvent;
        this.pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
        this.globalGridData = globalGridData;
    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {
        bool isReachable = false;
        List<PathNode> tiles = playerStateContainer.reachableTiles;
        // todo move to central pos
        Vector2Int mousePos = WorldPosToGridPos(MousePosition.GetMouseWorldPosition(Vector3.up, 1));

        for (int i = 0; i < tiles.Count && !isReachable; i++)
        {
            if (tiles[i].x == mousePos.x &&
                tiles[i].y == mousePos.y)
            {
                isReachable = true;

                Vector3Int startNode = new Vector3Int(playerStateContainer.gridPosition.x,
                                                    playerStateContainer.gridPosition.z, 
                                                    0);
                Vector3Int endNode = new Vector3Int(tiles[i].x,
                                                    tiles[i].y,
                                                    0);

                pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, drawPath);
            }
        }
        if (!isReachable)
            clearPathEvent.RaiseEvent();
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    public void drawPath(List<PathNode> nodes)
    {
        drawPathEvent.RaiseEvent(nodes);
    }

    // TODO: Codeverdopplung vermeiden (copy paste aus PathfindingController) 
    public Vector2Int WorldPosToGridPos(Vector3 worldPos)
    {
        var lowerBounds = Vector3Int.FloorToInt(globalGridData.OriginPosition);
        var flooredPos = Vector3Int.FloorToInt(worldPos);
        return new Vector2Int(
            x: flooredPos.x + Mathf.Abs(lowerBounds.x),
            y: flooredPos.z + Mathf.Abs(lowerBounds.z));
    }
}
