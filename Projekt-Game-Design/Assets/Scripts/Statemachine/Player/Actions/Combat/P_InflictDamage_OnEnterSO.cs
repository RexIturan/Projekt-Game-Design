using Ability;
using Ability.ScriptableObjects;
using Characters;
using Characters.Ability;
using Combat;
using Events.ScriptableObjects;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "P_InflictDamage_OnEnter",
	menuName = "State Machines/Actions/Player/Inflict Damage On Enter")]
public class P_InflictDamage_OnEnterSO : StateActionSO {
	[Header("Sending events on: ")] [SerializeField]
	private CreateFloatingTextEventChannelSO createTextEC;

	[SerializeField] private AbilityContainerSO abilityContainer;

	public override StateAction CreateAction() =>
		new P_InflictDamage_OnEnter(createTextEC, abilityContainer);
}

public class P_InflictDamage_OnEnter : StateAction {
	protected new P_InflictDamage_OnEnterSO OriginSO => ( P_InflictDamage_OnEnterSO )base.OriginSO;

	private readonly CreateFloatingTextEventChannelSO _createTextEC;
	private readonly AbilityContainerSO _abilityContainer;
	
	private AbilityController _abilityController;
	private Statistics _statistics;
	private Attacker _attacker;

	public P_InflictDamage_OnEnter(CreateFloatingTextEventChannelSO createTextEC,
		AbilityContainerSO abilityContainer) {
		this._createTextEC = createTextEC;
		this._abilityContainer = abilityContainer;
	}

	public override void Awake(StateMachine stateMachine) {
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_statistics = stateMachine.gameObject.GetComponent<Statistics>();
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
	}

	public override void OnUpdate() { }

	public override void OnStateEnter() {
		//TODO(combat) move to Combat System 

		// TODO: check conditions for targeted effects and don't 
		//       just inflict damage on target
		//AbilitySO ability = playerCharacterSC.Abilities[playerCharacterSC.AbilityID];
		AbilitySO ability = _abilityContainer.abilities[_abilityController.SelectedAbilityID];
		Debug.Log("Ability: " + ability.name);

		int damage = 0;
		foreach ( TargetedEffect targetedEffect in ability.targetedEffects ) {
			Effect effect = targetedEffect.effect;
			var stats = _statistics.StatusValues;
			int effectDamage = effect.baseDamage +
			                   ( int )effect.strengthBonus * stats.Strength.value +
			                   ( int )effect.dexterityBonus * stats.Dexterity.value +
			                   ( int )effect.intelligenceBonus * stats.Intelligence.value;

			if ( effect.type == DamageType.Healing )
				effectDamage *= -1;

			damage += effectDamage;
		}

		Color damageColor;

		if ( damage > 0 )
			damageColor = Color.red;
		else if ( damage < 0 )
			damageColor = Color.green;
		else
			damageColor = Color.grey;


		if ( _attacker.enemyTarget ) {
			_attacker.enemyTarget.ReceivesDamage(damage);

			_createTextEC.RaiseEvent(Mathf.Abs(damage).ToString(),
				_attacker.enemyTarget.gameObject.transform.position + Vector3.up,
				damageColor);
		}

		if ( _attacker.playerTarget ) {
			_attacker.playerTarget.ReceivesDamage(damage);

			_createTextEC.RaiseEvent(Mathf.Abs(damage).ToString(),
				_attacker.playerTarget.gameObject.transform.position + Vector3.up,
				damageColor);
		}
	}
}