using System;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Characters.Types;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using GDP01;
using GDP01.Characters.Component;
using GDP01.Player.Player;
using GDP01.UI.Components;
using Input;
using UI.Components.Character;
using UnityEngine;
using UnityEngine.UIElements;
using Util.Extensions;
using Object = System.Object;

public class OverlayUIController : MonoBehaviour {
	[Header("Receiving Events On")] 
	[SerializeField] private GamePhaseEventChannelSO gamePhaseAnnouncementEC;
	[SerializeField] private BoolEventChannelSO setGameOverlayVisibilityEC;
	[SerializeField] private BoolEventChannelSO setTurnIndicatorVisibilityEC;
	[SerializeField] private EquipItemEC_SO EquipEvent;
	[SerializeField] private UnequipItemEC_SO UnequipEvent;
	[SerializeField] private GameObjEventChannelSO playerDeselectedEC;
	[SerializeField] private GameObjActionIntEventChannelSO playerSelectedEC;
	[SerializeField] private GameObjEventChannelSO playerSelectedPreviewEC; // selected in player controller, but not in selected state

	[SerializeField] private VoidEventChannelSO unfocusActionButtonEC;
	
	[SerializeField] private VoidEventChannelSO abilityConfirmedEC;
	[SerializeField] private VoidEventChannelSO abilityExecutedEC;
	[SerializeField] private VoidEventChannelSO previewChangedEC;
	// [SerializeField] private Vector3IntEventChannelSO targetChangedEC;

	[Header("Sending Events On")] 
	[SerializeField] private VoidEventChannelSO enableGamplayInput;
	[SerializeField] private EFactionEventChannelSO endTurnEC;
	[SerializeField] private EFactionEventChannelSO newTurnEC;
	[SerializeField] private VoidEventChannelSO uiToggleMenuEC;

	// [Header("Sending and Receiving Events On")] [SerializeField]
	// private BoolEventChannelSO setMenuVisibilityEC;

	[SerializeField] private InputReader inputReader;

///// Private Variables ////////////////////////////////////////////////////////////////////////////	
	
	// Für die UI Elemente
	private VisualElement _overlayContainer;

	// Action Container
	private ActionBar _actionBar;

	// End Turn Button
	private Button _endTurnButton;

	// PlayerView Container
	private CharacterStatusValuePanel _characterStatusValuePanel;

	// Zur Identifikation des gewaehlten Spielers
	private GameObject _selectedPlayer;

	private GameObject _selectedPlayerPreview;

	private TemplateContainer _turnIndicator;

	// Callbackfunktion für die Abilitys
	private Action<int> _callBackAction;

	// Game state variables (TODO: Maybe don't save them here, but query them dynamically instead?)
	private GamePhase currentGamePhase;
	private bool executionInProgress;

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
	
