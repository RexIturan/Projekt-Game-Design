using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "P_FinishAbilityExecution_OnUpdate", menuName = "State Machines/Actions/Player/Finish Ability Execution")]
public class P_FinishAbilityExecution_OnUpdateSO : StateActionSO
{
	public override StateAction CreateAction() => new P_FinishAbilityExecution_OnUpdate();
}

public class P_FinishAbilityExecution_OnUpdate : StateAction
{
	protected new P_FinishAbilityExecution_OnUpdateSO OriginSO => ( P_FinishAbilityExecution_OnUpdateSO )base.OriginSO;

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
