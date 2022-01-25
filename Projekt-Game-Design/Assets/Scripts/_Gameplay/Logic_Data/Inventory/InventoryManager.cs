using Characters.Equipment.ScriptableObjects;
using Events.ScriptableObjects;
using GDP01.Characters.Component;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
	[SerializeField] private ItemContainerSO itemContainer;
	[SerializeField] private InventorySO inventory;
	[SerializeField] private EquipmentContainerSO equipmentContainer;

	[Header("Sending Events On")]
	/*
	[SerializeField] private IntEventChannelSO onItemPickupEventChannel;
	[SerializeField] private IntEventChannelSO onItemDropEventChannel;
	*/

	[Header("Receiving Events On")]
	[SerializeField] private EquipItemEC_SO equipItemEC;
	[SerializeField] private UnequipItemEC_SO unequipItemEC;
	[SerializeField] private IntEventChannelSO itemPickupEventChannel;

	private void OnEnable() {
		equipItemEC.OnEventRaised += EquipItem;
		unequipItemEC.OnEventRaised += UnequipItem;
		itemPickupEventChannel.OnEventRaised += PickupItem;
	}

	private void OnDisable() {
		equipItemEC.OnEventRaised -= EquipItem;
		unequipItemEC.OnEventRaised -= UnequipItem;
		itemPickupEventChannel.OnEventRaised -= PickupItem;
	}

  private void PickupItem(int itemID) {
		if(itemID >= 0)
			inventory.playerInventory.Add(itemContainer.itemList[itemID]);
	}

	private void UnequipItem(int playerId, EquipmentPosition pos) {
		// remove item from equipment inventory
		ItemSO former = equipmentContainer.UnequipItemFor(playerId, pos);
		// add item to player inventory
		if(former)
			inventory.playerInventory.Add(former);

		Debug.Log("Unequip");
		
		RefreshAllEquipments();
	}

	private void EquipItem(int itemID, int playerID, EquipmentPosition pos) {
		ItemSO equippingItem = itemContainer.itemList[itemID];

		if(inventory.playerInventory.Contains(equippingItem)) { 
			inventory.playerInventory.Remove(equippingItem);
			
			ItemSO equippedItem = equipmentContainer.SetItemInEquipment(playerID, pos, equippingItem);
			
			if(equippedItem) { 
				Debug.Log("Item position was already occupied. Putting item back to player inventory. ");
				inventory.playerInventory.Add(equippedItem);
			}

			Debug.Log("Equip");
			
			RefreshAllEquipments();
		}
		else
			Debug.LogError("Item was not in possession (or at least not in the player inventory)! ");
	}

	private void RefreshAllEquipments() {
		//todo use event channel
		//todo dont use characters + controller
		CharacterList characterList = FindObjectOfType<CharacterList>();
		if ( characterList ) {
			foreach ( GameObject player in characterList.playerContainer) {
				player.GetComponent<EquipmentController>().RefreshEquipment();
			}
		}
	}
}