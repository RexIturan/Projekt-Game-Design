using Events.ScriptableObjects;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_MoveToTarget_OnUpdate",
	menuName = "State Machines/Actions/Enemy/Move To Target On Update")]
public class E_MoveToTarget_OnUpdateSO : StateActionSO {
	[SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;

	public override StateAction CreateAction() =>
		new E_MoveToTarget_OnUpdate(pathfindingPathQueryEventChannel);
}

public class E_MoveToTarget_OnUpdate : StateAction {
	protected new MoveToTargetSO OriginSO => ( MoveToTargetSO )base.OriginSO;
	
	private const float TimePerStep = 0.5f;
	private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;
	
	private EnemyCharacterSC _enemyCharacterSC;
	private float _timeSinceLastStep = 0;
	private List<PathNode> _path;
	private int _currentStep;
	
	private CharacterList _characterList;

	public E_MoveToTarget_OnUpdate(
		PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel) {
		this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
	}

	public override void Awake(StateMachine stateMachine) {
		_enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	public override void OnStateEnter() {
		_characterList = GameObject.FindObjectOfType<CharacterList>();
		Vector3Int startNode = _enemyCharacterSC.gridPosition;
		Vector3Int endNode = _enemyCharacterSC.movementTarget.pos;

		_pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, SavePath);

		_timeSinceLastStep = 0;
		_currentStep = 1;
		_enemyCharacterSC.movementDone = false;
	}

	public override void OnUpdate() {
		Vector3Int stepPosition = _path[_currentStep].pos;

		if ( _currentStep >= _path.Count || isFieldOccupied(stepPosition) ) {
			_enemyCharacterSC.movementDone = true;
			_enemyCharacterSC.abilityExecuted = true;
			_enemyCharacterSC.isDone = true;
		}

		if ( !_enemyCharacterSC.movementDone ) {
			_timeSinceLastStep += Time.deltaTime;
			if ( _timeSinceLastStep >= TimePerStep ) {
				_timeSinceLastStep -= TimePerStep;

				_enemyCharacterSC.gridPosition = _path[_currentStep].pos;
				_enemyCharacterSC.MoveToGridPosition();

				_currentStep++;
			}
		}
	}

	private void SavePath(List<PathNode> path) {
		this._path = path;
	}

	//todo(grid/graph/pathfinding) move methode to GridContainer or GraphContainer
	private bool isFieldOccupied(Vector3Int position) {
		bool fieldOccupied = false;

		foreach ( GameObject player in _characterList.playerContainer ) {
			//todo move get component outside of methode?? OR cache playerCharcterSC
			if ( player.GetComponent<GridTransform>().gridPosition.Equals(position) )
				fieldOccupied = true;
		}

		return fieldOccupied;
	}
}