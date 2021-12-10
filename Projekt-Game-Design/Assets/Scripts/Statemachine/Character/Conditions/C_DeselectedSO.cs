using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_Deselected", menuName = "State Machines/Conditions/Character/Deselected")]
public class C_DeselectedSO : StateConditionSO {
	protected override Condition CreateCondition() => new C_Deselected();
}

public class C_Deselected : Condition {
	protected new C_DeselectedSO OriginSO => ( C_DeselectedSO )base.OriginSO;

	private Selectable _selectable;

	public override void Awake(StateMachine stateMachine) {
		_selectable = stateMachine.gameObject.GetComponent<Selectable>();
	}

	protected override bool Statement() {
		return !_selectable.isSelected;
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}