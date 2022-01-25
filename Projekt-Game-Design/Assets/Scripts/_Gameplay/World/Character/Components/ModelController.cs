using System.Linq;
using Characters.Types;
using GDP01._Gameplay.Logic_Data.Equipment.Types;
using GDP01.Characters.Component;
using UnityEngine;
using static EquipmentPosition;
using static WeaponPositionType;

namespace Characters {
	public class ModelController : MonoBehaviour {
		[SerializeField] private MaterialReferenceStore visualsContainerSO;
		[SerializeField] private GameObject model;
		
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
		public GameObject Model { get => model; private set => model = value; }
		[SerializeField] private CharacterModelController characterModelController;
		[SerializeField] private CharacterAnimationController animationController;
		
		public void Initialize() {
			//init model
			if ( Model is null ) {
				Model = Instantiate(prefab, this.transform);
				characterModelController = Model.GetComponentInChildren<CharacterModelController>();
				animationController = Model.GetComponent<CharacterAnimationController>();
			}
		}
		
		public CharacterAnimationController GetAnimationController() {
			return animationController;
		}

		public void SetMeshLeft(Mesh left, Material material) {
			weaponLeft = left;
      animationController.ChangeEquipment(LEFT, weaponLeft, material);
		}

		public void SetMeshRight(Mesh right, Material material) {
			weaponRight = right;
			animationController.ChangeEquipment(RIGHT, weaponRight, material);
		}

		public void SetMeshHead(Mesh head) {
			headArmor = head;
			animationController.ChangeEquipment(HEAD, headArmor, null);
		}

		public void SetMeshBody(Mesh body) {
			bodyArmor = body;
			animationController.ChangeEquipment(BODY, bodyArmor, null);
		}

		public void SetMeshShield(Mesh shield, Material material) {
			this.shield = shield;
			animationController.ChangeEquipment(SHIELD, shield, material);
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

		public void UpdateWeaponPositions(EquipmentPosition activeHand) {
			switch ( activeHand ) {
				case LEFT:
					animationController.ChangeWeaponPosition(LEFT, EQUIPPED);
					animationController.ChangeWeaponPosition(RIGHT, BACK_UPWARDS);
					break;
				case RIGHT:
					animationController.ChangeWeaponPosition(LEFT, BACK_UPWARDS);
					animationController.ChangeWeaponPosition(RIGHT, EQUIPPED);
					break;
				case NONE:
					animationController.ChangeWeaponPosition(LEFT, BACK_UPWARDS);
					animationController.ChangeWeaponPosition(RIGHT, BACK_UPWARDS);
					break;
			}
		}
	}
}