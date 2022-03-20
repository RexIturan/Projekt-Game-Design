﻿using Ability.ScriptableObjects;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Combat;
using UnityEngine;
using Util;
using GDP01.World.Components;

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
		[SerializeField] private int lastAbilityID = -1;
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

		public bool singleTarget; // is set to true, if there is only one proper target for the ability at the moment
		public Vector3Int singleTargetPos; // is set, if there is only one proper target for the ability at the moment
		
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

			//todo this is just used by player characters
			EquipmentController equipmentController = gameObject.GetComponent<EquipmentController>();
			if(equipmentController is {}) {
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
			// Debug.Log($"{cost}\n{movementController.PreviewPath?.AllToString()}");
			return cost;
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// Sets single target flag and single target position (vector) 
		/// depending on whehter or not only one tile can be targeted at the moment. 
		/// </summary>
		public void UpdateSingleTargetFlag() { 
			Attacker attacker = GetComponent<Attacker>();
			List<Vector3Int> range = PathNode.ConvertPathNodeListToVector3IntList(attacker.tilesInRange);

			// ability is single target if it either:
			// targets ground and has one tile in its range or
			// there is one targetable in its range 
			if ( GetSelectedAbility().targets.HasFlag(TargetRelationship.Ground) && range.Count == 1 ) { 
				singleTargetPos = range[0];
				singleTarget = true;
			}
			else {
				List<Targetable> targetables = CombatUtils.FindAllTargets(range, attacker, GetSelectedAbility().targets);
				if (!GetSelectedAbility().targets.HasFlag(TargetRelationship.Ground) && targetables.Count == 1) { 
					singleTargetPos = targetables[0].GetGridPosition();
					singleTarget = true;
				}
				else { 
					singleTargetPos = Vector3Int.zero;
					singleTarget = false;
				}
			}
		}

		private void Start() {
			damageInflicted = true;
		}
	}
}