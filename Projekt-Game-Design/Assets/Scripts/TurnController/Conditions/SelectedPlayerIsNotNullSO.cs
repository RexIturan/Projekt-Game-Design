using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SelectedPlayerIsNotNull", menuName = "State Machines/Conditions/TurnState/SelectedPlayerIsNotNull")]
public class SelectedPlayerIsNotNullSO : StateConditionSO
{
	protected override Condition CreateCondition() => new SelectedPlayerIsNotNull();
}

public class SelectedPlayerIsNotNull : Condition
{
	private TurnContainerCO dataContainer;

	public override void Awake(StateMachine stateMachine)
	{
		dataContainer = stateMachine.gameObject.GetComponent<TurnContainerCO>();
	}
	
	protected override bool Statement()
	{
        return dataContainer.selectedPlayer != null;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{

	}
}
