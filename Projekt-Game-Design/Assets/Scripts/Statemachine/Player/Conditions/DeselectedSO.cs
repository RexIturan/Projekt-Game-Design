using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "Deselected", menuName = "State Machines/Conditions/Deselected")]
public class DeselectedSO : StateConditionSO {
	protected override Condition CreateCondition() => new Deselected();
}

public class Deselected : Condition {
	protected new DeselectedSO OriginSO => ( DeselectedSO )base.OriginSO;

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