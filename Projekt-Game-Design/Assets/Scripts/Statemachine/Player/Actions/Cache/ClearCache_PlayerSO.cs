using Characters.Ability;
using Characters.Movement;
using Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClearCache_player",
	menuName = "State Machines/Actions/Clear Cache_player")]
public class ClearCache_PlayerSO : StateActionSO {
	public override StateAction CreateAction() => new ClearCache_Player();
}

public class ClearCache_Player : StateAction {
	protected new ClearCache_PlayerSO OriginSO => ( ClearCache_PlayerSO )base.OriginSO;
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
		_attacker.playerTarget = null;
		_attacker.enemyTarget = null;
		_attacker.waitForAttackToFinish = false;
	}
}