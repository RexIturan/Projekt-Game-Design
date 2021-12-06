using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Events.ScriptableObjects.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "SaveReachableNodes",
	menuName = "State Machines/Actions/Player/SaveReachableNodes")]
public class SaveReachableNodesSO : StateActionSO {
	[SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
	public override StateAction CreateAction() => new SaveReachableNodes(pathfindingQueryEvent);
}

public class SaveReachableNodes : StateAction {
	private readonly PathfindingQueryEventChannelSO _pathfindingQueryEvent;
	private MovementController _movementController;
	private GridTransform _gridTransform;
	
	public SaveReachableNodes(PathfindingQueryEventChannelSO pathfindingQueryEvent) {
		this._pathfindingQueryEvent = pathfindingQueryEvent;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnStateEnter() {
		// Debug.Log("Calculate new reachable tiles... max distance = " + playerStateContainer.GetMaxMoveDistance());
		_pathfindingQueryEvent.RaiseEvent(_gridTransform.gridPosition,
			_movementController.GetMaxMoveDistance(), SaveToStateContainer);
	}

	public void SaveToStateContainer(List<PathNode> reachableTiles) {
		_movementController.reachableTiles = reachableTiles;
	}
}