using System;
using Events.ScriptableObjects;
using GDP01.UI;
using SceneManagement.ScriptableObjects;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct MainMenuButtonNames {
	public string startGame;
	public string loadLevel;
	public string loadTestLevel;
	public string levelEditor;
	public string settings;
	public string exit;
}

public class MainMenuUIController : MonoBehaviour {
	//todo move to LevelEditorLoadingButton or so
	[Header("Scene Loading")] 
	[SerializeField] private LoadEventChannelSO loadLocationEC;
	[SerializeField] private GameSceneSO[] levelEditor;

	[Header("Sending Event On")] [SerializeField]
	private IntEventChannelSO loadGame;

	[Header("Button Settings")]
	[SerializeField] private MainMenuButtonNames buttonNames;
	[SerializeField] private bool showSettings;
	[SerializeField] private bool showLevelEditor;
	[SerializeField] private bool showTestLevel;
	[SerializeField] private bool showDebugMessages = false;

	[Header("Screen Handeling")] 
	[SerializeField] private ScreenManager screenManager;
	[SerializeField] private ScreenController mainMenuScreen;
	[SerializeField] private ScreenController settingsScreen;
	
///// Private Variables ////////////////////////////////////////////////////////////////////////////	
	
	private Button _startGameButton;
	private Button _loadLevelButton;
	private Button _loadTestLevelButton;
	private Button _levelEditorButton;
	private Button _settingsButton;
	private Button _exitButton;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

 	
	
///// Private Functions ////////////////////////////////////////////////////////////////////////////

	private void UnbindButton(Button button, Action action) {
		if ( button is { } ) {
			button.clicked -= action;	
		} else {
			Debug.LogError($"UnbindButton\n{name} Button not Found.");
		}
	}

	private void BindButton(ref Button button, VisualElement root, string name, Action action) {
		button = root.Q<Button>(name);
		if ( button is { } ) {
			button.clicked += action;	
		} else {
			Debug.LogError($"BindButton\n{name} Button not Found.");
		}
	} 

	private void BindButtons() {
		var root = GetComponent<UIDocument>().rootVisualElement;
		BindButton(ref     _startGameButton, root, buttonNames.startGame, HandleNewGame);
		BindButton(ref     _loadLevelButton, root, buttonNames.loadLevel, HandleLoadLevel);
		BindButton(ref _loadTestLevelButton, root, buttonNames.loadTestLevel, HandleTestLevelButton);
		BindButton(ref   _levelEditorButton, root, buttonNames.levelEditor, HandleLevelEditorButton);
		BindButton(ref      _settingsButton, root, buttonNames.settings, HandleSettingsButton);
		BindButton(ref          _exitButton, root, buttonNames.exit, HandleExitButton);
	}

	private void UnbindButtons() {
		UnbindButton(    _startGameButton, HandleNewGame);
		UnbindButton(    _loadLevelButton, HandleLoadLevel);
		UnbindButton(_loadTestLevelButton, HandleLoadLevel);
		UnbindButton(  _levelEditorButton, HandleLevelEditorButton);
		UnbindButton(     _settingsButton, HandleSettingsButton);
		UnbindButton(         _exitButton, HandleExitButton);
	}
	
	private void SetElementVisibility(VisualElement element, bool visible) {
		element.style.visibility = visible
			? new StyleEnum<Visibility>(Visibility.Visible)
			: new StyleEnum<Visibility>(Visibility.Hidden);
	}
	//
	// void LoadLevelButtonPressed() {
	// 	// Menü ausblenden und Einstellungen zeigen
	// 	_menuContainer.style.display = DisplayStyle.None;
	// 	_loadGame.style.display = DisplayStyle.Flex;
	// }
	//
	// void BackButtonPressed() {
	// 	// Einstellungen ausblenden und Menü zeigen
	// 	_menuContainer.style.display = DisplayStyle.Flex;
	// 	_settingsContainer.style.display = DisplayStyle.None;
	// }
	//
	// void BackButtonLoadGamePressed() {
	// 	// Einstellungen ausblenden und Menü zeigen
	// 	_menuContainer.style.display = DisplayStyle.Flex;
	// 	_loadGame.style.display = DisplayStyle.None;
	// }

