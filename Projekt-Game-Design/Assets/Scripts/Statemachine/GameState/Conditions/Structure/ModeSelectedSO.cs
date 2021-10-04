using System;
using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ModeSelected", menuName = "State Machines/Conditions/GameState/Mode Selected")]
public class ModeSelectedSO : StateConditionSO {
	[SerializeField] private EMode mode;
	protected override Condition CreateCondition() => new ModeSelected(mode);
}

public class ModeSelected : Condition
{
	protected new ModeSelectedSO OriginSO => (ModeSelectedSO)base.OriginSO;
	private GameSC gameSc;
	private EMode mode;

	public ModeSelected(EMode mode) {
		this.mode = mode;
	}
	
	public override void Awake(StateMachine stateMachine) {
		gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}

	protected override bool Statement() {
		bool statement = false;
		switch (mode) {
			case EMode.tactics:
				statement = gameSc.isInTacticsMode;
				break;
			case EMode.macro:
				throw new ArgumentOutOfRangeException();
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
		return statement;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}

public enum EMode {
	tactics,
	macro
}
