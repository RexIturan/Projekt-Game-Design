using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsVarSet", menuName = "State Machines/Conditions/Is Var Set")]
public class IsVarSetSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsVarSet();
}

public class IsVarSet : Condition
{
	protected new IsVarSetSO OriginSO => (IsVarSetSO)base.OriginSO;
	private PlayerCharacterSC root;

	public override void Awake(StateMachine stateMachine)
	{
		root = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return root.isSelected;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
