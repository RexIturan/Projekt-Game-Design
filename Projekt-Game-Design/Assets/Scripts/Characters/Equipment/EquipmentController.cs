using Characters.Ability;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Equipment
{
		public class EquipmentController : MonoBehaviour
		{
				[SerializeField] private InventorySO inventory;
				public int playerID;

				private bool rightIsActive = true;

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
								ItemSO itemLeft;
								ItemSO itemRight;

								if ( rightIsActive )
								{
										itemLeft = inventory.equipmentInventories[playerID].weaponLeft;
										itemRight = inventory.equipmentInventories[playerID].weaponRight;
								}
								else
								{
										itemLeft = inventory.equipmentInventories[playerID].weaponRight;
										itemRight = inventory.equipmentInventories[playerID].weaponLeft;
								}

								ItemSO itemHead = inventory.equipmentInventories[playerID].headArmor;
								ItemSO itemBody = inventory.equipmentInventories[playerID].bodyArmor;
								ItemSO itemShield = inventory.equipmentInventories[playerID].shield;

								modelController.SetMeshLeft(itemLeft ? itemLeft.mesh : null);
								modelController.SetMeshRight(itemRight ? itemRight.mesh : null);
								modelController.SetMeshHead(itemHead ? itemHead.mesh : null);
								modelController.SetMeshBody(itemBody ? itemBody.mesh : null);
								modelController.SetMeshShield(itemShield ? itemShield.mesh : null);
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

				public WeaponSO GetWeaponLeft()
				{
						return inventory.equipmentInventories[playerID].weaponLeft;
				}

				public WeaponSO GetWeaponRight()
				{
						return inventory.equipmentInventories[playerID].weaponRight;
				}

				public void ActivateRight()
				{
						rightIsActive = true;
						RefreshModels();
				}

				public void ActivateLeft()
				{
						rightIsActive = false;
						RefreshModels();
				}
		}
}