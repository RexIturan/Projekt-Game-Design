using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
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
    private readonly NodeListEventChannelSO _drawPathEvent;
    private readonly VoidEventChannelSO _clearPathEvent;
    private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;
    private readonly GridDataSO _globalGridData;
    private PlayerCharacterSC _playerStateContainer;

    public DrawPathPreview(NodeListEventChannelSO drawPathEvent, VoidEventChannelSO clearPathEvent, 
                           PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel, GridDataSO globalGridData)
    {
        this._drawPathEvent = drawPathEvent;
        this._clearPathEvent = clearPathEvent;
        this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
        this._globalGridData = globalGridData;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {
        bool isReachable = false;
        List<PathNode> tiles = _playerStateContainer.reachableTiles;
        // todo move to central pos
        Vector2Int mousePos = WorldPosToGridPos(MousePosition.GetMouseWorldPosition(Vector3.up, 1));

        for (int i = 0; i < tiles.Count && !isReachable; i++)
        {
            if (tiles[i].x == mousePos.x &&
                tiles[i].y == mousePos.y)
            {
                isReachable = true;

                Vector3Int startNode = new Vector3Int(_playerStateContainer.gridPosition.x,
                                                    _playerStateContainer.gridPosition.z, 
                                                    0);
                Vector3Int endNode = new Vector3Int(tiles[i].x,
                                                    tiles[i].y,
                                                    0);

                _pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, DrawPath);
            }
        }
        if (!isReachable)
            _clearPathEvent.RaiseEvent();
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    public void DrawPath(List<PathNode> nodes)
    {
        _drawPathEvent.RaiseEvent(nodes);
    }

    // TODO: Codeverdopplung vermeiden (copy paste aus PathfindingController) 
    public Vector2Int WorldPosToGridPos(Vector3 worldPos)
    {
        var lowerBounds = Vector3Int.FloorToInt(_globalGridData.OriginPosition);
        var flooredPos = Vector3Int.FloorToInt(worldPos);
        return new Vector2Int(
            x: flooredPos.x + Mathf.Abs(lowerBounds.x),
            y: flooredPos.z + Mathf.Abs(lowerBounds.z));
    }
}
