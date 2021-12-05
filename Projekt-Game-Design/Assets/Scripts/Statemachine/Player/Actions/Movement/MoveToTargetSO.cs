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

[CreateAssetMenu(fileName = "MoveToTarget",
	menuName = "State Machines/Actions/Player/MoveToTarget")]
public class MoveToTargetSO : StateActionSO {
	[SerializeField] private GridDataSO gridDataSO;
	[SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

	public override StateAction CreateAction() => new MoveToTarget(pathfindingPathQueryEventChannel, gridDataSO);
}

public class MoveToTarget : StateAction {
	protected new MoveToTargetSO OriginSO => ( MoveToTargetSO )base.OriginSO;

	private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;
	private readonly GridDataSO _gridDataSO;
	
	// Game Object Components

	private MovementController _movementController;
	private GridTransform _gridTransform;
	
	// local variables
	
	private float TimePerStep;
	private float _timeSinceLastStep;
	private List<PathNode> _path;
	private int _currentStep;

	public MoveToTarget(PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel, GridDataSO gridDataSO) {
		this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
		_gridDataSO = gridDataSO;
	}

	public override void Awake(StateMachine stateMachine) {
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
		TimePerStep = _gridDataSO.CellSize / _movementController.moveSpeed;
	}

	public override void OnUpdate() {
		if ( _currentStep >= _path.Count && _timeSinceLastStep >= TimePerStep )
			_movementController.MovementDone = true;

		if ( !_movementController.MovementDone ) {
			_movementController.FaceMovingDirection();

			_timeSinceLastStep += Time.deltaTime;

			if ( _timeSinceLastStep >= TimePerStep && _currentStep < _path.Count ) {
				_timeSinceLastStep -= TimePerStep;

				_gridTransform.gridPosition = _path[_currentStep].pos;

				_movementController.MoveToGridPosition();

				_currentStep++;
			}
		}
	}

	public override void OnStateEnter() {
		Vector3Int startNode = _gridTransform.gridPosition;
		Vector3Int endNode = _movementController.movementTarget.pos; 

		_pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, SavePath);

		_timeSinceLastStep = 0;
		_currentStep = 1;
		_movementController.MovementDone = false;
	}

	private void SavePath(List<PathNode> path) {
		this._path = path;
	}
}

