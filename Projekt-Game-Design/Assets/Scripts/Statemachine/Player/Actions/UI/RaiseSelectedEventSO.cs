using Characters.Ability;
using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "RaiseSelectedEvent",
	menuName = "State Machines/Actions/Player/RaiseSelectedEvent")]
public class RaiseSelectedEventSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	public GameObjActionEventChannelSO selectNewPlayer;

	public override StateAction CreateAction() => new RaiseSelectedEvent(selectNewPlayer);
}

public class RaiseSelectedEvent : StateAction {
	private GameObject _gameObject;
	private StateMachine _stateMachine;
	private GameObjActionEventChannelSO _selectNewPlayer;

	private AbilityController _abilityController;

	public RaiseSelectedEvent(GameObjActionEventChannelSO gameObjEventChannel) {
		_selectNewPlayer = gameObjEventChannel;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_stateMachine = stateMachine;
		_gameObject = stateMachine.gameObject;
		Debug.Log($"Awake raiseSelectedEvent {this}");
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	//TODO: Muss geändert werden
	private void AbilityCallback(int value) {
		Debug.Log("Es wurde die Ability mit der ID: " + value + " gedrückt.");

		int lastId = _abilityController.SelectedAbilityID;

		// deselect old ability
		_abilityController.abilitySelected = false;
		_abilityController.SelectedAbilityID = -1;
		_abilityController.LastSelectedAbilityID = -1;

		// select new ability if it wasn't the old one
		// will be done next update
		if ( lastId != value ) {
			_stateMachine.TransitionManually();

			_abilityController.abilitySelected = true;
			_abilityController.SelectedAbilityID = value;
			_abilityController.LastSelectedAbilityID = value;
						
			_stateMachine.TransitionManually();
		}
	}

	public override void OnStateEnter() {
	  _abilityController.RefreshAbilities();
		_selectNewPlayer.RaiseEvent(_gameObject, AbilityCallback);
	}
}