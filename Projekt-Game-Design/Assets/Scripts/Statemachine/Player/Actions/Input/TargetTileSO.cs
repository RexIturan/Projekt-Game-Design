using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using UnityEngine.InputSystem;
using Grid;
using Util;
using System.Collections.Generic;
using Characters;
using Characters.Ability;
using Characters.Movement;

[CreateAssetMenu(fileName = "TargetTile", menuName = "State Machines/Actions/Player/TargetTile")]
public class TargetTileSO : StateActionSO {
	[SerializeField] private GridDataSO globalGridData;

	public override StateAction CreateAction() => new TargetTile(globalGridData);
}

public class TargetTile : StateAction {
	private const float TimeBeforeAcceptingInput = 0.5f;

	private readonly GridDataSO _globalGridData;
	private Timer _timer;
	private MovementController _movementController;
	private AbilityController _abilityController;

	public TargetTile(GridDataSO globalGridData) {
		this._globalGridData = globalGridData;
	}

	public override void Awake(StateMachine stateMachine) {
		_timer = stateMachine.gameObject.GetComponent<Timer>();
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	public override void OnUpdate() {
		if ( _timer.timeSinceTransition > TimeBeforeAcceptingInput &&
		     Mouse.current.leftButton.wasPressedThisFrame ) {
			bool isReachable = false;
			List<PathNode> tiles = _movementController.reachableTiles;
			//todo get from input cache
			var pos = MousePosition.GetTilePositionFromMousePosition(_globalGridData, true,
				out bool hitBottom);
			Vector3Int mouseGridPos = _globalGridData.GetGridPos3DFromWorldPos(pos);

			// Debug.DrawLine(pos + Vector3.forward, pos + Vector3.back, Color.green, 100);
			// Debug.DrawLine(pos + Vector3.left, pos + Vector3.right, Color.green, 100);

			for ( int i = 0; i < tiles.Count && !isReachable; i++ ) {
				if ( tiles[i].pos.Equals(mouseGridPos) ) {
					isReachable = true;
					_movementController.movementTarget = tiles[i];
					_abilityController.abilityConfirmed = true;
				}
			}

			// if (!isReachable)
			//    Debug.Log("Tile not reachable");
		}
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }

	// TODO: Codeverdopplung vermeiden (copy paste aus PathfindingController) 
	public Vector2Int WorldPosToGridPos(Vector3 worldPos) {
		var lowerBounds = Vector3Int.FloorToInt(_globalGridData.OriginPosition);
		var flooredPos = Vector3Int.FloorToInt(worldPos);
		return new Vector2Int(
			x: flooredPos.x + Mathf.Abs(lowerBounds.x),
			y: flooredPos.z + Mathf.Abs(lowerBounds.z));
	}
}