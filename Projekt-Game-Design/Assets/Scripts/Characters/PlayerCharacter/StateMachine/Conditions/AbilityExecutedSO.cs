using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityExecuted", menuName = "State Machines/Conditions/Ability Executed")]
public class AbilityExecutedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new AbilityExecuted();
}

public class AbilityExecuted : Condition
{
	protected new AbilityExecutedSO OriginSO => (AbilityExecutedSO)base.OriginSO;

	private PlayerCharacterSC playerCharacterSc;
	
	public override void Awake(StateMachine stateMachine) {
		playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return playerCharacterSc.abilityExecuted;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
