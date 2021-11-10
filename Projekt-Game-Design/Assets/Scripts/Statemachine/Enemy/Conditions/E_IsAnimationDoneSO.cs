using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_AnimationDone",
	menuName = "State Machines/Conditions/Enemy/Animation Done")]
public class E_IsAnimationDoneSO : StateConditionSO
{
	protected override Condition CreateCondition() => new E_IsAnimationDone();
}

public class E_IsAnimationDone : Condition
{
	private EnemyCharacterSC _enemySC;

	public override void Awake(StateMachine stateMachine)
	{
		_enemySC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}

	protected override bool Statement()
	{
		return !_enemySC.GetAnimationController().IsAnimationInProgress();
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}