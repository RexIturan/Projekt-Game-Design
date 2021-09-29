using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    public List<InventorySlot> InventoryItems = new List<InventorySlot>();
    public List<InventorySlot> EquipmentInventoryItems = new List<InventorySlot>();

    public ItemListSO ItemList;

    private VisualElement InventorySlotContainer;
    [SerializeField] private int EquipmentInventoryItemQuantity = 1;
    [SerializeField] private int InventoryItemQuantity = 28;

    // Für das Inventar
    private VisualElement inventoryContainer;
    // Für das EquipmentInventar
    private VisualElement EquipmentInventoryContainer;

    [Header("Receiving Events On")] [SerializeField]
    private BoolEventChannelSO VisibilityMenuEventChannel;

    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;
    [SerializeField] private IntEventChannelSO OnItemPickupEventChannel;
    [SerializeField] private IntEventChannelSO OnItemDropEventChannel;
    [SerializeField] private IntListEventChannelSO ChangeInventoryListEventChannel;

    [Header("Sending and Receiving Events On")] [SerializeField]
    private BoolEventChannelSO VisibilityGameOverlayEventChannel;

    [Header("Sending Events On")]
    // Inputchannel für das Inventar
    [SerializeField]
    private VoidEventChannelSO enableInventoryInput;
    [SerializeField]
    private InventoryTabEventChannelSO changeInventoryTab;

    //Für das Inventar
    public enum inventoryTab
    {
        NONE,
        ITEMS,
        ARMORY,
        WEAPONS
    }

    private void Start()
    {

        InitializeInventory();
        //InitializeEquipmentInventory();

        inventoryContainer.Q<Button>("Tab1").clicked += HandleItemTabPressed;
        inventoryContainer.Q<Button>("Tab2").clicked += HandleArmoryTabPressed;
        inventoryContainer.Q<Button>("Tab3").clicked += HandleWeaponsTabPressed;
    }

    private void Awake()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        //Store the root from the UI Document component
        root = GetComponent<UIDocument>().rootVisualElement;
        inventoryContainer = root.Q<VisualElement>("InventoryOverlay");
        EquipmentInventoryContainer = root.Q<VisualElement>("PlayerEquipmentInventory");

        //Search the root for the SlotContainer Visual Element
        InventorySlotContainer = root.Q<VisualElement>("InventoryContent");
        VisibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityInventoryEventChannel.OnEventRaised += HandleInventoryOverlay;
        VisibilityGameOverlayEventChannel.OnEventRaised += HandleOtherScreensOpened;
        OnItemPickupEventChannel.OnEventRaised += HandleItemPickup;
        OnItemDropEventChannel.OnEventRaised += HandleItemDrop;
        ChangeInventoryListEventChannel.OnEventRaised += HandleTabChanged;
    }

    private void InitializeEquipmentInventory()
    {
        for (int i = 0; i < EquipmentInventoryItemQuantity; i++)
        {
            InventorySlot item = new InventorySlot();

            EquipmentInventoryItems.Add(item);
            EquipmentInventoryContainer.Add(item);
        }
    }
    
    private void InitializeInventory()
    {
        //Create InventorySlots and add them as children to the SlotContainer
        for (int i = 0; i < InventoryItemQuantity; i++)
        {
            InventorySlot item = new InventorySlot();

            InventoryItems.Add(item);

            InventorySlotContainer.Add(item);
        }
    }

    void resetAllTabs()
    {
        inventoryContainer.Q<Button>("Tab1").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab2").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab3").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab4").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab5").RemoveFromClassList("ClickedTab");


        inventoryContainer.Q<Button>("Tab1").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab2").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab3").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab4").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab5").AddToClassList("UnclickedTab");
    }

    void InventoryManager(inventoryTab tab, Button button)
    {
        resetAllTabs();
        button.RemoveFromClassList("UnclickedTab");
        button.AddToClassList("ClickedTab");

        switch (tab)
        {
            case inventoryTab.ITEMS:
                changeInventoryTab.RaiseEvent(inventoryTab.ITEMS);
                break;
            case inventoryTab.ARMORY:
                changeInventoryTab.RaiseEvent(inventoryTab.ARMORY);
                break;
            case inventoryTab.WEAPONS:
                changeInventoryTab.RaiseEvent(inventoryTab.WEAPONS);
                break;
        }
    }


    void HandleItemTabPressed()
    {
        InventoryManager(inventoryTab.ITEMS, inventoryContainer.Q<Button>("Tab1"));
    }

    void HandleArmoryTabPressed()
    {
        InventoryManager(inventoryTab.ARMORY, inventoryContainer.Q<Button>("Tab2"));
    }

    void HandleWeaponsTabPressed()
    {
        InventoryManager(inventoryTab.WEAPONS, inventoryContainer.Q<Button>("Tab3"));
    }

    void HandleInventoryOverlay(bool value)
    {
        Debug.Log(value);

        if (value)
        {
            enableInventoryInput.RaiseEvent();
            inventoryContainer.style.display = DisplayStyle.Flex;
        }
        else
        {
            inventoryContainer.style.display = DisplayStyle.None;
        }
    }

    void HandleOtherScreensOpened(bool value)
    {
        HandleInventoryOverlay(false);
    }

    private void HandleItemPickup(int itemGuid)
    {
        AddItemToInventoryOverlay(itemGuid);
    }
    
    private void HandleItemDrop(int itemGuid)
    {
        RemoveItemFromInventoryOverlay(itemGuid);
    }
    
    private void HandleTabChanged(List<int> itemGuids)
    {
        AddItemListToInventoryOverlay(itemGuids);
    }

    private void AddItemToInventoryOverlay(int itemGuid)
    {
        Debug.Log("TestAddItemToInventoryOverlay");
        var emptySlot = InventoryItems.FirstOrDefault(x => x.ItemGuid.Equals(-1));

        if (emptySlot != null)
        {
            emptySlot.HoldItem(ItemList.ItemList[itemGuid]);
        }
    }
    
    private void RemoveItemFromInventoryOverlay(int itemGuid)
    {
        var emptySlot = InventoryItems.FirstOrDefault(x => x.ItemGuid.Equals(itemGuid));

        if (emptySlot != null)
        {
            emptySlot.DropItem();
        }
    }

    private void CleanAllItemSlots()
    {
        foreach (var itemSlot in InventoryItems)
        {
            if (itemSlot != null && itemSlot.ItemGuid != -1)
            {
                itemSlot.DropItem();
            }
        }
    }

    private void AddItemListToInventoryOverlay(List<int> list)
    {
        CleanAllItemSlots();
        foreach (int itemId in list)
        {
            AddItemToInventoryOverlay(itemId);
        }
    }
    
}