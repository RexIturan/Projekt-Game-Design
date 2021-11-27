using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InventorySO
/// All items a player has (all equipped and all not equipped items in possession)
/// </summary>
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{
		[SerializeField] private ItemContainerSO itemContainer;
    public List<int> itemIDs = new List<int>(); // id within item container
		public List<int> equipmentID = new List<int>(); // if item is equipped, this list saves id of equipment within EquipmentContainer
																										// id is -1 if item is not currently equipped

		public InventorySO ()
		{
				equipmentID = new List<int>();
				foreach ( int itemID in itemIDs )
						equipmentID.Add(-1);
		}

		public ItemSO GetItem(int idInInventory)
		{
				if ( idInInventory >= 0 && idInInventory < itemIDs.Count )
				{
						int itemID = itemIDs[idInInventory];
						if ( itemID >= 0 && itemID < itemContainer.itemList.Count )
								return itemContainer.itemList[itemID];
						else
						{
								// Debug.LogWarning("Item not in Item Container. ID: " + itemID);
								return null;
						}
				}
				else
				{
						// Debug.LogWarning("Item not in Player Inventory. ID: " + idInInventory);
						return null;
				}
		}
}
