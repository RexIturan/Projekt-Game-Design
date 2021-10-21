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

	private PlayerCharacterSC _playerCharacterSc;
	
	public override void Awake(StateMachine stateMachine) {
		_playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return _playerCharacterSc.abilityConfirmed;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
