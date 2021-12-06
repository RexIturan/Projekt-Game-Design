using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsDead", menuName = "State Machines/Conditions/Is Dead")]
public class IsDeadSO : StateConditionSO {
	protected override Condition CreateCondition() => new IsDead();
}

public class IsDead : Condition {
	protected new IsDeadSO OriginSO => ( IsDeadSO )base.OriginSO;

	private Statistics _statistics;

	public override void Awake(StateMachine stateMachine) {
		_statistics = stateMachine.gameObject.GetComponent<Statistics>();
	}

	protected override bool Statement() {
		return _statistics.StatusValues.HitPoints.IsMin();
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}