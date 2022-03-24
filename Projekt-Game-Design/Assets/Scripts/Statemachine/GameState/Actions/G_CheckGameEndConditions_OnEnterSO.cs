using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_CheckGameEndConditions_OnEnter", menuName = "State Machines/Actions/GameState/Check Game End Conditions On Enter")]
public class G_CheckGameEndConditions_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new G_CheckGameEndConditions_OnEnter();
}

public class G_CheckGameEndConditions_OnEnter : StateAction {

	private GameSC _gameSC;
	
	public override void Awake(StateMachine stateMachine){
		_gameSC = stateMachine.GetComponent<GameSC>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter() {
		_gameSC.CheckGameEndingConditions();
	}
	
	public override void OnStateExit()
	{
	}
}
