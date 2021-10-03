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
    
    private static ItemListSO staticItemList;

    private VisualElement InventorySlotContainer;
    [SerializeField] private int InventoryItemQuantity = 28;
    // Für das Inventar
    private VisualElement inventoryContainer;
    // Für das EquipmentInventar
    private VisualElement EquipmentInventoryContainer;
    // Für das Ghost Icon
    private static VisualElement GhostIcon;
    
    
    // Zum Draggen der Icons
    private static bool IsDragging;
    private static InventorySlot OriginalSlot;
    
    // Der aktuell ausgewählte Spieler im Inventar
    private static int CurrentPlayerSelected = 0;

    [Header("Receiving Events On")] [SerializeField]
    private BoolEventChannelSO VisibilityMenuEventChannel;

    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;
    [SerializeField] private IntEventChannelSO OnItemPickupEventChannel;
    [SerializeField] private IntEventChannelSO OnItemDropEventChannel;
    [SerializeField] private IntListEventChannelSO ChangeInventoryListEventChannel;
    [SerializeField] private IntListEventChannelSO ChangeEquipmentListEventChannel;

    [Header("Sending and Receiving Events On")] [SerializeField]
    private BoolEventChannelSO VisibilityGameOverlayEventChannel;

    [Header("Sending Events On")]
    // Inputchannel für das Inventar
    [SerializeField]
    private VoidEventChannelSO enableInventoryInput;
    [SerializeField]
    private InventoryTabEventChannelSO changeInventoryTab;
    
    // OutputChannel zwischen den Inventaren
    [SerializeField]
    private IntIntToEquipmentEventChannelSO toEquipmentEventChannel;
    [SerializeField]
    private IntIntToInventoryEventChannelSO toInventoryEventChannel;

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
        InitializeEquipmentInventory();

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
        GhostIcon = root.Query<VisualElement>("GhostIcon");
        
        // Lifehack
        staticItemList = ItemList;

        //Search the root for the SlotContainer Visual Element
        InventorySlotContainer = root.Q<VisualElement>("InventoryContent");
        VisibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityInventoryEventChannel.OnEventRaised += HandleInventoryOverlay;
        VisibilityGameOverlayEventChannel.OnEventRaised += HandleOtherScreensOpened;
        OnItemPickupEventChannel.OnEventRaised += HandleItemPickup;
        OnItemDropEventChannel.OnEventRaised += HandleItemDrop;
        ChangeInventoryListEventChannel.OnEventRaised += HandleTabChanged;
        ChangeEquipmentListEventChannel.OnEventRaised += addEquipmentItems;
        
        // Callbacks fürs draggen
        GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }
    
    private void InitializeInventory()
    {
        //Create InventorySlots and add them as children to the SlotContainer
        for (int i = 0; i < InventoryItemQuantity; i++)
        {
            InventorySlot item = new InventorySlot(InventorySlot.inventorySlotType.NORMAL_INVENTORY);

            InventoryItems.Add(item);

            InventorySlotContainer.Add(item);
        }
    }
    
    private void InitializeEquipmentInventory()
    {
        // Hinzufügen der Item Slots für das Equipment Menü
        // Muss nicht dynamisch generiert werden, wird nur der Liste hinzugefügt
        EquipmentInventoryItems.Add(EquipmentInventoryContainer.Q<InventorySlot>("HelmetEquipment1"));
        EquipmentInventoryItems.Add(EquipmentInventoryContainer.Q<InventorySlot>("HeadEquipment1"));
        EquipmentInventoryItems.Add(EquipmentInventoryContainer.Q<InventorySlot>("BodyEquipment1"));
        EquipmentInventoryItems.Add(EquipmentInventoryContainer.Q<InventorySlot>("BodyEquipment2"));
        EquipmentInventoryItems.Add(EquipmentInventoryContainer.Q<InventorySlot>("BodyEquipment3"));
        EquipmentInventoryItems.Add(EquipmentInventoryContainer.Q<InventorySlot>("LegEquipment1"));
        EquipmentInventoryItems.Add(EquipmentInventoryContainer.Q<InventorySlot>("FeetEquipment1"));
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
        // Debug.Log("TestAddItemToInventoryOverlay");
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
    public static void StartDrag(Vector2 position, InventorySlot originalSlot)
    {
        //Set tracking variables
        IsDragging = true;
        OriginalSlot = originalSlot;
        //Set the new position
        GhostIcon.style.top = position.y - GhostIcon.layout.height / 2;
        GhostIcon.style.left = position.x - GhostIcon.layout.width / 2;
        //Set the image
        GhostIcon.style.backgroundImage = staticItemList.ItemList[originalSlot.ItemGuid].icon.texture;
        //Flip the visibility on
        GhostIcon.style.visibility = Visibility.Visible;
    }
    private void OnPointerMove(PointerMoveEvent evt)
    {
        //Only take action if the player is dragging an item around the screen
        if (!IsDragging)
        {
            return;
        }
        //Set the new position
        GhostIcon.style.top = evt.position.y - GhostIcon.layout.height / 2;
        GhostIcon.style.left = evt.position.x - GhostIcon.layout.width / 2;
    }
    private void OnPointerUp(PointerUpEvent evt)
    {
        if (!IsDragging)
        {
            return;
        }
        //Check to see if they are dropping the ghost icon over any inventory slots.
        IEnumerable<InventorySlot> slotsInventory = InventoryItems.Where(x => 
            x.worldBound.Overlaps(GhostIcon.worldBound));
        
        IEnumerable<InventorySlot> slotsEquipment = EquipmentInventoryItems.Where(x => 
            x.worldBound.Overlaps(GhostIcon.worldBound));
        //TODO could be made better, make it abstract
        //Found at least one in Normal Inventory
        if (slotsInventory.Count() != 0)
        {
            InventorySlot closestSlot = slotsInventory.OrderBy(x => Vector2.Distance
                (x.worldBound.position, GhostIcon.worldBound.position)).First();
            
            //Set the new inventory slot with the data
            // TODO Wenn Slot bereits belegt, dann tausche closestSlot mit OriginalSlot
            closestSlot.HoldItem(ItemList.ItemList[OriginalSlot.ItemGuid]);

            // Zum Verschieben von einem Gegenstand zum normalen Inventory
            if (OriginalSlot.SlotType == InventorySlot.inventorySlotType.EQUIPMENT_INVENTORY)
            {
                toInventoryEventChannel.RaiseEvent(OriginalSlot.ItemGuid, CurrentPlayerSelected);
                closestSlot.ItemGuid = OriginalSlot.ItemGuid;
            }
            
            if (!closestSlot.Equals(OriginalSlot))
            {
                //Clear the original slot
                OriginalSlot.DropItem();
            }
        }
        // Found atleast one in Equipment Inventory
        else if (slotsEquipment.Count() != 0)
        {
            InventorySlot closestSlot = slotsEquipment.OrderBy(x => Vector2.Distance
                (x.worldBound.position, GhostIcon.worldBound.position)).First();
        
            //Set the new inventory slot with the data
            // TODO Wenn Slot bereits belegt, dann tausche closestSlot mit OriginalSlot
            closestSlot.HoldItem(ItemList.ItemList[OriginalSlot.ItemGuid]);

            // Zum Verschieben von einem Gegenstand zum Equipment Inventory
            if (OriginalSlot.SlotType == InventorySlot.inventorySlotType.NORMAL_INVENTORY)
            {
                toEquipmentEventChannel.RaiseEvent(OriginalSlot.ItemGuid, CurrentPlayerSelected);
                closestSlot.ItemGuid = OriginalSlot.ItemGuid;
            }
            
            if (!closestSlot.Equals(OriginalSlot))
            {
                //Clear the original slot
                OriginalSlot.DropItem();
            }

        }
        //Didn't find any (dragged off the window)
        else
        {
            OriginalSlot.Icon.image = ItemList.ItemList[OriginalSlot.ItemGuid].icon.texture; 
        }
        //Clear dragging related visuals and data
        IsDragging = false;
        OriginalSlot = null;
        GhostIcon.style.visibility = Visibility.Hidden;
    }

    private void CleanAllItemEquipmentSlots()
    {
        foreach (var itemSlot in EquipmentInventoryItems)
        {
            if (itemSlot != null && itemSlot.ItemGuid != -1)
            {
                itemSlot.DropItem();
            }
        }
    }

    private void AddItemToEquipmentInventoryOverlay(int itemId)
    {
        //TODO Seperation von den unterschiedlichen Equipment Sachen
        
        var emptySlot = EquipmentInventoryItems.FirstOrDefault(x => x.ItemGuid.Equals(-1));

        if (emptySlot != null)
        {
            emptySlot.HoldItem(ItemList.ItemList[itemId]);
        }
    }
    
    private void addEquipmentItems(List<int> list)
    {
        CleanAllItemEquipmentSlots();
        foreach (int itemId in list)
        {
            AddItemToEquipmentInventoryOverlay(itemId);
        }
    }
}