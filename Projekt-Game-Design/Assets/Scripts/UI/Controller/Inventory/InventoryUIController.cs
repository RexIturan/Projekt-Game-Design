using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Characters;
using Characters.Equipment.ScriptableObjects;
using Events.ScriptableObjects;
using GDP01._Gameplay.Logic_Data.Inventory.EventChannels;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using GDP01.Characters.Component;
using GDP01.Player.Player;
using Input;
using UI.Components.Character;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static GDP01._Gameplay.Logic_Data.Inventory.Types.InventoryTarget;

public class InventoryUIController : MonoBehaviour {
	
	public List<InventorySlot> inventoryItems = new List<InventorySlot>();
	public List<InventorySlot> equipmentInventoryItems = new List<InventorySlot>();

	[SerializeField] private InventorySO inventory;
	[SerializeField] private EquipmentContainerSO equipmentContainer;
	[SerializeField] private InputReader inputReader;
	
	//todo set inventory size in inventory manager or invetorySO
	[SerializeField] private int inventoryItemQuantity = 28;

	[Header("Receiving Events On")]
	[SerializeField] private BoolEventChannelSO setGameOverlayVisibilityEC;
	[SerializeField] private BoolEventChannelSO setMenuVisibilityEC;
	[SerializeField] private BoolEventChannelSO setInventoryVisibilityEC;

	// Inputchannel für das Inventar
	[SerializeField] private VoidEventChannelSO enableInventoryInput;
	[SerializeField] private InventoryTabEventChannelSO changeInventoryTab;

	[Header("Sending Events On")]
	// OutputChannel zwischen den Inventaren
	//todo shorter names?
	[SerializeField] private EquipItemEC_SO EquipItemEC;
	[SerializeField] private UnequipItemEC_SO UnequipItemEC;
	[SerializeField] private VoidEventChannelSO menuOpenedEvent;
	[SerializeField] private VoidEventChannelSO menuClosedEvent;
	[SerializeField] private MoveItemEventChannel moveItemEC; 
	[SerializeField] private SoundEventChannelSO playSoundEC;

	// public ItemContainerSO itemContainer;
	
	[SerializeField] private SoundSO openSound;
	[SerializeField] private SoundSO closeSound;
	[SerializeField] private SoundSO drinkSound;

///// Private Variables	////////////////////////////////////////////////////////////////////////////	

	private VisualElement _inventorySlotContainer;

	// Für das Inventar
	private VisualElement _inventoryContainer;

	// Close Button
	private Button _closeButton;

	// Für das EquipmentInventar
	private VisualElement _equipmentInventoryContainer;

	// for images of all players to switch between them
	private VisualElement _playerContainer;

	// For dialogues 
	private VisualElement _dialogueComponentLayer;

	// Fuer den PlayerView Container
		private CharacterStatusValuePanel _characterStatusValuePanel;

	// Für das Ghost Icon
	private static VisualElement _ghostIcon;

	// Zum Drinken von Potions
	private static InventorySlot _potionSlot;
	private InventorySlot _currentlyDrinking;

	// Zum Draggen der Icons
	private static bool _isDragging;
	private static InventorySlot _originalSlot;

	// Der aktuell ausgewählte Spieler im Inventar
	private Statistics _selectedPlayerStatistics;
	private PlayerCharacterSC _currentPlayerSelected;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

	private CharacterManager CharacterManager => GameplayProvider.Current.CharacterManager; 
	private PlayerCharacterSC SelectedPlayer {
		get {
			// get cached PC -> get selected PC in player controller -> get first PC from Charactermanager 
			return _currentPlayerSelected ??= 
				PlayerController.Current.Selected?.GetComponent<PlayerCharacterSC>() ?? 
				CharacterManager.GetFirstPlayerCharacter();
		}
		set {
			_selectedPlayerStatistics = value.GetComponent<Statistics>();
			_currentPlayerSelected = value;
		}
	}

	///// Private Functions	////////////////////////////////////////////////////////////////////////////
	
	//todo move to Inventory Component
	private void InitializeInventory() {
		//Create InventorySlots and add them as children to the SlotContainer
		for ( int i = 0; i < inventoryItemQuantity; i++ ) {
			InventorySlot item =
				new InventorySlot(InventorySlot.InventorySlotType.NormalInventory, i);

			inventoryItems.Add(item);

			_inventorySlotContainer.Add(item);
		}
	}

	private void CleanInventory() {
		inventoryItems.Clear();
		_inventorySlotContainer.Clear();
	}
	
	//todo move to Inventory Component
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

