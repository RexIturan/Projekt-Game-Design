using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilitySelected", menuName = "State Machines/Conditions/Ability Selected")]
public class AbilitySelectedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new AbilitySelected();
}

public class AbilitySelected : Condition
{
	protected new AbilitySelectedSO OriginSO => (AbilitySelectedSO)base.OriginSO;

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
