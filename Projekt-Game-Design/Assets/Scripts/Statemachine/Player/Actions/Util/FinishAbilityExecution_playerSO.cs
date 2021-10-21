using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FinishAbilityExecution_player", menuName = "State Machines/Actions/Finish Ability Execution _player")]
public class FinishAbilityExecution_PlayerSO : StateActionSO
{
	public override StateAction CreateAction() => new FinishAbilityExecution_Player();
}

public class FinishAbilityExecution_Player : StateAction
{
	protected new FinishAbilityExecution_PlayerSO OriginSO => (FinishAbilityExecution_PlayerSO)base.OriginSO;

	private PlayerCharacterSC _playerStateContainer;

	public override void Awake(StateMachine stateMachine)
	{
		_playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	public override void OnUpdate()
	{
		// todo finish if all effectys are applyed and all animations are finished
		// if (playerStateContainer.animationQueue.Count == 0) {
		if (true) {
			_playerStateContainer.abilityExecuted = true;	
		}
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
