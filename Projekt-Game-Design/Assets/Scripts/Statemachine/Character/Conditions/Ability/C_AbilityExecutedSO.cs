using Characters.Ability;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_AbilityExecuted",
	menuName = "State Machines/Conditions/Character/Ability Executed")]
public class C_AbilityExecutedSO : StateConditionSO {
	protected override Condition CreateCondition() => new C_AbilityExecuted();
}

public class C_AbilityExecuted : Condition {
	protected new C_AbilityExecutedSO OriginSO => ( C_AbilityExecutedSO )base.OriginSO;

	private AbilityController _abilityController;

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	protected override bool Statement() {
		return _abilityController.abilityExecuted;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}