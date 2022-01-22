using Ability.ScriptableObjects;
using Characters;
using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_AbilityAffordable",
	menuName = "State Machines/Conditions/Character/Ability Affordable")]
public class C_AbilityAffordableSO : StateConditionSO {
	[SerializeField] private AbilityContainerSO abilityContainer;

	protected override Condition CreateCondition() => new C_AbilityAffordable(abilityContainer);
}

public class C_AbilityAffordable : Condition {
	protected new C_AbilityAffordableSO OriginSO => ( C_AbilityAffordableSO )base.OriginSO;

	private readonly AbilityContainerSO _abilityContainer;
	private AbilityController _abilityController;
	private Statistics _statistics;

	public C_AbilityAffordable(AbilityContainerSO abilityContainer) {
		this._abilityContainer = abilityContainer;
	}

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_statistics = stateMachine.gameObject.GetComponent<Statistics>();
	}

	protected override bool Statement() {
		return _abilityController.IsAbilityAvailable(_abilityController.SelectedAbilityID);
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}