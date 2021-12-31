using System;
using System.Collections.Generic;
using Characters.Equipment;
using Events.ScriptableObjects;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
	[SerializeField] private ItemContainerSO itemContainer;
	[SerializeField] private InventorySO inventory;

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

	private void UnequipItem(int playerId, EquipmentPosition pos) {
		// remove item from equipment inventory
		ItemSO former = inventory.SetItemInEquipment(playerId, pos, null);
		// add item to player inventory
		if(former)
			inventory.playerInventory.Add(former);

		RefreshAllEquipments();
	}

	private void EquipItem(int itemID, int playerID, EquipmentPosition pos) {
		ItemSO equippingItem = itemContainer.itemList[itemID];

		if(inventory.playerInventory.Contains(equippingItem)) { 
			inventory.playerInventory.Remove(equippingItem);
			
			ItemSO equippedItem = inventory.SetItemInEquipment(playerID, pos, equippingItem);
			
			if(equippedItem) { 
				Debug.Log("Item position was already occupied. Putting item back to player inventory. ");
				inventory.playerInventory.Add(equippedItem);
			}

			RefreshAllEquipments();
		}
		else
			Debug.LogError("Item was not in possession (or at least not in the player inventory)! ");
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