using Characters.Ability;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AbilityCanceled", menuName = "State Machines/Conditions/Ability Canceled")]
public class AbilityCanceledSO : StateConditionSO
{
	protected override Condition CreateCondition() => new AbilityCanceled();
}

public class AbilityCanceled : Condition
{
	protected new AbilityCanceledSO OriginSO => (AbilityCanceledSO)base.OriginSO;
	private AbilityController _abilityController;
	
	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
	}
	
	protected override bool Statement()
	{
		return !_abilityController.abilitySelected;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
