using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_MovementDone",
	menuName = "State Machines/Conditions/Player/Movement Done")]
public class P_MovementDoneSO : StateConditionSO {
	protected override Condition CreateCondition() => new P_MovementDone();
}

public class P_MovementDone : Condition {
	private PlayerCharacterSC _playerSC;

	public override void Awake(StateMachine stateMachine) {
		_playerSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}

	protected override bool Statement() {
		return _playerSC.movementDone;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}