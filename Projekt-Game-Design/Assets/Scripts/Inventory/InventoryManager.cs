using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
	[SerializeField] private InventorySO playerInventory;

	[SerializeField] private EquipmentInventoryContainerSO equipmentInventory;

	[Header("Sending Events On")] [SerializeField]
	private IntEventChannelSO onItemPickupEventChannel;

	[SerializeField] private IntEventChannelSO onItemDropEventChannel;
	[SerializeField] private IntListEventChannelSO inventoryChanged_EC;
	[SerializeField] private IntListEventChannelSO equipmentChanged_EC;

	[Header("Receiving Events On")] [SerializeField]
	private InventoryTabEventChannelSO inventoryTabChanged;

	[SerializeField] private IntIntToEquipmentEventChannelSO toEquipmentEventChannel;
	[SerializeField] private IntIntToInventoryEventChannelSO toInventoryEventChannel;

	private void Awake() {
		inventoryTabChanged.OnEventRaised += AddItemsToPlayerOverlay;
		toEquipmentEventChannel.OnEventRaised += HandleItemTransaktionToEquipment;
		toInventoryEventChannel.OnEventRaised += HandleItemTransactionToInventory;
	}

	private void OnDestroy() {
		inventoryTabChanged.OnEventRaised -= AddItemsToPlayerOverlay;
		toEquipmentEventChannel.OnEventRaised -= HandleItemTransaktionToEquipment;
		toInventoryEventChannel.OnEventRaised -= HandleItemTransactionToInventory;
	}

	private void Start() {
		ChangeOverlayByItemType(ItemType.Usable);
		InitializeEquipmentInventory();
	}

	#region Implement ME Inventory Add / Remove

	// TODO needs event channels that add, remove items from a inventory
	
	// todo Add item to Inventory(ItemSO item, InventorySO inventory)
	private void AddItemToPlayerInventory(ItemSO item) {
		playerInventory.inventory.Add(item);
		onItemPickupEventChannel.RaiseEvent(item.id);
	}

	// todo rename -> remove item from Player inventory
	private void DeleteItemInPlayerInventory(ItemSO item) {
		playerInventory.inventory.Remove(item);
		onItemDropEventChannel.RaiseEvent(item.id);
	}

	#endregion

	private void AddItemsToPlayerOverlay(InventoryUIController.InventoryTab tab) {
		switch ( tab ) {
			case InventoryUIController.InventoryTab.None:
			case InventoryUIController.InventoryTab.Items:
				ChangeOverlayByItemType(ItemType.Usable);
				break;
			case InventoryUIController.InventoryTab.Armory:
				ChangeOverlayByItemType(ItemType.Armor);
				break;
			case InventoryUIController.InventoryTab.Weapons:
				ChangeOverlayByItemType(ItemType.Weapon);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(tab), tab, "AddItemsToPlayerOverlay");
		}
	}

	private void HandleItemTransactionToInventory(int itemId, int playerId) {
		ItemSO item = equipmentInventory.inventories[playerId].inventory
			.Find(x => x.id.Equals(itemId));
		equipmentInventory.inventories[playerId].inventory.Remove(item);
		playerInventory.inventory.Add(item);
	}

	private void HandleItemTransaktionToEquipment(int itemId, int playerId) {
		ItemSO item = playerInventory.inventory.Find(x => x.id.Equals(itemId));
		playerInventory.inventory.Remove(item);
		equipmentInventory.inventories[playerId].inventory.Add(item);
	}

	private void ChangeOverlayByItemType(ItemType itemType) {
		List<int> list = new List<int>();
		foreach ( var item in playerInventory.inventory.FindAll(x => ( x.type & itemType ) != 0) ) {
			list.Add(item.id);
			//Debug.Log("Führe Initialisierung für Item Nummer: " + item.id);
			//OnItemPickupEventChannel.RaiseEvent(item.id);
		}

		inventoryChanged_EC.RaiseEvent(list);
	}

	private void InitializeEquipmentInventory() {
		List<int> list = new List<int>();
		//TODO Für alle Spieler einbauen
		foreach ( var item in equipmentInventory.inventories[0].inventory ) {
			list.Add(item.id);
			//Debug.Log("Führe Initialisierung für Item Nummer: " + item.id);
			//OnItemPickupEventChannel.RaiseEvent(item.id);
		}

		equipmentChanged_EC.RaiseEvent(list);
	}
}