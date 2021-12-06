using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Util;
using Grid;

[CreateAssetMenu(fileName = "DrawPathPreview",
	menuName = "State Machines/Actions/Player/DrawPathPreview")]
public class DrawPathPreviewSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	private NodeListEventChannelSO drawPathEvent;

	[SerializeField] private VoidEventChannelSO clearPathEvent;

	[Header("Receiving Events On")] [SerializeField]
	private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

	[SerializeField] private GridDataSO globalGridData;

	public override StateAction CreateAction() => new DrawPathPreview(drawPathEvent, clearPathEvent,
		pathfindingPathQueryEventChannel, globalGridData);
}

public class DrawPathPreview : StateAction {
	private readonly NodeListEventChannelSO _drawPathEvent;
	private readonly VoidEventChannelSO _clearPathEvent;
	private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;
	private readonly GridDataSO _globalGridData;
	private MovementController _movementController;
	private GridTransform _gridTransform;

	//Constructor
	public DrawPathPreview(NodeListEventChannelSO drawPathEvent, VoidEventChannelSO clearPathEvent,
		PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel,
		GridDataSO globalGridData) {
		this._drawPathEvent = drawPathEvent;
		this._clearPathEvent = clearPathEvent;
		this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
		this._globalGridData = globalGridData;
	}

	public override void Awake(StateMachine stateMachine) {
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnUpdate() {
		bool isReachable = false;
		List<PathNode> tiles = _movementController.reachableTiles;
		// todo move to central pos
		var mousePos =
			MousePosition.GetTilePos(_globalGridData, true, out bool hitBottom);
		var mouse2DGridPos = _globalGridData.GetGridPos3DFromWorldPos(mousePos);

		for ( int i = 0; i < tiles.Count && !isReachable; i++ ) {
			if ( tiles[i].pos.Equals(mouse2DGridPos) ) {
				isReachable = true;

				Vector3Int startNode = _gridTransform.gridPosition;
				Vector3Int endNode = tiles[i].pos;

				_pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, DrawPath);
			}
		}

		if ( !isReachable )
			_clearPathEvent.RaiseEvent();
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }

	private void DrawPath(List<PathNode> nodes) {
		_drawPathEvent.RaiseEvent(nodes);
	}
}