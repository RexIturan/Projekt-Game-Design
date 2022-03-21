using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "LoadGame", menuName = "State Machines/Actions/Load Game")]
public class LoadGameSO : StateActionSO
{
	public override StateAction CreateAction() => new LoadGame();
}

public class LoadGame : StateAction
{
	protected new LoadGameSO OriginSO => (LoadGameSO)base.OriginSO;
	private GameSC _gameSc;

	public override void Awake(StateMachine stateMachine) {
		_gameSc = stateMachine.gameObject.GetComponent<GameSC>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter()
	{
		// _gameSc.LoadLocationLevel();
	}
	
	public override void OnStateExit()
	{
	}
}
