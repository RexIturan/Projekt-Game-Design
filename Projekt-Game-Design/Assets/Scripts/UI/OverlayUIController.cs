using System;
using System.Collections.Generic;
using Characters;
using Characters.Ability;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using UI.Components;
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
	private VisualElement _playerViewContainer;

	private TemplateContainer _turnIndicator;

	// Callbackfunktion für die Abilitys
	private Action<int> _callBackAction;


	private void Awake() {
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		var root = GetComponent<UIDocument>().rootVisualElement;
		_actionBar = root.Q<ActionBar>("ActionBar");
		_overlayContainer = root.Q<VisualElement>("OverlayContainer");
		_playerViewContainer = root.Q<VisualElement>("PlayerViewContainer");
		_turnIndicator = root.Q<TemplateContainer>("TurnIndicator");
		_overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
		_overlayContainer.Q<Button>("EndTurnButton").clicked += HandleEndTurnUI;
		visibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
		visibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
		showGameOverlayEC.OnEventRaised += SetGameOverlayVisibility;
		showTurnIndicatorEC.OnEventRaised += SetTurnIndicatorVisibility;
		playerDeselectedEC.OnEventRaised += HandlePlayerDeselected;

		playerSelectedEC.OnEventRaised += HandlePlayerSelected;
	}

	private void HandleEndTurnUI() {
		endTurnEC.RaiseEvent(Faction.Player);
	}

	void SetTurnIndicatorVisibility(bool show) {
		_turnIndicator.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
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
		//VisualElement manaBar = PlayerViewContainer.Q<VisualElement>("ProgressBarManaOverlay");
		VisualElement healthBar = _playerViewContainer.Q<VisualElement>("ProgressBarHealthOverlay");
		VisualElement abilityBar = _playerViewContainer.Q<VisualElement>("ProgressBarAbilityOverlay");

		var statistics = obj.GetComponent<Statistics>();
		var chracterStats = statistics.StatusValues;

		healthBar.style.width =
			new StyleLength(
				Length.Percent(( 100 * ( float )chracterStats.HitPoints.value / chracterStats.HitPoints.max )));
		
		abilityBar.style.width =
			new StyleLength(
				Length.Percent(( 100 * ( float )chracterStats.Energy.value / chracterStats.Energy.max )));
		
		//manaBar.style.width = new StyleLength(Length.Percent((100* (float)playerSC.EnergyPoints/playerStats.maxEnergy)));

		// Labels für Stats
		_playerViewContainer.Q<Label>("StrengthLabel").text = chracterStats.Strength.value.ToString();
		_playerViewContainer.Q<Label>("DexterityLabel").text = chracterStats.Dexterity.value.ToString();
		_playerViewContainer.Q<Label>("IntelligenceLabel").text = chracterStats.Intelligence.value.ToString();
		_playerViewContainer.Q<Label>("MovementLabel").text = chracterStats.ViewDistance.value.ToString();

		// Image
		VisualElement image = _playerViewContainer.Q<VisualElement>("PlayerPicture");
		image.Clear();
		Image newProfile = new Image();
		image.Add(newProfile);
		newProfile.image = statistics.DisplayImage.texture;

		// Name and Level
		_playerViewContainer.Q<Label>("PlayerName").text = statistics.DisplayName;
		_playerViewContainer.Q<Label>("LevelLabel").text = chracterStats.Level.value.ToString();
	}

	void ShowMenu() {
		visibilityMenuEventChannel.RaiseEvent(true);
	}

	void HandlePlayerDeselected(GameObject obj) {
		_actionBar.SetVisibility(false);

		_playerViewContainer.style.display = DisplayStyle.None;
	}

	void ShowPlayerViewContainer() {
		_playerViewContainer.style.display = DisplayStyle.Flex;
	}

	void HidePlayerViewContainer() {
		_playerViewContainer.style.display = DisplayStyle.None;
	}

	void ShowActionMenu() {
		_actionBar.SetVisibility(true);
	}
}