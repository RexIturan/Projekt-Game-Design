using System.Collections.Generic;
using UnityEngine;

namespace Characters.Ability {
	public class AbilityController : MonoBehaviour {
		[SerializeField] private int abilityID;
		[SerializeField] private AbilitySO[] abilities;
		[SerializeField] private AbilitySO[] baseAbilities;
		
		public AbilitySO[] Abilities => abilities;

		//todo get all equiped items from EquipmentController
		private ItemSO item;
		
		//state
		public bool abilitySelected;
		public bool abilityConfirmed;
		public bool abilityExecuted;
		
		public int AbilityID {
			get => abilityID;
			set => abilityID = value;
		}

		public void RefreshAbilities() {
			List<AbilitySO> currentAbilities = new List<AbilitySO>(baseAbilities);
			if ( item is { } ) {
				foreach ( AbilitySO ability in item.abilities )
					if ( !currentAbilities.Contains(ability) )
						currentAbilities.Add(ability);
			}

			abilities = currentAbilities.ToArray();
		}
	}
}