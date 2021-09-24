using System;
using Grid;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Util;
using Visual;

namespace LevelEditor {
    public class LevelEditor : MonoBehaviour {
        
        [Header("Receiving Events On")]
        [SerializeField] private VoidEventChannelSO loadLevel;
        
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
        [SerializeField] private InputReader inputReader;
        
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
        private bool leftClicked;
        private bool rightClicked;
        private bool dragEnd;
        private bool currentInput;

        
        public void Awake() {
            selectedTileType = tileTypesContainer.tileTypes[1];
            loadLevel.OnEventRaised += RedrawLevel;
        }

        public void Start() {
            loadLevel.RaiseEvent();
        }

        private void RedrawLevel() {
            drawer.DrawGrid();
        }
        
        private void Update() {

            if (!inputReader.GameInput.Gameplay.enabled) {
                return;
            }            
            
            //todo getMousePosition at level 
            Vector3 mousePosition = MousePosition.GetMouseWorldPosition(Vector3.up);

            var mouse = Mouse.current;
            bool leftMouseWasPressed = mouse.leftButton.wasPressedThisFrame;
            bool leftMouseWasReleased = mouse.leftButton.wasReleasedThisFrame;
            bool leftMousePressed = mouse.leftButton.isPressed;
            bool rightMousePressed = mouse.rightButton.isPressed;

            if (rightMousePressed) {
                rightClicked = true;
                clickPos = mousePosition;
            }
            
            switch (mode) {
                case ECursorMode.paint:

                    drawer.DrawCursorAt(mousePosition);

                    if (rightClicked) {
                        HandleRemove();
                        break;
                    }
                    
                    if (leftClicked) {
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
                        if (leftClicked) {
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
            leftClicked = false;
            dragEnd = false;
            clickPos = Vector3Int.zero;
            dragPos = Vector3Int.zero;
            drawer.clearCursor();
        }

        private void HandleRemove() {
            controller.AddTileAt(clickPos, tileTypesContainer.tileTypes[0]);
            clickPos = Vector3Int.zero;
            rightClicked = false;
        }
        
        private void HandlePaint() {
            AddTileAt(clickPos); 
            clickPos = Vector3Int.zero;
            leftClicked = false;
        }


        public void HandleMouseClick(Vector3 pos) {
            leftClicked = true;
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

        public void ResetLevel() {
            controller.ResetGrid();
            drawer.DrawGrid();
        }
    }
}