using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_IsDamageInflicted",
	menuName = "State Machines/Conditions/Character/Is Damage Inflicted")]
public class C_IsDamageInflictedSO : StateConditionSO {
	protected override Condition CreateCondition() => new C_IsDamageInflicted();
}

public class C_IsDamageInflicted : Condition {
	private AbilityController _abilityController;

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	protected override bool Statement() {
		return _abilityController.damageInflicted;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}