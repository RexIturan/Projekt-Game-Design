using Characters.Ability;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityConfirmed", menuName = "State Machines/Conditions/Ability Confirmed")]
public class AbilityConfirmedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new AbilityConfirmed();
}

public class AbilityConfirmed : Condition
{
	protected new AbilityConfirmedSO OriginSO => (AbilityConfirmedSO)base.OriginSO;

	private AbilityController _abilityController;
	
	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}
	
	protected override bool Statement()
	{
		return _abilityController.abilityConfirmed;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
