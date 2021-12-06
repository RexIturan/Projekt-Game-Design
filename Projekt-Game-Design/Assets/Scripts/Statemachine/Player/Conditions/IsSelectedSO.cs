using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsVarSet", menuName = "State Machines/Conditions/Is Var Set")]
public class IsSelectedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsSelected();
}

public class IsSelected : Condition
{
	protected new IsSelectedSO OriginSO => (IsSelectedSO)base.OriginSO;

	private Selectable _selectable;
	
	public override void Awake(StateMachine stateMachine)
	{
		_selectable = stateMachine.gameObject.GetComponent<Selectable>(); 
	}
	
	protected override bool Statement()
	{
		return _selectable.isSelected;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
