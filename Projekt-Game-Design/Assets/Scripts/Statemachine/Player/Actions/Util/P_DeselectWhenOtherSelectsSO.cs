using Events.ScriptableObjects;
using System;
using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_DeselectWhenOtherSelects",
	menuName = "State Machines/Actions/Player/Deselect When Other Selects")]
public class P_DeselectWhenOtherSelectsSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	public GameObjActionIntEventChannelSO selectNewPlayer;

	public override StateAction CreateAction() => new P_DeselectWhenOtherSelects(selectNewPlayer);
}

public class P_DeselectWhenOtherSelects : StateAction {
	private GameObject _gameObject;
	private readonly GameObjActionIntEventChannelSO _selectPlayerEC;

	public P_DeselectWhenOtherSelects(GameObjActionIntEventChannelSO selectPlayerEC) {
		_selectPlayerEC = selectPlayerEC;
	}

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		_gameObject = stateMachine.gameObject;
	}

	public override void OnStateEnter() {
		_selectPlayerEC.OnEventRaised += DeselectSelf;
	}

	private void DeselectSelf(GameObject selectedPlayer, Action<int> callback) {
		if ( !selectedPlayer.Equals(_gameObject) ) {
			_gameObject.GetComponent<Selectable>().isSelected = false;
		}
	}
}