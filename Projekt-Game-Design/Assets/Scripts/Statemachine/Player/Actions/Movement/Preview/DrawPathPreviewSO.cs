using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Movement;
using Util;
using Input;

[CreateAssetMenu(fileName = "DrawPathPreview",
	menuName = "State Machines/Actions/Player/DrawPathPreview")]
public class DrawPathPreviewSO : StateActionSO {
	[Header("Sending Events On")] 
	[SerializeField] private NodeListEventChannelSO drawPathEvent;
	[SerializeField] private VoidEventChannelSO clearPathEvent;
	[SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;
	[SerializeField] private VoidEventChannelSO previewChangedEC;

	[Header("Recieving Events On")]
	[SerializeField] private Vector3IntEventChannelSO targetChangedEC;

	public override StateAction CreateAction() => new DrawPathPreview(drawPathEvent, clearPathEvent,
		pathfindingPathQueryEventChannel, targetChangedEC, previewChangedEC);
}

public class DrawPathPreview : StateAction {
	//in
	private readonly Vector3IntEventChannelSO _targetChangedEC;
	
	//out
	private readonly NodeListEventChannelSO _drawPathEvent;
	private readonly VoidEventChannelSO _clearPathEvent;
	private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;
	private readonly VoidEventChannelSO _previewChangedEC;
	
	private MovementController _movementController;
	private GridTransform _gridTransform;

	private Vector3Int lastValidGridPos;
	private bool isDrawn;

///// Private Functions

	private void HandleTargetChanged(Vector3Int newPos) {
		List<PathNode> tiles = _movementController.reachableTiles;
		Vector3Int cursorPos = newPos;

		var tile = tiles.SingleOrDefault(node => node.pos.Equals(cursorPos));
		if ( tile != null ) {
			if ( !isDrawn || !lastValidGridPos.Equals(cursorPos) ) {
				Vector3Int startNode = _gridTransform.gridPosition;
				Vector3Int endNode = tile.pos;

				_pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, DrawPath);

				isDrawn = true;
				lastValidGridPos = tile.pos;
			}
		}
		else {
			_clearPathEvent.RaiseEvent();
			isDrawn = false;
		}
	}
	
///// Public Functions
	
	//Constructor
	public DrawPathPreview(
		NodeListEventChannelSO drawPathEvent, 
		VoidEventChannelSO clearPathEvent,
		PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel, 
		Vector3IntEventChannelSO targetChangedEC,
		VoidEventChannelSO previewChangedEC) {
		//in
		_targetChangedEC = targetChangedEC;
		
		//out
		_previewChangedEC = previewChangedEC;
		this._drawPathEvent = drawPathEvent;
		this._clearPathEvent = clearPathEvent;
		this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;

		isDrawn = false;
	}

	public override void Awake(StateMachine stateMachine) {
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnStateEnter() {
		lastValidGridPos = new Vector3Int(-1,-1,-1);
		isDrawn = false;

		_targetChangedEC.OnEventRaised += HandleTargetChanged;
	}

	public override void OnUpdate() { }
	
	public override void OnStateExit() {
		_targetChangedEC.OnEventRaised -= HandleTargetChanged;
	}

	private void DrawPath(List<PathNode> nodes) {
		//todo central cache for preview?
		// preview changed tell everyone
		_previewChangedEC.RaiseEvent();
		_drawPathEvent.RaiseEvent(nodes);
		_movementController.PreviewPath = nodes;
	}
}