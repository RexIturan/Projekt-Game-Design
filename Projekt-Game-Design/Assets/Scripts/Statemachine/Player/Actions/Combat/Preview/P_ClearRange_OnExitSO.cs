using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

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