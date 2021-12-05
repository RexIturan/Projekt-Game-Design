using Ability.ScriptableObjects;
using Characters.Ability;
using Characters.Movement;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions {
	[CreateAssetMenu(fileName = "p_ReduceEnergy_OnEnter",
		menuName = "State Machines/Actions/Player/Reduce Energy")]
	public class P_ReduceEnergy_OnEnterSO : StateActionSO {
		[SerializeField] private AbilityContainerSO abilityContainer;
		public override StateAction CreateAction() => new P_ReduceEnergy_OnEnter(abilityContainer);
	}

	public class P_ReduceEnergy_OnEnter : StateAction {
		protected new P_ReduceEnergy_OnEnterSO OriginSO => ( P_ReduceEnergy_OnEnterSO )base.OriginSO;
		private readonly AbilityContainerSO _abilityContainer;
		private AbilityController _abilityController;
		private MovementController _movementController;
		private Statistics _statistics;

		public P_ReduceEnergy_OnEnter(AbilityContainerSO abilityContainer) {
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

			var energyReduction = currentAbility.costs + ( currentAbility.moveToTarget
				? _movementController.GetEnergyUseUpFromMovement()
				: 0 );
			_statistics.StatusValues.Energy.Decrease(energyReduction);
		}

		public override void OnStateExit() { }
	}
}