	private void CleanEquipmentSheet() {
		equipmentInventoryItems.Clear();
	}
	
	/**
		 * takes all items within the Inventory that are not equipped
		 * and updates the respective InventorySlots
		 */
	private void UpdateInventorySlots() {
		CleanAllItemSlots();

		int slotIdx = 0;
		for ( int inventoryIdx = 0; inventoryIdx < inventory.InventorySlots.Length; inventoryIdx++ ) {
			// if item not equipped, display it in ItemSlot Container
			if ( slotIdx < inventoryItems.Count ) {
				inventoryItems[slotIdx].HoldItem(inventory.InventorySlots[inventoryIdx]);
				slotIdx++;
			}
			else
				Debug.LogError("Too many items for Inventory UI to display. Item count: " + slotIdx +
				               ", slots in UI: " + inventoryItems.Count);
		}
	}

	/// Character Selector In the inventory
	private void UpdatePlayerCharacterSelector() {
		_playerContainer.Clear();
		
		List<PlayerCharacterSC> playerCharacters = CharacterManager.GetPlayerCharacters();
		
		for ( int i = 0; i < playerCharacters.Count; i++ ) {
			int index = i;
			PlayerCharacterSC player = playerCharacters[i];
			Statistics stats = player.GetComponent<Statistics>();
			if ( stats ) {
				
				CharacterIcon icon = new CharacterIcon();
				icon.name += $"-{stats.DisplayImage} ????";
				icon.CharacterName = stats.DisplayName;
				icon.Image = stats.DisplayImage;
				icon.Level = stats.StatusValues.Level.Value;
				icon.SetHoverOnButton(true);
				icon.UpdateComponent();
				
				//todo refactor this, it works but idont like it
				icon.SetCallback(() => {
					SelectedPlayer = player;
					UpdateEquipmentContainer();
					RefreshStats();
				});

				_playerContainer.Add(icon);
			}
		}
	}

	private void UpdateEquipmentContainer() {
		CleanAllItemEquipmentSlots();

		//why qery every update?
		InventorySlot weaponLeft = _equipmentInventoryContainer.Q<InventorySlot>("WeaponLeft");
		InventorySlot weaponRight = _equipmentInventoryContainer.Q<InventorySlot>("WeaponRight");
		InventorySlot headArmor = _equipmentInventoryContainer.Q<InventorySlot>("HeadArmor");
		InventorySlot bodyArmor = _equipmentInventoryContainer.Q<InventorySlot>("BodyArmor");
		InventorySlot shield = _equipmentInventoryContainer.Q<InventorySlot>("Shield");

		PlayerCharacterSC currPlayer = SelectedPlayer;

		int equipmentID = currPlayer.GetComponent<EquipmentController>().EquipmentID;

		var equipmentSheet = equipmentContainer.EquipmentSheets[equipmentID];
		
		if ( equipmentSheet.weaponTypeLeft) {
			weaponLeft.HoldItem(equipmentSheet.weaponTypeLeft);
		}
		
		if ( equipmentSheet.weaponTypeRight) {
			weaponRight.HoldItem(equipmentSheet.weaponTypeRight);
		}
		
		if ( equipmentSheet.headArmorType) {
			headArmor.HoldItem(equipmentSheet.headArmorType);
		}
		
		if ( equipmentSheet.bodyArmorType) {
			bodyArmor.HoldItem(equipmentSheet.bodyArmorType);
		}
		
		if ( equipmentSheet.shieldType) {
			shield.HoldItem(equipmentSheet.shieldType);
		}
	}

	// Sets to selected player or first in container if none selected
	// TODO: copied from OverlayUIController
	//todo move to own class
	private void RefreshStats() {
		GameObject obj = SelectedPlayer.gameObject;
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
		charIcon.Level = chracterStats.Level.Value;
		charIcon.Image = statistics.DisplayImage; 
		charIcon.UpdateComponent();
		
		healthBar.Max = chracterStats.HitPoints.Max;
		healthBar.Value = chracterStats.HitPoints.Value;
		healthBar.UpdateComponent();

		energyBar.Max = chracterStats.Energy.Max;
		energyBar.Value = chracterStats.Energy.Value;
		energyBar.UpdateComponent();

		armorBar.Max = chracterStats.Armor.Max;
		armorBar.Value = chracterStats.Armor.Value;
		armorBar.UpdateComponent();
		
		strengthField.Value = chracterStats.Strength.Value;
		strengthField.UpdateComponents();
		
		dexterityField.Value = chracterStats.Dexterity.Value;
		dexterityField.UpdateComponents();
		
		intelligenceField.Value = chracterStats.Intelligence.Value;
		intelligenceField.UpdateComponents();

		movementField.Value = chracterStats.MovementRange.Value;
		movementField.UpdateComponents();
		
		visionField.Value = chracterStats.ViewDistance.Value;
		visionField.UpdateComponents();
	}

