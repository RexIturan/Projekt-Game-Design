using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "ResetTimeSinceTransition",
	menuName = "State Machines/Actions/Player/ResetTimeSinceTransition")]
public class ResetTimerOnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new ResetTimerOnEnter();
}

public class ResetTimerOnEnter : StateAction {

	private Timer _timer;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_timer = stateMachine.gameObject.GetComponent<Timer>();
	}

	public override void OnStateEnter() {
		_timer.timeSinceTransition = 0;
	}
}