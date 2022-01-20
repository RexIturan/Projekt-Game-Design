using Characters.Movement;
using GDP01.Characters.Component;
using GDP01.World.Components;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_ClearCache_OnExit",
	menuName = "State Machines/Actions/Character/Clear Cache On Exit")]
public class C_ClearCache_OnExitSO : StateActionSO {
	public override StateAction CreateAction() => new C_ClearCache_OnExit();
}

public class C_ClearCache_OnExit : StateAction {
	protected new C_ClearCache_OnExitSO OriginSO => ( C_ClearCache_OnExitSO )base.OriginSO;
	private AbilityController _abilityController;
	private MovementController _movementController;
	private Attacker _attacker;

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() { }

	public override void OnStateExit() {
		_abilityController.SelectedAbilityID = -1;
		_abilityController.abilitySelected = false;
		_abilityController.abilityConfirmed = false;
		_movementController.movementTarget = default;
		// _attacker.playerTarget = null;
		// _attacker.enemyTarget = null;
		_attacker.SetTarget(null);
		_attacker.SetGroundTarget(Vector3Int.zero);
		_attacker.groundTargetSet = false;
		_attacker.waitForAttackToFinish = false;
	}
}