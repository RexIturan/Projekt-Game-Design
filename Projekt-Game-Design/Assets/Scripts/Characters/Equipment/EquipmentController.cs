using Characters.Ability;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Equipment
{
		public class EquipmentController : MonoBehaviour
		{
				[SerializeField] private EquipmentContainerSO equipmentContainer;
				public int equipmentID;

				public void RefreshEquipment()
				{
						// model wise
						ModelController modelController = gameObject.GetComponent<ModelController>();
						if(modelController)
						{
								ItemSO itemLeft = equipmentContainer.GetItemLeft(equipmentID);
								ItemSO itemRight = equipmentContainer.GetItemRight(equipmentID);

								modelController.SetMeshLeft(itemLeft ? itemLeft.mesh : null);
								modelController.SetMeshRight(itemRight ? itemRight.mesh : null);
						}

						AbilityController abilityController = gameObject.GetComponent<AbilityController>();
						if ( abilityController )
								abilityController.RefreshAbilities();
				}

				public List<ItemSO> GetEquippedItems()
				{
						List<ItemSO> items = new List<ItemSO>();

						ItemSO item = equipmentContainer.GetItemLeft(equipmentID);
						if ( item )
								items.Add(item);

						item = equipmentContainer.GetItemRight(equipmentID);
						if ( item )
								items.Add(item);

						return items;
				}
		}
}