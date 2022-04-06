using Ability;
using Characters;
using Events.ScriptableObjects;
using GDP01.Characters.Component;
using GDP01.World.Components;
using Grid;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_SpawnBeamProjectile_OnUpdate", menuName = "State Machines/Actions/Character/Create Beam Projectile On Update")]
public class C_SpawnBeamProjectile_OnUpdateSO : StateActionSO {
	[SerializeField] private CreateProjectileEventChannelSO createProjectileEvent;
	[SerializeField] private GridDataSO gridData;
	[Header("Is the effect from attacker to target or does it have a pre-defined length? ")]
	[SerializeField] private bool absoluteLength;
	[SerializeField] private int length;
	[SerializeField] private GameObject projectile;
	[SerializeField] private float livingTime;

	public override StateAction CreateAction() => 
		new C_SpawnBeamProjectile_OnUpdate(createProjectileEvent, gridData, absoluteLength, length, projectile, livingTime);
}

public class C_SpawnBeamProjectile_OnUpdate : StateAction {
	private const float START_HEIGHT = 0.5f;

	private CreateProjectileEventChannelSO _createProjectileEvent;
	private GridDataSO _gridData;
	private bool _absoluteLength;
	private int _length;
	private GameObject _projectile;
	private float _livingTime;
	
	private AbilityController _abilityController;
	private Attacker _attacker;
  private Timer _timer;

	public C_SpawnBeamProjectile_OnUpdate(CreateProjectileEventChannelSO createProjectileEvent, GridDataSO gridData,
			bool absoluteLength, int length, GameObject projectile, float livingTime) {

		_createProjectileEvent = createProjectileEvent;
		_gridData = gridData;
		_absoluteLength = absoluteLength;
		_length = length;
		_projectile = projectile;
		_livingTime = livingTime;
	}

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_timer = stateMachine.gameObject.GetComponent<Timer>();
	}

	public override void OnUpdate() {
		AbilitySO ability = _abilityController.GetSelectedAbility();

		if(ability && !_abilityController.beamProjectileSpawned && _timer.timeSinceTransition > ability.timeUntilDamage) {
			Vector3 startPos = _attacker.transform.position;
			Vector3 endPos;

			Vector3Int targetPos = _attacker.groundTargetSet ? _attacker.GetGroundTarget() : _attacker.GetTargetPosition();
			Vector3 targetWorldPos = _gridData.GetWorldPosFromGridPos(targetPos);
			
			if(_absoluteLength)
				endPos = startPos + _length * (targetWorldPos - startPos).normalized;
			else
				endPos = targetWorldPos;

			startPos += Vector3.up * START_HEIGHT;
			endPos += Vector3.up * START_HEIGHT;

			_createProjectileEvent.RaiseEvent(startPos, endPos, _livingTime, _projectile);

			_abilityController.beamProjectileSpawned = true;
		}
	}

	public override void OnStateEnter() {
		_abilityController.beamProjectileSpawned = false;
	}
}