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

	private PlayerCharacterSC playerCharacterSc;
	
	public override void Awake(StateMachine stateMachine) {
		playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return !playerCharacterSc.abilitySelected;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
