using Characters;
using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "b_TimePassed", menuName = "State Machines/Conditions/Basic/Time Passed")]
public class B_TimePassedSO : StateConditionSO {
	[SerializeField] private float time;

	protected override Condition CreateCondition() => new B_TimePassed(time);
}

public class B_TimePassed : Condition {
	protected new C_DeselectedSO OriginSO => ( C_DeselectedSO )base.OriginSO;

	private float time;
	private Timer _timer;

	public B_TimePassed(float time) {
		this.time = time;
	}

	public override void Awake(StateMachine stateMachine) {
		_timer = stateMachine.gameObject.GetComponent<Timer>();
	}

	protected override bool Statement() {
		return _timer.timeSinceTransition >= time;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}