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
	[Header("Receiving Events On")] 
	[SerializeField] private BoolEventChannelSO setGameOverlayVisibilityEC;
	[SerializeField] private BoolEventChannelSO setTurnIndicatorVisibilityEC;
	[SerializeField] private IntIntEquipmentPositionEquipEventChannelSO EquipEvent;
	[SerializeField] private IntEquipmentPositionUnequipEventChannelSO UnequipEvent;
	// [SerializeField] private BoolEventChannelSO visibilityInventoryEventChannel;

	// Action Menu
	[SerializeField] private GameObjEventChannelSO playerDeselectedEC;
	[SerializeField] private GameObjActionEventChannelSO playerSelectedEC;

	[Header("Sending Events On")] 
	[SerializeField] private VoidEventChannelSO enableGamplayInput;
	[SerializeField] private EFactionEventChannelSO endTurnEC;
	[SerializeField] private EFactionEventChannelSO newTurnEC;
	[SerializeField] private VoidEventChannelSO uiToggleMenuEC;

	[Header("Sending and Receiving Events On")] [SerializeField]
	// private BoolEventChannelSO setMenuVisibilityEC;

///// Private Variables ////////////////////////////////////////////////////////////////////////////	
	
	// Für die UI Elemente
	private VisualElement _overlayContainer;

	// Action Container
	private ActionBar _actionBar;

	// PlayerView Container
	private CharacterStatusValuePanel _characterStatusValuePanel;

	// Zur Identifikation des gewaehlten Spielers
	private GameObject _selectedPlayer;

	private TemplateContainer _turnIndicator;

	// Callbackfunktion für die Abilitys
	private Action<int> _callBackAction;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

	private void SetTurnIndicatorVisibility(bool show) {
		//todo fix this
		// _turnIndicator.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
	}
	
	private void SetGameOverlayVisibility(bool value) {
		if ( value ) {
			//todo move outside of here
			enableGamplayInput.RaiseEvent();
			_overlayContainer.style.display = DisplayStyle.Flex;
		}
		else {
			_overlayContainer.style.display = DisplayStyle.None;
		}
	}
	
	private void FlushAbilityListIcons() {
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
	
	
	
	//todo move to own class
	private void RefreshStats(GameObject obj) {
	
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
	
	
	private void ShowPlayerViewContainer() {
		_characterStatusValuePanel.SetViibility(true);
	}
	
	private void HidePlayerViewContainer() {
		_characterStatusValuePanel.SetViibility(false);
	}
	
	private void ShowActionMenu() {
		_actionBar.SetVisibility(true);
	}

	private void UpdateActionBar() {
		if(_selectedPlayer is {}) {
			FlushAbilityListIcons();
			List<AbilitySO> abilities =
				new List<AbilitySO>(_selectedPlayer.GetComponent<AbilityController>().Abilities);
			int counter = 0;

			foreach ( var ability in abilities ) {

				// new AvtionBar
				var id = ability.id;
				var args = new Object[] { id };

				void AbilityCallback(object[] args) {
					Debug.Log($"AbilityCallback {args[0]}");
					_callBackAction(( int )args[0]);
				}

				_actionBar.actionButtons[counter++]
					.BindAction(AbilityCallback, args, ability.icon, ability.name);
			}

			RefreshStats(_selectedPlayer);
			//TODO: Hier die Stats einbauen, für den ausgewählten Spieler
		}
	}
	
///// Callbacks	////////////////////////////////////////////////////////////////////////////////////

	private void HandleEndTurnUI() {
		endTurnEC.RaiseEvent(Faction.Player);
	}
	
	// private void HandleOtherScreensOpened(bool value) {
	// 	SetGameOverlayVisibility(!value);
	// }
	
	/// <summary>
	/// HandlePlayerSelected
	/// </summary>
	/// <param name="player">player Character Game Object</param>
	/// <param name="callBackAction">callback to call when a action ist clicked</param>
	private void HandlePlayerSelected(GameObject player, Action<int> callBackAction) {
		_selectedPlayer = player;
		_callBackAction = callBackAction;
		
		// Anzeigen der notwendigen Komponenten
		ShowActionMenu();
		ShowPlayerViewContainer();
		
		UpdateActionBar();
	}

	private void HandleOpenMenuButton() {
		uiToggleMenuEC.RaiseEvent();
	}

	private void HandlePlayerDeselected(GameObject obj) {
		if(obj == _selectedPlayer) { 
			_actionBar.SetVisibility(false);
			_characterStatusValuePanel.SetViibility(false);
		}
	}

	private void HandleSetGameOverlayVisibilityEC(bool value) {
		SetGameOverlayVisibility(value);
		UpdateActionBar();
	}

	private void HandleSetTurnIndicatorVisibilityEC(bool value) {
		SetTurnIndicatorVisibility(value);
	}
	
	private void HandleEndTurn(Faction faction) {
		UpdateActionBar();
	}
	
///// Public Functions	////////////////////////////////////////////////////////////////////////////

///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

	private void Start() {
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		var root = GetComponent<UIDocument>().rootVisualElement;
		_actionBar = root.Q<ActionBar>("ActionBar");
		_overlayContainer = root.Q<VisualElement>("OverlayContainer");
		_characterStatusValuePanel = root.Q<CharacterStatusValuePanel>("CharacterStatusValuePanel");
		_turnIndicator = root.Q<TemplateContainer>("TurnIndicator");

		_overlayContainer.Q<Button>("IngameMenuButton").clicked += HandleOpenMenuButton;
		_overlayContainer.Q<Button>("EndTurnButton").clicked += HandleEndTurnUI;
		
		_actionBar.SetVisibility(false);
		_characterStatusValuePanel.SetViibility(false);
	}

	private void Awake() {
		setGameOverlayVisibilityEC.OnEventRaised += HandleSetGameOverlayVisibilityEC;
		setTurnIndicatorVisibilityEC.OnEventRaised += HandleSetTurnIndicatorVisibilityEC;
		playerSelectedEC.OnEventRaised += HandlePlayerSelected;
		playerDeselectedEC.OnEventRaised += HandlePlayerDeselected;

		//todo updating ui when turn changes -> handle otherwise
		newTurnEC.OnEventRaised += HandleEndTurn;
	}

	

	private void OnDisable() {
		setGameOverlayVisibilityEC.OnEventRaised -= HandleSetGameOverlayVisibilityEC;
		setTurnIndicatorVisibilityEC.OnEventRaised -= HandleSetTurnIndicatorVisibilityEC;
		playerSelectedEC.OnEventRaised -= HandlePlayerSelected;
		playerDeselectedEC.OnEventRaised -= HandlePlayerDeselected;
		
		//todo updating ui when turn changes -> handle otherwise
		newTurnEC.OnEventRaised -= HandleEndTurn;
	}
}