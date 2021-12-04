using UnityEngine;

namespace Characters.Equipment {
	public class EquipmentController : MonoBehaviour {
		[SerializeField] private EquipmentInventoryContainerSO equipmentContainerSO;
		public int equipmentID;

		//visual model relevance
		public Mesh weaponLeft;
		public Mesh weaponRight;
		
		//animation controller relevant
		public StanceType stance;
		public WeaponPositionType weaponRightPosition;
		
		// private CharacterAnimationController animationController;
		//
		// public CharacterAnimationController GetAnimationController() {
		// 	return animationController;
		// }
		//
		// public void FixedUpdateAnimation() {
		// 	animationController.TakeStance(stance);
		// 	animationController.ChangeWeapon(EquipmentPosition.LEFT, weaponLeft);
		// 	animationController.ChangeWeapon(EquipmentPosition.RIGHT, weaponRight);
		// 	animationController.ChangeWeaponPosition(EquipmentPosition.RIGHT, weaponRightPosition);
		// }
		
		private void RefreshEquipment() {
			Debug.Log("!!!!!!!!!!!!!! Implement ME !!!!!!!!!!!!!!!!!");
		}
	}
}