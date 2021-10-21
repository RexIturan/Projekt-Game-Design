using Events.ScriptableObjects;
using System.Collections.Generic;
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
	private const float TimePerStep = 0.2f;

	protected new MoveToTargetSO OriginSO => ( MoveToTargetSO )base.OriginSO;

	private EnemyCharacterSC _enemyCharacterSC;
	private float _timeSinceLastStep;
	private List<PathNode> _path;
	private int _currentStep;
	private readonly PathFindingPathQueryEventChannelSO _pathfindingPathQueryEventChannel;
	private CharacterList _characterList;

	public E_MoveToTarget_OnUpdate(
		PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel) {
		this._pathfindingPathQueryEventChannel = pathfindingPathQueryEventChannel;
	}

	public override void Awake(StateMachine stateMachine) {
		_enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	public override void OnStateEnter() {
		_characterList = Object.FindObjectOfType<CharacterList>();
		Vector3Int startNode = new Vector3Int(_enemyCharacterSC.gridPosition.x,
			_enemyCharacterSC.gridPosition.z,
			0);
		Vector3Int endNode = new Vector3Int(_enemyCharacterSC.movementTarget.x,
			_enemyCharacterSC.movementTarget.y,
			0);

		_pathfindingPathQueryEventChannel.RaiseEvent(startNode, endNode, SavePath);

		_timeSinceLastStep = 0;
		_currentStep = 1;
		_enemyCharacterSC.movementDone = false;
	}

	public override void OnUpdate() {
		if ( _currentStep >= _path.Count || playerOnField() ) {
			_enemyCharacterSC.movementDone = true;
			_enemyCharacterSC.abilityExecuted = true;
			_enemyCharacterSC.isDone = true;
		}

		if ( !_enemyCharacterSC.movementDone ) {
			_timeSinceLastStep += Time.deltaTime;
			if ( _timeSinceLastStep >= TimePerStep ) {
				_timeSinceLastStep -= TimePerStep;

				_enemyCharacterSC.gridPosition = new Vector3Int(_path[_currentStep].x,
					1,
					_path[_currentStep].y);
				_enemyCharacterSC.MoveToGridPosition();

				_currentStep++;
			}
		}
	}

	private void SavePath(List<PathNode> path) {
		this._path = path;
	}

	private bool playerOnField() {
		bool fieldOccupied = false;

		Vector3Int stepPosition = new Vector3Int(_path[_currentStep].x,
			1,
			_path[_currentStep].y);
		foreach ( GameObject player in _characterList.playerContainer ) {
			//todo move get component outside of methode?? OR cache playerCharcterSC
			if ( player.GetComponent<PlayerCharacterSC>().gridPosition.Equals(stepPosition) )
				fieldOccupied = true;
		}

		return fieldOccupied;
	}
}