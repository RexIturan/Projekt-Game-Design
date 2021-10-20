using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilitySelected_enemy",
	menuName = "State Machines/Conditions/Enemy/Ability Selected")]
public class AbilitySelected_EnemySO : StateConditionSO {
	protected override Condition CreateCondition() => new AbilitySelected_Enemy();
}

public class AbilitySelected_Enemy : Condition {
	private EnemyCharacterSC _enemyCharacterSc;

	public override void Awake(StateMachine stateMachine) {
		_enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	protected override bool Statement() {
		return _enemyCharacterSc.abilitySelected;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}