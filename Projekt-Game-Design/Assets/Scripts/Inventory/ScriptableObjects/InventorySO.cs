using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InventorySO
/// All items a player has (all equipped and all not equipped items in possession)
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
		[System.Serializable]
		public class Equipment
		{
				public WeaponSO weaponLeft;
				public WeaponSO weaponRight;
				public HeadArmorSO headArmor;
				public BodyArmorSO bodyArmor;
				public ShieldSO shield;

				/**
				 * returns the number of equipment slots
				 */
				public int GetCount() { return 5; }

				/**
				 * relevant for SaveWriter
				 * Converts equipment to array of Items. 
				 * Not equipped equipment slots are set to null
				 * The elements of the list are ordered as such (the order of
				 * the elements is relevant for valid save data):
				 *	1. weaponLeft
				 *	2. weaponRight
				 *	3. headArmor
				 *	4. bodyArmor
				 *	5. shield
				 */
				public ItemSO[] EquipmentToArray()
				{
						ItemSO[] items = new ItemSO[GetCount()];
						items[0] = weaponLeft;
						items[1] = weaponRight;
						items[2] = headArmor;
						items[3] = bodyArmor;
						items[4] = shield;
						return items;
				}
		}

		public List<ItemSO> playerInventory;
		public List<Equipment> equipmentInventories;

		public ItemSO GetItemFromEquipment(int playerID, EquipmentPosition equipmentPosition)
		{
				switch ( equipmentPosition )
				{
						case EquipmentPosition.LEFT:
								return equipmentInventories[playerID].weaponLeft;
						case EquipmentPosition.RIGHT:
								return equipmentInventories[playerID].weaponRight;
						case EquipmentPosition.HEAD:
								return equipmentInventories[playerID].headArmor;
						case EquipmentPosition.BODY:
								return equipmentInventories[playerID].bodyArmor;
						case EquipmentPosition.SHIELD:
								return equipmentInventories[playerID].shield;
						default:
								return null;
				}
		}

		public ItemSO SetItemInEquipment(int playerID, EquipmentPosition equipmentPosition, ItemSO item)
		{
				ItemSO previous = null;

				switch ( equipmentPosition )
				{
						case EquipmentPosition.LEFT:
								previous = equipmentInventories[playerID].weaponLeft;
								equipmentInventories[playerID].weaponLeft = (WeaponSO)item;
								break;
						case EquipmentPosition.RIGHT:
								previous = equipmentInventories[playerID].weaponRight;
								equipmentInventories[playerID].weaponRight = ( WeaponSO )item;
								break;
						case EquipmentPosition.HEAD:
								previous = equipmentInventories[playerID].headArmor;
								equipmentInventories[playerID].headArmor = ( HeadArmorSO )item;
								break;
						case EquipmentPosition.BODY:
								previous = equipmentInventories[playerID].bodyArmor;
								equipmentInventories[playerID].bodyArmor = ( BodyArmorSO )item;
								break;
						case EquipmentPosition.SHIELD:
								previous = equipmentInventories[playerID].shield;
								equipmentInventories[playerID].shield = ( ShieldSO )item;
								break;
				}

				return previous;
		}
}
