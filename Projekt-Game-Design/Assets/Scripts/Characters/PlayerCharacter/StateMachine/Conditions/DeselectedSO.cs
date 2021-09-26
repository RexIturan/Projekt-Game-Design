using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Deselected", menuName = "State Machines/Conditions/Deselected")]
public class DeselectedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new Deselected();
}

public class Deselected : Condition
{
	protected new DeselectedSO OriginSO => (DeselectedSO)base.OriginSO;

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
