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

	private Vector3Int lastValidGridPos;
	private Vector3Int lastCursorPos;
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
		List<PathNode> tiles = _movementController.reachableTiles;
		Vector3Int cursorPos = inputCache.cursor.abovePos.gridPos;

		if (!lastCursorPos.Equals(cursorPos)) {
			lastCursorPos = cursorPos;
			var tile = tiles.SingleOrDefault(node => node.pos.Equals(cursorPos)); 
			if(tile != null) {
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
		
		// for ( int i = 0; i < tiles.Count && !isReachable; i++ ) {
		// 	if ( tiles[i].pos.Equals(cursorPos) ) {
		// 		isReachable = true;
		//
		// 		// only draw path if it wasn't already
		// 		if(isDrawn == false || !tiles[i].Equals(lastDrawnGridPos)) { 
		//   		Vector3Int startNode = _gridTransform.gridPosition;
	 //  			Vector3Int endNode = tiles[i].pos;
		//
  // 				_pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, DrawPath);
		//
		// 			isDrawn = true;
		// 			lastDrawnGridPos = tiles[i].pos;
		// 		}
		// 	}
		// }

		
	}

	public override void OnStateEnter() {
		lastCursorPos = new Vector3Int(-1,-1,-1);
		lastValidGridPos = new Vector3Int(-1,-1,-1);
		isDrawn = false;
	}

	public override void OnStateExit() { }

	private void DrawPath(List<PathNode> nodes) {
		_drawPathEvent.RaiseEvent(nodes);
		_movementController.PreviewPath = nodes;
	}
}