using System.Collections.Generic;
using Characters;
using Characters.Equipment.ScriptableObjects;
using UnityEngine;

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
				[SerializeField] private ActiveEquipmentPosition activeEquipment = 0; 
				// the active hand for the ability (animation)
				// e.g. Cast A uses the left hand, Attack R uses the right hand
				[SerializeField] private ActiveEquipmentPosition activeHands = 0;

				//todo move to Equipment/DataTypes
				[System.Flags, System.Serializable]
				public enum ActiveEquipmentPosition
				{
						LEFT = 1,
						RIGHT = 2,
				}

				public void Start()
				{
						/*
						inventory.equipmentInventories.Add(new InventorySO.Equipment());
						playerID = inventory.equipmentInventories.Count - 1;
						*/
				}

				public void RefreshEquipment()
				{
						// Model-wise
						RefreshModels();
						RefreshWeaponPositions();

						// Ability-wise
						AbilityController abilityController = gameObject.GetComponent<AbilityController>();
						if ( abilityController )
								abilityController.RefreshAbilities();
				}

				public void RefreshModels()
				{
						ModelController modelController = gameObject.GetComponent<ModelController>();
						if ( modelController )
						{
								// Find the proper Items for their respective hands
								//
								ItemSO itemLeftHand;
								ItemSO itemRightHand;


								// if the active hand is the left, and the active weapon is in the right equipment position
								// of if the active hand is the right, and the active weapon is in the left equipment position,
								// swap the hands
								if(activeHands.Equals(ActiveEquipmentPosition.LEFT) && activeEquipment.Equals(ActiveEquipmentPosition.RIGHT) ||
										activeHands.Equals(ActiveEquipmentPosition.RIGHT) && activeEquipment.Equals(ActiveEquipmentPosition.LEFT))
								{
										itemLeftHand = GetWeaponRight();
										itemRightHand = GetWeaponLeft();
								}
								else
								{
										// else put the right weapon to the right hand, and the left weapon to the left by default
										itemLeftHand = GetWeaponLeft();
										itemRightHand = GetWeaponRight();
								}

								ItemSO itemHead = equipmentContainer.equipmentSheets[equipmentID].headArmor;
								ItemSO itemBody = equipmentContainer.equipmentSheets[equipmentID].bodyArmor;
								ItemSO itemShield = equipmentContainer.equipmentSheets[equipmentID].shield;

								modelController.SetMeshLeft(itemLeftHand ? itemLeftHand.mesh : null);
								modelController.SetMeshRight(itemRightHand ? itemRightHand.mesh : null);
								modelController.SetMeshHead(itemHead ? itemHead.mesh : null);
								modelController.SetMeshBody(itemBody ? itemBody.mesh : null);
								modelController.SetMeshShield(itemShield ? itemShield.mesh : null);
						}
				}

				public void RefreshWeaponPositions()
				{
						ModelController modelController = gameObject.GetComponent<ModelController>();
						if ( modelController )
						{
								modelController.animationController
										.ChangeWeaponPosition(EquipmentPosition.LEFT, 
										activeHands.HasFlag(ActiveEquipmentPosition.LEFT) ? WeaponPositionType.EQUIPPED : WeaponPositionType.BACK_UPWARDS);

								modelController.animationController
										.ChangeWeaponPosition(EquipmentPosition.RIGHT,
										activeHands.HasFlag(ActiveEquipmentPosition.RIGHT) ? WeaponPositionType.EQUIPPED : WeaponPositionType.BACK_UPWARDS);
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
				public void SetActiveWeapon(ActiveEquipmentPosition sides)
				{
						activeEquipment = sides;
				}

				/**
				 * sets the active hand(s) to given side,
				 * mostly the hands used for ability's animation
				 */
				public void SetActiveHands(ActiveEquipmentPosition sides)
				{
						activeHands = sides;
				}
		}
}