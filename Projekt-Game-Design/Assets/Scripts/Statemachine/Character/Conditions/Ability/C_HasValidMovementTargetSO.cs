using Ability.ScriptableObjects;
using Characters.Movement;
using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;

[CreateAssetMenu(fileName = "c_HasValidMovementTarget",
	menuName = "State Machines/Conditions/Character/Has Valid Movement Target")]
public class C_HasValidMovementTargetSO : StateConditionSO
{
		[SerializeField] private AbilityContainerSO abilityContainer;

		protected override Condition CreateCondition() => new C_HasValidMovementTarget(abilityContainer);
}

public class C_HasValidMovementTarget : Condition
{
		private readonly AbilityContainerSO _abilityContainer;
		private AbilityController _abilityController;
		private MovementController _movementController;

		public C_HasValidMovementTarget(AbilityContainerSO abilityContainer)
		{
				this._abilityContainer = abilityContainer;
		}

		public override void Awake(StateMachine stateMachine)
		{
				_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
				_movementController = stateMachine.GetComponent<MovementController>();
		}

		protected override bool Statement()
		{
				if ( _abilityController.SelectedAbilityID < 0 )
						return false;

				bool movesToTarget = _abilityContainer.abilities[_abilityController.SelectedAbilityID].moveToTarget;

				bool isInRange = false;
				if ( _movementController.movementTarget != null )
				{
						foreach ( PathNode tile in _movementController.reachableTiles )
						{
								if ( tile.pos.Equals(_movementController.movementTarget.pos) )
										isInRange = true;
						}
				}

				return movesToTarget && isInRange; 
		}

		public override void OnStateEnter() { }

		public override void OnStateExit() { }
}