using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_IsAnimationDone",
	menuName = "State Machines/Conditions/Character/Is Animation Done")]
public class C_IsAnimationDoneSO : StateConditionSO {
	protected override Condition CreateCondition() => new C_IsAnimationDone();
}

public class C_IsAnimationDone : Condition {
	private ModelController _modelController;

	public override void Awake(StateMachine stateMachine) {
		_modelController = stateMachine.gameObject.GetComponent<ModelController>();
	}

	protected override bool Statement() {
		return !_modelController.GetAnimationController().IsAnimationInProgress();
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}