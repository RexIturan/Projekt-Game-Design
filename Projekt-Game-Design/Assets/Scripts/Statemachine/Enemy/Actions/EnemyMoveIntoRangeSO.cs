using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyMoveIntoRange", menuName = "State Machines/Actions/Enemy Move Into Range")]
public class EnemyMoveIntoRangeSO : StateActionSO
{
	public override StateAction CreateAction() => new EnemyMoveIntoRange();
}

public class EnemyMoveIntoRange : StateAction
{
	protected new EnemyMoveIntoRangeSO OriginSO => (EnemyMoveIntoRangeSO)base.OriginSO;

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
