using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
using Characters.Equipment;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour {
	public List<InventorySlot> inventoryItems = new List<InventorySlot>();
	public List<InventorySlot> equipmentInventoryItems = new List<InventorySlot>();

	public ItemContainerSO itemContainer;
	[SerializeField] private InventorySO inventory;
	[SerializeField] private EquipmentContainerSO equipmentContainerSO;
	
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
	private VisualElement _playerViewContainer;

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

	private void Awake() {
		//Store the root from the UI Document component
		var root = GetComponent<UIDocument>().rootVisualElement;
		_inventoryContainer = root.Q<VisualElement>("InventoryOverlay");
		_equipmentInventoryContainer = root.Q<VisualElement>("PlayerEquipmentInventory");
		_ghostIcon = root.Query<VisualElement>("GhostIcon");
		_playerContainer = root.Query<VisualElement>("PlayerContainer");
		_playerViewContainer = _inventoryContainer.Q<VisualElement>("PlayerViewContainer");
		_inventorySlotContainer = root.Q<VisualElement>("InventoryContent");

		// Lifehack
		

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

		InventorySlot weaponLeft = _equipmentInventoryContainer.Q<InventorySlot>("WeaponLeft");
		weaponLeft.userData = EquipmentPosition.LEFT;
		equipmentInventoryItems.Add(weaponLeft);

		InventorySlot weaponRight = _equipmentInventoryContainer.Q<InventorySlot>("WeaponRight");
		weaponRight.userData = EquipmentPosition.RIGHT;
		equipmentInventoryItems.Add(weaponRight);
	}

	/**
		 * takes all items within the Inventory that are not equipped
		 * and updates the respective InventorySlots
		 */
	public void UpdateInventorySlots() {
		CleanAllItemSlots();

		int slotIdx = 0;
		for ( int inventoryIdx = 0; inventoryIdx < inventory.itemIDs.Count; inventoryIdx++ ) {
			// if item not equipped, display it in ItemSlot Container
			if ( inventoryIdx >= inventory.equipmentID.Count ||
			     inventory.equipmentID[inventoryIdx] < 0 ) {
				if ( slotIdx < inventoryItems.Count ) {
					inventoryItems[slotIdx].HoldItem(itemContainer.itemList[inventory.itemIDs[inventoryIdx]],
						inventoryIdx);
					slotIdx++;
				}
				else
					Debug.LogError("Too many items for Inventory UI to display. Item count: " + slotIdx +
					               ", slots in UI: " + inventoryItems.Count);
			}
		}
	}

	public void UpdatePlayerContainer() {
		_playerContainer.Clear();
		characterList = CharacterList.FindInstant(); 
		
		for ( int i = 0; i < characterList.playerContainer.Count; i++ ) {
			GameObject playerCharacter = characterList.playerContainer[i];
			var stats = playerCharacter.GetComponent<Statistics>();
			if ( stats ) {
				Debug.Log("Adding player to Character List. ");
				Image playerIcon = new Image();
				playerIcon.image = stats.DisplayImage.texture;
				playerIcon.AddToClassList("PlayerContainer");
				_playerContainer.Add(playerIcon);
			}
		}
	}

	public void UpdateEquipmentContainer() {
		CleanAllItemEquipmentSlots();

		InventorySlot weaponLeft = _equipmentInventoryContainer.Q<InventorySlot>("WeaponLeft");
		InventorySlot weaponRight = _equipmentInventoryContainer.Q<InventorySlot>("WeaponRight");

		PlayerCharacterSC currPlayer = characterList.playerContainer[_currentPlayerSelected]
			.GetComponent<PlayerCharacterSC>();

		var equipmentID = currPlayer.GetComponent<EquipmentController>().equipmentID;
		Equipment equipment = equipmentContainerSO.equipmentInventories[equipmentID];

		if ( equipment.items[( int )EquipmentPosition.LEFT] >= 0 ) {
			int inventoryItemID = equipment.items[( int )EquipmentPosition.LEFT];
			ItemSO itemLeft = inventory.GetItem(inventoryItemID);
			weaponLeft.HoldItem(itemLeft, inventoryItemID);
		}

		if ( equipment.items[( int )EquipmentPosition.RIGHT] >= 0 ) {
			int inventoryItemID = equipment.items[( int )EquipmentPosition.RIGHT];
			ItemSO itemRight = inventory.GetItem(inventoryItemID);
			weaponRight.HoldItem(itemRight, inventoryItemID);
		}
	}


	// Sets to selected player or first in container if none selected
	// TODO: copied from OverlayUIController
	//
	private void RefreshPlayerViewContainer() {
		Debug.Log("Player selected: " + _currentPlayerSelected);
		Debug.Log("Character List: " + characterList.playerContainer.Count);
		PlayerCharacterSC _selectedPlayer = characterList.playerContainer[_currentPlayerSelected]
			.GetComponent<PlayerCharacterSC>();

		//VisualElement manaBar = PlayerViewContainer.Q<VisualElement>("ProgressBarManaOverlay");
		VisualElement healthBar = _playerViewContainer.Q<VisualElement>("ProgressBarHealthOverlay");
		VisualElement abilityBar = _playerViewContainer.Q<VisualElement>("ProgressBarAbilityOverlay");

		StatusValues playerStats = _selectedPlayerStatistics.StatusValues;

		//todo ui refactoring 
		healthBar.style.width =
			new StyleLength(Length.Percent(( 100 * ( float )playerStats.HitPoints.value /
			                                 playerStats.HitPoints.max )));
		abilityBar.style.width =
			new StyleLength(Length.Percent(( 100 * ( float )playerStats.Energy.value /
			                                 playerStats.Energy.max )));

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

	void HandleInventoryOverlay(bool value) {
		// add items to ItemSlots
		UpdateInventorySlots();
		
		// add all players to container
		UpdatePlayerContainer();

		// add equipped items to equipment container
		UpdateEquipmentContainer();

		if ( value ) {
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

			int originalID = _originalSlot.inventoryItemID;
			ItemSO originalItem = _originalSlot.item;

			// if targetSlot is not empty, put it to original
			if ( targetSlot.inventoryItemID >= 0 ) {
				_originalSlot.HoldItem(targetSlot.item, targetSlot.inventoryItemID);

				// if original slot was in equipment, equip new item
				if ( typeof(EquipmentPosition).IsInstanceOfType(_originalSlot.userData) ) {
					EquipmentPosition pos = ( EquipmentPosition )_originalSlot.userData;
					EquipEvent.RaiseEvent(targetSlot.inventoryItemID, _currentPlayerSelected, pos);
				}
			}
			else {
				//Clear the original slot
				_originalSlot.DropItem();

				// if originalSlot was in equipment, unequip original (if target and original were not the same)
				if ( typeof(EquipmentPosition).IsInstanceOfType(_originalSlot.userData) &&
				     !_originalSlot.Equals(targetSlot) ) {
					EquipmentPosition pos = ( EquipmentPosition )_originalSlot.userData;
					UnequipEvent.RaiseEvent(_currentPlayerSelected, pos);
				}
			}

			targetSlot.HoldItem(originalItem, originalID);

			// if targetSlot was in equipment, send events to InventoryManager to change equipment
			if ( typeof(EquipmentPosition).IsInstanceOfType(targetSlot.userData) ) {
				EquipmentPosition pos = ( EquipmentPosition )targetSlot.userData;
				EquipEvent.RaiseEvent(originalID, _currentPlayerSelected, pos);
			}
		}
		else {
			// didn't find anything, reset image in original slot
			_originalSlot.icon.image = _originalSlot.item.icon.texture;
		}

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