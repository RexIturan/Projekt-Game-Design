using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
	[SerializeField] private ItemContainerSO itemContainer;
	[SerializeField] private InventorySO playerInventory;
	[SerializeField] private EquipmentContainerSO equipmentContainer;

	[Header("Sending Events On")]
	/*
	[SerializeField] private IntEventChannelSO onItemPickupEventChannel;
	[SerializeField] private IntEventChannelSO onItemDropEventChannel;
	[SerializeField] private IntListEventChannelSO inventoryChanged_EC;
	[SerializeField] private IntListEventChannelSO equipmentChanged_EC;
	*/

	[Header("Receiving Events On")]
	// [SerializeField] private InventoryTabEventChannelSO inventoryTabChanged;
	[SerializeField] private IntIntEquipmentPositionEquipEventChannelSO equipEventChannel;
	[SerializeField] private IntEquipmentPositionUnequipEventChannelSO unequipEventChannel;

	private void Awake() {
		// inventoryTabChanged.OnEventRaised += AddItemsToPlayerOverlay;

		equipEventChannel.OnEventRaised += EquipItem;
		unequipEventChannel.OnEventRaised += UnequipItem;
	}

	private void OnDestroy() {
		// inventoryTabChanged.OnEventRaised -= AddItemsToPlayerOverlay;
		equipEventChannel.OnEventRaised -= EquipItem;
		unequipEventChannel.OnEventRaised -= UnequipItem;
	}

	private void Start() {
		ChangeOverlayByItemType(ItemType.Usable);
		InitializeEquipmentInventory();
	}

	#region Implement ME Inventory Add / Remove

	// TODO needs event channels that add, remove items from a inventory
	
	// todo Add item to Inventory(ItemSO item, InventorySO inventory)
	// itemID is item in ItemContainer
	private void AddItemToPlayerInventory(int itemID) {
		playerInventory.itemIDs.Add(itemID);
		playerInventory.equipmentID.Add(-1);
		// onItemPickupEventChannel.RaiseEvent(itemID);
	}

	// todo rename -> remove item from Player inventory
	// itemID is id within inventory
	private void DeleteItemInPlayerInventory(int itemID) {
		playerInventory.equipmentID.RemoveAt(itemID);
		playerInventory.itemIDs.RemoveAt(itemID);
		// onItemDropEventChannel.RaiseEvent(itemID);
	}

	#endregion

	private void AddItemsToPlayerOverlay(InventoryUIController.InventoryTab tab) {
			/*
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
		*/
	}

	private void UnequipItem(int playerId, EquipmentPosition pos) {
				// set the player id of the once equipped item in the inventory to -1
				playerInventory.equipmentID[equipmentContainer.equipmentInventories[playerId].items[(int) pos]] = -1;
				// unequip item
				equipmentContainer.equipmentInventories[playerId].items[(int) pos] = -1;
	}

	private void EquipItem(int inventoryItemID, int playerID, EquipmentPosition pos) {
		playerInventory.equipmentID[inventoryItemID] = playerID;
		equipmentContainer.equipmentInventories[playerID].items[(int) pos] = inventoryItemID;
	}

	private void ChangeOverlayByItemType(ItemType itemType) {
				/*
		List<int> list = new List<int>();
				// TODO: work this out
				//foreach ( var item in playerInventory.FindAll(x => ( x.type & itemType ) != 0) ) {
			//list.Add(item.id);
			//Debug.Log("Führe Initialisierung für Item Nummer: " + item.id);
			//OnItemPickupEventChannel.RaiseEvent(item.id);
		//}

		inventoryChanged_EC.RaiseEvent(list);
		*/
	}

		private void InitializeEquipmentInventory() {
				CharacterList characterList = FindObjectOfType<CharacterList>();
				if ( characterList ) {
						equipmentContainer.equipmentInventories = new List<Equipment>();
						foreach ( GameObject player in characterList.playerContainer)
						{
								equipmentContainer.equipmentInventories.Add(new Equipment());
						}
				}
		/*
		List<int> list = new List<int>();
		//TODO Für alle Spieler einbauen
		//foreach ( var item in equipmentInventory.inventories[0].inventory ) {
			//list.Add(item.id);
			//Debug.Log("Führe Initialisierung für Item Nummer: " + item.id);
			//OnItemPickupEventChannel.RaiseEvent(item.id);
		//}

		equipmentChanged_EC.RaiseEvent(list);
		*/
	}
}