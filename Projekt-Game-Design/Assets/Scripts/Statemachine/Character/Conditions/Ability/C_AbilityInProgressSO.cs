using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_AbilityInProgress",
	menuName = "State Machines/Conditions/Character/Ability In Progress")]
public class C_AbilityInProgressSO : StateConditionSO {
	[SerializeField] private VoidEventChannelSO abilityConfirmedEC;
	[SerializeField] private VoidEventChannelSO abilityExecutedEC;

	protected override Condition CreateCondition() => new C_AbilityInProgress(abilityConfirmedEC, abilityExecutedEC);
}

public class C_AbilityInProgress : Condition {
	protected new C_AbilityInProgressSO OriginSO => ( C_AbilityInProgressSO )base.OriginSO;
		
	private VoidEventChannelSO _abilityConfirmedEC;
	private VoidEventChannelSO _abilityExecutedEC;

	private bool inProgress;
	
	public C_AbilityInProgress(VoidEventChannelSO abilityConfirmedEC, VoidEventChannelSO abilityExecutedEC) {
		_abilityConfirmedEC = abilityConfirmedEC;
		_abilityExecutedEC = abilityExecutedEC;
	}
		
	public override void Awake(StateMachine stateMachine) {
		_abilityConfirmedEC.OnEventRaised += () => { inProgress = true; };
		_abilityExecutedEC.OnEventRaised += () => { inProgress = false; };
	}

	protected override bool Statement() {
		return inProgress;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}