using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "FinishAbilityExecution_player", menuName = "State Machines/Actions/Finish Ability Execution _player")]
public class FinishAbilityExecution_playerSO : StateActionSO
{
	public override StateAction CreateAction() => new FinishAbilityExecution_player();
}

public class FinishAbilityExecution_player : StateAction
{
	protected new FinishAbilityExecution_playerSO OriginSO => (FinishAbilityExecution_playerSO)base.OriginSO;

	private PlayerCharacterSC playerStateContainer;

	public override void Awake(StateMachine stateMachine)
	{
		playerStateContainer = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	public override void OnUpdate()
	{
		// todo finish if all effectys are applyed and all animations are finished
		// if (playerStateContainer.animationQueue.Count == 0) {
		if (true) {
			playerStateContainer.abilityExecuted = true;	
		}
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
