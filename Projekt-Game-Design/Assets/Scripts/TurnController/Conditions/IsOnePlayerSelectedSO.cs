using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsPlayerSelected", menuName = "State Machines/Conditions/Is Player Selected")]
public class IsOnePlayerSelectedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsPlayerSelected();
}

public class IsPlayerSelected : Condition
{
	protected new IsOnePlayerSelectedSO OriginSO => (IsOnePlayerSelectedSO)base.OriginSO;

	private TurnContainerCO root;
	public override void Awake(StateMachine stateMachine)
	{
		root = stateMachine.gameObject.GetComponent<TurnContainerCO>();
	}
	
	protected override bool Statement()
	{
		return root.PlayersSelected.Count == 1;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
