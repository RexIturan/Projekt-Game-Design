using Characters.Ability;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "P_UpdateAbilities_OnEnter",
	menuName = "State Machines/Actions/Player/Update Abilities On Enter")]
public class P_UpdateAbilities_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new P_UpdateAbilities_OnEnter();
}

public class P_UpdateAbilities_OnEnter : StateAction {
	protected new P_UpdateAbilities_OnEnterSO OriginSO =>
		( P_UpdateAbilities_OnEnterSO )base.OriginSO;

	private AbilityController _abilityController;

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() {
		_abilityController.RefreshAbilities();
	}

	public override void OnStateExit() { }
}