using Events.ScriptableObjects;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Grid;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_MoveToTarget_OnUpdate",
	menuName = "State Machines/Actions/Character/Move To Target On Update")]
public class C_MoveToTarget_OnUpdateSO : StateActionSO {
	[SerializeField] private GridDataSO gridDataSO;
	[SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

	public override StateAction CreateAction() => new C_MoveToTarget_OnUpdate(pathfindingPathQueryEventChannel, gridDataSO);
}

public class C_MoveToTarget_OnUpdate : StateAction {
	protected new C_MoveToTarget_OnUpdateSO OriginSO => ( C_MoveToTarget_OnUpdateSO )base.OriginSO;

	private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEC;
	private readonly GridDataSO _gridDataSO;
	
	// Game Object Components

	private MovementController _movementController;
	private GridTransform _gridTransform;
	
	// local variables
	
	private float TimePerStep;
	private float _timeSinceLastStep;
	private List<PathNode> _path;
	private int _currentStep;

	public C_MoveToTarget_OnUpdate(PathFindingPathQueryEventChannelSO pathfindingPathQueryEC, GridDataSO gridDataSO) {
		this._pathfindingPathQueryEC = pathfindingPathQueryEC;
		_gridDataSO = gridDataSO;
	}

	public override void Awake(StateMachine stateMachine) {
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
		
		//todo calc in movementcontroller
		TimePerStep = _gridDataSO.CellSize / _movementController.moveSpeed;
	}

	public override void OnUpdate() {
		
		//todo move to movement controller
		// if ( _currentStep >= _path.Count && _timeSinceLastStep >= TimePerStep )
		// 	_movementController.MovementDone = true;
		//
		// if ( !_movementController.MovementDone ) {
		// 	_movementController.FaceMovingDirection();
		//
		// 	_timeSinceLastStep += Time.deltaTime;
		//
		// 	if ( _timeSinceLastStep >= TimePerStep && _currentStep < _path.Count ) {
		// 		_timeSinceLastStep -= TimePerStep;
		//
		// 		_gridTransform.gridPosition = _path[_currentStep].pos;
		//
		// 		_currentStep++;
		// 	}
		// }
	}

	public override void OnStateEnter() {
		Vector3Int startNode = _gridTransform.gridPosition;
		Vector3Int endNode = _movementController.movementTarget.pos; 

		_pathfindingPathQueryEC.RaiseEvent(startNode, endNode, SavePath);

		_timeSinceLastStep = 0;
		_currentStep = 1;
		_movementController.MovementDone = false;
	}

	private void SavePath(List<PathNode> path) {
		this._path = path;
		_movementController.StartNewMove(path);
	}
}

