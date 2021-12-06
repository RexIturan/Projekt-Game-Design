using Combat;
using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "P_DrawRange_OnEnter",
	menuName = "State Machines/Actions/Player/Draw Range On Enter")]
public class P_DrawRange_OnEnterSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	private NodeListEventChannelSO drawRangeEC;

	public override StateAction CreateAction() => new P_DrawRange_OnEnter(drawRangeEC);
}

public class P_DrawRange_OnEnter : StateAction {
	private readonly NodeListEventChannelSO _drawRangeEC;
	private Attacker _attacker;

	public P_DrawRange_OnEnter(NodeListEventChannelSO drawRangeEC) {
		this._drawRangeEC = drawRangeEC;
	}

	public override void Awake(StateMachine stateMachine) {
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() {
		_drawRangeEC.RaiseEvent(_attacker.tilesInRange);
	}

	public override void OnStateExit() { }
}