using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityExecuted_enemy", menuName = "State Machines/Conditions/Enemy/Ability Executed")]
public class AbilityExecuted_enemySO : StateConditionSO
{
	protected override Condition CreateCondition() => new AbilityExecuted_enemy();
}

public class AbilityExecuted_enemy : Condition
{
	private EnemyCharacterSC enemyCharacterSc;

	public override void Awake(StateMachine stateMachine)
	{
		this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return this.enemyCharacterSc.abilityExecuted;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
