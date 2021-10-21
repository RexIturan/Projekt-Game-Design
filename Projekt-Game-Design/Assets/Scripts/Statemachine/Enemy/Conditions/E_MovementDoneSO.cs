using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_MovementDone",
	menuName = "State Machines/Conditions/Enemy/Movement Done")]
public class E_MovementDoneSO : StateConditionSO {
	protected override Condition CreateCondition() => new E_MovementDone();
}

public class E_MovementDone : Condition {
	private EnemyCharacterSC _enemySC;

	public override void Awake(StateMachine stateMachine) {
		_enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	protected override bool Statement() {
		return _enemySC.movementDone;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}