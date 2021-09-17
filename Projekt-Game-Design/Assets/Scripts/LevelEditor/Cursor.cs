using System;
using Grid;
using Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using GDP01.Util;

namespace LevelEditor {
    public class Cursor : MonoBehaviour {

        public enum ECursorMode {
            select,
            paint,
            box,
            fill,
        }
        
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase cursorTile;

        [SerializeField] private InputReader inputReader;
        [SerializeField] private ECursorMode mode = ECursorMode.paint;

        // TODO Hack
        [SerializeField] private LevelEditor levelEditor;
        
        public ECursorMode Mode {
            get => mode;
            set => mode = value;
        }

        // read data
        private Vector3Int clickPos;
        private Vector3Int dragPos;
        private bool clicked;
        private bool dragEnd;
        private bool currentInput;
        
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
                case ECursorMode.paint:
                    DrawSingleCursor(tilePos);
                    
                    //TODO
                    if (clicked) {
                        
                        levelEditor.AddTileAt(clickPos);
                        clickPos = Vector3Int.zero;
                        clicked = false;
                    }
                    
                    if (leftMousePressed) {
                        HandleMouseClick();
                    }
                    
                    break;
                case ECursorMode.box:
                    Debug.Log("box");
                    if (leftMouseWasPressed) {
                        HandleMouseClick();                   
                    }
                    if (leftMousePressed) {
                        HandleMouseDrag();                        
                    }
                    else if (leftMouseWasReleased) {
                        HandleMouseDragEnd();
                    }

                    if (dragEnd) {
                        //TODO
                        levelEditor.AddMultipleTilesAt(clickPos, dragPos);
                        clicked = false;
                        dragEnd = false;
                        clickPos = Vector3Int.zero;
                        dragPos = Vector3Int.zero;
                    }

                    DrawBoxCursor(clickPos, dragPos);
                    Debug.Log($"{clickPos} {dragPos}");
                    break;
                case ECursorMode.fill:
                    // break;
                default:
                    break;
            }
            
            // DrawSingleCursor(tilePos);
        }

        private void DrawBoxCursor(Vector3Int startPos, Vector3Int endPos) {
            tilemap.ClearAllTiles();

            if (!dragEnd) {
                int xMin = Mathf.Min(startPos.x, endPos.x);
                int yMin = Mathf.Min(startPos.y, endPos.y);
                int xMax = Mathf.Max(startPos.x, endPos.x);
                int yMax = Mathf.Max(startPos.y, endPos.y);
            
                for (int x = xMin; x <= xMax; x++) {
                    for (int y = yMin; y <= yMax; y++) {
                        Vector3Int tilePos = new Vector3Int(x, y, 0);
                        tilemap.SetTile(tilePos, cursorTile);
                    }
                }    
            }
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
            dragPos = MousePosToTilemapPos();
        }
        
        public void HandleMouseDragEnd() {
            dragEnd = true;
        }

        public Vector3Int MousePosToTilemapPos() {
            Vector3Int mousePosition = Vector3Int.FloorToInt(MousePosition.GetMouseWorldPosition(Vector3.up));
            return new Vector3Int(mousePosition.x, mousePosition.z, 0);
        }
    }
}