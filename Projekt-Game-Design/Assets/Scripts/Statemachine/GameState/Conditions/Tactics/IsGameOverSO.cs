using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsGameOver", menuName = "State Machines/Conditions/Is Game Over")]
public class IsGameOverSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsGameOver();
}

public class IsGameOver : Condition
{
	protected new IsGameOverSO OriginSO => (IsGameOverSO)base.OriginSO;
	private GameSC gameSc;

	public override void Awake(StateMachine stateMachine) {
		gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}
	
	protected override bool Statement()
	{
		return gameSc.gameOver;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
