using Characters.Ability;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilitySelected",
	menuName = "State Machines/Conditions/Ability Selected")]
public class AbilitySelectedSO : StateConditionSO {
	protected override Condition CreateCondition() => new AbilitySelected();
}

public class AbilitySelected : Condition {
	protected new AbilitySelectedSO OriginSO => ( AbilitySelectedSO )base.OriginSO;

	private AbilityController _abilityController;

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	protected override bool Statement() {
		return _abilityController.abilitySelected;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}