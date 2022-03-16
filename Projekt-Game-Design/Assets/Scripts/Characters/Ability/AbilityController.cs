using Ability;
using Ability.ScriptableObjects;
using Characters.Equipment;
using Combat;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Characters.Ability {
	public class AbilityController : MonoBehaviour {
		[SerializeField] private int abilityID;
		[SerializeField] private int lastAbilityID;
		[SerializeField] private AbilitySO[] baseAbilities;
		[SerializeField] private AbilityContainerSO abilityContainer;
		
		public AbilitySO[] Abilities;
		public AbilitySO[] BaseAbilities {
			get => baseAbilities;
			set => baseAbilities = value;
		}
				
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
			if ( GetSelectedAbility().targets.HasFlag(AbilityTarget.Ground) && range.Count == 1 ) { 
				singleTargetPos = range[0];
				singleTarget = true;
			}
			else {
				List<Targetable> targetables = CombatUtils.FindAllTargets(range, attacker, GetSelectedAbility().targets);

				if (!GetSelectedAbility().targets.HasFlag(AbilityTarget.Ground) && targetables.Count == 1) { 
					singleTargetPos = targetables[0].GetGridPosition();
					singleTarget = true;
				}
				else { 
					singleTargetPos = Vector3Int.zero;
					singleTarget = false;
				}
			}
		}
	}
}