	void QuitGame() {
		
		//todo send Quit Game Event
		
		// Spiel beenden
		Application.Quit();
		// TODO dont do it here, but with a quit game event channel
	}

///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

	private void HandleNewGame() {
		if ( showDebugMessages ) {
			Debug.Log("New Game Button Pressed");	
		}
		
		loadGame.RaiseEvent(0);
	}

	private void HandleLoadLevel() {
		if ( showDebugMessages ) {
			Debug.Log("LoadLevel Button Pressed");	
		}
		
		// loadGame.RaiseEvent(1);
	}
	
	private void HandleLevelEditorButton() {
		if ( showDebugMessages ) {
			Debug.Log("LevelEditor Button Pressed");	
		}
		
		// loadLocationEC.RaiseEvent(levelEditor);
	}

	private void HandleTestLevelButton() {
		if ( showDebugMessages ) {
			Debug.Log("Test Level Button Pressed");	
		}
		
		// _loadTestLevelScreen.visible = true;
		// _menuContainer.visible = false;
	}
	
	private void HandleBackButton() {
		if ( showDebugMessages ) {
			Debug.Log("Back Button Pressed");	
		}
		
		// _loadTestLevelScreen.visible = false;
		// _menuContainer.visible = true;
	}
	
	private void HandleSettingsButton() {
		if ( showDebugMessages ) {
			Debug.Log("Settings Button Pressed");	
		}
		
		// tell the screen manager to switch this off and settings on
		screenManager.SetScreenVisibility(settingsScreen, true);
		
		// Menü ausblenden und Einstellungen zeigen
		// _menuContainer.style.display = DisplayStyle.None;
		// _settingsContainer.style.display = DisplayStyle.Flex;
	}
	
	private void HandleExitButton() {
		if ( showDebugMessages ) {
			Debug.Log("Exit Button Pressed");	
		}
		
		// QuitGame();
	}
	
///// Public Functions /////////////////////////////////////////////////////////////////////////////	

	public void UpdateVisibilities() {
		SetElementVisibility(_settingsButton, showSettings);
		SetElementVisibility(_loadTestLevelButton, showTestLevel);
		SetElementVisibility(_levelEditorButton, showLevelEditor);
	}

///// Unity Functions //////////////////////////////////////////////////////////////////////////////

	// Start is called before the first frame update
	private void OnEnable() {
		BindButtons();

		UpdateVisibilities();
		
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		// var root = GetComponent<UIDocument>().rootVisualElement;

		// Holen der Buttons
		// _startButton = root.Q<Button>("startButton");
		// _loadLevelButton = root.Q<Button>("loadLevelButton");
		// _settingsButton = root.Q<Button>("settingsButton");
		// _exitButton = root.Q<Button>("exitButton");
		// _backButton = root.Q<Button>("backButton");

		// Holen der Menü Container
		// _loadGame = root.Q<VisualElement>("LoadScreen");

		// Level Editor Button
		// _levelEditorButton = root.Q<Button>("LevelEditorButton");
		// _levelEditorButton.clicked += HandleLevelEditorButton; 

		// TODO move to injection point
		// load testlevel stuff
		// _loadTestLevelScreen = root.Q<TemplateContainer>("LoadTestLevelScreen");
		// var testLevelButton = root.Q<Button>("LoadTestLevelButton");
		// testLevelButton.clicked += HandleTestLevelButton;
		//
		// if ( _loadTestLevelScreen is {} ) {
		// 	_loadTestLevelScreen.Q<Button>("BackButton").clicked += HandleBackButton;	
		// }

		
		// _startGameButton.clicked += HandleStartGameGame;
		// _loadLevelButton.clicked += HandleLoadLevel;
		// _exitButton.clicked += HandleExitButton;
		//
		// _backButton.clicked += BackButtonPressed;
		// _settingsButton.clicked += HandleSettingsButton;

		
		//
		// //todo refactor & remove magic string
		// if ( !FileManager.FileExists("tutorial0") ) {
		// 	SetElementVisibility(_loadLevelButton, false);
		// }

		// todo move to lode game screen
		// _loadGame.Q<Button>("BackButton").clicked += BackButtonLoadGamePressed;
	}

	private void OnDisable() {
		UnbindButtons();
	}
}