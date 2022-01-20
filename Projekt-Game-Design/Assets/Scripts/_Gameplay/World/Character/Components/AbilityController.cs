using Ability.ScriptableObjects;
using System.Collections.Generic;
using Characters.Equipment;
using UnityEngine;

namespace GDP01.Characters.Component {
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
	}
}