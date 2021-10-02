using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsDead_enemy", menuName = "State Machines/Conditions/Enemy/Is Dead")]
public class IsDead_enemySO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsDead_enemy();
}

public class IsDead_enemy : Condition
{
	private EnemyCharacterSC enemyCharacterSc;

	public override void Awake(StateMachine stateMachine)
	{
		this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return this.enemyCharacterSc.healthPoints <= 0;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{

	}
}
