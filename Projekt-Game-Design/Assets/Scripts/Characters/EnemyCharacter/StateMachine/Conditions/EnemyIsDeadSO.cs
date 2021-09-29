using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyIsDead", menuName = "State Machines/Conditions/Enemy Is Dead")]
public class EnemyIsDeadSO : StateConditionSO
{
	protected override Condition CreateCondition() => new EnemyIsDead();
}

public class EnemyIsDead : Condition
{
	private EnemyCharacterSC enemyCharacterSc;
	protected new EnemyIsDeadSO OriginSO => (EnemyIsDeadSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return this.enemyCharacterSc.healthPoints == 0;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
