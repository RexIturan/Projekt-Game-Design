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
using Input;

[CreateAssetMenu(fileName = "DrawPathPreview",
	menuName = "State Machines/Actions/Player/DrawPathPreview")]
public class DrawPathPreviewSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	private NodeListEventChannelSO drawPathEvent;

	[SerializeField] private VoidEventChannelSO clearPathEvent;

	[Header("Receiving Events On")] [SerializeField]
	private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

	[SerializeField] private InputCache inputCache;

	public override StateAction CreateAction() => new DrawPathPreview(drawPathEvent, clearPathEvent,
		pathfindingPathQueryEventChannel, inputCache);
}

public class DrawPathPreview : StateAction {
	private readonly NodeListEventChannelSO _drawPathEvent;
	private readonly VoidEventChannelSO _clearPathEvent;
	private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;
	private readonly InputCache inputCache;
	private MovementController _movementController;
	private GridTransform _gridTransform;

	private Vector3Int lastDrawnGridPos;
	private bool isDrawn;

	//Constructor
	public DrawPathPreview(NodeListEventChannelSO drawPathEvent, VoidEventChannelSO clearPathEvent,
		PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel,
		InputCache inputCache) {
		this._drawPathEvent = drawPathEvent;
		this._clearPathEvent = clearPathEvent;
		this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
		this.inputCache = inputCache;

		isDrawn = false;
	}

	public override void Awake(StateMachine stateMachine) {
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnUpdate() {
		bool isReachable = false;
		List<PathNode> tiles = _movementController.reachableTiles;

		Vector3Int abovePos = inputCache.cursor.abovePos.gridPos;

		for ( int i = 0; i < tiles.Count && !isReachable; i++ ) {
			if ( tiles[i].pos.Equals(abovePos) ) {
				isReachable = true;

				// only draw path if it wasn't already
				if(isDrawn == false || !tiles[i].Equals(lastDrawnGridPos)) { 
		  		Vector3Int startNode = _gridTransform.gridPosition;
	  			Vector3Int endNode = tiles[i].pos;

  				_pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, DrawPath);

					isDrawn = true;
					lastDrawnGridPos = tiles[i].pos;
				}
			}
		}

		if ( !isReachable ) { 
			_clearPathEvent.RaiseEvent();
			isDrawn = false;
		}
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }

	private void DrawPath(List<PathNode> nodes) {
		_drawPathEvent.RaiseEvent(nodes);
	}
}