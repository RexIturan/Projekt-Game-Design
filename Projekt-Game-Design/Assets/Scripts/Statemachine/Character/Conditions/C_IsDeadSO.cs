using Characters;
using Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_IsDead", menuName = "State Machines/Conditions/Character/Is Dead")]
public class C_IsDeadSO : StateConditionSO {
	protected override Condition CreateCondition() => new C_IsDead();
}

public class C_IsDead : Condition {
	protected new C_IsDeadSO OriginSO => ( C_IsDeadSO )base.OriginSO;

	private Targetable _targetable;

	public override void Awake(StateMachine stateMachine) {
		_targetable = stateMachine.gameObject.GetComponent<Targetable>();
	}

	protected override bool Statement() {
		return _targetable.IsDead;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}