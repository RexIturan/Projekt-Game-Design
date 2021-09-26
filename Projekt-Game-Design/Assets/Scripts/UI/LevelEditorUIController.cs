using System;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = LevelEditor.Cursor;

namespace UI {
    public class LevelEditorUIController : MonoBehaviour {
        
        private Button selectModeButton;
        private Button paintModeButton;
        private Button boxModeButton;
        private Button fillModeButton;
        
        private Button upLevelButton;
        private Button downLevelButton;

        // menu button
        private Button menuButton;
        
        // container
        private VisualElement levelEditorContainer;
        private VisualElement ingameMenuContainer;

        [SerializeField] private LevelEditor.LevelEditor levelEditor;
        
        [Header("Receiving Events On")]
        [SerializeField] private BoolEventChannelSO VisibilityMenuEventChannel;
    
        [Header("Sending Events On")]
        //input
        [SerializeField] private VoidEventChannelSO enableMenuInput;
        [SerializeField] private VoidEventChannelSO enableGamplayInput;
        // save manager
        [SerializeField] private VoidEventChannelSO saveLevel;
        [SerializeField] private VoidEventChannelSO loadLevel;
        
        private void Start() {
            var root = GetComponent<UIDocument>().rootVisualElement;

            ingameMenuContainer = root.Q<VisualElement>("IngameMenu");
            levelEditorContainer = root.Q<VisualElement>("LevelEditorContainer");
            
            selectModeButton = root.Q<Button>("select");
            paintModeButton = root.Q<Button>("paint");
            boxModeButton = root.Q<Button>("box");
            fillModeButton = root.Q<Button>("fill");
            
            upLevelButton = root.Q<Button>("up");
            downLevelButton = root.Q<Button>("down");

            
            selectModeButton.clicked += HandleSelectModusButtonClicked;
            paintModeButton.clicked += HandlePaintModusButtonClicked;
            boxModeButton.clicked += HandleBoxModusButtonClicked;
            fillModeButton.clicked += HandleFillModusButtonClicked;

            // ingame menu
            // ingameMenuContainer.Q<Button>("MainMenuButton").clicked += MainMenuButtonPressed;
            ingameMenuContainer.Q<Button>("ResumeButton").clicked += HideMenu;
            // ingameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;

            ingameMenuContainer.Q<Button>("SaveButton").clicked += HandleSaveGame;
            ingameMenuContainer.Q<Button>("LoadButton").clicked += HandleLoadGame;
            
            // menu button
            root.Q<Button>("menuButton").clicked += ShowMenu;

            // input events
            
        }

        private void Awake() {
            // subscribe to input events
            VisibilityMenuEventChannel.OnEventRaised += SetMenuVisibility;
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
            ingameMenuContainer.style.display = DisplayStyle.Flex;
            levelEditorContainer.style.display = DisplayStyle.None;
        }
    
        void HideMenu()
        {
            enableGamplayInput.RaiseEvent();
            // Einstellungen ausblenden und Menü zeigen
            levelEditorContainer.style.display = DisplayStyle.Flex;
            ingameMenuContainer.style.display = DisplayStyle.None;
        }
        
        
        void HandleSelectModusButtonClicked() {
            levelEditor.Mode = LevelEditor.LevelEditor.ECursorMode.select;
        }
        
        void HandlePaintModusButtonClicked() {
            levelEditor.Mode = LevelEditor.LevelEditor.ECursorMode.paint;
        }
        
        void HandleBoxModusButtonClicked() {
            levelEditor.Mode = LevelEditor.LevelEditor.ECursorMode.box;
        }
        
        void HandleFillModusButtonClicked() {
            levelEditor.Mode = LevelEditor.LevelEditor.ECursorMode.fill;
        }
        
    }
}