	/// <summary>
	/// Heals player character by given value. Note that this will not update the statusvalue bars. 
	/// </summary>
	/// <param name="playerStats">Player character statistics </param>
	private void HealPlayer(Statistics playerStats, int value) {
		playerStats.StatusValues.HitPoints.Value += value;
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

	public static void SetDrinkPotion(InventorySlot potionSlot) {
		_potionSlot = potionSlot;
	}

	private void StartDrinkPotion() {
		PotionTypeSO potion = (PotionTypeSO)_potionSlot.itemType;
		_dialogueComponentLayer.Add(new PotionConsumptionDialogue(potion, HandleDrinkPotion, HandleConsumptionCancelled));
		_dialogueComponentLayer.pickingMode = PickingMode.Position;
		_dialogueComponentLayer.visible = true;
		_dialogueComponentLayer.style.display = DisplayStyle.Flex;
		_currentlyDrinking = _potionSlot;
		_potionSlot = null;
	}
	
	//TODO Inventory Component
	public static void StartDrag(Vector2 position, InventorySlot originalSlot) {
		//Set tracking variables
		_isDragging = true;
		_originalSlot = originalSlot;
		//Set the new position
		_ghostIcon.style.top = position.y - _ghostIcon.layout.height / 2;
		_ghostIcon.style.left = position.x - _ghostIcon.layout.width / 2;
		//Set the image
		_ghostIcon.style.backgroundImage = _originalSlot.itemType.icon.texture;
		//Flip the visibility on
		_ghostIcon.style.visibility = Visibility.Visible;
	}
	
	//TODO Inventory Component
	private void OnPointerMove() {
		//Only take action if the player is dragging an item around the screen
		if ( !_isDragging ) {
			return;
		}

		// mouse position
		Vector3 mousePos = Mouse.current.position.ReadValue();
		float x = mousePos.x / Screen.width;
		float y = 1 - mousePos.y / Screen.height; // = (Screen.height - mousePos.y) / Screen.height

		//Set the new position
		_ghostIcon.style.left = x * _ghostIcon.parent.resolvedStyle.width - _ghostIcon.layout.width / 2;
		_ghostIcon.style.top = y * _ghostIcon.parent.resolvedStyle.height - _ghostIcon.layout.height / 2;
	}

	//TODO Inventory Component
	private void OnPointerUp(PointerUpEvent mouseEvent) {
		if ( !_isDragging ) {
			return;
		}

		//Check to see if they are dropping the ghost icon over any slots.
		List<InventorySlot> slots = new List<InventorySlot>();
		slots.AddRange(inventoryItems);
		slots.AddRange(equipmentInventoryItems);

		IEnumerable<InventorySlot> overlappedSlots =
			slots.Where(x => x.worldBound.Overlaps(_ghostIcon.worldBound));

		if ( overlappedSlots.Count() != 0 ) {
			InventorySlot targetSlot = overlappedSlots.OrderBy(x => Vector2.Distance
				(x.worldBound.position, _ghostIcon.worldBound.position)).First();

			ItemTypeSO originalItemType = _originalSlot.itemType;
			ItemTypeSO targetItemType = targetSlot.itemType;

			int fromID = _originalSlot.slotId;
			int toID = targetSlot.slotId;
			int playerID = SelectedPlayer.id;
			
			// There are four cases (sorry about the spaghetti code -.- )
			//	1. both slots are in player inventory: just swap the inventory slots
			if(_originalSlot.userData == null && targetSlot.userData == null) {
				targetSlot.HoldItem(originalItemType);

				moveItemEC.RaiseEvent(Inventory, fromID, Inventory, toID, playerID);
				
				if(targetItemType)
					_originalSlot.HoldItem(targetItemType);
				else
					_originalSlot.DropItem();
			}
			
			//	2. original slot is equipment, target is player inventory: 
			//		If target slot wields an item, check if swap is possible:
			//		If not, do nothing. Else if it's possible, swap items
			//		If target slot wields no item, just unequip original
			else if (_originalSlot.userData is EquipmentPosition pos && targetSlot.userData == null) {
				//Unequip
				if(!targetItemType) {
					moveItemEC.RaiseEvent(Equipment, ( int )pos, Inventory, toID, playerID);
					// UnequipItemEC.RaiseEvent(_currentPlayerSelected, targetSlot.slotId, pos);
					targetSlot.HoldItem(originalItemType);
					_originalSlot.DropItem();
				}
				else if(targetItemType.ValidForPosition(pos)) { 
					moveItemEC.RaiseEvent(Equipment, ( int )pos, Inventory, toID, playerID);
					
					// EquipItemEC.RaiseEvent(targetItem.id, _currentPlayerSelected, pos);
					targetSlot.HoldItem(originalItemType);
					_originalSlot.HoldItem(targetItemType);
				}
				else { 
					Debug.LogWarning("The item " + targetItemType.id + 
							" you want to swap is not valid for the item slot (" + pos + ")! ");
				}
			}
			
			//	3. original slot is in player inventory, target is equipment:
			//		If original item is not valid for equipment slot, do nothing
			//		Else, equip original item. Unequip target item if necessary
			else if (_originalSlot.userData == null && targetSlot.userData is EquipmentPosition targetSlotEquipmentPos) {
				
				if(originalItemType.ValidForPosition(targetSlotEquipmentPos)) {
					moveItemEC.RaiseEvent(Inventory, fromID, Equipment, ( int )targetSlotEquipmentPos, playerID);
					
					// EquipItemEC.RaiseEvent(originalItem.id, _currentPlayerSelected, _originalSlot.slotId, targetPos);
					targetSlot.HoldItem(originalItemType);
					
					if(targetItemType)
						_originalSlot.HoldItem(targetItemType);
					else
						_originalSlot.DropItem();
				}
				else
					Debug.LogWarning("Item " + originalItemType.id + " is not valid for target slot " + targetSlotEquipmentPos + "! ");
			}
			
			//	4. original and target slots are equipment:
			//		Only do anything if the original item is valid for the target slot
			//		Swap items if necessary
			else if (_originalSlot.userData is EquipmentPosition originalPos && 
			         targetSlot.userData is EquipmentPosition targetPos) {
				
				moveItemEC.RaiseEvent(Equipment, ( int )originalPos, Equipment, ( int )targetPos, playerID);
				
				if(originalItemType.ValidForPosition(targetPos)) { 
					if(targetItemType) { 
						if(targetItemType.ValidForPosition(originalPos)) { 
							// UnequipItemEC.RaiseEvent(_currentPlayerSelected, originalPos);
							// UnequipItemEC.RaiseEvent(_currentPlayerSelected, targetPos);

							// EquipItemEC.RaiseEvent(originalItem.id, _currentPlayerSelected, _originalSlot.slotId, targetPos);
							targetSlot.HoldItem(originalItemType);

							// EquipItemEC.RaiseEvent(targetItem.id, _currentPlayerSelected, targetSlot.slotId, originalPos);
							_originalSlot.HoldItem(targetItemType);
						}
						else
							Debug.LogWarning("Item " + originalItemType.id + " is not valid for target slot " + targetPos + "! ");
					} else {  
						// UnequipItemEC.RaiseEvent(_currentPlayerSelected, originalPos);
						// EquipItemEC.RaiseEvent(originalItem.id, _currentPlayerSelected, _originalSlot.slotId, targetPos);
						targetSlot.HoldItem(originalItemType);
						
						_originalSlot.DropItem();
					}
				}
				else
					Debug.LogWarning("Item " + originalItemType.id + " is not valid for target slot " + targetPos + "! ");
			}
			else
				Debug.LogError("You should not see me. ");
		}

		if( _originalSlot.itemType )
			_originalSlot.icon.image = _originalSlot.itemType.icon.texture;

		// clear dragging related visuals and data
		_isDragging = false;
		_originalSlot = null;
		_ghostIcon.style.visibility = Visibility.Hidden;
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

	private void SetInventoryVisibility(bool value) {
		//todo use screen manager
		_inventoryContainer.style.display = value ? DisplayStyle.Flex : DisplayStyle.None;
	}

	private void ClearDialogue() {
		// unbind dialogues 
		foreach (VisualElement child in _dialogueComponentLayer.Children()) {
			if(child is AffirmationDialogue)
				((AffirmationDialogue)child).UnbindActions();
		}

		_dialogueComponentLayer.Clear();
		_dialogueComponentLayer.pickingMode = PickingMode.Ignore;
		_dialogueComponentLayer.visible = false;
	}

	private void BindElements() {
		//Store the root from the UI Document component
		// var root = GetComponent<UIDocument>().rootVisualElement;
		var root = GetComponent<UIDocument>().rootVisualElement;
		_inventoryContainer = root;
		_equipmentInventoryContainer = root.Q<VisualElement>("PlayerEquipmentInventory");
		_closeButton = root.Q<Button>("CloseInventoryButton");
		_ghostIcon = root.Query<VisualElement>("GhostIcon");
		_playerContainer = root.Query<VisualElement>("PlayerContainer");
		_dialogueComponentLayer = root.Query<VisualElement>("DialogueComponentLayer");
		_characterStatusValuePanel = root.Q<CharacterStatusValuePanel>("CharacterStatusValuePanel");
		_inventorySlotContainer = root.Q<VisualElement>("InventoryContent");
		
		//todo move to drag and drop
		// Callbacks fürs draggen
		// _ghostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
		_ghostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
		_closeButton.clicked += inputReader.SimulateOnCancelInventory;
		
		InitializeInventory();
		InitializeEquipmentInventory();
		ClearDialogue();
	}

	private void UnbindElements() {
		
		CleanInventory();
		CleanEquipmentSheet();
		
		//todo move to drag and drop
		// Callbacks fürs draggen
		// _ghostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
		_ghostIcon.UnregisterCallback<PointerUpEvent>(OnPointerUp);
		_closeButton.clicked -= inputReader.SimulateOnCancelInventory;
		
		_inventoryContainer = null;
		_equipmentInventoryContainer = null;
		_closeButton = null;
		_ghostIcon = null;
		_playerContainer = null;
		_characterStatusValuePanel = null;
		_inventorySlotContainer = null;
	}
	
///// Callbacks	////////////////////////////////////////////////////////////////////////////////////


	/**
	 * set visibility of this Inventory menu window
	 * only send menuOpened/menuClosed events if no else is open
	 */
	private void HandleInventoryOverlay(bool enableInventory, bool othersOpened) {
		playSoundEC.RaiseEvent(enableInventory ? openSound : closeSound);

		// add items to ItemSlots
		UpdateInventorySlots();
		
		// add all players to container
		UpdatePlayerCharacterSelector();

		// add equipped items to equipment container
		UpdateEquipmentContainer();

		SetInventoryVisibility(enableInventory);
		
		if ( enableInventory ) {
			if(!othersOpened)
				menuOpenedEvent.RaiseEvent();
			
		}
		else {
			if(!othersOpened)
				menuClosedEvent.RaiseEvent();
		}
	}

	/// <summary>
	/// if no additional information is given,
	/// assume no other menu window is opened
	/// </summary>
	/// <param name="value"></param>
	private void HandleInventoryOverlay(bool value) {
		HandleInventoryOverlay(value, true);
	}
	
	private void HandleOtherScreensOpened(bool value) {
		HandleInventoryOverlay(false, true);
	}

	private void HandleDrinkPotion() {
		Debug.Log("Drinking potion. ");
		if ( _selectedPlayerStatistics ) {
			playSoundEC.RaiseEvent(drinkSound);
			HealPlayer(_selectedPlayerStatistics, ( ( PotionTypeSO )_currentlyDrinking.itemType ).healing);
			RefreshStats();
			_currentlyDrinking.DropItem();
			moveItemEC.RaiseEvent(Inventory, _currentlyDrinking.slotId, Trash, 0, 0);
			_currentlyDrinking = null;
		}
		else
			Debug.LogError("No player selected! ");
		ClearDialogue();
	}

	private void HandleConsumptionCancelled() {
		ClearDialogue();
	}

///// Public Functions	////////////////////////////////////////////////////////////////////////////

///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

	private void Update() {
		//todo why??????
		OnPointerMove();

		if(_potionSlot != null) {
			StartDrinkPotion();
		}
	}

	private void OnEnable() {
		BindElements();
		
		ClearDialogue();

		RefreshStats();
		HandleInventoryOverlay(true, true);

		// setInventoryVisibilityEC.OnEventRaised += HandleInventoryOverlay;
		// setMenuVisibilityEC.OnEventRaised += HandleOtherScreensOpened;
		// setGameOverlayVisibilityEC.OnEventRaised += HandleOtherScreensOpened;
		
		enableInventoryInput.RaiseEvent();
	}

	private void OnDisable() {
		HandleInventoryOverlay(false, true);
		
		UnbindElements();
		
		// setInventoryVisibilityEC.OnEventRaised -= HandleInventoryOverlay;
		// setMenuVisibilityEC.OnEventRaised -= HandleOtherScreensOpened;
		// setGameOverlayVisibilityEC.OnEventRaised -= HandleOtherScreensOpened;
	}
}