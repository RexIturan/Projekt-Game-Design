using Events.ScriptableObjects;
using GDP01.Characters.Component;
using GDP01.World.Components;
using Grid;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_SpawnProjectile_OnEnter",
	menuName = "State Machines/Actions/Character/Spawn Projectile On Enter")]
public class C_SpawnProjectile_OnEnterSO : StateActionSO {
	[Header("Sending events on: ")]
	[SerializeField] private CreateProjectileEventChannelSO createProjectileEvent;

	[SerializeField] private GridDataSO gridData;

	public override StateAction CreateAction() => new C_SpawnProjectile_OnEnter(createProjectileEvent, gridData);
}

public class C_SpawnProjectile_OnEnter : StateAction {
	private const float START_HEIGHT = 0.5f;

	private readonly CreateProjectileEventChannelSO _createProjectileEvent;
	private readonly GridDataSO _gridData;

	private AbilityController _abilityController;
	private Attacker _attacker;

	public C_SpawnProjectile_OnEnter(CreateProjectileEventChannelSO createProjectileEvent, GridDataSO gridData) {
		_createProjectileEvent = createProjectileEvent;
		_gridData = gridData;
	}

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() {
		AbilitySO ability = _abilityController.GetSelectedAbility();
		if(ability.projectilePrefab) { 
			Vector3 start = _attacker.transform.position;
			Vector3 end;

			if (_attacker.GetTarget())
				end = _attacker.GetTarget().transform.position;
			else
				end = _gridData.GetWorldPosFromGridPos(_attacker.GetGroundTarget());

			start += Vector3.up * START_HEIGHT;
			end += Vector3.up * START_HEIGHT;

			_createProjectileEvent.RaiseEvent(start, end, ability.timeUntilDamage, ability.projectilePrefab);
		}
	}
}