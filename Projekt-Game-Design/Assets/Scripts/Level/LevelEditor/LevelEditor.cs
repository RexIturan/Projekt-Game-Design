using System;
using Grid;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Util;
using Visual;
using WorldObjects;

namespace LevelEditor {
    public class LevelEditor : MonoBehaviour {
        public enum ECursorMode {
            select,
            paint,
            box,
            fill,
        }
        
        [Header("Receiving Events On")]
        [SerializeField] private VoidEventChannelSO levelLoaded;
        
        [Header("SendingEventsOn")]
        [SerializeField] private VoidEventChannelSO loadLevel;
        [SerializeField] private VoidEventChannelSO updateMeshEC;
        
        
        [Header("scene References")]
        //todo can not use IMapDrawer because unity doesnt use generics :(
        [SerializeField] private TileMapDrawer drawer;
        [SerializeField] private GridController controller;
        [SerializeField] private InputReader inputReader;
        [SerializeField] private WorldObjectGridController objectController;
        [SerializeField] private PrefabGridDrawer prefabDrawer;

        [Header("SO References")]
        [SerializeField] private WorldObjectContainerSO worldObjectContainer;
        [SerializeField] private TileTypeContainerSO tileTypesContainer;
        
        
        [Header("Settings")]
        [SerializeField] private ECursorMode mode = ECursorMode.paint;
        public ECursorMode Mode {
            get => mode;
            set => mode = value;
        }

        [SerializeField] private TileTypeSO selectedTileType;
        

        // runtime input data data
        private Vector3 clickPos;
        private Vector3 dragPos;
        private bool leftClicked;
        private bool rightClicked;
        private bool dragEnd;
        private bool currentInput;

        
        public void Awake() {
            selectedTileType = tileTypesContainer.tileTypes[1];
            levelLoaded.OnEventRaised += RedrawLevel;
            // updateMeshEC.OnEventRaised += RedrawLevel;
        }

        public void Start() {
            //todo move to scene manager or so??
            // loadLevel.RaiseEvent();
        }

        private void RedrawLevel() {
            drawer.DrawGrid();
            updateMeshEC.RaiseEvent();
        }
        
        private void Update() {

            if (!inputReader.GameInput.Gameplay.enabled) {
                return;
            }            
            
            //todo getMousePosition at level 
            Vector3 mousePosition = MousePosition.GetMouseWorldPosition(Vector3.up, 1);

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
            RedrawLevel();
        }

        private void HandleRemove() {
            controller.AddTileAt(clickPos, tileTypesContainer.tileTypes[0].id);
            clickPos = Vector3Int.zero;
            rightClicked = false;
            RedrawLevel();
        }
        
        private void HandlePaint() {
            AddTileAt(clickPos); 
            clickPos = Vector3Int.zero;
            leftClicked = false;
            RedrawLevel();
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
            controller.AddMultipleTilesAt(clickPos, dragPos, selectedTileType.id);
        }

        public void AddTileAt(Vector3 clickPos) {
            controller.AddTileAt(clickPos, selectedTileType.id);
        }
        
        public void ResetLevel() {
            controller.ResetGrid();
            RedrawLevel();
        }

        // public void AddObjectAt(Vector3 clickPos) {
        //     objectController.AddTileAt(clickPos, worldObjectContainer.worldObjects[1]);
        // }
    }
}