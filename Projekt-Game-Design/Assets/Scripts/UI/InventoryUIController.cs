using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Events.ScriptableObjects;
using Player;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour {
    public List<InventorySlot> inventoryItems = new List<InventorySlot>();
    public List<InventorySlot> equipmentInventoryItems = new List<InventorySlot>();

    public ItemContainerSO itemContainer;

    private static ItemContainerSO _staticItemContainer;

    private VisualElement _inventorySlotContainer;

    [SerializeField] private int inventoryItemQuantity = 28;

    // Für das Inventar
    private VisualElement _inventoryContainer;

    // Für das EquipmentInventar
    private VisualElement _equipmentInventoryContainer;

    // Fuer den PlayerView Container
    private VisualElement _playerViewContainer;

    // Für das Ghost Icon
    private static VisualElement _ghostIcon;


    // Zum Draggen der Icons
    private static bool _isDragging;
    private static InventorySlot _originalSlot;

    // Der aktuell ausgewählte Spieler im Inventar
    private Statistics _selectedPlayerStatistics;

    private static int _currentPlayerSelected = 0;
    [SerializeField] private CharacterList characterList;
    
    [Header("Receiving Events On")] 
    [SerializeField] private BoolEventChannelSO visibilityMenuEventChannel;
    [SerializeField] private BoolEventChannelSO visibilityInventoryEventChannel;
    [SerializeField] private IntEventChannelSO onItemPickupEventChannel;
    [SerializeField] private IntEventChannelSO onItemDropEventChannel;
    [SerializeField] private IntListEventChannelSO inventoryChanged_EC;
    [SerializeField] private IntListEventChannelSO equipmentChanged_EC;
    // Selected events
    [SerializeField] private GameObjEventChannelSO playerDeselectedEC;
    [SerializeField] private GameObjActionEventChannelSO playerSelectedEC;

    [Header("Sending and Receiving Events On")] 
    [SerializeField] private BoolEventChannelSO visibilityGameOverlayEventChannel;

    [Header("Sending Events On")]
    // Inputchannel für das Inventar
    [SerializeField] private VoidEventChannelSO enableInventoryInput;
    [SerializeField] private InventoryTabEventChannelSO changeInventoryTab;

    // OutputChannel zwischen den Inventaren
    [SerializeField] private IntIntToEquipmentEventChannelSO toEquipmentEventChannel;
    [SerializeField] private IntIntToInventoryEventChannelSO toInventoryEventChannel;

    //Für das Inventar
    public enum InventoryTab {
        None,
        Items,
        Armory,
        Weapons
    }

    private void Awake() {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        //Store the root from the UI Document component
        root = GetComponent<UIDocument>().rootVisualElement;
        _inventoryContainer = root.Q<VisualElement>("InventoryOverlay");
        _equipmentInventoryContainer = root.Q<VisualElement>("PlayerEquipmentInventory");
        _ghostIcon = root.Query<VisualElement>("GhostIcon");
        _playerViewContainer = _inventoryContainer.Q<VisualElement>("PlayerViewContainer");

        // Lifehack
        _staticItemContainer = itemContainer;

        //Search the root for the SlotContainer Visual Element
        _inventorySlotContainer = root.Q<VisualElement>("InventoryContent");
        visibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        visibilityInventoryEventChannel.OnEventRaised += HandleInventoryOverlay;
        visibilityGameOverlayEventChannel.OnEventRaised += HandleOtherScreensOpened;
        onItemPickupEventChannel.OnEventRaised += HandleItemPickup;
        onItemDropEventChannel.OnEventRaised += HandleItemDrop;
        inventoryChanged_EC.OnEventRaised += HandleTabChanged;
        equipmentChanged_EC.OnEventRaised += AddEquipmentItems;

        // Callbacks fürs draggen
        _ghostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        _ghostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }
    
    private void Start() {
        InitializeInventory();
        InitializeEquipmentInventory();

        _inventoryContainer.Q<Button>("Tab1").clicked += HandleItemTabPressed;
        _inventoryContainer.Q<Button>("Tab2").clicked += HandleArmoryTabPressed;
        _inventoryContainer.Q<Button>("Tab3").clicked += HandleWeaponsTabPressed;

        // initially select first player
        if (characterList == null) {
            characterList = FindObjectOfType<CharacterList>();    
        }

        // BUG adakdlkah?
        if (characterList.playerContainer?.Count > 0) {
            _selectedPlayerStatistics = characterList.playerContainer[0]?.GetComponent<Statistics>();    
        }
        else {
            _selectedPlayerStatistics = null;
        }

        playerDeselectedEC.OnEventRaised += HandlePlayerDeselected;
        playerSelectedEC.OnEventRaised += HandlePlayerSelected;
    }

    private void InitializeInventory() {
        //Create InventorySlots and add them as children to the SlotContainer
        for (int i = 0; i < inventoryItemQuantity; i++) {
            InventorySlot item =
                new InventorySlot(InventorySlot.InventorySlotType.NormalInventory);

            inventoryItems.Add(item);

            _inventorySlotContainer.Add(item);
        }
    }

    private void InitializeEquipmentInventory() {
        // Hinzufügen der Item Slots für das Equipment Menü
        // Muss nicht dynamisch generiert werden, wird nur der Liste hinzugefügt
        equipmentInventoryItems.Add(
            _equipmentInventoryContainer.Q<InventorySlot>("HelmetEquipment1"));
        equipmentInventoryItems.Add(_equipmentInventoryContainer.Q<InventorySlot>("HeadEquipment1"));
        equipmentInventoryItems.Add(_equipmentInventoryContainer.Q<InventorySlot>("BodyEquipment1"));
        equipmentInventoryItems.Add(_equipmentInventoryContainer.Q<InventorySlot>("BodyEquipment2"));
        equipmentInventoryItems.Add(_equipmentInventoryContainer.Q<InventorySlot>("BodyEquipment3"));
        equipmentInventoryItems.Add(_equipmentInventoryContainer.Q<InventorySlot>("LegEquipment1"));
        equipmentInventoryItems.Add(_equipmentInventoryContainer.Q<InventorySlot>("FeetEquipment1"));
    }

    void ResetAllTabs() {
        _inventoryContainer.Q<Button>("Tab1").RemoveFromClassList("ClickedTab");
        _inventoryContainer.Q<Button>("Tab2").RemoveFromClassList("ClickedTab");
        _inventoryContainer.Q<Button>("Tab3").RemoveFromClassList("ClickedTab");
        _inventoryContainer.Q<Button>("Tab4").RemoveFromClassList("ClickedTab");
        _inventoryContainer.Q<Button>("Tab5").RemoveFromClassList("ClickedTab");


        _inventoryContainer.Q<Button>("Tab1").AddToClassList("UnclickedTab");
        _inventoryContainer.Q<Button>("Tab2").AddToClassList("UnclickedTab");
        _inventoryContainer.Q<Button>("Tab3").AddToClassList("UnclickedTab");
        _inventoryContainer.Q<Button>("Tab4").AddToClassList("UnclickedTab");
        _inventoryContainer.Q<Button>("Tab5").AddToClassList("UnclickedTab");
    }

    void InventoryManager(InventoryTab tab, Button button) {
        ResetAllTabs();
        button.RemoveFromClassList("UnclickedTab");
        button.AddToClassList("ClickedTab");

        switch (tab) {
            case InventoryTab.Items:
                changeInventoryTab.RaiseEvent(InventoryTab.Items);
                break;
            case InventoryTab.Armory:
                changeInventoryTab.RaiseEvent(InventoryTab.Armory);
                break;
            case InventoryTab.Weapons:
                changeInventoryTab.RaiseEvent(InventoryTab.Weapons);
                break;
        }
    }

    // Sets to selected player or first in container if none selected
    // TODO: copied from OverlayUIController
    //
    private void RefreshPlayerViewContainer() {
        //VisualElement manaBar = PlayerViewContainer.Q<VisualElement>("ProgressBarManaOverlay");
        VisualElement healthBar = _playerViewContainer.Q<VisualElement>("ProgressBarHealthOverlay");
        VisualElement abilityBar =
            _playerViewContainer.Q<VisualElement>("ProgressBarAbilityOverlay");

        StatusValues playerStats = _selectedPlayerStatistics.StatusValues;

        //todo ui refactoring 
        healthBar.style.width =
            new StyleLength(Length.Percent((100 * (float)playerStats.HitPoints.value /
                                            playerStats.HitPoints.max)));
        abilityBar.style.width =
            new StyleLength(Length.Percent((100 * (float)playerStats.Energy.value /
                                            playerStats.Energy.max)));
        
        //manaBar.style.width = new StyleLength(Length.Percent((100* (float)playerSC.EnergyPoints/playerStats.maxEnergy)));

        // Labels für Stats
        _playerViewContainer.Q<Label>("StrengthLabel").text = playerStats.Strength.value.ToString();
        _playerViewContainer.Q<Label>("DexterityLabel").text = playerStats.Dexterity.value.ToString();
        _playerViewContainer.Q<Label>("IntelligenceLabel").text =
            playerStats.Intelligence.value.ToString();
        _playerViewContainer.Q<Label>("MovementLabel").text = playerStats.ViewDistance.value.ToString();

        // Image
        VisualElement image = _playerViewContainer.Q<VisualElement>("PlayerPicture");
        image.Clear();
        Image newProfile = new Image();
        image.Add(newProfile);
        newProfile.image = _selectedPlayerStatistics.DisplayImage.texture;

        // Name and Level
        _playerViewContainer.Q<Label>("PlayerName").text = _selectedPlayerStatistics.DisplayName;
        _playerViewContainer.Q<Label>("LevelLabel").text = playerStats.Level.value.ToString();
    }

    // refresh menu and select
    private void HandlePlayerSelected(GameObject player, Action<int> callback) {
        _selectedPlayerStatistics = player.GetComponent<Statistics>();
        RefreshPlayerViewContainer();
    }

    // refresh menu and select first in container
    private void HandlePlayerDeselected(GameObject player) {
        _selectedPlayerStatistics = characterList.playerContainer[0].GetComponent<Statistics>();
        RefreshPlayerViewContainer();
    }

    void HandleItemTabPressed() {
        InventoryManager(InventoryTab.Items, _inventoryContainer.Q<Button>("Tab1"));
    }

    void HandleArmoryTabPressed() {
        InventoryManager(InventoryTab.Armory, _inventoryContainer.Q<Button>("Tab2"));
    }

    void HandleWeaponsTabPressed() {
        InventoryManager(InventoryTab.Weapons, _inventoryContainer.Q<Button>("Tab3"));
    }

    void HandleInventoryOverlay(bool value) {
        if (value) {
            enableInventoryInput.RaiseEvent();
            _inventoryContainer.style.display = DisplayStyle.Flex;
        }
        else {
            _inventoryContainer.style.display = DisplayStyle.None;
        }
    }

    void HandleOtherScreensOpened(bool value) {
        HandleInventoryOverlay(false);
    }

    private void HandleItemPickup(int itemGuid) {
        AddItemToInventoryOverlay(itemGuid);
    }

    private void HandleItemDrop(int itemGuid) {
        RemoveItemFromInventoryOverlay(itemGuid);
    }

    private void HandleTabChanged(List<int> itemGuids) {
        AddItemListToInventoryOverlay(itemGuids);
    }

    private void AddItemToInventoryOverlay(int itemGuid) {
        // Debug.Log("TestAddItemToInventoryOverlay");
        var emptySlot = inventoryItems.FirstOrDefault(x => x.itemGuid.Equals(-1));

        if (emptySlot != null) {
            emptySlot.HoldItem(itemContainer.itemList[itemGuid]);
        }
    }

    private void RemoveItemFromInventoryOverlay(int itemGuid) {
        var emptySlot = inventoryItems.FirstOrDefault(x => x.itemGuid.Equals(itemGuid));

        if (emptySlot != null) {
            emptySlot.DropItem();
        }
    }

    private void CleanAllItemSlots() {
        foreach (var itemSlot in inventoryItems) {
            if (itemSlot != null && itemSlot.itemGuid != -1) {
                itemSlot.DropItem();
            }
        }
    }


    private void AddItemListToInventoryOverlay(List<int> list) {
        CleanAllItemSlots();
        foreach (int itemId in list) {
            AddItemToInventoryOverlay(itemId);
        }
    }

    public static void StartDrag(Vector2 position, InventorySlot originalSlot) {
        //Set tracking variables
        _isDragging = true;
        _originalSlot = originalSlot;
        //Set the new position
        _ghostIcon.style.top = position.y - _ghostIcon.layout.height / 2;
        _ghostIcon.style.left = position.x - _ghostIcon.layout.width / 2;
        //Set the image
        _ghostIcon.style.backgroundImage =
            _staticItemContainer.itemList[originalSlot.itemGuid].icon.texture;
        //Flip the visibility on
        _ghostIcon.style.visibility = Visibility.Visible;
    }

    private void OnPointerMove(PointerMoveEvent evt) {
        //Only take action if the player is dragging an item around the screen
        if (!_isDragging) {
            return;
        }

        //Set the new position
        _ghostIcon.style.top = evt.position.y - _ghostIcon.layout.height / 2;
        _ghostIcon.style.left = evt.position.x - _ghostIcon.layout.width / 2;
    }

    private void OnPointerUp(PointerUpEvent evt) {
        if (!_isDragging) {
            return;
        }

        //Check to see if they are dropping the ghost icon over any inventory slots.
        IEnumerable<InventorySlot> slotsInventory = inventoryItems.Where(x =>
            x.worldBound.Overlaps(_ghostIcon.worldBound));

        IEnumerable<InventorySlot> slotsEquipment = equipmentInventoryItems.Where(x =>
            x.worldBound.Overlaps(_ghostIcon.worldBound));
        //TODO could be made better, make it abstract
        //Found at least one in Normal Inventory
        var inventorySlots = slotsInventory as InventorySlot[] ?? slotsInventory.ToArray();
        if (inventorySlots.Count() != 0) {
            InventorySlot closestSlot = inventorySlots.OrderBy(x => Vector2.Distance
                (x.worldBound.position, _ghostIcon.worldBound.position)).First();

            //Set the new inventory slot with the data
            // TODO Wenn Slot bereits belegt, dann tausche closestSlot mit OriginalSlot
            closestSlot.HoldItem(itemContainer.itemList[_originalSlot.itemGuid]);

            // Zum Verschieben von einem Gegenstand zum normalen Inventory
            if (_originalSlot.slotType == InventorySlot.InventorySlotType.EquipmentInventory) {
                toInventoryEventChannel.RaiseEvent(_originalSlot.itemGuid, _currentPlayerSelected);
                closestSlot.itemGuid = _originalSlot.itemGuid;
            }

            if (!closestSlot.Equals(_originalSlot)) {
                //Clear the original slot
                _originalSlot.DropItem();
            }
        }
        // Found at least one in Equipment Inventory
        else {
	        var equipmentSlotsList = slotsEquipment.ToList();
	        if (equipmentSlotsList.Count() != 0) {
		        InventorySlot closestSlot = equipmentSlotsList.OrderBy(x => Vector2.Distance
			        (x.worldBound.position, _ghostIcon.worldBound.position)).First();

		        //Set the new inventory slot with the data
		        // TODO Wenn Slot bereits belegt, dann tausche closestSlot mit OriginalSlot
		        closestSlot.HoldItem(itemContainer.itemList[_originalSlot.itemGuid]);

		        // Zum Verschieben von einem Gegenstand zum Equipment Inventory
		        if (_originalSlot.slotType == InventorySlot.InventorySlotType.NormalInventory) {
			        toEquipmentEventChannel.RaiseEvent(_originalSlot.itemGuid, _currentPlayerSelected);
			        closestSlot.itemGuid = _originalSlot.itemGuid;
		        }

		        if (!closestSlot.Equals(_originalSlot)) {
			        //Clear the original slot
			        _originalSlot.DropItem();
		        }
	        }
	        //Didn't find any (dragged off the window)
	        else {
		        _originalSlot.icon.image = itemContainer.itemList[_originalSlot.itemGuid].icon.texture;
	        }
        }

        //Clear dragging related visuals and data
        _isDragging = false;
        _originalSlot = null;
        _ghostIcon.style.visibility = Visibility.Hidden;
    }

    private void CleanAllItemEquipmentSlots() {
        foreach (var itemSlot in equipmentInventoryItems) {
            if (itemSlot != null && itemSlot.itemGuid != -1) {
                itemSlot.DropItem();
            }
        }
    }

    private void AddItemToEquipmentInventoryOverlay(int itemId) {
        //TODO Seperation von den unterschiedlichen Equipment Sachen

        var emptySlot = equipmentInventoryItems.FirstOrDefault(x => x.itemGuid.Equals(-1));

        if (emptySlot != null) {
            emptySlot.HoldItem(itemContainer.itemList[itemId]);
        }
    }

    private void AddEquipmentItems(List<int> list) {
        CleanAllItemEquipmentSlots();
        foreach (int itemId in list) {
            AddItemToEquipmentInventoryOverlay(itemId);
        }
    }
}
