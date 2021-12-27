using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_IsVictory", menuName = "State Machines/Conditions/GameState/Is Victory")]
public class G_IsVictorySO : StateConditionSO
{
	protected override Condition CreateCondition() => new G_IsVictory();
}

public class G_IsVictory : Condition
{
	private GameSC _gameSc;

	public override void Awake(StateMachine stateMachine) {
		_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}
	
	protected override bool Statement()
	{
		return _gameSc.victory;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
