using Ability;
using Ability.ScriptableObjects;
using Characters;
using Characters.Ability;
using Combat;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;

[CreateAssetMenu(fileName = "c_HasValidGroundTarget",
	menuName = "State Machines/Conditions/Character/Has Valid Ground Target")]
public class C_HasValidGroundTargetSO : StateConditionSO
{
		[SerializeField] private AbilityContainerSO abilityContainer;

		protected override Condition CreateCondition() => new C_HasValidGroundTarget(abilityContainer);
}

public class C_HasValidGroundTarget : Condition
{
		private readonly AbilityContainerSO _abilityContainer;
		private AbilityController _abilityController;
		private Attacker _attacker;

		public C_HasValidGroundTarget(AbilityContainerSO abilityContainer)
		{
				this._abilityContainer = abilityContainer;
		}

		public override void Awake(StateMachine stateMachine)
		{
				_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
				_attacker = stateMachine.GetComponent<Attacker>();
		}

		protected override bool Statement()
		{
				if ( _abilityController.SelectedAbilityID < 0 )
						return false;

				bool groundIsValid = _abilityContainer.abilities[_abilityController.SelectedAbilityID].targets.HasFlag(AbilityTarget.Ground);

				bool isInRange = false;
				foreach(PathNode tile in _attacker.tilesInRange)
				{
						if ( tile.pos.Equals(_attacker.GetGroundTarget()) )
								isInRange = true;
				}

				return _attacker.groundTargetSet && groundIsValid && isInRange; 
		}

		public override void OnStateEnter() { }

		public override void OnStateExit() { }
}