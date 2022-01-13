using System;
using Characters.Ability;
using UnityEngine;

namespace Player {
	public class Selectable : MonoBehaviour {

		private GameObject selectionIndicator;
		
		[SerializeField] private GameObject indicatorPrefab;
		public bool isSelected;

		private void Start() {
			selectionIndicator = Instantiate(indicatorPrefab, transform);
			selectionIndicator.SetActive(false);	
		}

		public void Select() {
			isSelected = true;
			selectionIndicator.SetActive(true);
		}

		public void Deselect() {
			isSelected = false;
			selectionIndicator.SetActive(false);

			AbilityController abilityController = gameObject.GetComponent<AbilityController>();
			if(abilityController) {
				abilityController.abilitySelected = false;
				abilityController.LastSelectedAbilityID = -1;
			}
		}
		
		public static Selectable[] GetAllInstances() {
			return FindObjectsOfType<Selectable>();
		}
	}
}