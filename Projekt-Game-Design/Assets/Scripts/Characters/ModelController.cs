using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Characters {
	public class ModelController : MonoBehaviour {
		//old init
		// public PlayerTypeSO playerType;
		
		//todo get equiped items and show them 
		//todo wrap in scriptable object
		public GameObject prefab;
		public Material material;

		//visual model relevance
		public Mesh weaponLeft;
		public Mesh weaponRight;
		
		//animation controller relevant
		public StanceType stance;
		public WeaponPositionType weaponRightPosition;
		
		//control model
		public CharacterAnimationController animationController;
		
		private void Start() {
			//init model
			var charModel = Instantiate(prefab, this.transform);
			animationController = charModel.GetComponent<CharacterAnimationController>();
		}
		
		public CharacterAnimationController GetAnimationController() {
			return animationController;
		}

		public void SetMeshLeft(Mesh left) {
			weaponLeft = left;
      animationController.ChangeWeapon(EquipmentPosition.LEFT, weaponLeft);
		}

		public void SetMeshRight(Mesh right) {
			weaponRight = right;
			animationController.ChangeWeapon(EquipmentPosition.RIGHT, weaponRight);
		}
	}
}