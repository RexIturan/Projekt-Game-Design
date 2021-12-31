using UnityEngine;

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
		public Mesh headArmor;
		public Mesh bodyArmor;
		public Mesh shield;
		
		//animation controller relevant
		public StanceType stance;
		public WeaponPositionType weaponRightPosition;
		
		//control model
		public CharacterAnimationController animationController;
		
		public void Initialize() {
			//init model
			var charModel = Instantiate(prefab, this.transform);
			animationController = charModel.GetComponent<CharacterAnimationController>();
		}
		
		public CharacterAnimationController GetAnimationController() {
			return animationController;
		}

		public void SetMeshLeft(Mesh left) {
			weaponLeft = left;
      animationController.ChangeEquipment(EquipmentPosition.LEFT, weaponLeft);
		}

		public void SetMeshRight(Mesh right) {
			weaponRight = right;
			animationController.ChangeEquipment(EquipmentPosition.RIGHT, weaponRight);
		}

		public void SetMeshHead(Mesh head) {
			headArmor = head;
			animationController.ChangeEquipment(EquipmentPosition.HEAD, headArmor);
		}

		public void SetMeshBody(Mesh body) {
			bodyArmor = body;
			animationController.ChangeEquipment(EquipmentPosition.BODY, bodyArmor);
		}

		public void SetMeshShield(Mesh shield) {
			this.shield = shield;
			animationController.ChangeEquipment(EquipmentPosition.SHIELD, shield);
		}

		public void SetStandardHead(Mesh mesh) {
			animationController.SetStandardHead(mesh);
		}

		public void SetStandardBody(Mesh mesh) {
			animationController.SetStandardBody(mesh);
		}
	}
}