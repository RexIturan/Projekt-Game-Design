using Characters.Ability;
using Characters.Movement;
using Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "ClearFullCacheOnEnter_player", menuName = "State Machines/Actions/Clear Full Cache On Enter_player")]
public class ClearFullCacheOnEnter_PlayerSO : StateActionSO
{
	public override StateAction CreateAction() => new ClearFullCacheOnEnter_Player();
}

public class ClearFullCacheOnEnter_Player : StateAction
{
	protected new ClearFullCacheOnEnter_PlayerSO OriginSO => (ClearFullCacheOnEnter_PlayerSO)base.OriginSO;

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
		_abilityController.abilityExecuted = false;
		_movementController.movementTarget = default;
		_movementController.reachableTiles.Clear();
		_attacker.playerTarget = null;
		_attacker.enemyTarget = null;
		_attacker.ClearTilesInRange();
		_attacker.waitForAttackToFinish = false;
	}
}
