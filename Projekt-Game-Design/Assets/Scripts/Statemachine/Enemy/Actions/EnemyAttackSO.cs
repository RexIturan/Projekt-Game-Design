using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyAttack", menuName = "State Machines/Actions/Enemy Attack")]
public class EnemyAttackSO : StateActionSO
{
	public override StateAction CreateAction() => new EnemyAttack();
}

public class EnemyAttack : StateAction
{
	protected new EnemyAttackSO OriginSO => (EnemyAttackSO)base.OriginSO;

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
