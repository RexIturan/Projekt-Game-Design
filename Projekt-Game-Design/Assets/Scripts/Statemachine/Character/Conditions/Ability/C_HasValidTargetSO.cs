using Ability.ScriptableObjects;
using Characters;
using Characters.Types;
using Combat;
using GDP01.Characters.Component;
using GDP01.World.Components;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;

[CreateAssetMenu(fileName = "c_HasValidTargetSO",
	menuName = "State Machines/Conditions/Character/Has Valid Target")]
public class C_HasValidTargetSO : StateConditionSO
{
		[SerializeField] private AbilityContainerSO abilityContainer;

		protected override Condition CreateCondition() => new C_HasValidTarget(abilityContainer);
}

public class C_HasValidTarget : Condition
{
		protected new C_HasValidTargetSO OriginSO => ( C_HasValidTargetSO )base.OriginSO;

		private readonly AbilityContainerSO _abilityContainer;
		private AbilityController _abilityController;
		private Attacker _attacker;

		public C_HasValidTarget(AbilityContainerSO abilityContainer)
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
				Targetable _target = _attacker.GetTarget();

				if ( _abilityController.SelectedAbilityID < 0 )
						return false;

				bool targetExists = (_target != null);
				bool targetRelationshipValid = false;
				bool targetInRange = false;

				AbilitySO ability = _abilityContainer.abilities[_abilityController.SelectedAbilityID];

				// relationship valid for ability?
				if ( targetExists ) {
						targetRelationshipValid = AbilityController.HasRightRelationshipForAbility(ability, _attacker, _target);
				}

				// in range?
				if(targetRelationshipValid)
				{
						foreach(PathNode tile in _attacker.tilesInRange)
						{
								if ( tile.pos.Equals(_target.GetGridPosition()) )
										targetInRange = true;
						}
				}

				return targetInRange; // && targetRelationshipValid && targetExists;
		}

		public override void OnStateEnter() { }

		public override void OnStateExit() { }
}