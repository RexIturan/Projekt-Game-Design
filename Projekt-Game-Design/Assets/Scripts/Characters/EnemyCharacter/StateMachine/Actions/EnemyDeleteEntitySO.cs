using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyDeleteEntity", menuName = "State Machines/Actions/Enemy Delete Entity")]
public class EnemyDeleteEntitySO : StateActionSO
{
	public override StateAction CreateAction() => new EnemyDeleteEntity();
}

public class EnemyDeleteEntity : StateAction
{
	protected new EnemyDeleteEntitySO OriginSO => (EnemyDeleteEntitySO)base.OriginSO;

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
