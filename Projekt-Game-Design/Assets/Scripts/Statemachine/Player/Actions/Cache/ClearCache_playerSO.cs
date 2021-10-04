using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClearCache_player", menuName = "State Machines/Actions/Clear Cache_player")]
public class ClearCache_playerSO : StateActionSO
{
	public override StateAction CreateAction() => new ClearCache_player();
}

public class ClearCache_player : StateAction
{
	protected new ClearCache_playerSO OriginSO => (ClearCache_playerSO)base.OriginSO;
	private PlayerCharacterSC playerCharacterSC;
	
	
	public override void Awake(StateMachine stateMachine) {
		playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit() {
		playerCharacterSC.AbilityID = -1;
		playerCharacterSC.abilitySelected = false;
		playerCharacterSC.abilityConfirmed = false;
		playerCharacterSC.movementTarget = default;
        playerCharacterSC.playerTarget = null;
        playerCharacterSC.enemyTarget = null;
	}
}
