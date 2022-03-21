using Ability;
using Ability.ScriptableObjects;
using Characters;
using Combat;
using Events.ScriptableObjects;
using System.Collections.Generic;
using GDP01.Characters.Component;
using GDP01.World.Components;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "c_InflictDamage_OnEnter",
	menuName = "State Machines/Actions/Character/Inflict Damage On Enter")]
public class C_InflictDamage_OnEnterSO : StateActionSO {
	[Header("Sending events on: ")] [SerializeField]
	private CreateFloatingTextEventChannelSO createTextEC;

	[SerializeField] private AbilityContainerSO abilityContainer;

	public override StateAction CreateAction() =>
		new C_InflictDamage_OnEnter(createTextEC, abilityContainer);
}

public class C_InflictDamage_OnEnter : StateAction {
	protected new C_InflictDamage_OnEnterSO OriginSO => ( C_InflictDamage_OnEnterSO )base.OriginSO;

	private readonly CreateFloatingTextEventChannelSO _createTextEC;
	private readonly AbilityContainerSO _abilityContainer;
	
	private AbilityController _abilityController;
	private Statistics _statistics;
	private Attacker _attacker;
  private Characters.Timer _timer;

	public C_InflictDamage_OnEnter(CreateFloatingTextEventChannelSO createTextEC,
		AbilityContainerSO abilityContainer) {
		this._createTextEC = createTextEC;
		this._abilityContainer = abilityContainer;
	}

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_statistics = stateMachine.gameObject.GetComponent<Statistics>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_timer = stateMachine.gameObject.GetComponent<Timer>();
	}

	public override void OnUpdate() {
		AbilitySO ability = _abilityController.GetSelectedAbility();
		if(ability && !_abilityController.damageInflicted && _timer.timeSinceTransition > ability.timeUntilDamage) { 

			//TODO(combat) move to Combat System 

			// TODO: check conditions for targeted effects and don't 
			//       just inflict damage on target
			Debug.Log("Ability: " + ability.name);
				
			foreach ( TargetedEffect targetedEffect in ability.targetedEffects ) {
				int damage = CombatUtils.CalculateDamage(targetedEffect.effect, _statistics.StatusValues);

	 			Color damageColor;

	      //todo healing shouldnt be negative damage in itself
		    //todo negative damage can exist, healing should be handled differently 
  			if ( damage > 0 )
					damageColor = Color.red;
		  	else if ( damage < 0 )
		  		damageColor = Color.green;
		  	else
		  		damageColor = Color.grey;
			
				Vector3Int targetPos = _attacker.GetTargetPosition();

				// rotations of pattern depending on the angle the attacker is facing
				//todo get rotation from targetpos and attacker pos
				int rotations = _attacker.GetRotationsToTarget(targetPos);

				List<Targetable> targets = CombatUtils.FindAllTargets(
					targetedEffect.area.GetTargetedTiles(targetPos, rotations),
					_attacker, targetedEffect.targets);
			
				// HashSet<Targetable> targets = CombatUtils.FindAllTargets(targetPos, 
					// 	targetedEffect.area.GetPattern(rotations), targetedEffect.area.GetAnchor(rotations), _attacker, targetedEffect.targets);

				foreach(Targetable target in targets) {
					if ( target.IsAlive ) {
						Debug.Log("Target in range. Dealing damage/healing. ");
						target.ReceivesDamage(damage);

						_createTextEC.RaiseEvent(Mathf.Abs(damage).ToString(),
							target.gameObject.transform.position + Vector3.up,
							damageColor);	
					}
  			}
			}

			_abilityController.damageInflicted = true;
		}
	}

	public override void OnStateEnter() {
		_abilityController.damageInflicted = false;
	}
}