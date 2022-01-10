using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Equipment;
using Events.ScriptableObjects;
using UI.Components.Character;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour {
	public List<InventorySlot> inventoryItems = new List<InventorySlot>();
	public List<InventorySlot> equipmentInventoryItems = new List<InventorySlot>();

	public ItemContainerSO itemContainer;
	[SerializeField] private InventorySO inventory;
	
	private CharacterList characterList;

	private VisualElement _inventorySlotContainer;

	[SerializeField] private int inventoryItemQuantity = 28;

	// Für das Inventar
	private VisualElement _inventoryContainer;

	// Für das EquipmentInventar
	private VisualElement _equipmentInventoryContainer;

	// for images of all players to switch between them
	private VisualElement _playerContainer;

	// Fuer den PlayerView Container
	private CharacterStatusValuePanel _characterStatusValuePanel;

	// Für das Ghost Icon
	private static VisualElement _ghostIcon;


	// Zum Draggen der Icons
	private static bool _isDragging;
	private static InventorySlot _originalSlot;

	// Der aktuell ausgewählte Spieler im Inventar
	private Statistics _selectedPlayerStatistics;
	private static int _currentPlayerSelected = 0;

	[Header("Receiving Events On")] 
	[SerializeField] private BoolEventChannelSO visibilityMenuEventChannel;
	[SerializeField] private BoolEventChannelSO visibilityInventoryEventChannel;

	// Selected events
	[SerializeField] private GameObjEventChannelSO playerDeselectedEC;
	[SerializeField] private GameObjActionEventChannelSO playerSelectedEC;

	// Inputchannel für das Inventar
	[SerializeField] private VoidEventChannelSO enableInventoryInput;
	[SerializeField] private InventoryTabEventChannelSO changeInventoryTab;
	
	
	[Header("Sending and Receiving Events On")] [SerializeField]
	private BoolEventChannelSO visibilityGameOverlayEventChannel;

	
	[Header("Sending Events On")]
	// OutputChannel zwischen den Inventaren
	[SerializeField] private IntIntEquipmentPositionEquipEventChannelSO EquipEvent;
	[SerializeField] private IntEquipmentPositionUnequipEventChannelSO UnequipEvent;

	[SerializeField] private VoidEventChannelSO menuOpenedEvent;
	[SerializeField] private VoidEventChannelSO menuClosedEvent;

	private void Awake() {
		//Store the root from the UI Document component
		var root = GetComponent<UIDocument>().rootVisualElement;
		_inventoryContainer = root.Q<VisualElement>("InventoryOverlay");
		_equipmentInventoryContainer = root.Q<VisualElement>("PlayerEquipmentInventory");
		_ghostIcon = root.Query<VisualElement>("GhostIcon");
		_playerContainer = root.Query<VisualElement>("PlayerContainer");
		_characterStatusValuePanel = _inventoryContainer.Q<CharacterStatusValuePanel>("CharacterStatusValuePanel");
		_inventorySlotContainer = root.Q<VisualElement>("InventoryContent");

		visibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
		visibilityInventoryEventChannel.OnEventRaised += HandleInventoryOverlay;
		visibilityGameOverlayEventChannel.OnEventRaised += HandleOtherScreensOpened;

		// Callbacks fürs draggen
		// _ghostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
		_ghostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
	}

	private void Start() {
		InitializeInventory();
		InitializeEquipmentInventory();

		// initially select first player
		if ( characterList == null ) {
			characterList = FindObjectOfType<CharacterList>();
		}
		characterList = CharacterList.FindInstant(); 

		// BUG adakdlkah?
		if ( characterList.playerContainer?.Count > 0 ) {
			_selectedPlayerStatistics = characterList.playerContainer[0]?.GetComponent<Statistics>();
		}
		else {
			_selectedPlayerStatistics = null;
		}

		playerDeselectedEC.OnEventRaised += HandlePlayerDeselected;
		playerSelectedEC.OnEventRaised += HandlePlayerSelected;
	}

	private void Update() {
		OnPointerMove();
	}

	private void InitializeInventory() {
		//Create InventorySlots and add them as children to the SlotContainer
		for ( int i = 0; i < inventoryItemQuantity; i++ ) {
			InventorySlot item =
				new InventorySlot(InventorySlot.InventorySlotType.NormalInventory);

			inventoryItems.Add(item);

			_inventorySlotContainer.Add(item);
		}
	}

	private void InitializeEquipmentInventory() {
		// Hinzufügen der Item Slots für das Equipment Menü
		// Muss nicht dynamisch generiert werden, wird nur der Liste hinzugefügt

		InventorySlot equipmentSlot = _equipmentInventoryContainer.Q<InventorySlot>("WeaponLeft");
		equipmentSlot.userData = EquipmentPosition.LEFT;
		equipmentInventoryItems.Add(equipmentSlot);

		equipmentSlot = _equipmentInventoryContainer.Q<InventorySlot>("WeaponRight");
		equipmentSlot.userData = EquipmentPosition.RIGHT;
		equipmentInventoryItems.Add(equipmentSlot);

		equipmentSlot = _equipmentInventoryContainer.Q<InventorySlot>("HeadArmor");
		equipmentSlot.userData = EquipmentPosition.HEAD;
		equipmentInventoryItems.Add(equipmentSlot);

		equipmentSlot = _equipmentInventoryContainer.Q<InventorySlot>("BodyArmor");
		equipmentSlot.userData = EquipmentPosition.BODY;
		equipmentInventoryItems.Add(equipmentSlot);

		equipmentSlot = _equipmentInventoryContainer.Q<InventorySlot>("Shield");
		equipmentSlot.userData = EquipmentPosition.SHIELD;
		equipmentInventoryItems.Add(equipmentSlot);
	}

	/**
		 * takes all items within the Inventory that are not equipped
		 * and updates the respective InventorySlots
		 */
	public void UpdateInventorySlots() {
		CleanAllItemSlots();

		int slotIdx = 0;
		for ( int inventoryIdx = 0; inventoryIdx < inventory.playerInventory.Count; inventoryIdx++ ) {
			// if item not equipped, display it in ItemSlot Container
			if ( slotIdx < inventoryItems.Count ) {
				inventoryItems[slotIdx].HoldItem(inventory.playerInventory[inventoryIdx]);
				slotIdx++;
			}
			else
				Debug.LogError("Too many items for Inventory UI to display. Item count: " + slotIdx +
				               ", slots in UI: " + inventoryItems.Count);
		}
	}

	public void UpdatePlayerContainer() {
		_playerContainer.Clear();
		characterList = CharacterList.FindInstant(); 
		
		for ( int i = 0; i < characterList.playerContainer.Count; i++ ) {
			int index = i;
			GameObject playerCharacter = characterList.playerContainer[i];
			var stats = playerCharacter.GetComponent<Statistics>();
			if ( stats ) {
				
				CharacterIcon icon = new CharacterIcon();
				icon.name += $"-{stats.DisplayImage}";
				icon.CharacterName = stats.DisplayName;
				icon.Image = stats.DisplayImage;
				icon.Level = stats.StatusValues.Level.value;
				icon.SetHoverOnButton(true);
				icon.UpdateComponent();
				
				//todo refactor this, it works but idont like it
				icon.SetCallback(() => {
					_currentPlayerSelected = index;
					_selectedPlayerStatistics = playerCharacter.GetComponent<Statistics>();
					UpdateEquipmentContainer();
					RefreshStats(playerCharacter);
				});

				_playerContainer.Add(icon);
			}
		}
	}

	public void UpdateEquipmentContainer() {
		CleanAllItemEquipmentSlots();

		InventorySlot weaponLeft = _equipmentInventoryContainer.Q<InventorySlot>("WeaponLeft");
		InventorySlot weaponRight = _equipmentInventoryContainer.Q<InventorySlot>("WeaponRight");
		InventorySlot headArmor = _equipmentInventoryContainer.Q<InventorySlot>("HeadArmor");
		InventorySlot bodyArmor = _equipmentInventoryContainer.Q<InventorySlot>("BodyArmor");
		InventorySlot shield = _equipmentInventoryContainer.Q<InventorySlot>("Shield");

		PlayerCharacterSC currPlayer = characterList.playerContainer[_currentPlayerSelected]
			.GetComponent<PlayerCharacterSC>();

		int playerID = currPlayer.GetComponent<EquipmentController>().playerID;

		if ( inventory.equipmentInventories[playerID].weaponLeft) {
			weaponLeft.HoldItem(inventory.equipmentInventories[playerID].weaponLeft);
		}
		
		if ( inventory.equipmentInventories[playerID].weaponRight) {
			weaponRight.HoldItem(inventory.equipmentInventories[playerID].weaponRight);
		}
		
		if ( inventory.equipmentInventories[playerID].headArmor) {
			headArmor.HoldItem(inventory.equipmentInventories[playerID].headArmor);
		}
		
		if ( inventory.equipmentInventories[playerID].bodyArmor) {
			bodyArmor.HoldItem(inventory.equipmentInventories[playerID].bodyArmor);
		}
		
		if ( inventory.equipmentInventories[playerID].shield) {
			shield.HoldItem(inventory.equipmentInventories[playerID].shield);
		}
	}

	// Sets to selected player or first in container if none selected
	// TODO: copied from OverlayUIController
	//
	void RefreshStats(GameObject obj) {
		//todo move to own class
		var charIcon = _characterStatusValuePanel.CharIcon;
		
		var healthBar = _characterStatusValuePanel.HealthBar;
		var energyBar = _characterStatusValuePanel.EnergyBar;
		var armorBar = _characterStatusValuePanel.ArmorBar;

		var strengthField = _characterStatusValuePanel.StrengthField;
		var dexterityField = _characterStatusValuePanel.DexterityField;
		var intelligenceField = _characterStatusValuePanel.IntelligenceField;
		var movementField = _characterStatusValuePanel.MovementField;
		var visionField = _characterStatusValuePanel.VisionField;
		
		var statistics = obj.GetComponent<Statistics>();
		var chracterStats = statistics.StatusValues;

		charIcon.CharacterName = statistics.DisplayName;
		charIcon.Level = chracterStats.Level.value;
		charIcon.Image = statistics.DisplayImage; 
		charIcon.UpdateComponent();
		
		healthBar.Max = chracterStats.HitPoints.max;
		healthBar.Value = chracterStats.HitPoints.value;
		healthBar.UpdateComponent();

		energyBar.Max = chracterStats.Energy.max;
		energyBar.Value = chracterStats.Energy.value;
		energyBar.UpdateComponent();

		armorBar.Max = chracterStats.Armor.max;
		armorBar.Value = chracterStats.Armor.value;
		armorBar.UpdateComponent();
		
		strengthField.Value = chracterStats.Strength.value;
		strengthField.UpdateComponents();
		
		dexterityField.Value = chracterStats.Dexterity.value;
		dexterityField.UpdateComponents();
		
		intelligenceField.Value = chracterStats.Intelligence.value;
		intelligenceField.UpdateComponents();

		movementField.Value = chracterStats.MovementRange.value;
		movementField.UpdateComponents();
		
		visionField.Value = chracterStats.ViewDistance.value;
		visionField.UpdateComponents();
	}

	// refresh menu and select
	private void HandlePlayerSelected(GameObject player, Action<int> callback) {
		_selectedPlayerStatistics = player.GetComponent<Statistics>();
		RefreshStats(player);
	}

	// refresh menu and select first in container
	private void HandlePlayerDeselected(GameObject player) {
		_selectedPlayerStatistics = player.GetComponent<Statistics>();
		RefreshStats(player);
	}

	/**
	 * if no additional information is given, 
	 * assume no other menu window is opened
	 */
	void HandleInventoryOverlay(bool value) {
		HandleInventoryOverlay(value, false);
	}

	/**
	 * set visibility of this Inventory menu window
	 * only send menuOpened/menuClosed events if no else is open
	 */
	void HandleInventoryOverlay(bool enableInventory, bool othersOpened) {
		// add items to ItemSlots
		UpdateInventorySlots();
		
		// add all players to container
		UpdatePlayerContainer();

		// add equipped items to equipment container
		UpdateEquipmentContainer();

		if ( enableInventory ) {
			if(!othersOpened)
				menuOpenedEvent.RaiseEvent();

			enableInventoryInput.RaiseEvent();
			_inventoryContainer.style.display = DisplayStyle.Flex;
		}
		else {
			if(!othersOpened)
				menuClosedEvent.RaiseEvent();
			
      _inventoryContainer.style.display = DisplayStyle.None;
		}
	}

	void HandleOtherScreensOpened(bool value) {
		HandleInventoryOverlay(false, true);
	}

	private void CleanAllItemSlots() {
		foreach ( var itemSlot in inventoryItems ) {
			if ( itemSlot != null && itemSlot.inventoryItemID != -1 ) {
				itemSlot.DropItem();
			}
		}
	}

	private void CleanAllItemEquipmentSlots() {
		foreach ( var itemSlot in equipmentInventoryItems ) {
			if ( itemSlot != null ) {
				itemSlot.DropItem();
			}
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
		_ghostIcon.style.backgroundImage = _originalSlot.item.icon.texture;
		//Flip the visibility on
		_ghostIcon.style.visibility = Visibility.Visible;
	}

	private void OnPointerMove() {
		//Only take action if the player is dragging an item around the screen
		if ( !_isDragging ) {
			return;
		}

		// mouse position
		Vector3 mousePos = Mouse.current.position.ReadValue();
		float x = mousePos.x;
		float y = Screen.height - mousePos.y;

		//Set the new position
		_ghostIcon.style.top = y - _ghostIcon.layout.height / 2;
		_ghostIcon.style.left = x - _ghostIcon.layout.width / 2;
	}

	private void OnPointerUp(PointerUpEvent mouseEvent) {
		if ( !_isDragging ) {
			return;
		}

		//Check to see if they are dropping the ghost icon over any slots.
		//
		List<InventorySlot> slots = new List<InventorySlot>();
		slots.AddRange(inventoryItems);
		slots.AddRange(equipmentInventoryItems);

		IEnumerable<InventorySlot> overlappedSlots =
			slots.Where(x => x.worldBound.Overlaps(_ghostIcon.worldBound));

		if ( overlappedSlots.Count() != 0 ) {
			InventorySlot targetSlot = overlappedSlots.OrderBy(x => Vector2.Distance
				(x.worldBound.position, _ghostIcon.worldBound.position)).First();

			ItemSO originalItem = _originalSlot.item;
			ItemSO targetItem = targetSlot.item;
						
			// There are four cases (sorry about the spaghetti code -.- )
			//	1. both slots are in player inventory: just swap the inventory slots
			if(_originalSlot.userData == null && targetSlot.userData == null) {
				targetSlot.HoldItem(originalItem);

				if(targetItem)
					_originalSlot.HoldItem(targetItem);
				else
					_originalSlot.DropItem();
			}
			//	2. original slot is equipment, target is player inventory: 
			//		If target slot wields an item, check if swap is possible:
			//		If not, do nothing. Else if it's possible, swap items
			//		If target slot wields no item, just unequip original
			else if (_originalSlot.userData != null && _originalSlot.userData is EquipmentPosition && 
								targetSlot.userData == null) {
				EquipmentPosition originalPos = (EquipmentPosition)_originalSlot.userData;

				if(!targetItem) { 
					UnequipEvent.RaiseEvent(_currentPlayerSelected, originalPos);
					targetSlot.HoldItem(originalItem);
					_originalSlot.DropItem();
				}
				else if(targetItem.ValidForPosition(originalPos)) { 
					EquipEvent.RaiseEvent(targetItem.id, _currentPlayerSelected, originalPos);
					targetSlot.HoldItem(originalItem);
					_originalSlot.HoldItem(targetItem);
				}
				else { 
					Debug.LogWarning("The item " + targetItem.id + 
							" you want to swap is not valid for the item slot (" + originalPos + ")! ");
				}
			}
			//	3. original slot is in player inventory, target is equipment:
			//		If original item is not valid for equipment slot, do nothing
			//		Else, equip original item. Unequip target item if necessary
			else if (_originalSlot.userData == null && 
								targetSlot.userData != null && targetSlot.userData is EquipmentPosition) { 
				EquipmentPosition targetPos = (EquipmentPosition) targetSlot.userData;

				if(originalItem.ValidForPosition(targetPos)) { 
					EquipEvent.RaiseEvent(originalItem.id, _currentPlayerSelected, targetPos);
					targetSlot.HoldItem(originalItem);
					
					if(targetItem)
						_originalSlot.HoldItem(targetItem);
					else
						_originalSlot.DropItem();
				}
				else
					Debug.LogWarning("Item " + originalItem.id + " is not valid for target slot " + targetPos + "! ");
			}
			//	4. original and target slots are equipment:
			//		Only do anything if the original item is valid for the target slot
			//		Swap items if necessary
			else if (_originalSlot.userData != null && _originalSlot.userData is EquipmentPosition &&
								targetSlot.userData != null && targetSlot.userData is EquipmentPosition) { 
				EquipmentPosition originalPos = (EquipmentPosition) _originalSlot.userData;
				EquipmentPosition targetPos = (EquipmentPosition) targetSlot.userData;

				if(originalItem.ValidForPosition(targetPos)) { 
					if(targetItem) { 
						if(targetItem.ValidForPosition(originalPos)) { 
							UnequipEvent.RaiseEvent(_currentPlayerSelected, originalPos);
							UnequipEvent.RaiseEvent(_currentPlayerSelected, targetPos);

							EquipEvent.RaiseEvent(originalItem.id, _currentPlayerSelected, targetPos);
							targetSlot.HoldItem(originalItem);

							EquipEvent.RaiseEvent(targetItem.id, _currentPlayerSelected, originalPos);
							_originalSlot.HoldItem(targetItem);
						}
						else
							Debug.LogWarning("Item " + originalItem.id + " is not valid for target slot " + targetPos + "! ");
					}
					else {  
						UnequipEvent.RaiseEvent(_currentPlayerSelected, originalPos);
						EquipEvent.RaiseEvent(originalItem.id, _currentPlayerSelected, targetPos);
						targetSlot.HoldItem(originalItem);
						
						_originalSlot.DropItem();
					}
				}
				else
					Debug.LogWarning("Item " + originalItem.id + " is not valid for target slot " + targetPos + "! ");
			}
			else
				Debug.LogError("You should not see me. ");
		}

		if( _originalSlot.item )
			_originalSlot.icon.image = _originalSlot.item.icon.texture;

		// clear dragging related visuals and data
		_isDragging = false;
		_originalSlot = null;
		_ghostIcon.style.visibility = Visibility.Hidden;
	}

	// tab stuff
	//
	//Für das Inventar
	public enum InventoryTab {
		None,
		Items,
		Armory,
		Weapons
	}

	/*
  void ResetAllTabs() {
      _inventoryContainer.Q<Button>("Tab1").RemoveFromClassList("ClickedTab");

      _inventoryContainer.Q<Button>("Tab1").AddToClassList("UnclickedTab");
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
	
  void HandleItemTabPressed() {
      InventoryManager(InventoryTab.Items, _inventoryContainer.Q<Button>("Tab1"));
  }

  void HandleArmoryTabPressed() {
      InventoryManager(InventoryTab.Armory, _inventoryContainer.Q<Button>("Tab2"));
  }

  void HandleWeaponsTabPressed() {
      InventoryManager(InventoryTab.Weapons, _inventoryContainer.Q<Button>("Tab3"));
  }

	*/
}