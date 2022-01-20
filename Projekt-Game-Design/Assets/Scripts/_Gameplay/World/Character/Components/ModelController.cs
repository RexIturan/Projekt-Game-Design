using System.Linq;
using Characters.Types;
using UnityEngine;

namespace Characters {
	public class ModelController : MonoBehaviour {
		[SerializeField] private MaterialReferenceStore visualsContainerSO;
		
		//todo get equiped items and show them 
		//todo wrap in scriptable object
		public GameObject prefab;

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
		public GameObject Model { get; private set; }
		private CharacterModelController characterModelController;
		public CharacterAnimationController animationController;
		
		public void Initialize() {
			//init model
			Model = Instantiate(prefab, this.transform);
			characterModelController = Model.GetComponentInChildren<CharacterModelController>();
			animationController = Model.GetComponent<CharacterAnimationController>();
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

		public void SetFactionMaterial(Faction faction) {

			var material = visualsContainerSO.GetMaterial(faction);

			if ( material ) {
				characterModelController.SetHeadMaterial(material);
				characterModelController.SetBodyMaterial(material);	
			}
		}
	}
}