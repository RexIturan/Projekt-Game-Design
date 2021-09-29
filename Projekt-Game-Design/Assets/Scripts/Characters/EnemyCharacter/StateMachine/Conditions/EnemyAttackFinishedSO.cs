using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyAttackFinished", menuName = "State Machines/Conditions/Enemy Attack Finished")]
public class EnemyAttackFinishedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new EnemyAttackFinished();
}

public class EnemyAttackFinished : Condition
{
	private EnemyCharacterSC enemyCharacterSc;
	protected new EnemyAttackFinishedSO OriginSO => (EnemyAttackFinishedSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return this.enemyCharacterSc.attackFinished;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
