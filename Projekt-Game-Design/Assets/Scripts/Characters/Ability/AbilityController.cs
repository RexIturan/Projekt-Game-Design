using Characters.Equipment;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Ability {
	public class AbilityController : MonoBehaviour {
		[SerializeField] private int abilityID;
		[SerializeField] private AbilitySO[] abilities;
		[SerializeField] private AbilitySO[] baseAbilities;
		
		public AbilitySO[] Abilities => abilities;
		public AbilitySO[] BaseAbilities {
			get => baseAbilities;
			set => baseAbilities = value;
		}
				
		//state
		public bool abilitySelected;
		public bool abilityConfirmed;
		public bool abilityExecuted;
		
		public int SelectedAbilityID {
			get => abilityID;
			set => abilityID = value;
		}

		public void RefreshAbilities() {
			List<AbilitySO> currentAbilities = new List<AbilitySO>(baseAbilities);

			EquipmentController equipmentController = gameObject.GetComponent<EquipmentController>();
			if(equipmentController) {
				foreach(WeaponSO item in equipmentController.GetEquippedWeapons()) {
					currentAbilities.AddRange(item.abilities);
				}
			}

			abilities = currentAbilities.ToArray();
		}
	}
}