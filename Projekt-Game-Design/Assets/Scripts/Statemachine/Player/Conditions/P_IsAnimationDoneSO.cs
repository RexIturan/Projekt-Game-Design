using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_AnimationDone",
	menuName = "State Machines/Conditions/Player/Animation Done")]
public class P_IsAnimationDoneSO : StateConditionSO {
	protected override Condition CreateCondition() => new P_IsAnimationDone();
}

public class P_IsAnimationDone : Condition {
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