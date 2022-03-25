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
using System;

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
				
			foreach(Tuple<Targetable, int> targetDamagePair in CombatUtils.GetCumulatedDamage(_attacker.GetTargetPosition(), ability, _attacker)) {
				if ( targetDamagePair.Item1.IsAlive ) {
					Color damageColor;

		      //todo healing shouldnt be negative damage in itself
			    //todo negative damage can exist, healing should be handled differently 
  				if ( targetDamagePair.Item2 > 0 )
						damageColor = Color.red;
			  	else if ( targetDamagePair.Item2 < 0 )
			  		damageColor = Color.green;
			  	else
						damageColor = Color.grey;

					Debug.Log("Target in range. Dealing damage/healing. ");
					targetDamagePair.Item1.ReceivesDamage(targetDamagePair.Item2);

					_createTextEC.RaiseEvent(Mathf.Abs(targetDamagePair.Item2).ToString(),
						targetDamagePair.Item1.gameObject.transform.position + Vector3.up,
						damageColor);	
				}
  		}

			_abilityController.damageInflicted = true;
		}
	}

	public override void OnStateEnter() {
		_abilityController.damageInflicted = false;
	}
}