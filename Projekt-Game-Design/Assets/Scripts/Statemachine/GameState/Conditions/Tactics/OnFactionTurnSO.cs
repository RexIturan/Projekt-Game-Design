using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_OnFactionTurn", menuName = "State Machines/Conditions/GameState/On Faction Turn")]
public class OnFactionTurnSO : StateConditionSO
{
	[SerializeField] private TacticsGameDataSO tacticsGameData;
	[SerializeField] private Faction faction;
	protected override Condition CreateCondition() => new OnFactionTurn(tacticsGameData, faction);
}

public class OnFactionTurn : Condition
{
	protected new OnFactionTurnSO OriginSO => (OnFactionTurnSO)base.OriginSO;
	private readonly TacticsGameDataSO _tacticsGameData;
	private readonly Faction _faction;
	
	public OnFactionTurn( TacticsGameDataSO tacticsGameData, Faction faction ) {
		this._tacticsGameData = tacticsGameData;
		this._faction = faction;
	}
	
	public override void Awake(StateMachine stateMachine)
	{
	}
	
	protected override bool Statement()
	{
		return _tacticsGameData.currentPlayer == _faction;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
