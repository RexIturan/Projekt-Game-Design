using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AreMorePlayersSelected", menuName = "State Machines/Conditions/Are More Players Selected")]
public class AreMorePlayersSelectedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new AreMorePlayersSelected();
}

public class AreMorePlayersSelected : Condition
{
	protected new AreMorePlayersSelectedSO OriginSO => (AreMorePlayersSelectedSO)base.OriginSO;
	private TurnContainerCO root;

	public override void Awake(StateMachine stateMachine)
	{
		root = stateMachine.gameObject.GetComponent<TurnContainerCO>();
	}
	
	protected override bool Statement()
	{
		return root.PlayersSelected.Count >1;;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
