using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "new_B_InvokeBoolEvent",
	menuName = "State Machines/Actions/new Invoke Bool Event")]
public class B_InvokeBoolEventSO : StateActionSO {
	[SerializeField] private StateAction.SpecificMoment phase;
	[SerializeField] private BoolEventChannelSO boolEventChannelSO;
	[SerializeField] private bool value;
	public override StateAction CreateAction() => new B_InvokeBoolEvent(phase, boolEventChannelSO, value);
}

public class B_InvokeBoolEvent : StateAction {
	private StateAction.SpecificMoment phase;
	private BoolEventChannelSO _boolEventChannelSO;
	private bool _value;

	public B_InvokeBoolEvent(StateAction.SpecificMoment phase, BoolEventChannelSO boolEventChannelSO, bool value) {
		this.phase = phase;
		_boolEventChannelSO = boolEventChannelSO;
		_value = value;
	}

	public override void Awake(StateMachine stateMachine) { }

	public override void OnUpdate() {
		if ( phase == SpecificMoment.OnUpdate ) {
			_boolEventChannelSO.RaiseEvent(_value);
		}
	}

	public override void OnStateEnter() {
		if ( phase == SpecificMoment.OnStateEnter ) {
			_boolEventChannelSO.RaiseEvent(_value);
		}
	}

	public override void OnStateExit() {
		if ( phase == SpecificMoment.OnStateExit ) {
			_boolEventChannelSO.RaiseEvent(_value);
		}
	}
}