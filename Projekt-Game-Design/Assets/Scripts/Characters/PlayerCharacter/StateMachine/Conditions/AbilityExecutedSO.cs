using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityExecuted", menuName = "State Machines/Conditions/Ability Executed")]
public class AbilityExecutedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new AbilityExecuted();
}

public class AbilityExecuted : Condition
{
	protected new AbilityExecutedSO OriginSO => (AbilityExecutedSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
	}
	
	protected override bool Statement()
	{
		return true;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
