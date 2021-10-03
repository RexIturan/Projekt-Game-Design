using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "TargetInRange", menuName = "State Machines/Conditions/Target In Range")]
public class TargetInRangeSO : StateConditionSO
{
	protected override Condition CreateCondition() => new TargetInRange();
}

public class TargetInRange : Condition
{
	protected new TargetInRangeSO OriginSO => (TargetInRangeSO)base.OriginSO;

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
