using System;
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


        [SerializeField] private LevelEditor.LevelEditor levelEditor;
        [SerializeField] private LevelEditor.Cursor cursor;
        
        private void Awake() {
            var root = GetComponent<UIDocument>().rootVisualElement;

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
        }

        // private void OnDisable() {
        //     selectModeButton.clicked -= HandleSelectModusButtonClicked;
        //     paintModeButton.clicked -= HandlePaintModusButtonClicked;
        //     boxModeButton.clicked -= HandleBoxModusButtonClicked;
        //     fillModeButton.clicked -= HandleFillModusButtonClicked;
        // }

        void HandleSelectModusButtonClicked() {
            cursor.Mode = Cursor.ECursorMode.select;
        }
        
        void HandlePaintModusButtonClicked() {
            cursor.Mode = Cursor.ECursorMode.paint;
        }
        
        void HandleBoxModusButtonClicked() {
            cursor.Mode = Cursor.ECursorMode.box;
        }
        
        void HandleFillModusButtonClicked() {
            cursor.Mode = Cursor.ECursorMode.fill;
        }
        
    }
}