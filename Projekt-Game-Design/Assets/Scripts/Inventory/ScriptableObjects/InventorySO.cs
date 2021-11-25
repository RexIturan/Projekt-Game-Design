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
    public List<int> itemIDs = new List<int>();

		public ItemSO GetItem(int idInInventory) {
				if(idInInventory >= 0 && idInInventory < itemIDs.Count)
					return itemContainer.itemList[idInInventory];
				else
				{
						Debug.LogError("Item not in Player Inventory. ID: " + idInInventory);
						return null;
				}
		}
}
