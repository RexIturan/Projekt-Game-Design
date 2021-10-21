using System;
using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ModeSelected", menuName = "State Machines/Conditions/GameState/Mode Selected")]
public class ModeSelectedSO : StateConditionSO {
	[SerializeField] private Mode mode;
	protected override Condition CreateCondition() => new ModeSelected(mode);
}

public class ModeSelected : Condition
{
	protected new ModeSelectedSO OriginSO => (ModeSelectedSO)base.OriginSO;
	private GameSC _gameSc;
	private Mode _mode;

	public ModeSelected(Mode mode) {
		this._mode = mode;
	}
	
	public override void Awake(StateMachine stateMachine) {
		_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}

	protected override bool Statement() {
		bool statement = false;
		switch (_mode) {
			case Mode.Tactics:
				statement = _gameSc.isInTacticsMode;
				break;
			case Mode.Macro:
				throw new ArgumentOutOfRangeException();
				// break;
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

public enum Mode {
	Tactics,
	Macro
}
