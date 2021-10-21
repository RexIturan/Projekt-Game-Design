using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClearCache_player", menuName = "State Machines/Actions/Clear Cache_player")]
public class ClearCache_PlayerSO : StateActionSO
{
	public override StateAction CreateAction() => new ClearCache_Player();
}

public class ClearCache_Player : StateAction
{
	protected new ClearCache_PlayerSO OriginSO => (ClearCache_PlayerSO)base.OriginSO;
	private PlayerCharacterSC _playerCharacterSC;
	
	
	public override void Awake(StateMachine stateMachine) {
		_playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit() {
		_playerCharacterSC.AbilityID = -1;
		_playerCharacterSC.abilitySelected = false;
		_playerCharacterSC.abilityConfirmed = false;
		_playerCharacterSC.movementTarget = default;
        _playerCharacterSC.playerTarget = null;
        _playerCharacterSC.enemyTarget = null;
        _playerCharacterSC.waitForAttackToFinish = false;
    }
}
