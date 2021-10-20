using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_AlwaysFalse", menuName = "State Machines/Conditions/g_Always False")]
public class G_AlwaysFalseSO : StateConditionSO
{
	protected override Condition CreateCondition() => new G_AlwaysFalse();
}

public class G_AlwaysFalse : Condition
{
	protected new G_AlwaysFalseSO OriginSO => (G_AlwaysFalseSO)base.OriginSO;

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
