using Events.ScriptableObjects;
using SaveSystem;
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

	[SerializeField] private IntEventChannelSO saveGame;
	[SerializeField] private IntEventChannelSO loadGame;
	[SerializeField] private bool showLoadLevel;
	[SerializeField] private bool showSaveLevel;
	[SerializeField] private bool showOptionsLevel;

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

	private void SetElementVisibility(VisualElement element, bool visible) {
		element.style.visibility = visible ? new StyleEnum<Visibility>(Visibility.Visible) : new StyleEnum<Visibility>(Visibility.Hidden);
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
	
	private void HandleLoad() {
		if ( !FileManager.FileExists("tutorial1") ) {
			loadGame.RaiseEvent(1);
		}
		else {
			loadGame.RaiseEvent(0);
		}
	}

	private void HandleSave() {
		saveGame.RaiseEvent(0);
	}
	
///// Public Functions	////////////////////////////////////////////////////////////////////////////

///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

	private void OnEnable() {
		
		//todo bind elements
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		var root = GetComponent<UIDocument>().rootVisualElement;
		_inGameMenuContainer = root.Q<VisualElement>("IngameMenu");
		var saveButton = _inGameMenuContainer.Q<Button>("SaveButton");
		var optionsButton = _inGameMenuContainer.Q<Button>("OptionsButton");
		var loadButton = _inGameMenuContainer.Q<Button>("LoadButton");
		var backToMenuButton = _inGameMenuContainer.Q<Button>("MainMenuButton");
		saveButton.clicked += HandleSave;
		optionsButton.clicked += ShowOptionsScreen;
		loadButton.clicked += HandleLoad;
		backToMenuButton.clicked += MainMenuButtonPressed;

		_inGameMenuContainer.Q<Button>("ResumeButton").clicked += HandleResumeButton;
		_inGameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;

		SetElementVisibility(saveButton, showSaveLevel);
		
		SetElementVisibility(optionsButton, showOptionsLevel);
		//todo doesnt work for now!
		SetElementVisibility(loadButton, false);
		SetElementVisibility(backToMenuButton, false);
		
		HideMenu();
		
		SetMenuVisibilityEC.OnEventRaised += HandleMenuToggleEvent;
	}
	
	private void OnDisable() {
		SetMenuVisibilityEC.OnEventRaised -= HandleMenuToggleEvent;
	}
}