using System.Collections.Generic;
using Characters;
using Characters.Equipment.ScriptableObjects;
using GDP01._Gameplay.Logic_Data.Equipment.Types;
using UnityEngine;
using static EquipmentPosition;

namespace GDP01.Characters.Component {
		[RequireComponent(typeof(ModelController))]
		[RequireComponent(typeof(AbilityController))]
		public class EquipmentController : MonoBehaviour {
			//todo equipment Container SO
				[SerializeField] private EquipmentContainerSO equipmentContainer;
				//todo -> player id??
				public int equipmentID;

				// the position in the equipment inventory
				// it is the item that grants the ability
				[SerializeField] private EquipmentPosition activeEquipment = NONE; 
				// the active hand for the ability (animation)
				// e.g. Cast A uses the left hand, Attack R uses the right hand
				[SerializeField] private EquipmentPosition activeHand = NONE;

				private ItemSO activeItem;
				
				public ItemSO RightWeapon {
					get { return equipmentContainer.equipmentSheets[equipmentID].GetEquipedItem(RIGHT); }
				}
				
				public ItemSO LeftWeapon {
					get { return equipmentContainer.equipmentSheets[equipmentID].GetEquipedItem(LEFT); }
				}
				
				public void RefreshEquipment() {
						// Model-wise
						RefreshModels();
						RefreshWeaponPositions();

						// Ability-wise
						AbilityController abilityController = gameObject.GetComponent<AbilityController>();
						if ( abilityController )
								abilityController.RefreshAbilities();
				}

				public void RefreshModels() {
						ModelController modelController = gameObject.GetComponent<ModelController>();
						if ( modelController ) {
								// Find the proper Items for their respective hands
								//
								ItemSO itemLeftHand;
								ItemSO itemRightHand;

								// if the active hand is the left, and the active weapon is in the right equipment position
								// of if the active hand is the right, and the active weapon is in the left equipment position,
								// swap the hands
								if(activeHand != activeEquipment) {
										itemLeftHand = RightWeapon;
										itemRightHand = LeftWeapon;
								}
								else {
										// else put the right weapon to the right hand, and the left weapon to the left by default
										itemLeftHand = LeftWeapon;
										itemRightHand = RightWeapon;
								}

								ItemSO itemHead = equipmentContainer.equipmentSheets[equipmentID].headArmor;
								ItemSO itemBody = equipmentContainer.equipmentSheets[equipmentID].bodyArmor;
								ItemSO itemShield = equipmentContainer.equipmentSheets[equipmentID].shield;

								if ( itemLeftHand ) {
									modelController.SetMeshLeft(itemLeftHand.mesh, itemLeftHand.material);	
								}
								else {
									modelController.SetMeshLeft(null, null);
								}
								
								if ( itemRightHand ) {
									modelController.SetMeshRight(itemRightHand.mesh, itemRightHand.material);	
								}
								else {
									modelController.SetMeshRight(null, null);
								}
								
								if ( itemShield ) {
									modelController.SetMeshShield(itemShield.mesh, itemShield.material);	
								}
								else {
									modelController.SetMeshShield(null, null);
								}
								
								modelController.SetMeshHead(itemHead ? itemHead.mesh : null);
								modelController.SetMeshBody(itemBody ? itemBody.mesh : null);
						}
				}

				public void RefreshWeaponPositions() {
						ModelController modelController = gameObject.GetComponent<ModelController>();
						if ( modelController ) {
							modelController.UpdateWeaponPositions(activeHand);
						}
				}

				public List<WeaponSO> GetEquippedWeapons()
				{
						List<WeaponSO> items = new List<WeaponSO>();

						WeaponSO item = equipmentContainer.equipmentSheets[equipmentID].weaponLeft;
						if ( item )
								items.Add(item);

						item = equipmentContainer.equipmentSheets[equipmentID].weaponRight;
						if ( item )
								items.Add(item);

						return items;
				}

				/**
				 * returns the weapon equipped in the left position in the equipment
				 */
				public WeaponSO GetWeaponLeft() {
						return equipmentContainer.equipmentSheets[equipmentID].weaponLeft;
				}
				
				/**
				 * returns the weapon equipped in the right position in the equipment
				 */
				public WeaponSO GetWeaponRight() {
						return equipmentContainer.equipmentSheets[equipmentID].weaponRight;
				}

				/**
				 * sets the active weapon(s) to the given side, 
				 * mostly the item that grants the respective ability
				 */
				public void SetActiveWeapon(EquipmentPosition sides) {
						activeEquipment = sides;
				}

				/**
				 * sets the active hand(s) to given side,
				 * mostly the hands used for ability's animation
				 */
				public void SetActiveHands(EquipmentPosition sides) {
						activeHand = sides;
				}
		}
}