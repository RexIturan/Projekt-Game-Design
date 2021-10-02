using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyFIndTarget", menuName = "State Machines/Actions/Enemy FInd Target")]
public class EnemyFIndTargetSO : StateActionSO
{
	public override StateAction CreateAction() => new EnemyFIndTarget();
}

public class EnemyFIndTarget : StateAction
{
	protected new EnemyFIndTargetSO OriginSO => (EnemyFIndTargetSO)base.OriginSO;

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
