using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_FinishAbilityExecution_OnUpdate",
	menuName = "State Machines/Actions/Character/Finish Ability Execution On Update")]
public class C_FinishAbilityExecution_OnUpdateSO : StateActionSO {
	public override StateAction CreateAction() => new C_FinishAbilityExecution_OnUpdate();
}

public class C_FinishAbilityExecution_OnUpdate : StateAction {
	protected new C_FinishAbilityExecution_OnUpdateSO OriginSO =>
		( C_FinishAbilityExecution_OnUpdateSO )base.OriginSO;

	private AbilityController _abilityController;

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	public override void OnUpdate() {
		// todo finish if all effectys are applyed and all animations are finished
		// if (playerStateContainer.animationQueue.Count == 0) {
		if ( true ) {
			_abilityController.abilityExecuted = true;
		}
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}