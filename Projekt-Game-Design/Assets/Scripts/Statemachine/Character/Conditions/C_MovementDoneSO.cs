using Characters.Movement;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_MovementDone",
	menuName = "State Machines/Conditions/Character/Movement Done")]
public class C_MovementDoneSO : StateConditionSO {
	protected override Condition CreateCondition() => new C_MovementDone();
}

public class C_MovementDone : Condition {
	private MovementController _movementController;

	public override void Awake(StateMachine stateMachine) {
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
	}

	protected override bool Statement() {
		return _movementController.MovementDone;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}