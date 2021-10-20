using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI {
    public class LevelEditorUIController : MonoBehaviour {
        
        private Button _selectModeButton;
        private Button _paintModeButton;
        private Button _boxModeButton;
        private Button _fillModeButton;
        
        // private Button _upLevelButton;
        // private Button _downLevelButton;

        // menu button
        // private Button _menuButton;
        
        // container
        private VisualElement _levelEditorContainer;
        private VisualElement _ingameMenuContainer;

        [SerializeField] private LevelEditor.LevelEditor levelEditor;
        
        [Header("Receiving Events On")]
        [SerializeField] private BoolEventChannelSO visibilityMenuEventChannel;
    
        [Header("Sending Events On")]
        //input
        [SerializeField] private VoidEventChannelSO enableMenuInput;
        [SerializeField] private VoidEventChannelSO enableGamplayInput;
        // save manager
        [SerializeField] private VoidEventChannelSO saveLevel;
        [SerializeField] private VoidEventChannelSO loadLevel;
        
        private void Start() {
            var root = GetComponent<UIDocument>().rootVisualElement;

            _ingameMenuContainer = root.Q<VisualElement>("IngameMenu");
            _levelEditorContainer = root.Q<VisualElement>("LevelEditorContainer");
            
            _selectModeButton = root.Q<Button>("select");
            _paintModeButton = root.Q<Button>("paint");
            _boxModeButton = root.Q<Button>("box");
            _fillModeButton = root.Q<Button>("fill");
            
            // _upLevelButton = root.Q<Button>("up");
            // _downLevelButton = root.Q<Button>("down");

            
            _selectModeButton.clicked += HandleSelectModusButtonClicked;
            _paintModeButton.clicked += HandlePaintModusButtonClicked;
            _boxModeButton.clicked += HandleBoxModusButtonClicked;
            _fillModeButton.clicked += HandleFillModusButtonClicked;

            // ingame menu
            // ingameMenuContainer.Q<Button>("MainMenuButton").clicked += MainMenuButtonPressed;
            _ingameMenuContainer.Q<Button>("ResumeButton").clicked += HideMenu;
            // ingameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;

            _ingameMenuContainer.Q<Button>("SaveButton").clicked += HandleSaveGame;
            _ingameMenuContainer.Q<Button>("LoadButton").clicked += HandleLoadGame;
            
            // menu button
            root.Q<Button>("menuButton").clicked += ShowMenu;

            // input events
            
        }

        private void Awake() {
            // subscribe to input events
            visibilityMenuEventChannel.OnEventRaised += SetMenuVisibility;
        }

        private void HandleSaveGame() {
            saveLevel.RaiseEvent();
        }
        
        private void HandleLoadGame() {
            loadLevel.RaiseEvent();
        }
        
        // private void OnDisable() {
        //     selectModeButton.clicked -= HandleSelectModusButtonClicked;
        //     paintModeButton.clicked -= HandlePaintModusButtonClicked;
        //     boxModeButton.clicked -= HandleBoxModusButtonClicked;
        //     fillModeButton.clicked -= HandleFillModusButtonClicked;
        // }
        
        // void MainMenuButtonPressed()
        // {
        //     // Szene laden
        //     SceneManager.LoadScene("MainMenu");
        // }
        //
        // void QuitGame()
        // {
        //     // Spiel beenden
        //     Application.Quit();
        // }
        
        void SetMenuVisibility(bool value) {
            if (value) {
                ShowMenu();
            }
            else {
                HideMenu();
            }
        }
    
        void ShowMenu()
        {
            enableMenuInput.RaiseEvent();
            // Einstellungen ausblenden und Menü zeigen
            _ingameMenuContainer.style.display = DisplayStyle.Flex;
            _levelEditorContainer.style.display = DisplayStyle.None;
        }
    
        void HideMenu()
        {
            enableGamplayInput.RaiseEvent();
            // Einstellungen ausblenden und Menü zeigen
            _levelEditorContainer.style.display = DisplayStyle.Flex;
            _ingameMenuContainer.style.display = DisplayStyle.None;
        }
        
        
        void HandleSelectModusButtonClicked() {
            levelEditor.Mode = LevelEditor.LevelEditor.CursorMode.Select;
        }
        
        void HandlePaintModusButtonClicked() {
            levelEditor.Mode = LevelEditor.LevelEditor.CursorMode.Paint;
        }
        
        void HandleBoxModusButtonClicked() {
            levelEditor.Mode = LevelEditor.LevelEditor.CursorMode.Box;
        }
        
        void HandleFillModusButtonClicked() {
            levelEditor.Mode = LevelEditor.LevelEditor.CursorMode.Fill;
        }
        
    }
}
