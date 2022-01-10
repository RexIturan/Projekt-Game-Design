using Characters.Ability;
using UnityEngine;

namespace Player {
	public class Selectable : MonoBehaviour {
		public bool isSelected;

		public void Select() {
			isSelected = true;
		}

		public void Deselect() {
			isSelected = false;

			AbilityController abilityController = gameObject.GetComponent<AbilityController>();
			if(abilityController) {
				abilityController.abilitySelected = false;
				abilityController.LastSelectedAbilityID = -1;
			}
		}
	}
}