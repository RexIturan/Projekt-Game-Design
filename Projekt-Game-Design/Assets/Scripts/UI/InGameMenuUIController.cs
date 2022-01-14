using System;
using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameMenuUIController : MonoBehaviour {
	// In Game Menu Screens 
	private enum MenuScreenContent {
		None,
		LoadScreen,
		SaveScreen,
		SettingsScreen
	}

	[Header("Loading settings")] 
	[SerializeField] private GameSceneSO[] menuToLoad;
	[SerializeField] private LoadEventChannelSO loadMenuEC;

	[Header("Receiving Events On")] 
	[SerializeField] private BoolEventChannelSO SetMenuVisibilityEC;

	[Header("Sending Events On")]
	// [SerializeField] private BoolEventChannelSO SetGameOverlayVisibilityEC;
	[SerializeField] private VoidEventChannelSO uiToggleMenuEC;
	//todo move to ui cache v-----------------------------------v
	[SerializeField] private VoidEventChannelSO enableMenuInput;
	[SerializeField] private VoidEventChannelSO menuOpenedEvent;
	[SerializeField] private VoidEventChannelSO menuClosedEvent;
	

///// Private Variables	////////////////////////////////////////////////////////////////////////////
	private VisualElement _inGameMenuContainer;
	
///// Private Functions	////////////////////////////////////////////////////////////////////////////

	//todo refactor
	private void MenuScreenContentManager(MenuScreenContent menuScreen) {
		// Einzelne Screens getten
		VisualElement saveScreen = _inGameMenuContainer.Q<VisualElement>("SaveScreen");
		VisualElement loadScreen = _inGameMenuContainer.Q<VisualElement>("LoadScreen");
		VisualElement settingsScreen = _inGameMenuContainer.Q<VisualElement>("SettingsContainer");

		switch ( menuScreen ) {
			case MenuScreenContent.LoadScreen:
				// todo(vincent) GUI refactor 
				loadScreen.style.display = DisplayStyle.Flex;
				// Ausblenden aller anderen Screens
				settingsScreen.style.display = DisplayStyle.None;
				saveScreen.style.display = DisplayStyle.None;
				break;
			case MenuScreenContent.SaveScreen:
				saveScreen.style.display = DisplayStyle.Flex;
				// Ausblenden aller anderen Screens
				settingsScreen.style.display = DisplayStyle.None;
				loadScreen.style.display = DisplayStyle.None;
				break;
			case MenuScreenContent.SettingsScreen:
				settingsScreen.style.display = DisplayStyle.Flex;
				// Ausblenden aller anderen Screens
				saveScreen.style.display = DisplayStyle.None;
				loadScreen.style.display = DisplayStyle.None;
				break;
			case MenuScreenContent.None:
				settingsScreen.style.display = DisplayStyle.None;
				saveScreen.style.display = DisplayStyle.None;
				loadScreen.style.display = DisplayStyle.None;
				break;
		}
	}

	void MainMenuButtonPressed() {
		// load Scene
		loadMenuEC.RaiseEvent(menuToLoad, true);
	}

	void QuitGame() {
		// Spiel beenden
		Application.Quit();
	}

	private void SetMenuVisibility(bool menuVisible) {
		if ( menuVisible ) {
			_inGameMenuContainer.style.display = DisplayStyle.Flex;
		}
		else {
			_inGameMenuContainer.style.display = DisplayStyle.None;
		}
	}
	
	void HideMenu() {
		SetMenuVisibility(false);
	}

///// Callbacks	////////////////////////////////////////////////////////////////////////////////////

	private void HandleMenuToggleEvent(bool value) {
		SetMenuVisibility(value);
		
		if ( value ) {
			menuOpenedEvent.RaiseEvent();
			enableMenuInput.RaiseEvent();
		}
		else {
			//todo maybe Ui statecache??
			menuClosedEvent.RaiseEvent();
		}
	}

	private void HandleResumeButton() {
		uiToggleMenuEC.RaiseEvent();
	}

	private void ShowSaveScreen() {
		MenuScreenContentManager(MenuScreenContent.SaveScreen);
	}

	private void ShowLoadScreen() {
		MenuScreenContentManager(MenuScreenContent.LoadScreen);
	}

	private void ShowOptionsScreen() {
		MenuScreenContentManager(MenuScreenContent.SettingsScreen);
	}

///// Public Functions	////////////////////////////////////////////////////////////////////////////

///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

	private void Awake() {
		SetMenuVisibilityEC.OnEventRaised += HandleMenuToggleEvent;
	}

	private void OnDisable() {
		SetMenuVisibilityEC.OnEventRaised -= HandleMenuToggleEvent;
	}

	private void Start() {
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		var root = GetComponent<UIDocument>().rootVisualElement;
		_inGameMenuContainer = root.Q<VisualElement>("IngameMenu");
		_inGameMenuContainer.Q<Button>("SaveButton").clicked += ShowSaveScreen;
		_inGameMenuContainer.Q<Button>("OptionsButton").clicked += ShowOptionsScreen;
		_inGameMenuContainer.Q<Button>("LoadButton").clicked += ShowLoadScreen;

		_inGameMenuContainer.Q<Button>("ResumeButton").clicked += HandleResumeButton;
		_inGameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;

		_inGameMenuContainer.Q<Button>("MainMenuButton").clicked += MainMenuButtonPressed;
	}
}