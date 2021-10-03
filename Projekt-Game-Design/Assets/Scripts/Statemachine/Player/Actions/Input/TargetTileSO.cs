using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using UnityEngine.InputSystem;
using Grid;
using Pathfinding;
using Util;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TargetTile", menuName = "State Machines/Actions/Player/TargetTile")]
public class TargetTileSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private PathNodeEventChannelSO targetTileEvent;

    [SerializeField] private GridDataSO globalGridData;

    public override StateAction CreateAction() => new TargetTile(targetTileEvent, globalGridData);
}

public class TargetTile : StateAction
{
    private const float TIME_BEFORE_ACCEPTING_INPUT = 0.5f;

    private PathNodeEventChannelSO targetTileEvent;
    private GridDataSO globalGridData;
    private PlayerCharacterSC playerStateContainer;

    public TargetTile(PathNodeEventChannelSO targetTileEvent, GridDataSO globalGridData)
    {
        this.targetTileEvent = targetTileEvent;
        this.globalGridData = globalGridData;
    }

    public override void Awake(StateMachine stateMachine)
    {
        playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {
        if (playerStateContainer.timeSinceTransition > TIME_BEFORE_ACCEPTING_INPUT && Mouse.current.leftButton.wasPressedThisFrame)
        {
            bool isReachable = false;
            List<PathNode> tiles = playerStateContainer.reachableTiles;
            // todo fix this !!!
            var pos = MousePosition.GetMouseWorldPosition(Vector3.up, 1f);
            Vector2Int mousePos = WorldPosToGridPos(pos);

            // Debug.DrawLine(pos + Vector3.forward, pos + Vector3.back, Color.green, 100);
            // Debug.DrawLine(pos + Vector3.left, pos + Vector3.right, Color.green, 100);
            
            for (int i = 0; i < tiles.Count && !isReachable; i++)
            {
                if (tiles[i].x == mousePos.x &&
                    tiles[i].y == mousePos.y)
                {
                    isReachable = true;
                    targetTileEvent.RaiseEvent(tiles[i]); 
                }
            }

            // if (!isReachable)
            //    Debug.Log("Tile not reachable");
        }
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

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
