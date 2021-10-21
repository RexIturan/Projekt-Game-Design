using Events.ScriptableObjects;
using System;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_DeselectWhenOtherSelects",
	menuName = "State Machines/Actions/Player/Deselect When Other Selects")]
public class P_DeselectWhenOtherSelectsSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	public GameObjActionEventChannelSO selectNewPlayer;

	public override StateAction CreateAction() => new P_DeselectWhenOtherSelects(selectNewPlayer);
}

public class P_DeselectWhenOtherSelects : StateAction {
	private GameObject _gameObject;
	private readonly GameObjActionEventChannelSO _selectNewPlayer;

	public P_DeselectWhenOtherSelects(GameObjActionEventChannelSO gameObjEventChannel) {
		_selectNewPlayer = gameObjEventChannel;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_gameObject = stateMachine.gameObject;
	}

	public override void OnStateEnter() {
		_selectNewPlayer.OnEventRaised += DeselectSelf;
	}

	private void DeselectSelf(GameObject selectedPlayer, Action<int> callback) {
		if ( !selectedPlayer.Equals(_gameObject) ) {
			_gameObject.GetComponent<PlayerCharacterSC>().isSelected = false;
		}
	}
}