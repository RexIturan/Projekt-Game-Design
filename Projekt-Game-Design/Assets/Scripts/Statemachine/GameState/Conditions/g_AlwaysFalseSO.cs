using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_AlwaysFalse", menuName = "State Machines/Conditions/g_Always False")]
public class g_AlwaysFalseSO : StateConditionSO
{
	protected override Condition CreateCondition() => new g_AlwaysFalse();
}

public class g_AlwaysFalse : Condition
{
	protected new g_AlwaysFalseSO OriginSO => (g_AlwaysFalseSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
	}
	
	protected override bool Statement()
	{
		return false;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
