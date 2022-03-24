using System.Collections.Generic;
using Characters;
using Characters.Equipment.ScriptableObjects;
using GDP01._Gameplay.Logic_Data.Equipment.Types;
using GDP01.Equipment;
using UnityEngine;
using static EquipmentPosition;

namespace GDP01.Characters.Component {
		[RequireComponent(typeof(ModelController))]
		[RequireComponent(typeof(AbilityController))]
		public class EquipmentController : MonoBehaviour {
			//todo equipment Container SO
				[SerializeField] private EquipmentContainerSO equipmentContainer;

				//index on the equipmentContainer sheet
				[SerializeField] private int _equipmentID = -1;
				public int EquipmentID {
					//todo check if id is valid, error or create new equipment sheet
					get => _equipmentID;

					set {
						if(value < 0) Debug.LogError($"EquipmentId is {value}, but must be >= 0");
						
						equipmentContainer.UnclaimId(_equipmentID);

						if ( equipmentContainer.IdExists(value) ) {
							_equipmentID = equipmentContainer.IdClaimed(value) ? equipmentContainer.CreateNewEquipmentSheet() : value;
						}
						else {
							_equipmentID = equipmentContainer.CreateNewEquipmentSheet();
						}
						
						equipmentContainer.ClaimId(_equipmentID);
					}
				}

				// the position in the equipment inventory
				// it is the item that grants the ability
				[SerializeField] private EquipmentPosition activeEquipment = NONE; 
				// the active hand for the ability (animation)
				// e.g. Cast A uses the left hand, Attack R uses the right hand
				[SerializeField] private EquipmentPosition activeHand = NONE;

				private ItemSO activeItem;
				
				public WeaponSO RightWeapon => 
					equipmentContainer.GetItemFromEquipment(EquipmentID, RIGHT) as WeaponSO;
				
				public WeaponSO LeftWeapon => 
					equipmentContainer.GetItemFromEquipment(EquipmentID, LEFT) as WeaponSO;
				
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

								ItemSO itemHead = equipmentContainer.EquipmentSheets[EquipmentID].headArmor;
								ItemSO itemBody = equipmentContainer.EquipmentSheets[EquipmentID].bodyArmor;
								ItemSO itemShield = equipmentContainer.EquipmentSheets[EquipmentID].shield;

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

						WeaponSO item = LeftWeapon;
						if ( item )
								items.Add(item);

						item = RightWeapon;
						if ( item )
								items.Add(item);

						return items;
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