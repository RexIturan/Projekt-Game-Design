using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "MoveIntoRange_OnEnter", menuName = "State Machines/Actions/Move Into Range_On Enter")]
public class MoveIntoRange_OnEnterSO : StateActionSO
{
	public override StateAction CreateAction() => new MoveIntoRange_OnEnter();
}

public class MoveIntoRange_OnEnter : StateAction
{
	protected new MoveIntoRange_OnEnterSO OriginSO => (MoveIntoRange_OnEnterSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
