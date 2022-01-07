using Characters.Ability;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Equipment
{
		public class EquipmentController : MonoBehaviour
		{
				[SerializeField] private InventorySO inventory;
				public int playerID;

				// the position in the equipment inventory
				// it is the item that grants the ability
				[SerializeField] private ActiveEquipmentPosition activeEquipment = 0; 
				// the active hand for the ability (animation)
				// e.g. Cast A uses the left hand, Attack R uses the right hand
				[SerializeField] private ActiveEquipmentPosition activeHands = 0;

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

								ItemSO itemHead = inventory.equipmentInventories[playerID].headArmor;
								ItemSO itemBody = inventory.equipmentInventories[playerID].bodyArmor;
								ItemSO itemShield = inventory.equipmentInventories[playerID].shield;

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

						WeaponSO item = inventory.equipmentInventories[playerID].weaponLeft;
						if ( item )
								items.Add(item);

						item = inventory.equipmentInventories[playerID].weaponRight;
						if ( item )
								items.Add(item);

						return items;
				}

				/**
				 * returns the weapon equipped in the left position in the equipment
				 */
				public WeaponSO GetWeaponLeft()
				{
						return inventory.equipmentInventories[playerID].weaponLeft;
				}
				
				/**
				 * returns the weapon equipped in the right position in the equipment
				 */
				public WeaponSO GetWeaponRight()
				{
						return inventory.equipmentInventories[playerID].weaponRight;
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