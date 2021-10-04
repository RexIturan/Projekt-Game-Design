using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "exitedGame", menuName = "State Machines/Conditions/GameState/exited Game")]
public class ExitedGameSO : StateConditionSO
{
	protected override Condition CreateCondition() => new ExitedGame();
}

public class ExitedGame : Condition
{
	protected new ExitedGameSO OriginSO => (ExitedGameSO)base.OriginSO;
	private GameSC gameSc;

	public override void Awake(StateMachine stateMachine) {
		gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}

	protected override bool Statement() {
		return gameSc.exited;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
