using System;
using System.Collections.Generic;
using Characters.Equipment;
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
		InitializePlayerInventory();
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

		RefreshAllEquipments();
	}

	private void EquipItem(int inventoryItemID, int playerID, EquipmentPosition pos) {
		playerInventory.equipmentID[inventoryItemID] = playerID;
		equipmentContainer.equipmentInventories[playerID].items[(int) pos] = inventoryItemID;
				
		RefreshAllEquipments();
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

  /**
	 * sets all items in the equipment inventory to -1 (unequiped)
	 * then sets all equipped items to their respective equipment inventories
	 */
	private void InitializePlayerInventory() {
		for(int i = 0; i < playerInventory.equipmentID.Count; i++)
			playerInventory.equipmentID[i] = -1;

		for(int player = 0; player < equipmentContainer.equipmentInventories.Count; player++) {
			foreach(int inventoryID in equipmentContainer.equipmentInventories[player].items) {
				if(inventoryID >= 0) {
					if(playerInventory.equipmentID[inventoryID] != -1)
						Debug.LogError("Equipment already equipped! Inventory ID: " + inventoryID);
					else
						playerInventory.equipmentID[inventoryID] = player;
				}
			}
		}
	}

	private void RefreshAllEquipments() {
		CharacterList characterList = FindObjectOfType<CharacterList>();
		if ( characterList ) {
			foreach ( GameObject player in characterList.playerContainer)
			{
				player.GetComponent<EquipmentController>().RefreshEquipment();
			}
		}
	}
}