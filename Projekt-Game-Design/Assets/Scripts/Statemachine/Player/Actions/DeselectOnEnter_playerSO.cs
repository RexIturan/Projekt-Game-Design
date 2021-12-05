using Characters.Ability;
using Events.ScriptableObjects;
using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

// Sets Variable in GameObject to false and raises unselected Event
[CreateAssetMenu(fileName = "DeselectOnEnter_player",
	menuName = "State Machines/Actions/Player/DeselectOnEnter")]
public class DeselectOnEnter_PlayerSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	private GameObjEventChannelSO deselectEvent;

	public override StateAction CreateAction() => new DeselectOnEnter_Player(deselectEvent);
}

public class DeselectOnEnter_Player : StateAction {
	private readonly GameObjEventChannelSO _deselectEvent;
	private StateMachine _stateMachine;

	private Selectable _selectable;
	private AbilityController _abilityController;

	public DeselectOnEnter_Player(GameObjEventChannelSO deselectEvent) {
		this._deselectEvent = deselectEvent;
	}

	public override void Awake(StateMachine stateMachine) {
		this._stateMachine = stateMachine;
		_selectable = _stateMachine.gameObject.GetComponent<Selectable>();
		_abilityController = _stateMachine.gameObject.GetComponent<AbilityController>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() {
		_selectable.isSelected = false;
		_abilityController.abilityConfirmed = false;
		_abilityController.abilitySelected = false;
		_deselectEvent.RaiseEvent(_selectable.gameObject);
	}
}