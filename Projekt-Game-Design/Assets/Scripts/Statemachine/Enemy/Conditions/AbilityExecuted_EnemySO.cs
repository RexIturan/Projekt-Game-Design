using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityExecuted_enemy",
	menuName = "State Machines/Conditions/Enemy/Ability Executed")]
public class AbilityExecuted_EnemySO : StateConditionSO {
	protected override Condition CreateCondition() => new AbilityExecuted_Enemy();
}

public class AbilityExecuted_Enemy : Condition {
	private EnemyCharacterSC _enemyCharacterSc;

	public override void Awake(StateMachine stateMachine) {
		_enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	protected override bool Statement() {
		return _enemyCharacterSc.abilityExecuted;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}