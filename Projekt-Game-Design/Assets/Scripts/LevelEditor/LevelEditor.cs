using System;
using Grid;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using GDP01.Util;
using Visual;

namespace LevelEditor {
    public class LevelEditor : MonoBehaviour {
        
        public enum ECursorMode {
            select,
            paint,
            box,
            fill,
        }
        
        [Header("Refferences")]
        //todo can not use IMapDrawer because unity doesnt use generics :(
        [SerializeField] private TileMapDrawer drawer;
        [SerializeField] private GridController controller;
        
        [Header("Settings")]
        [SerializeField] private ECursorMode mode = ECursorMode.paint;
        public ECursorMode Mode {
            get => mode;
            set => mode = value;
        }

        
        [SerializeField] private TileTypeSO selectedTileType;
        [SerializeField] private TileTypeContainerSO tileTypesContainer;

        // runtime input data data
        private Vector3 clickPos;
        private Vector3 dragPos;
        private bool clicked;
        private bool dragEnd;
        private bool currentInput;

        
        public void Awake() {
            selectedTileType = tileTypesContainer.tileTypes[1];
        }
        
        private void Update() {

            //todo getMousePosition at level 
            Vector3 mousePosition = MousePosition.GetMouseWorldPosition(Vector3.up);

            var mouse = Mouse.current;
            bool leftMouseWasPressed = mouse.leftButton.wasPressedThisFrame;
            bool leftMouseWasReleased = mouse.leftButton.wasReleasedThisFrame;
            bool leftMousePressed = mouse.leftButton.isPressed;
            
            switch (mode) {
                case ECursorMode.paint:

                    drawer.DrawCursorAt(mousePosition);
                    
                    if (clicked) {
                        HandlePaint();
                    }
                    
                    if (leftMousePressed) {
                        HandleMouseClick(mousePosition);
                    }
                    
                    break;
                case ECursorMode.box:

                    if (leftMouseWasPressed) {
                        HandleMouseClick(mousePosition);                   
                    }
                    if (leftMousePressed) {
                        HandleMouseDrag(mousePosition);            
                    }
                    else if (leftMouseWasReleased) {
                        HandleMouseDragEnd();
                    }

                    if (dragEnd) {
                        HandleBox();
                    }
                    else {
                        if (clicked) {
                            drawer.DrawBoxCursorAt(clickPos, dragPos);    
                        }
                    }
                    
                    break;
                case ECursorMode.fill:
                    // break;
                default:
                    break;
            }
            
            // DrawSingleCursor(tilePos);
        }

        private void HandleBox() {
            AddMultipleTilesAt(clickPos, dragPos);
            clicked = false;
            dragEnd = false;
            clickPos = Vector3Int.zero;
            dragPos = Vector3Int.zero;
            drawer.clearCursor();
        }

        private void HandlePaint() {
            AddTileAt(clickPos); 
            clickPos = Vector3Int.zero;
            clicked = false;
        }


        public void HandleMouseClick(Vector3 pos) {
            clicked = true;
            clickPos = pos;
        }
        
        public void HandleMouseDrag(Vector3 pos) {
            dragPos = pos;
        }
        
        public void HandleMouseDragEnd() {
            dragEnd = true;
        }
        
        public void AddMultipleTilesAt(Vector3 clickPos, Vector3 dragPos) {
            controller.AddMultipleTilesAt(clickPos, dragPos, selectedTileType);
        }

        public void AddTileAt(Vector3 clickPos) {
            controller.AddTileAt(clickPos, selectedTileType);
        }
    }
}