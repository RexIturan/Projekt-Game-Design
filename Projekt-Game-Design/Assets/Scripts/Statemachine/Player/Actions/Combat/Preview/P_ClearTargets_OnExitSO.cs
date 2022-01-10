using Ability.ScriptableObjects;
using Characters.Ability;
using Combat;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_ClearTargets_OnExitSO", menuName = "State Machines/Actions/Player/Clear Targets On Exit")]
public class P_ClearTargets_OnExitSO : StateActionSO {
	[Header("Sending Events On")]
	[SerializeField] private VoidEventChannelSO clearTargetsEC;

	public override StateAction CreateAction() => new P_ClearTargets_OnExit(clearTargetsEC);
}

public class P_ClearTargets_OnExit : StateAction {
	private readonly VoidEventChannelSO _clearTargetsEC;
		
	public P_ClearTargets_OnExit(VoidEventChannelSO clearTargetsEC) {
		_clearTargetsEC = clearTargetsEC;
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() { }

	public override void OnStateExit() {
		_clearTargetsEC.RaiseEvent();
	}
}