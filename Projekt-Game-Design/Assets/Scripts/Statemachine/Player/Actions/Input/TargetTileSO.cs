using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using UnityEngine.InputSystem;
using Grid;
using Util;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TargetTile", menuName = "State Machines/Actions/Player/TargetTile")]
public class TargetTileSO : StateActionSO
{
    [SerializeField] private GridDataSO globalGridData;

    public override StateAction CreateAction() => new TargetTile(globalGridData);
}

public class TargetTile : StateAction
{
    private const float TimeBeforeAcceptingInput = 0.5f;
    
    private GridDataSO _globalGridData;
    private PlayerCharacterSC _playerStateContainer;

    public TargetTile(GridDataSO globalGridData)
    {
        this._globalGridData = globalGridData;
    }

    public override void Awake(StateMachine stateMachine)
    {
        _playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }

    public override void OnUpdate()
    {
        if (_playerStateContainer.timeSinceTransition > TimeBeforeAcceptingInput && Mouse.current.leftButton.wasPressedThisFrame)
        {
            bool isReachable = false;
            List<PathNode> tiles = _playerStateContainer.reachableTiles;
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
                    _playerStateContainer.movementTarget = tiles[i];
                    _playerStateContainer.abilityConfirmed = true;
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
        var lowerBounds = Vector3Int.FloorToInt(_globalGridData.OriginPosition);
        var flooredPos = Vector3Int.FloorToInt(worldPos);
        return new Vector2Int(
            x: flooredPos.x + Mathf.Abs(lowerBounds.x),
            y: flooredPos.z + Mathf.Abs(lowerBounds.z));
    }
}
