using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySO playerInventory;

    [Header("Sending Events On")]
    [SerializeField] private IntEventChannelSO OnItemPickupEventChannel;
    [SerializeField] private IntEventChannelSO OnItemDropEventChannel;

    private void Awake()
    {
        
        //TODO einfügen von Item Event Channels
    }

    private void Start()
    {
        initalizeInventoryOverlay();
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

    private void initalizeInventoryOverlay()
    {
        foreach (var item in playerInventory.Inventory)
        {
            Debug.Log("Führe Initialisierung für Item Nummer: " + item.id);
            OnItemPickupEventChannel.RaiseEvent(item.id);
        }
    }
    
    
}
