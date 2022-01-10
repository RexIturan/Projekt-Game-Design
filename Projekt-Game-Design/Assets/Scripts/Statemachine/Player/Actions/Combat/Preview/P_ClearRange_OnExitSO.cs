using Ability.ScriptableObjects;
using Characters.Ability;
using Combat;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_ClearRange_OnExit", menuName = "State Machines/Actions/Player/Clear Range Tiles On Exit")]
public class P_ClearRange_OnExitSO : StateActionSO {
	[Header("Sending Events On")]
	[SerializeField] private VoidEventChannelSO clearRangeTilesEC;

	public override StateAction CreateAction() => new P_ClearRange_OnExit(clearRangeTilesEC);
}

public class P_ClearRange_OnExit : StateAction {
	private readonly VoidEventChannelSO _clearRangeTilesEC;
		
	public P_ClearRange_OnExit(VoidEventChannelSO clearRangeTilesEC) {
		_clearRangeTilesEC = clearRangeTilesEC;
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() { }

	public override void OnStateExit() {
		_clearRangeTilesEC.RaiseEvent();
	}
}