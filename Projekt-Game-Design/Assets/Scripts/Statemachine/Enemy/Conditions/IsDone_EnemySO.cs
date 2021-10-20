using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsDone_enemy", menuName = "State Machines/Conditions/Enemy/Is Done")]
public class IsDone_EnemySO : StateConditionSO {
	protected override Condition CreateCondition() => new IsDone_Enemy();
}

public class IsDone_Enemy : Condition {
	private EnemyCharacterSC _enemyCharacterSc;

	public override void Awake(StateMachine stateMachine) {
		_enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	protected override bool Statement() {
		return _enemyCharacterSc.isDone;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}