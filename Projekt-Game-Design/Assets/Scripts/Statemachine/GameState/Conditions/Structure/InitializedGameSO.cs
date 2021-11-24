﻿using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "InitializedGame", menuName = "State Machines/Conditions/GameState/Initialized Game")]
public class InitializedGameSO : StateConditionSO
{
	protected override Condition CreateCondition() => new InitializedGame();
}

public class InitializedGame : Condition
{
	protected new InitializedGameSO OriginSO => (InitializedGameSO)base.OriginSO;
	private GameSC _gameSc;

	public override void Awake(StateMachine stateMachine) {
		_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}

	protected override bool Statement() {
		return _gameSc.initializedGame;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}