using Events.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameMenuUIController : MonoBehaviour {
    
	
	[Header("Loading settings")] 
	[SerializeField] private GameSceneSO[] menuToLoad;
	[SerializeField] private LoadEventChannelSO loadMenuEC;
	
	private VisualElement _inGameMenuContainer;

    [Header("Receiving Events On")] [SerializeField]
    private BoolEventChannelSO visibilityMenuEventChannel;

    [SerializeField] private BoolEventChannelSO visibilityInventoryEventChannel;

    [Header("Sending Events On")] [SerializeField]
    private VoidEventChannelSO enableMenuInput;

    [Header("Sending and Receiving Events On")] [SerializeField]
    private BoolEventChannelSO visibilityGameOverlayEventChannel;

    // In Game Menu Screens 
    private enum MenuScreenContent {
        None,
        LoadScreen,
        SaveScreen,
        SettingsScreen
    }

    private void Start() {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        _inGameMenuContainer = root.Q<VisualElement>("IngameMenu");
        _inGameMenuContainer.Q<Button>("SaveButton").clicked += ShowSaveScreen;
        _inGameMenuContainer.Q<Button>("OptionsButton").clicked += ShowOptionsScreen;
        _inGameMenuContainer.Q<Button>("LoadButton").clicked += ShowLoadScreen;

        _inGameMenuContainer.Q<Button>("ResumeButton").clicked += HideMenu;
        _inGameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;

        _inGameMenuContainer.Q<Button>("MainMenuButton").clicked += MainMenuButtonPressed;
    }

    void HideMenu() {
        visibilityGameOverlayEventChannel.RaiseEvent(true);
    }

    private void Awake() {
        visibilityMenuEventChannel.OnEventRaised += HandleGameOverlay;
        visibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
        visibilityGameOverlayEventChannel.OnEventRaised += HandleOtherScreensOpened;
    }

    void HandleGameOverlay(bool value) {
        if (value) {
            enableMenuInput.RaiseEvent();
            _inGameMenuContainer.style.display = DisplayStyle.Flex;
        }
        else {
            _inGameMenuContainer.style.display = DisplayStyle.None;
            //HideMenu();
        }
    }

    void HandleOtherScreensOpened(bool value) {
        HandleGameOverlay(false);
    }

    void ShowSaveScreen() {
        MenuScreenContentManager(MenuScreenContent.SaveScreen);
    }

    void ShowLoadScreen() {
        MenuScreenContentManager(MenuScreenContent.LoadScreen);
    }

    void ShowOptionsScreen() {
        MenuScreenContentManager(MenuScreenContent.SettingsScreen);
    }

    // todo(vincent) refactor
    void MenuScreenContentManager(MenuScreenContent menuScreen) {
        // Einzelne Screens getten
        VisualElement saveScreen = _inGameMenuContainer.Q<VisualElement>("SaveScreen");
        VisualElement loadScreen = _inGameMenuContainer.Q<VisualElement>("LoadScreen");
        VisualElement settingsScreen = _inGameMenuContainer.Q<VisualElement>("SettingsContainer");

        switch (menuScreen) {
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
}