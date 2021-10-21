using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClearFullCacheOnEnter_player", menuName = "State Machines/Actions/Clear Full Cache On Enter_player")]
public class ClearFullCacheOnEnter_PlayerSO : StateActionSO
{
	public override StateAction CreateAction() => new ClearFullCacheOnEnter_Player();
}

public class ClearFullCacheOnEnter_Player : StateAction
{
	protected new ClearFullCacheOnEnter_PlayerSO OriginSO => (ClearFullCacheOnEnter_PlayerSO)base.OriginSO;

	private PlayerCharacterSC _playerCharacterSC;

	public override void Awake(StateMachine stateMachine)
	{
		_playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter() {
		_playerCharacterSC.AbilityID = -1;
		_playerCharacterSC.abilityConfirmed = false;
		_playerCharacterSC.abilityExecuted = false;
		_playerCharacterSC.abilitySelected = false;
		_playerCharacterSC.reachableTiles.Clear();
		_playerCharacterSC.movementTarget = null;
        _playerCharacterSC.playerTarget = null;
        _playerCharacterSC.enemyTarget = null;
        _playerCharacterSC.tilesInRange.Clear();
        _playerCharacterSC.waitForAttackToFinish = false;
    }
	
	public override void OnStateExit()
	{
	}
}
