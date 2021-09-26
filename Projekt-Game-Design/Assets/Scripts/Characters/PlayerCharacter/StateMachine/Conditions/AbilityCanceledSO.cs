using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityCanceled", menuName = "State Machines/Conditions/Ability Canceled")]
public class AbilityCanceledSO : StateConditionSO
{
	protected override Condition CreateCondition() => new AbilityCanceled();
}

public class AbilityCanceled : Condition
{
	protected new AbilityCanceledSO OriginSO => (AbilityCanceledSO)base.OriginSO;

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
