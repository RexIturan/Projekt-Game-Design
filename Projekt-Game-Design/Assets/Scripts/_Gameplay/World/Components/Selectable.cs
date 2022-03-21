using GDP01.Characters.Component;
using UnityEngine;

namespace Player {
	public class Selectable : MonoBehaviour {
		[SerializeField] private GameObject indicatorPrefab;
		public bool isSelected;
		
///// Private Functions ////////////////////////////////////////////////////////////////////////////
	
		private GameObject selectionIndicator;
		//todo refactor and remove from here -> use Action callback 
		private AbilityController abilityController;

///// Public Function //////////////////////////////////////////////////////////////////////////////

		public void Select() {
			isSelected = true;
			selectionIndicator.SetActive(true);
		}

		public void Deselect() {
			isSelected = false;
			selectionIndicator.SetActive(false);

			if(abilityController) {
				abilityController.abilitySelected = false;
				abilityController.LastSelectedAbilityID = -1;
			}
		}
		
		public static Selectable[] GetAllInstances() {
			return FindObjectsOfType<Selectable>();
		}

///// Unity Function ///////////////////////////////////////////////////////////////////////////////

		private void Start() {
			selectionIndicator = Instantiate(indicatorPrefab, transform);
			selectionIndicator.SetActive(false);
			
			abilityController = gameObject.GetComponent<AbilityController>();
		}
	}
}