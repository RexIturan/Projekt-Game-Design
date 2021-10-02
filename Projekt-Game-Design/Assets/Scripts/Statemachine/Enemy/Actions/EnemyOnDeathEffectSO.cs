using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyOnDeathEffect", menuName = "State Machines/Actions/Enemy On Death Effect")]
public class EnemyOnDeathEffectSO : StateActionSO
{
	public override StateAction CreateAction() => new EnemyOnDeathEffect();
}

public class EnemyOnDeathEffect : StateAction
{
	protected new EnemyOnDeathEffectSO OriginSO => (EnemyOnDeathEffectSO)base.OriginSO;

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
