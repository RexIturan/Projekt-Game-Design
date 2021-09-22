using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Events.ScriptableObjects;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO playerInventory;

    [Header("Sending Events On")]
    [SerializeField] private IntEventChannelSO OnItemPickupEventChannel;
    [SerializeField] private IntEventChannelSO OnItemDropEventChannel;
    [SerializeField] private IntListEventChannelSO ChangeInventoryListEventChannel;

    [Header("Receiving Events On")] [SerializeField]
    private InventoryTabEventChannelSO inventoryTabChanged;

    private void Awake()
    {
        
        //TODO einfügen von Item Event Channels
        inventoryTabChanged.OnEventRaised += AddItemsToPlayerOverlay;
    }

    private void Start()
    {
        changeOverlayByItemType(EItemType.USABLE);
    }

    private void AddItemToPlayerInventory(ItemSO item)
    {
        playerInventory.Inventory.Add(item);
        OnItemPickupEventChannel.RaiseEvent(item.id);
    }
    
    private void DeleteItemInPlayerInventory(ItemSO item)
    {
        playerInventory.Inventory.Remove(item);
        OnItemDropEventChannel.RaiseEvent(item.id);
    }

    private void AddItemsToPlayerOverlay(InventoryUIController.inventoryTab tab)
    {
        switch (tab)
        {
            case InventoryUIController.inventoryTab.NONE:
            case InventoryUIController.inventoryTab.ITEMS:
                changeOverlayByItemType(EItemType.USABLE);
                break;
            case InventoryUIController.inventoryTab.ARMORY:
                changeOverlayByItemType(EItemType.ARMOR);
                break;
            case InventoryUIController.inventoryTab.WEAPONS:
                changeOverlayByItemType(EItemType.WEAPON);
                break;
            
        }
    }

    private void changeOverlayByItemType(EItemType itemType)
    {
        List<int> list = new List<int>();
        foreach (var item in playerInventory.Inventory.FindAll(x => (x.type & itemType) != 0))
        {
            list.Add(item.id);
            //Debug.Log("Führe Initialisierung für Item Nummer: " + item.id);
            //OnItemPickupEventChannel.RaiseEvent(item.id);
        }

        ChangeInventoryListEventChannel.RaiseEvent(list);
    }
    
    
}
