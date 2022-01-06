using Characters.Movement;
using Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_ClearTargetCache_OnEnter", menuName = "State Machines/Actions/Character/Clear Target Cache On Enter")]
public class C_ClearTargetCache_OnEnterSO : StateActionSO
{
	public override StateAction CreateAction() => new C_ClearTargetCache_OnEnter();
}

public class C_ClearTargetCache_OnEnter : StateAction
{
	private Attacker _attacker;
	private MovementController _movementController;

	public override void Awake(StateMachine stateMachine) {
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() { 
		_attacker.SetTarget(null);
		_attacker.SetGroundTarget(Vector3Int.zero);
		_attacker.groundTargetSet = false;

		if(_movementController)
			_movementController.movementTarget = null;
	}

	public override void OnStateExit() { }
}
