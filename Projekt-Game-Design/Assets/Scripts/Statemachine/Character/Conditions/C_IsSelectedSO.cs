using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_IsSelected", menuName = "State Machines/Conditions/Character/Is Selected")]
public class C_IsSelectedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new C_IsSelected();
}

public class C_IsSelected : Condition
{
	protected new C_IsSelectedSO OriginSO => (C_IsSelectedSO)base.OriginSO;

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
