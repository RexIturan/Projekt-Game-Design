using Characters.Ability;
using Characters.Movement;
using Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_ClearFullCache_OnEnter", menuName = "State Machines/Actions/Character/Clear Full Cache On Enter")]
public class C_ClearFullCache_OnEnterSO : StateActionSO
{
	public override StateAction CreateAction() => new C_ClearFullCache_OnEnter();
}

public class C_ClearFullCache_OnEnter : StateAction
{
	protected new C_ClearFullCache_OnEnterSO OriginSO => (C_ClearFullCache_OnEnterSO)base.OriginSO;

	private AbilityController _abilityController;
	private MovementController _movementController;
	private Attacker _attacker;

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() { 
		_abilityController.SelectedAbilityID = -1;
		_abilityController.abilitySelected = false;
		_abilityController.abilityConfirmed = false;
		_abilityController.abilityExecuted = false;
		_movementController.movementTarget = default;
		_movementController.reachableTiles.Clear();
		// _attacker.playerTarget = null;
		// _attacker.enemyTarget = null;
		_attacker.SetTarget(null);
		_attacker.SetGroundTarget(Vector3Int.zero);
		_attacker.groundTargetSet = false;
		_attacker.ClearTilesInRange();
		_attacker.waitForAttackToFinish = false;
	}

	public override void OnStateExit() { }
}
