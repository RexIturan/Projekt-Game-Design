using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "InitializedTacticsMode", menuName = "State Machines/Conditions/GameState/Initialized Tactics Mode")]
public class InitializedTacticsModeSO : StateConditionSO
{
	protected override Condition CreateCondition() => new InitializedTacticsMode();
}

public class InitializedTacticsMode : Condition
{
	protected new InitializedTacticsModeSO OriginSO => (InitializedTacticsModeSO)base.OriginSO;
	private GameSC _gameSc;

	public override void Awake(StateMachine stateMachine) {
		_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}

	protected override bool Statement() {
		// return _gameSc.initializedTactics;
		return true;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
