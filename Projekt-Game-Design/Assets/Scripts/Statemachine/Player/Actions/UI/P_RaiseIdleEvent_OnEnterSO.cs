using Characters.Ability;
using Events.ScriptableObjects;
using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

// Sets Variable in GameObject to false and raises unselected Event
[CreateAssetMenu(fileName = "p_RaiseIdleEvent_OnEnter", menuName = "State Machines/Actions/Player/Raise Idle Event On Enter")]
public class P_RaiseIdleEvent_OnEnterSO : StateActionSO {
	[Header("Sending Events On")]
	[SerializeField] private GameObjEventChannelSO deselectEvent;

	public override StateAction CreateAction() => new P_RaiseDeselectedEvent_OnExit(deselectEvent);
}

public class P_RaiseDeselectedEvent_OnExit : StateAction {
	private readonly GameObjEventChannelSO _deselectEvent;
	private StateMachine _stateMachine;

	public P_RaiseDeselectedEvent_OnExit(GameObjEventChannelSO deselectEvent) {
		_deselectEvent = deselectEvent;
	}

	public override void Awake(StateMachine stateMachine) {
		_stateMachine = stateMachine;
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() {
		_deselectEvent.RaiseEvent(_stateMachine.gameObject);
	}
}