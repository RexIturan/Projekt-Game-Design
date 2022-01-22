using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "new_B_InvokeVoidEvent",
	menuName = "State Machines/Actions/new Invoke Void Event")]
public class B_InvokeVoidEventSO : StateActionSO {
	[SerializeField] private StateAction.SpecificMoment phase;
	[SerializeField] private VoidEventChannelSO voidEventChannelSO;
	public override StateAction CreateAction() => new B_InvokeVoidEvent(phase, voidEventChannelSO);
}

public class B_InvokeVoidEvent : StateAction {
	protected new B_InvokeVoidEventSO OriginSO => ( B_InvokeVoidEventSO )base.OriginSO;
	private StateAction.SpecificMoment phase;
	private VoidEventChannelSO _voidEventChannelSO;

	public B_InvokeVoidEvent(StateAction.SpecificMoment phase, VoidEventChannelSO voidEventChannelSO) {
		this.phase = phase;
		_voidEventChannelSO = voidEventChannelSO;
	}

	public override void Awake(StateMachine stateMachine) { }

	public override void OnUpdate() {
		if ( phase == SpecificMoment.OnUpdate ) {
			_voidEventChannelSO.RaiseEvent();
		}
	}

	public override void OnStateEnter() {
		if ( phase == SpecificMoment.OnStateEnter ) {
			_voidEventChannelSO.RaiseEvent();
		}
	}

	public override void OnStateExit() {
		if ( phase == SpecificMoment.OnStateExit ) {
			_voidEventChannelSO.RaiseEvent();
		}
	}
}