using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyCanAct", menuName = "State Machines/Conditions/Enemy Can Act")]
public class EnemyCanActSO : StateConditionSO
{
	protected override Condition CreateCondition() => new EnemyCanAct();
}

public class EnemyCanAct : Condition
{
	private EnemyCharacterSC enemyCharacterSc;
	protected new EnemyCanActSO OriginSO => (EnemyCanActSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return this.enemyCharacterSc.energy > 0 && !this.enemyCharacterSc.noActionPossible;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
