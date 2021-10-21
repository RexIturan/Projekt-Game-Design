using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsDead_enemy", menuName = "State Machines/Conditions/Enemy/Is Dead")]
public class IsDead_EnemySO : StateConditionSO {
	protected override Condition CreateCondition() => new IsDead_Enemy();
}

public class IsDead_Enemy : Condition {
	private EnemyCharacterSC _enemyCharacterSc;

	public override void Awake(StateMachine stateMachine) {
		_enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	protected override bool Statement() {
		return _enemyCharacterSc.healthPoints <= 0;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}