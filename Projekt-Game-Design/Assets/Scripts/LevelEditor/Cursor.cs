using System;
using Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Util;

namespace LevelEditor {
    public class Cursor : MonoBehaviour {

        enum ECursorMode {
            single,
            box,
            fill,
        }
        
        
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase cursorTile;

        [SerializeField] private InputReader inputReader;
        [SerializeField] private ECursorMode mode = ECursorMode.single;

        // read data
        private Vector3Int clickPos;
        private Vector3Int dragPos;
        private bool clicked;
        private bool dragEnd;
        
        private void Awake() {
            // subscribe to input reader
        }

        private void OnDisable() {
            // unsupscribe
        }

        private void Update() {

            Vector3Int tilePos = MousePosToTilemapPos();

            var mouse = Mouse.current;

            bool leftMouseWasPressed = mouse.leftButton.wasPressedThisFrame;
            bool leftMouseWasReleased = mouse.leftButton.wasReleasedThisFrame;
            bool leftMousePressed = mouse.leftButton.isPressed;
            
            switch (mode) {
                case ECursorMode.single:
                    DrawSingleCursor(tilePos);

                    if (leftMouseWasReleased) {
                        HandleMouseClick();
                    }
                    
                    break;
                case ECursorMode.box:
                    
                    // drawBoxCursor

                    if (leftMouseWasPressed) {
                        HandleMouseClick();                   
                    }
                    
                    
                    
                    break;
                case ECursorMode.fill:
                    // break;
                default:
                    break;
            }
            
            DrawSingleCursor(tilePos);
        }

        public void DrawSingleCursor(Vector3Int tilePos) {
            tilemap.ClearAllTiles();
            tilemap.SetTile(tilePos, cursorTile);
        }

        public void HandleMouseClick() {
            clicked = true;
            clickPos = MousePosToTilemapPos();
        }
        
        public void HandleMouseDrag() {
            
        }
        
        public void HandleMouseDragEnd() {
            
        }

        public Vector3Int MousePosToTilemapPos() {
            Vector3Int mousePosition = Vector3Int.FloorToInt(MousePosition.GetMouseWorldPosition(Vector3.up));
            return new Vector3Int(mousePosition.x, mousePosition.z, 0);
        }
    }
}