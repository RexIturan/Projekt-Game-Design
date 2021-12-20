using Characters;
using Characters.Movement;
using Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_FaceTarget_OnEnter", menuName = "State Machines/Actions/Character/Face Target On Enter")]
public class C_FaceTarget_OnEnterSO : StateActionSO {
	public override StateAction CreateAction() => new C_FaceTarget_OnEnter();
}

public class C_FaceTarget_OnEnter : StateAction {
	private MovementController movementController;
	private Attacker attacker;
	private GridTransform attackerPos;

	public override void OnUpdate() { }

	public override void Awake(StateMachine stateMachine) {
		movementController = stateMachine.gameObject.GetComponent<MovementController>();
		attacker = stateMachine.gameObject.GetComponent<Attacker>();
		attackerPos = stateMachine.gameObject.GetComponent<GridTransform>();
	}

	public override void OnStateEnter() {
		Debug.Log("Facing direction of target. ");
		Vector3 direction = attacker.GetTarget().transform.position - attacker.transform.position;
		movementController.FaceDirection(direction);
	}
}