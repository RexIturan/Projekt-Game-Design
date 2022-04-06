using Events.ScriptableObjects;
using GameManager.Provider;
using GDP01.SceneManagement.EventChannels;
using GDP01.UI;
using SaveSystem;
using SceneManagement.ScriptableObjects;
using SceneManagement.Types;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUIController : MonoBehaviour {
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
	[SerializeField] private SceneLoadingInfoEventChannelSO loadSceneEC;
	[SerializeField] private SceneLoadingInfoEventChannelSO unloadSceneEC;
	
	[SerializeField] private bool showLoadLevel;
	[SerializeField] private bool showSaveLevel;
	[SerializeField] private bool showOptionsLevel;

	[SerializeField] private GameSceneSO mainMenuSceneData;
	[SerializeField] private GameSceneSO gameplaySceneData;
	
	// screenmanager reference
	[Header("Screen Handeling")] 
	[SerializeField] private ScreenManager screenManager;
	
///// Private Variables	////////////////////////////////////////////////////////////////////////////
	private VisualElement _pauseMenuContainer;
	private VisualElement _dialogueComponentLayer;
	
	private Button _saveButton;
	private Button _optionsButton;
	private Button _controllsButton;
	private Button _loadButton;
	private Button _backToMenuButton;
	private Button _quitButton;
	private Button _resumeButton;
	
///// Private Functions	////////////////////////////////////////////////////////////////////////////

	private void BindElements() {
		
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		_pauseMenuContainer = GetComponent<UIDocument>().rootVisualElement;
		_dialogueComponentLayer = _pauseMenuContainer.Q<VisualElement>("DialogueLayer");
		_saveButton = _pauseMenuContainer.Q<Button>("SaveButton");
		_optionsButton = _pauseMenuContainer.Q<Button>("OptionsButton");
		_controllsButton = _pauseMenuContainer.Q<Button>("ControllsButton");
		_loadButton = _pauseMenuContainer.Q<Button>("LoadButton");
		_backToMenuButton = _pauseMenuContainer.Q<Button>("MainMenuButton");
		_resumeButton = _pauseMenuContainer.Q<Button>("ResumeButton");
		_quitButton = _pauseMenuContainer.Q<Button>("QuitButton");
		
		_saveButton.clicked += HandleSave;
		_optionsButton.clicked += ShowOptionsScreen;
		_controllsButton.clicked += HandleControllsButton;
		_loadButton.clicked += HandleLoad;
		_backToMenuButton.clicked += HandleMainMenuButton;
		_resumeButton.clicked += HandleResumeButton;
		_quitButton.clicked += HandleQuitGame;

		SetElementVisibility(_saveButton, showSaveLevel);
		
		SetElementVisibility(_optionsButton, showOptionsLevel);
		//todo doesnt work for now!
		SetElementVisibility(_loadButton, false);
		// SetElementVisibility(_backToMenuButton, false);

		ClearDialogue();
	}

	private void UnbindElements() {
		ClearDialogue();

		_saveButton.clicked -= HandleSave;
		_optionsButton.clicked -= ShowOptionsScreen;
		_controllsButton.clicked -= HandleControllsButton;
		_loadButton.clicked -= HandleLoad;
		_backToMenuButton.clicked -= HandleMainMenuButton;
		_resumeButton.clicked -= HandleResumeButton;
		_quitButton.clicked -= HandleQuitGame;
		
		_dialogueComponentLayer = null;
		_saveButton = null;
		_optionsButton = null;
		_controllsButton = null;
		_loadButton = null;
		_backToMenuButton = null;
		_resumeButton = null;
		_quitButton = null;
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

	//todo refactor
	private void MenuScreenContentManager(MenuScreenContent menuScreen) {
		// Einzelne Screens getten
		VisualElement saveScreen = _pauseMenuContainer.Q<VisualElement>("SaveScreen");
		VisualElement loadScreen = _pauseMenuContainer.Q<VisualElement>("LoadScreen");
		VisualElement settingsScreen = _pauseMenuContainer.Q<VisualElement>("SettingsContainer");

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

	void HandleMainMenuButton() {
		// load Scene
		// loadMenuEC.RaiseEvent(menuToLoad, true);
		
		GameStateProvider.Current.GameSC.backToMainMenu = true;
		
		var loadingData = new SceneLoadingData {
			MainSceneData = mainMenuSceneData,
			ActivateOnLoad = true,
			DontUnload = false
		};
			
		loadSceneEC.RaiseEvent(loadingData);
		unloadSceneEC.RaiseEvent(new SceneLoadingData{ MainSceneData = gameplaySceneData });
		
		//todo new scene loading
	}

	void QuitGame() {
		// Spiel beenden
		Application.Quit();
		
		//todo quitgame event channel
	}

	private void SetMenuVisibility(bool menuVisible) {
		if ( menuVisible ) {
			_pauseMenuContainer.style.display = DisplayStyle.Flex;
		}
		else {
			_pauseMenuContainer.style.display = DisplayStyle.None;
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

	private void HandleControllsButton() {
		_dialogueComponentLayer.Add(new AffirmationDialogue(
				"Controlls",
				"Camera movement: WASD\n" +
				"Camera rotation: Q/E\n" +
				"Character selection: left click\n" +
				"Cancel/Deselect: right click",
				ClearDialogue, "Got it",
				null, ""));

		_dialogueComponentLayer.visible = true;
		_dialogueComponentLayer.style.display = DisplayStyle.Flex;
		_dialogueComponentLayer.pickingMode = PickingMode.Position;
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
	
	void HandleQuitGame() {
		QuitGame();
	}
	
///// Public Functions	////////////////////////////////////////////////////////////////////////////

///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

	private void OnEnable() {
		BindElements();
		// HideMenu();
		
		SetMenuVisibilityEC.OnEventRaised += HandleMenuToggleEvent;
	}
	
	private void OnDisable() {
		SetMenuVisibilityEC.OnEventRaised -= HandleMenuToggleEvent;
		
		UnbindElements();
	}
}