using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsOnTurn_enemy",
	menuName = "State Machines/Conditions/Enemy/Is On Turn")]
public class IsOnTurn_EnemySO : StateConditionSO {
	protected override Condition CreateCondition() => new IsOnTurn_Enemy();
}

public class IsOnTurn_Enemy : Condition {
	private EnemyCharacterSC _enemyCharacterSc;

	public override void Awake(StateMachine stateMachine) {
		_enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	protected override bool Statement() {
		return _enemyCharacterSc.isNextToAct;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}