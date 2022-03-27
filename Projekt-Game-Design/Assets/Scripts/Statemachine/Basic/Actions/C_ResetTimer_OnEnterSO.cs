using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_ResetTimer_OnEnter",
	menuName = "State Machines/Actions/Character/Reset Time Since Transition")]
public class C_ResetTimer_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new C_ResetTimer_OnEnter();
}

public class C_ResetTimer_OnEnter : StateAction {

	private Timer _timer;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_timer = stateMachine.gameObject.GetComponent<Timer>();
	}

	public override void OnStateEnter() {
		_timer.timeSinceTransition = 0;
	}
}