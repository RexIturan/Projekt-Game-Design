using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_IsDefeat", menuName = "State Machines/Conditions/GameState/Is Defeat")]
public class G_IsDefeatSO : StateConditionSO
{
	protected override Condition CreateCondition() => new G_IsDefeat();
}

public class G_IsDefeat : Condition
{
	private GameSC _gameSc;

	public override void Awake(StateMachine stateMachine) {
		_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}
	
	protected override bool Statement()
	{
		return _gameSc.defeat;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
