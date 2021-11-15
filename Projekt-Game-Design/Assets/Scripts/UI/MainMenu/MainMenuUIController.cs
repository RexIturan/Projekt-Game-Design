using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine.UIElements;
using UnityEngine;


public class MainMenuUIController : MonoBehaviour
{
	//todo move to LevelEditorLoadingButton or so
	[Header("Recieving Event On")] 
	[SerializeField] private LoadEventChannelSO loadLocationEC;
	[SerializeField] private GameSceneSO[] levelEditor;
	
    private Button _startButton;
    private Button _loadLevelButton;
    private Button _levelEditorButton;
    private Button _settingsButton;
    private Button _exitButton;
    private Button _backButton;
    private VisualElement _menuContainer;
    private VisualElement _settingsContainer;
    private VisualElement _loadGame;
    // todo remove from here
    private TemplateContainer _loadTestLevelScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        // Holen der Buttons
        _startButton = root.Q<Button>("startButton");
        _loadLevelButton = root.Q<Button>("loadLevelButton");
        _settingsButton = root.Q<Button>("settingsButton");
        _exitButton = root.Q<Button>("exitButton");
        _backButton = root.Q<Button>("backButton");
        
        // Holen der Menü Container
        _menuContainer = root.Q<VisualElement>("menuContainer");
        _settingsContainer = root.Q<VisualElement>("SettingsContainer");
        _loadGame = root.Q<VisualElement>("LoadScreen");
        
        // Level Editor Button
        _levelEditorButton = root.Q<Button>("LevelEditorButton");
        _levelEditorButton.clicked += () => {
	        loadLocationEC.RaiseEvent(levelEditor);
        };
        
        // TODO move to injection point
        // load testlevel stuff
        _loadTestLevelScreen = root.Q<TemplateContainer>("LoadTestLevelScreen");
        root.Q<Button>("loadTestLevelButton").clicked += () => {
            _loadTestLevelScreen.visible = true;
            _menuContainer.visible = false;
        };
        _loadTestLevelScreen.Q<Button>("BackButton").clicked += () => {
            _loadTestLevelScreen.visible = false;
            _menuContainer.visible = true;
        };
        
        _startButton.clicked += StartButtonPressed;
        _exitButton.clicked += QuitGame;
        _backButton.clicked += BackButtonPressed;
        _settingsButton.clicked += SettingsButtonPressed;
        _loadLevelButton.clicked += LoadLevelButtonPressed;
        
        // todo move to lode game screen
        _loadGame.Q<Button>("BackButton").clicked += BackButtonLoadGamePressed;
    }
    
    void SettingsButtonPressed()
    {
        // Menü ausblenden und Einstellungen zeigen
        _menuContainer.style.display = DisplayStyle.None;
        _settingsContainer.style.display = DisplayStyle.Flex;
    }
    
    void LoadLevelButtonPressed()
    {
        // Menü ausblenden und Einstellungen zeigen
        _menuContainer.style.display = DisplayStyle.None;
        _loadGame.style.display = DisplayStyle.Flex;
    }
    
    void BackButtonPressed()
    {
        // Einstellungen ausblenden und Menü zeigen
        _menuContainer.style.display = DisplayStyle.Flex;
        _settingsContainer.style.display = DisplayStyle.None;
    }
    
    void BackButtonLoadGamePressed()
    {
        // Einstellungen ausblenden und Menü zeigen
        _menuContainer.style.display = DisplayStyle.Flex;
        _loadGame.style.display = DisplayStyle.None;
    }

    void StartButtonPressed()
    {
        // Szene laden
        // SceneManager.LoadScene("GameDesign");
    }
    
    void QuitGame()
    {
        // Spiel beenden
        Application.Quit();
        // TODO dont do it here, but with a quit game event channel
    }

}
