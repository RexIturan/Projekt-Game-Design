using Characters.Ability;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Equipment
{
		public class EquipmentController : MonoBehaviour
		{
				[SerializeField] private InventorySO inventory;
				public int playerID;

				public void Start()
				{
						/*
						inventory.equipmentInventories.Add(new InventorySO.Equipment());
						playerID = inventory.equipmentInventories.Count - 1;
						*/
				}

				public void RefreshEquipment()
				{
						// model wise
						ModelController modelController = gameObject.GetComponent<ModelController>();
						if(modelController)
						{
								ItemSO itemLeft = inventory.equipmentInventories[playerID].weaponLeft;
								ItemSO itemRight = inventory.equipmentInventories[playerID].weaponRight;
								ItemSO itemHead = inventory.equipmentInventories[playerID].headArmor;
								ItemSO itemBody = inventory.equipmentInventories[playerID].bodyArmor;
								ItemSO itemShield = inventory.equipmentInventories[playerID].shield;

								modelController.SetMeshLeft(itemLeft ? itemLeft.mesh : null);
								modelController.SetMeshRight(itemRight ? itemRight.mesh : null);
								modelController.SetMeshHead(itemHead ? itemHead.mesh : null);
								modelController.SetMeshBody(itemBody ? itemBody.mesh : null);
								modelController.SetMeshShield(itemShield ? itemShield.mesh : null);
						}

						AbilityController abilityController = gameObject.GetComponent<AbilityController>();
						if ( abilityController )
								abilityController.RefreshAbilities();
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
		}
}