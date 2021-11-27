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
	*/

	[Header("Receiving Events On")]
	[SerializeField] private IntIntEquipmentPositionEquipEventChannelSO equipEventChannel;
	[SerializeField] private IntEquipmentPositionUnequipEventChannelSO unequipEventChannel;

	private void Awake() {
		equipEventChannel.OnEventRaised += EquipItem;
		unequipEventChannel.OnEventRaised += UnequipItem;
	}

	private void OnDestroy() {
		equipEventChannel.OnEventRaised -= EquipItem;
		unequipEventChannel.OnEventRaised -= UnequipItem;
	}

	private void Start() {
		InitializeEquipmentInventory();
	}
		
	// itemID is item in ItemContainer
	private void AddItemToPlayerInventory(int itemID) {
		playerInventory.itemIDs.Add(itemID);
		playerInventory.equipmentID.Add(-1);
	}
		
	// itemID is id within inventory
	private void RemoveItemFromPlayerInventory(int itemID) {
		playerInventory.equipmentID.RemoveAt(itemID);
		playerInventory.itemIDs.RemoveAt(itemID);
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

	private void InitializeEquipmentInventory() {
		CharacterList characterList = FindObjectOfType<CharacterList>();
		if ( characterList ) {
				equipmentContainer.equipmentInventories = new List<Equipment>();
				foreach ( GameObject player in characterList.playerContainer)
				{
						equipmentContainer.equipmentInventories.Add(new Equipment());
				}
		}
	}
}