using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Events.ScriptableObjects.Pathfinding;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_SaveReachableNodes_OnEnterSO",
	menuName = "State Machines/Actions/Character/Save Reachable Nodes On Enter")]
public class C_SaveReachableNodes_OnEnterSO : StateActionSO {
	[SerializeField] private PathfindingQueryEventChannelSO pathfindingQueryEvent;
	public override StateAction CreateAction() => new C_SaveReachableNodes_OnEnter(pathfindingQueryEvent);
}

public class C_SaveReachableNodes_OnEnter : StateAction {
	private readonly PathfindingQueryEventChannelSO _pathfindingQueryEvent;
	private MovementController _movementController;
	private GridTransform _gridTransform;
	
	public C_SaveReachableNodes_OnEnter(PathfindingQueryEventChannelSO pathfindingQueryEvent) {
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