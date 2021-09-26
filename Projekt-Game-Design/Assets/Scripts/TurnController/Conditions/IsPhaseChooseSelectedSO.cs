using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPhaseChooseSelected", menuName = "State Machines/Conditions/Is Phase Choose Selected")]
public class IsPhaseChooseSelectedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsPhaseChooseSelected();
}

public class IsPhaseChooseSelected : Condition
{
	protected new IsPhaseChooseSelectedSO OriginSO => (IsPhaseChooseSelectedSO)base.OriginSO;
	private TurnContainerCO root;

	public override void Awake(StateMachine stateMachine)
	{
		root = stateMachine.gameObject.GetComponent<TurnContainerCO>();
	}
	
	protected override bool Statement()
	{
		return root.getIsChoosePhase();
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