	private void FlushAbilityCallbacks() {
		_actionBar.ResetActionButtons();
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


	private void UpdateStats() {
		UpdateStats(_selectedPlayerPreview);
	}

	//todo move to own class
	private void UpdateStats(GameObject obj) {

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
		MovementController movementController = statistics.GetComponent<MovementController>();
	
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
	
		movementField.Value = movementController.GetMaxTileMoveDistance();
		movementField.UpdateComponents();
		
		visionField.Value = chracterStats.ViewDistance.Value;
		visionField.UpdateComponents();
	}
	
	
	private void ShowPlayerViewContainer() {
		_characterStatusValuePanel.SetVibility(true);
	}
	
	private void HidePlayerViewContainer() {
		_characterStatusValuePanel.SetVibility(false);
	}
	
	private void ShowActionBar() {
		_actionBar.SetVisibility(true);
	}

	//todo(vincent) move to action bar controller
	private void UpdateActionBar() {
		if(_selectedPlayer is {}) {
			FlushAbilityCallbacks();

			var abilityController = _selectedPlayer.GetComponent<AbilityController>();
			List<AbilitySO> abilities =
				new List<AbilitySO>(abilityController.Abilities);
			int counter;

			//disable buttons
			// _actionBar.actionButtons.ForEach(actionButton => actionButton.Button.SetEnabled(false));

			for ( int i = 0; i < _actionBar.actionButtons.Count; i++ ) {
				var actionButton = _actionBar.actionButtons[i];
				if ( abilities.IsValidIndex(i) ) {
					// actionButton.Button.focusable = true;
					actionButton.Button.SetEnabled(true);
					
					var ability = abilities[i];
					var id = ability.id;
					var arguments = new Object[] { id, ability, abilityController };

					void OnFocusCallback(object[] args) {
						Debug.Log($"OnFocusCallback {args[0]}");
						_callBackAction(( int )args[0]);
						PreviewEnergy((AbilitySO)args[1], (AbilityController)args[2]);
					}
					
					void OnBlurCallback(object[] args) {
						Debug.Log($"OnBlurCallback {args[0]}");
						ClearPreviewEnergy();
					}

					if ( !abilityController.IsAbilityAvailable(ability) ) {
						actionButton.Button.SetEnabled(false);	
					}
					actionButton.SetupActionButton(ability, abilityController.GetCoolDown(ability));
					actionButton.BindOnClickedAction(OnFocusCallback, OnBlurCallback, arguments);
				}
				else {
					// actionButton.Button.focusable = false;
					actionButton.Button.SetEnabled(false);
				}
			}
			
			UpdateStats(_selectedPlayer);
			//TODO: Hier die Stats einbauen, für den ausgewählten Spieler
		}
	}

	private void ClearPreviewEnergy() {
		_characterStatusValuePanel.EnergyBar.ShowChange = false;
		_characterStatusValuePanel.EnergyBar.UpdateComponent();
	}
	
	private void PreviewEnergy(AbilitySO ability, AbilityController abilityController) {
		// todo calculate abilitycost somewhere else
		var energyCost = abilityController.GetCurrentAbilityCost(ability);
		if ( energyCost < 0 ) {
			Debug.Log($"{energyCost}");	
		}
		_characterStatusValuePanel.EnergyBar.ChangeValue = -energyCost;
		_characterStatusValuePanel.EnergyBar.ShowChange = true;
		_characterStatusValuePanel.EnergyBar.UpdateComponent();
	}
	
	private void UpdateEnergyPreview() {
		
		if ( _selectedPlayer is { } ) {
			var abilityController = _selectedPlayer.GetComponent<AbilityController>();
			if ( abilityController.IsAbilitySelected ) {
				PreviewEnergy(abilityController.SelectedAbility, abilityController);
			}
			else {
				ClearPreviewEnergy();
			}
		}
	}
	
	private void BindEnergyChangeToCharPanel() {
		var statistics = _selectedPlayerPreview.GetComponent<Statistics>();
		statistics.StatusValues.Energy.OnValueChanged += UpdateStats;
	}
	
	private void UnbindEnergyChangeToCharPanel() {
		var statistics = _selectedPlayerPreview?.GetComponent<Statistics>();
		if ( statistics is { } ) {
			statistics.StatusValues.Energy.OnValueChanged -= UpdateStats;	
		}
	}

	private void BindElements() {
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		var root = GetComponent<UIDocument>().rootVisualElement;
		_actionBar = root.Q<ActionBar>();
		_overlayContainer = root.Q<VisualElement>("OverlayContainer");
		_endTurnButton = _overlayContainer.Q<Button>("EndTurnButton");
		_characterStatusValuePanel = root.Q<CharacterStatusValuePanel>("CharacterStatusValuePanel");
		_turnIndicator = root.Q<TemplateContainer>("TurnIndicator");

		// _overlayContainer.Q<Button>("IngameMenuButton").clicked += HandleOpenMenuButton;
		_endTurnButton.clicked += HandleEndTurnUI;
		
		_actionBar.SetVisibility(false);
		_characterStatusValuePanel.SetVibility(false);
	}

	private void UnbindElements() {
		_overlayContainer.Q<Button>("EndTurnButton").clicked -= HandleEndTurnUI;
		
		_actionBar = null;
		_overlayContainer = null;
		_characterStatusValuePanel = null;
		_turnIndicator = null;
	}

	private void UpdateEndTurnButtonEnable() {
		_endTurnButton.SetEnabled(!executionInProgress && currentGamePhase.Equals(GamePhase.PLAYER_TURN));
	}

///// Callbacks	////////////////////////////////////////////////////////////////////////////////////

	private void HandleGamePhaseChange(GamePhase currentPhase) {
		currentGamePhase = currentPhase;
		UpdateEndTurnButtonEnable();
	}

	private void HandleEndTurnUI() {
		Debug.Log("EndTurn Pressed");
		
		endTurnEC.RaiseEvent(Faction.Player);
	}
	
	// private void HandleOtherScreensOpened(bool value) {
	// 	SetGameOverlayVisibility(!value);
	// }
	
	/// <summary>
	/// HandlePlayerSelected
	/// </summary>
	/// <param name="player">player Character Game Object</param>
	/// <param name="selectAction">callback to call when a action ist clicked</param>
	private void HandlePlayerSelected(GameObject player, Action<int> selectAction) {
		_selectedPlayer = player;
		_selectedPlayerPreview = player;
		_callBackAction = selectAction;

		BindEnergyChangeToCharPanel();
		UpdateActionBar();
		
		// Anzeigen der notwendigen Komponenten
		ShowActionBar();
		ShowPlayerViewContainer();
	}

	/// <summary>
	/// Updates the character panel only. 
	/// </summary>
	/// <param name="player">Selected player whose stats are displayed </param>
	private void HandlePlayerSelectedPreview(GameObject player) {
		_selectedPlayerPreview = player;
		
		UpdateStats();
		ShowPlayerViewContainer();
	}

	private void HandleOpenMenuButton() {
		uiToggleMenuEC.RaiseEvent();
	}

	private void HandlePlayerDeselected(GameObject obj) {
		UnbindEnergyChangeToCharPanel();
		
		if(obj == _selectedPlayer) { 
			_actionBar.SetVisibility(false);
			
			_characterStatusValuePanel.SetVibility(false);
		}
	}

	private void HandleSetGameOverlayVisibilityEC(bool value) {
		SetGameOverlayVisibility(value);
		UpdateActionBar();
	}

	private void HandleSetTurnIndicatorVisibilityEC(bool value) {
		SetTurnIndicatorVisibility(value);
	}
	
	private void HandleNewTurn(Faction faction) {
		UpdateActionBar();
	}

	private void HandleSelectAbility(int value) {
		//if player selected
		//value in range
		//todo 0-9 input ability 
	}
	
	private void HandleAbilityExecuted() {
		UpdateActionBar();
		ClearPreviewEnergy();

		executionInProgress = false;
		UpdateEndTurnButtonEnable();
	}
	
	private void HandlePreviewChanged() {
		UpdateEnergyPreview();
	}

	private void HandleAbilityConfirmed() {
		//clear Preview
		ClearPreviewEnergy();
		
		_actionBar.SetVisibility(false);
		
		executionInProgress = true;
		UpdateEndTurnButtonEnable();
	}
	
///// Public Functions	////////////////////////////////////////////////////////////////////////////

///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

	private void OnEnable() {
		BindElements();
		
		gamePhaseAnnouncementEC.OnEventRaised += HandleGamePhaseChange;

		setGameOverlayVisibilityEC.OnEventRaised += HandleSetGameOverlayVisibilityEC;
		setTurnIndicatorVisibilityEC.OnEventRaised += HandleSetTurnIndicatorVisibilityEC;

		playerSelectedEC.OnEventRaised += HandlePlayerSelected;
		playerDeselectedEC.OnEventRaised += HandlePlayerDeselected;
		playerSelectedPreviewEC.OnEventRaised += HandlePlayerSelectedPreview;

		unfocusActionButtonEC.OnEventRaised += _actionBar.UnfocusActionButtonsEC;

		abilityConfirmedEC.OnEventRaised += HandleAbilityConfirmed;
		abilityExecutedEC.OnEventRaised += HandleAbilityExecuted;
		previewChangedEC.OnEventRaised += HandlePreviewChanged;
		
		//todo updating ui when turn changes -> handle otherwise
		newTurnEC.OnEventRaised += HandleNewTurn;
		inputReader.SelectAbilityEvent += HandleSelectAbility;
		
		//todo maybe move to ui manager
		enableGamplayInput.RaiseEvent();

		//TODO find something better
		//reproduces the behaviour when a PC is selected -> if a pc is selected show the action bar
		if ( PlayerController.Current?.Selected is { } ) {
			var playerObj = PlayerController.Current.Selected.gameObject;
			var abilityController = playerObj.GetComponent<AbilityController>();
			HandlePlayerSelected(playerObj, abilityController.SelectAbility);
		}
	}

	private void OnDisable() {
		unfocusActionButtonEC.OnEventRaised -= _actionBar.UnfocusActionButtonsEC;
		
		UnbindElements();
		
		gamePhaseAnnouncementEC.OnEventRaised -= HandleGamePhaseChange;

		setGameOverlayVisibilityEC.OnEventRaised -= HandleSetGameOverlayVisibilityEC;
		setTurnIndicatorVisibilityEC.OnEventRaised -= HandleSetTurnIndicatorVisibilityEC;
		playerSelectedEC.OnEventRaised -= HandlePlayerSelected;
		playerDeselectedEC.OnEventRaised -= HandlePlayerDeselected;
		playerSelectedPreviewEC.OnEventRaised -= HandlePlayerSelectedPreview;
		
		abilityConfirmedEC.OnEventRaised -= HandleAbilityConfirmed;
		abilityExecutedEC.OnEventRaised -= HandleAbilityExecuted;
		previewChangedEC.OnEventRaised -= HandlePreviewChanged;
		
		//todo updating ui when turn changes -> handle otherwise
		newTurnEC.OnEventRaised -= HandleNewTurn;
		inputReader.SelectAbilityEvent -= HandleSelectAbility;
	}
}