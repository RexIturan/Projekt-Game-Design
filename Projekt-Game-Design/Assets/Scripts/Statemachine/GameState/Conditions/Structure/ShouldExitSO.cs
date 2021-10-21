using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ShouldExit", menuName = "State Machines/Conditions/Should Exit")]
public class ShouldExitSO : StateConditionSO
{
	protected override Condition CreateCondition() => new ShouldExit();
}

public class ShouldExit : Condition
{
	protected new ShouldExitSO OriginSO => (ShouldExitSO)base.OriginSO;
	private GameSC _gameSc;

	public override void Awake(StateMachine stateMachine) {
		_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}
	
	protected override bool Statement()
	{
		return _gameSc.shouldExit;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
