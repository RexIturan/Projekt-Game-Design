using System;
using System.Collections.Generic;
using Characters;
using Characters.Ability;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using UI.Components;
using UI.Components.Character;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;


public class OverlayUIController : MonoBehaviour {
	[Header("Receiving Events On")] [SerializeField]
	private BoolEventChannelSO showGameOverlayEC;

	[SerializeField] private BoolEventChannelSO showTurnIndicatorEC;
	[SerializeField] private BoolEventChannelSO visibilityInventoryEventChannel;

	// Action Menu
	[SerializeField] private GameObjEventChannelSO playerDeselectedEC;
	[SerializeField] private GameObjActionEventChannelSO playerSelectedEC;

	[Header("Sending Events On")] [SerializeField]
	private VoidEventChannelSO enableGamplayInput;

	[SerializeField] private EFactionEventChannelSO endTurnEC;

	[Header("Sending and Receiving Events On")] [SerializeField]
	private BoolEventChannelSO visibilityMenuEventChannel;

	// Für die UI Elemente
	private VisualElement _overlayContainer;

	// Action Container
	private ActionBar _actionBar;

	// PlayerView Container
	private CharacterStatusValuePanel _characterStatusValuePanel;

	private TemplateContainer _turnIndicator;

	// Callbackfunktion für die Abilitys
	private Action<int> _callBackAction;


	private void Awake() {
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		var root = GetComponent<UIDocument>().rootVisualElement;
		_actionBar = root.Q<ActionBar>("ActionBar");
		_overlayContainer = root.Q<VisualElement>("OverlayContainer");
		_characterStatusValuePanel = root.Q<CharacterStatusValuePanel>("CharacterStatusValuePanel");
		_turnIndicator = root.Q<TemplateContainer>("TurnIndicator");
		_overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
		_overlayContainer.Q<Button>("EndTurnButton").clicked += HandleEndTurnUI;
		visibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
		visibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
		showGameOverlayEC.OnEventRaised += SetGameOverlayVisibility;
		showTurnIndicatorEC.OnEventRaised += SetTurnIndicatorVisibility;
		playerDeselectedEC.OnEventRaised += HandlePlayerDeselected;

		playerSelectedEC.OnEventRaised += HandlePlayerSelected;
		
		_actionBar.SetVisibility(false);
		_characterStatusValuePanel.SetViibility(false);
	}

	private void HandleEndTurnUI() {
		endTurnEC.RaiseEvent(Faction.Player);
	}

	void SetTurnIndicatorVisibility(bool show) {
		//todo fix this
		// _turnIndicator.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
	}

	void SetGameOverlayVisibility(bool value) {
		if ( value ) {
			enableGamplayInput.RaiseEvent();
			_overlayContainer.style.display = DisplayStyle.Flex;
		}
		else {
			_overlayContainer.style.display = DisplayStyle.None;
		}
	}

	void HandleOtherScreensOpened(bool value) {
		SetGameOverlayVisibility(false);
	}

	void FlushAbilityListIcons() {
		foreach ( var button in _actionBar.actionButtons ) {
			button.UnbindAction();
		}
	}

	//todo use for tooltip 
	// void CallBackMouseEnterAbility(MouseEnterEvent evt, string description) {
	//     Label text = _overlayContainer.Q<Label>("AbilityDescription");
	//     text.style.display = DisplayStyle.Flex;
	//     text.text = description;
	// }
	//
	// void CallBackMouseLeaveAbility(MouseLeaveEvent evt) {
	//     Label text = _overlayContainer.Q<Label>("AbilityDescription");
	//     text.style.display = DisplayStyle.None;
	// }

	// todo(vincent) -> show character stats
	/// <summary>
	/// HandlePlayerSelected
	/// </summary>
	/// <param name="player">player Character Game Object</param>
	/// <param name="callBackAction">callback to call when a action ist clicked</param>
	void HandlePlayerSelected(GameObject player, Action<int> callBackAction) {
		// Anzeigen der notwendigen Komponenten
		ShowActionMenu();
		ShowPlayerViewContainer();

		_callBackAction = callBackAction;
		FlushAbilityListIcons();
		List<AbilitySO> abilities =
			new List<AbilitySO>(player.GetComponent<AbilityController>().Abilities);
		int counter = 0;

		foreach ( var ability in abilities ) {

			// new AvtionBar
			var id = ability.abilityID;
			var args = new Object[] { id };

			void AbilityCallback(object[] args) {
				Debug.Log($"AbilityCallback {args[0]}");
				_callBackAction(( int )args[0]);
			}

			_actionBar.actionButtons[counter++]
				.BindAction(AbilityCallback, args, ability.icon, ability.name);
		}

		RefreshStats(player);
		//TODO: Hier die Stats einbauen, für den ausgewählten Spieler
	}

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
	}

	void ShowMenu() {
		visibilityMenuEventChannel.RaiseEvent(true);
	}

	void HandlePlayerDeselected(GameObject obj) {
		_actionBar.SetVisibility(false);
		_characterStatusValuePanel.SetViibility(false);
	}

	void ShowPlayerViewContainer() {
		_characterStatusValuePanel.SetViibility(true);
	}

	void HidePlayerViewContainer() {
		_characterStatusValuePanel.SetViibility(false);
	}

	void ShowActionMenu() {
		_actionBar.SetVisibility(true);
	}
}