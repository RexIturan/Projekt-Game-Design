using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using GDP01.Characters.Component;

/// <summary>
/// Sets the singleTarget flag in the ability controller 
/// if ability only has one proper target at the moment
/// </summary>
[CreateAssetMenu(fileName = "c_SetSingleTargetFlag_OnEnter",
	menuName = "State Machines/Actions/Character/Set Single Target Flag On Enter")]
public class C_SetSingleTargetFlag_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() =>
		new C_SetSingleTargetFlag_OnEnter();
}

public class C_SetSingleTargetFlag_OnEnter : StateAction {
	private AbilityController _abilityController;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}
	
	/// <summary>
	/// Sets the singleTarget flag in the ability controller 
	/// if ability only has one proper target at the moment
	/// </summary>
	public override void OnStateEnter() {
		_abilityController.UpdateSingleTargetFlag();
	}
}