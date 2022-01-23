using System;
using Ability.ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Equipment;
using Characters.Movement;
using UnityEngine;
using Util.Extensions;

namespace GDP01.Characters.Component {
	[RequireComponent(typeof(MovementController))]
	[RequireComponent(typeof(Statistics))]
	public class AbilityController : MonoBehaviour {
///// Private Variables ////////////////////////////////////////////////////////////////////////////

		private MovementController _movementController;
		private MovementController movementController {
			get {
				if ( _movementController ) {
					return _movementController;
				}
				else {
					_movementController = gameObject.GetComponent<MovementController>();
					return _movementController;
				}
			}
		}

///// Serialize Variables //////////////////////////////////////////////////////////////////////////	
		
		[SerializeField] private int abilityID;
		[SerializeField] private int lastAbilityID;
		[SerializeField] private AbilitySO[] baseAbilities;
		[SerializeField] private AbilityContainerSO abilityContainer;
		[SerializeField] private Statistics _statistics;
		
///// Properties ///////////////////////////////////////////////////////////////////////////////////
	
		public AbilitySO[] Abilities;
		public AbilitySO[] BaseAbilities {
			get => baseAbilities;
			set => baseAbilities = value;
		}

		// todo should be properties?
		//state
		public bool abilitySelected;
		public bool abilityConfirmed;
		public bool abilityExecuted;
		public bool damageInflicted;

		public int SelectedAbilityID {
			get => abilityID;
			set => abilityID = value;
		}
		
		public int LastSelectedAbilityID {
			get => lastAbilityID;
			set => lastAbilityID = value;
		}

		public AbilitySO SelectedAbility {
			get {
				return abilityContainer.TryGetAbility(SelectedAbilityID);
			}
		}

		public bool IsAbilitySelected => SelectedAbility != null;

		///// Public Functions /////////////////////////////////////////////////////////////////////////////		
		
		public AbilitySO GetSelectedAbility() {
			return SelectedAbilityID > -1 ? abilityContainer.abilities[SelectedAbilityID] : null;
		}

		public AbilitySO GetLastSelectedAbility() {
			return LastSelectedAbilityID >= 0 ? abilityContainer.abilities[LastSelectedAbilityID] : null;
		}

		public void RefreshAbilities() {
			List<AbilitySO> currentAbilities = new List<AbilitySO>(baseAbilities);

			EquipmentController equipmentController = gameObject.GetComponent<EquipmentController>();
			if(equipmentController) {
				foreach(WeaponSO item in equipmentController.GetEquippedWeapons()) {
					currentAbilities.AddRange(item.abilities);
				}
			}

			Abilities = currentAbilities.ToArray();
		}

		public bool IsAbilityAvailable(int id) {
			return IsAbilityAvailable(abilityContainer.abilities[SelectedAbilityID]);
		}
		
		public bool IsAbilityAvailable(AbilitySO ability) {
			int cost = GetMinimalCost(ability);
			return _statistics.StatusValues.Energy.Value >= cost;
		}

		public int GetMinimalCost(AbilitySO ability) {
			int cost = 0;
			//todo idk handle in ability or so?
			if ( ability.moveToTarget ) {
				cost += movementController.movementCostPerTile;
			}
			cost += ability.costs;
			return cost;
		}
		
		public int GetCurrentAbilityCost(AbilitySO ability) {
			int cost = 0;
			if ( ability.moveToTarget ) {
				cost += movementController.GetEnergyUseUpFromMovement();
			}
			cost += ability.costs;
			Debug.Log($"{cost}\n{movementController.PreviewPath?.AllToString()}");
			return cost;
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

	}
}