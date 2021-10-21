using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Deselected", menuName = "State Machines/Conditions/Deselected")]
public class DeselectedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new Deselected();
}

public class Deselected : Condition
{
	protected new DeselectedSO OriginSO => (DeselectedSO)base.OriginSO;

	private PlayerCharacterSC _playerCharacterSc;
	
	public override void Awake(StateMachine stateMachine) {
		_playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return !_playerCharacterSc.isSelected;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
