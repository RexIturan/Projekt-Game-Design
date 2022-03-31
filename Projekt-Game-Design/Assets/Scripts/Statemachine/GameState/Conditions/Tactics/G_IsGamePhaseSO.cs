using Characters.Types;
using GDP01;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_IsGamePhase", menuName = "State Machines/Conditions/GameState/Is Game Phase")]
public class G_IsGamePhaseSO : StateConditionSO
{
	[SerializeField] private TacticsGameDataSO tacticsGameData;
	[SerializeField] private GamePhase phase;

	protected override Condition CreateCondition() => new G_IsGamePhase(tacticsGameData, phase);
}

public class G_IsGamePhase : Condition
{
	protected new OnFactionTurnSO OriginSO => (OnFactionTurnSO)base.OriginSO;
	private readonly TacticsGameDataSO _tacticsGameData;
	private readonly GamePhase _phase;
	
	public G_IsGamePhase( TacticsGameDataSO tacticsGameData, GamePhase phase) {
		_tacticsGameData = tacticsGameData;
		_phase = phase;
	}
	
	public override void Awake(StateMachine stateMachine)
	{
	}
	
	protected override bool Statement()
	{
		return _tacticsGameData.currentPhase == _phase;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
