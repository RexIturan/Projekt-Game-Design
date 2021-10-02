using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsDead", menuName = "State Machines/Conditions/Is Dead")]
public class IsDeadSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsDead();
}

public class IsDead : Condition
{
	protected new IsDeadSO OriginSO => (IsDeadSO)base.OriginSO;

	private PlayerCharacterSC playerCharacterSc;
	
	public override void Awake(StateMachine stateMachine) {
		playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	protected override bool Statement() {
		return playerCharacterSc.HealthPoints <= 0;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
