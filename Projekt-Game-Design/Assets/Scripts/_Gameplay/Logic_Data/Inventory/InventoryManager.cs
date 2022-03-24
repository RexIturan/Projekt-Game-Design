using Characters.Equipment.ScriptableObjects;
using Events.ScriptableObjects;
using GDP01._Gameplay.Logic_Data.Inventory.EventChannels;
using GDP01._Gameplay.Logic_Data.Inventory.Types;
using GDP01.Characters.Component;
using UnityEngine;
using static GDP01._Gameplay.Logic_Data.Inventory.Types.InventoryTarget;

public class InventoryManager : MonoBehaviour {
	[SerializeField] private ItemTypeContainerSO itemTypeContainer;
	[SerializeField] private InventorySO inventory;
	[SerializeField] private EquipmentContainerSO equipmentContainer;

	[Header("Sending Events On")]
	/*
	[SerializeField] private IntEventChannelSO onItemPickupEventChannel;
	[SerializeField] private IntEventChannelSO onItemDropEventChannel;
	*/
	[Header("Receiving Events On")]
	[SerializeField] private MoveItemEventChannel moveItemEC;
	[SerializeField] private EquipItemEC_SO equipItemEC;
	[SerializeField] private UnequipItemEC_SO unequipItemEC;
	[SerializeField] private IntEventChannelSO pickupEC;

	private void OnEnable() {
		// equipItemEC.OnEventRaised += EquipItem;
		// unequipItemEC.OnEventRaised += UnequipItem;
		pickupEC.OnEventRaised += Pickup;
		moveItemEC.OnEventRaised += MoveItem;
	}

	private void OnDisable() {
		// equipItemEC.OnEventRaised -= EquipItem;
		// unequipItemEC.OnEventRaised -= UnequipItem;
		pickupEC.OnEventRaised -= Pickup;
		moveItemEC.OnEventRaised += MoveItem;
	}

	private void Pickup(int itemID) {
		if(itemID >= 0)
			inventory.AddItem(itemTypeContainer.itemList[itemID]);
	}

	private void MoveItem(InventoryTarget fromTarget, int fromID, InventoryTarget ToTarget, int toID, int equipmentId) {
		switch ( fromTarget, ToTarget ) {
			//Swap In Inventory
			case (Inventory, Inventory) :
				SwapItemsInIventory(fromID, toID);
				break;
			
			//Equip
			case (Inventory, Equipment) :
				EquipItem(equipmentId, fromID, ( EquipmentPosition )toID);
				break;
			
			//Unequip
			case (Equipment, Inventory) :
				UnequipItem(equipmentId, ( EquipmentPosition )fromID, toID);
				break;
			
			//Swap Equipment
			case (Equipment, Equipment) :
				SwapEquipment(equipmentId, ( EquipmentPosition )fromID, ( EquipmentPosition )toID);
				break;
			
			//Inventory -> Trash
			case (Inventory, Trash) :
				//TODO Implement Trash
				break;
			
			//Equipment -> Trash
			case (Equipment, Trash) :
				//TODO Implement Trash
				break;
		}
	}

	private void SwapItemsInIventory(int fromID, int toID) {
		var fromItem = inventory.InventorySlots[fromID];
		var toItem = inventory.InventorySlots[toID];

		inventory.InventorySlots[fromID] = toItem;
		inventory.InventorySlots[toID] = fromItem;
	}
	
	private void SwapEquipment(int equipmentId, EquipmentPosition fromPos, EquipmentPosition toPos) {
		ItemTypeSO fromItemType = equipmentContainer.UnequipItemFor(equipmentId, fromPos);
		ItemTypeSO toItemType = equipmentContainer.UnequipItemFor(equipmentId, toPos);
		
		equipmentContainer.SetItemInEquipment(equipmentId, fromPos, toItemType);
		equipmentContainer.SetItemInEquipment(equipmentId, toPos, fromItemType);
	}
	
	private void UnequipItem(int equipmentId, EquipmentPosition pos, int toId) {
		// remove item from equipment inventory
		ItemTypeSO fromItemType = equipmentContainer.UnequipItemFor(equipmentId, pos);
		
		inventory.AddItemAt(toId, fromItemType);

		Debug.Log($"Unequip Item: {fromItemType} at {pos}");
		
		RefreshAllEquipments();
	}

	private void EquipItem( int equipmentId, int inventorySlot, EquipmentPosition pos) {
		var item = inventory.RemoveItemAt(inventorySlot);

		ItemTypeSO equippedItemType = equipmentContainer.SetItemInEquipment(equipmentId, pos, item);
			
		if(equippedItemType is {}) { 
			Debug.LogError("Couldnt Equip Item");
			Debug.Log("Item position was already occupied. Putting item back to player inventory. ");
			// inventory.playerInventory.Add(equippedItem);
			inventory.AddItemAt(inventorySlot, item);
		}

		Debug.Log($"Equip Item: {item} At: {pos}");
			
		RefreshAllEquipments();
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