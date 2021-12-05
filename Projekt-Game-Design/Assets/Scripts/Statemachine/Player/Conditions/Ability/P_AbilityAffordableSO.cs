using Ability.ScriptableObjects;
using Characters;
using Characters.Ability;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_AbilityAffordable",
	menuName = "State Machines/Conditions/Ability Affordable ")]
public class P_AbilityAffordableSO : StateConditionSO {
	[SerializeField] private AbilityContainerSO abilityContainer;

	protected override Condition CreateCondition() => new P_AbilityAffordable(abilityContainer);
}

public class P_AbilityAffordable : Condition {
	protected new P_AbilityAffordableSO OriginSO => ( P_AbilityAffordableSO )base.OriginSO;

	private readonly AbilityContainerSO _abilityContainer;
	private AbilityController _abilityController;
	private Statistics _statistics;

	public P_AbilityAffordable(AbilityContainerSO abilityContainer) {
		this._abilityContainer = abilityContainer;
	}

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_statistics = stateMachine.gameObject.GetComponent<Statistics>();
	}

	protected override bool Statement() {
		return _statistics.StatusValues.Energy.value >=
		       _abilityContainer.abilities[_abilityController.SelectedAbilityID].costs;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}