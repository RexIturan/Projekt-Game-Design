using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClearFullCacheOnEnter_player", menuName = "State Machines/Actions/Clear Full Cache On Enter_player")]
public class ClearFullCacheOnEnter_playerSO : StateActionSO
{
	public override StateAction CreateAction() => new ClearFullCacheOnEnter_player();
}

public class ClearFullCacheOnEnter_player : StateAction
{
	protected new ClearFullCacheOnEnter_playerSO OriginSO => (ClearFullCacheOnEnter_playerSO)base.OriginSO;

	private PlayerCharacterSC playerCharacterSC;

	public override void Awake(StateMachine stateMachine)
	{
		playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter() {
		playerCharacterSC.AbilityID = -1;
		playerCharacterSC.abilityConfirmed = false;
		playerCharacterSC.abilityExecuted = false;
		playerCharacterSC.abilitySelected = false;
		playerCharacterSC.reachableTiles.Clear();
		playerCharacterSC.movementTarget = null;
        playerCharacterSC.playerTarget = null;
        playerCharacterSC.enemyTarget = null;
        playerCharacterSC.tilesInRange.Clear();
        playerCharacterSC.waitForAttackToFinish = false;
    }
	
	public override void OnStateExit()
	{
	}
}
