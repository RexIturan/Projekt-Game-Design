using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_AnimationDone",
	menuName = "State Machines/Conditions/Player/Animation Done")]
public class P_IsAnimationDoneSO : StateConditionSO
{
	protected override Condition CreateCondition() => new P_IsAnimationDone();
}

public class P_IsAnimationDone : Condition
{
	private PlayerCharacterSC _playerSC;

	public override void Awake(StateMachine stateMachine)
	{
		_playerSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}

	protected override bool Statement()
	{
		return !_playerSC.GetAnimationController().IsAnimationInProgress();
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}