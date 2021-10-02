using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyGEtDistanceToTarget", menuName = "State Machines/Actions/Enemy GEt Distance To Target")]
public class EnemyGetDistanceToTargetSO : StateActionSO
{
	public override StateAction CreateAction() => new EnemyGEtDistanceToTarget();
}

public class EnemyGEtDistanceToTarget : StateAction
{
	protected new EnemyGetDistanceToTargetSO OriginSO => (EnemyGetDistanceToTargetSO)base.OriginSO;

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
