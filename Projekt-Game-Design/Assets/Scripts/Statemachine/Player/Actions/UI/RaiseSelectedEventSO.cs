using Events.ScriptableObjects;
using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "RaiseSelectedEvent",
	menuName = "State Machines/Actions/Player/RaiseSelectedEvent")]
public class RaiseSelectedEventSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	public GameObjActionIntEventChannelSO selectNewPlayer;

	public override StateAction CreateAction() => new RaiseSelectedEvent(selectNewPlayer);
}

public class RaiseSelectedEvent : StateAction {
	private GameObject _gameObject;
	private StateMachine _stateMachine;
	private GameObjActionIntEventChannelSO _selectPlayerEC;

	private AbilityController _abilityController;

	public RaiseSelectedEvent(GameObjActionIntEventChannelSO selectPlayerEventChannel) {
		_selectPlayerEC = selectPlayerEventChannel;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_stateMachine = stateMachine;
		_gameObject = stateMachine.gameObject;
		// Debug.Log($"Awake raiseSelectedEvent {this}");
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}

	//TODO: Moved to AbillityController
	// private void AbilityCallback(int value) {
	// 	Debug.Log("Es wurde die Ability mit der ID: " + value + " gedrückt.");
	//
	// 	int lastId = _abilityController.SelectedAbilityID;
	//
	// 	// deselect old ability
	// 	_abilityController.abilitySelected = false;
	// 	_abilityController.SelectedAbilityID = -1;
	// 	_abilityController.LastSelectedAbilityID = -1;
	//
	// 	// select new ability if it wasn't the old one
	// 	// will be done next update
	// 	if ( lastId != value ) {
	// 		_stateMachine.TransitionManually();
	//
	// 		_abilityController.abilitySelected = true;
	// 		_abilityController.SelectedAbilityID = value;
	// 		_abilityController.LastSelectedAbilityID = value;
	// 					
	// 		_stateMachine.TransitionManually();
	// 	}
	// }

	public override void OnStateEnter() {
	  _abilityController.RefreshAbilities();
		_selectPlayerEC.RaiseEvent(_gameObject, _abilityController.SelectAbility);
	}
}