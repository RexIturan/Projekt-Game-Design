using Ability;
using Characters;
using Events.ScriptableObjects;
using GDP01.Characters.Component;
using GDP01.World.Components;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_CreateTileEffects_OnUpdate", menuName = "State Machines/Actions/Character/Create Tile Effects On Update")]
public class C_CreateTileEffects_OnUpdateSO : StateActionSO {
	[Header("Sending events on: ")]
	[SerializeField] private CreateTileEffectEventChannelSO createTileEffectEC;

	public override StateAction CreateAction() =>
		new C_CreateTileEffects_OnUpdate(createTileEffectEC);
}

public class C_CreateTileEffects_OnUpdate : StateAction {
	private readonly CreateTileEffectEventChannelSO createTileEffectEC;
	
	private AbilityController _abilityController;
	private Attacker _attacker;
  private Timer _timer;

	public C_CreateTileEffects_OnUpdate(CreateTileEffectEventChannelSO createTileEffectEC) {
		this.createTileEffectEC = createTileEffectEC;
	}

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_timer = stateMachine.gameObject.GetComponent<Timer>();
	}

	public override void OnUpdate() {
		AbilitySO ability = _abilityController.GetSelectedAbility();

		if(ability && !_abilityController.tileEffectsSpawned && _timer.timeSinceTransition > ability.timeUntilDamage) {
			Vector3Int targetPos = _attacker.groundTargetSet ? _attacker.GetGroundTarget() : _attacker.GetTargetPosition();

			foreach(TargetedEffect effect in ability.targetedEffects) {
				if(effect.tileEffect) {
					foreach(Vector3Int tileInArea in effect.area.GetTargetedTiles(targetPos, _attacker.GetRotationsToTarget(targetPos))) {
						createTileEffectEC.RaiseEvent(effect.tileEffect, tileInArea);
					}					
				}
			}
			
			_abilityController.tileEffectsSpawned = true;
		}
	}

	public override void OnStateEnter() {
		_abilityController.tileEffectsSpawned = false;
	}
}