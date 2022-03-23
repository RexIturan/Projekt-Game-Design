using Ability.ScriptableObjects;
using Characters.Movement;
using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions {
	[CreateAssetMenu(fileName = "c_ReduceEnergy_OnEnter",
		menuName = "State Machines/Actions/Character/Reduce Energy On Enter")]
	public class C_PayAbilityCosts_OnEnterSO : StateActionSO {
		[SerializeField] private AbilityContainerSO abilityContainer;
		public override StateAction CreateAction() => new C_PayAbilityCosts_OnEnter(abilityContainer);
	}

	public class C_PayAbilityCosts_OnEnter : StateAction {
		protected new C_PayAbilityCosts_OnEnterSO OriginSO => ( C_PayAbilityCosts_OnEnterSO )base.OriginSO;
		private readonly AbilityContainerSO _abilityContainer;
		private AbilityController _abilityController;
		private MovementController _movementController;
		private Statistics _statistics;

		public C_PayAbilityCosts_OnEnter(AbilityContainerSO abilityContainer) {
			this._abilityContainer = abilityContainer;
		}

		public override void Awake(UOP1.StateMachine.StateMachine stateMachine) {
			_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
			_movementController = stateMachine.gameObject.GetComponent<MovementController>();
			_statistics = stateMachine.gameObject.GetComponent<Statistics>();
		}

		public override void OnUpdate() { }

		public override void OnStateEnter() {
			var currentAbility = _abilityContainer.abilities[_abilityController.SelectedAbilityID];
						
			var energyReduction = currentAbility.costs;
			if(currentAbility.moveToTarget) {
				energyReduction += _movementController.GetEnergyUseUpFromMovement();
			}
			_statistics.StatusValues.Energy.Decrease(energyReduction);

			//todo idk move in its own action?
			//cooldown
			if ( currentAbility.HasCoolDown ) {
				_abilityController.SetCooldown(currentAbility);	
			}
			
			// Debug.Log("Reducing energy by " + energyReduction + " points.");
		}

		public override void OnStateExit() { }
	}
}