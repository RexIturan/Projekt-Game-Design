using Ability.ScriptableObjects;
using Characters.Ability;
using Combat;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_ClearPattern_OnExit", menuName = "State Machines/Actions/Player/Clear Pattern On Exit")]
public class P_ClearPattern_OnExitSO : StateActionSO {
	[Header("Sending Events On")]
	[SerializeField] private VoidEventChannelSO clearPatternEC;

	public override StateAction CreateAction() => new P_ClearPattern_OnExit(clearPatternEC);
}

public class P_ClearPattern_OnExit : StateAction {
	private readonly VoidEventChannelSO _clearPatternEC;
		
	public P_ClearPattern_OnExit(VoidEventChannelSO clearPatternEC) {
		_clearPatternEC = clearPatternEC;
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() { }

	public override void OnStateExit() {
		_clearPatternEC.RaiseEvent();
	}
}