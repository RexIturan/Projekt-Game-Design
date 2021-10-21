using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Util;

namespace LevelEditor {
    //todo remove
    [Obsolete]
    public class Cursor : MonoBehaviour {

        public enum CursorMode {
            Select,
            Paint,
            Box,
            Fill,
        }
        
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase cursorTile;

        [SerializeField] private CursorMode mode = CursorMode.Paint;

        // TODO Hack
        [SerializeField] private LevelEditor levelEditor;
        
        public CursorMode Mode {
            get => mode;
            set => mode = value;
        }

        // read data
        private Vector3Int _clickPos;
        private Vector3Int _dragPos;
        private bool _clicked;
        private bool _dragEnd;

        private void Update() {

            Vector3Int tilePos = MousePosToTilemapPos();

            var mouse = Mouse.current;

            bool leftMouseWasPressed = mouse.leftButton.wasPressedThisFrame;
            bool leftMouseWasReleased = mouse.leftButton.wasReleasedThisFrame;
            bool leftMousePressed = mouse.leftButton.isPressed;
            
            switch (mode) {
                case CursorMode.Paint:
                    DrawSingleCursor(tilePos);
                    
                    //TODO
                    if (_clicked) {
                        
                        levelEditor.AddTileAt(_clickPos);
                        _clickPos = Vector3Int.zero;
                        _clicked = false;
                    }
                    
                    if (leftMousePressed) {
                        HandleMouseClick();
                    }
                    
                    break;
                case CursorMode.Box:
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

                    if (_dragEnd) {
                        //TODO
                        levelEditor.AddMultipleTilesAt(_clickPos, _dragPos);
                        _clicked = false;
                        _dragEnd = false;
                        _clickPos = Vector3Int.zero;
                        _dragPos = Vector3Int.zero;
                    }

                    DrawBoxCursor(_clickPos, _dragPos);
                    Debug.Log($"{_clickPos} {_dragPos}");
                    break;
                case CursorMode.Fill:
                    break;
            }
            
            // DrawSingleCursor(tilePos);
        }

        private void DrawBoxCursor(Vector3Int startPos, Vector3Int endPos) {
            tilemap.ClearAllTiles();

            if (!_dragEnd) {
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
            _clicked = true;
            _clickPos = MousePosToTilemapPos();
        }
        
        public void HandleMouseDrag() {
            _dragPos = MousePosToTilemapPos();
        }
        
        public void HandleMouseDragEnd() {
            _dragEnd = true;
        }

        public Vector3Int MousePosToTilemapPos() {
            Vector3Int mousePosition = Vector3Int.FloorToInt(MousePosition.GetMouseWorldPosition(Vector3.up, 1));
            return new Vector3Int(mousePosition.x, mousePosition.z, 0);
        }
    }
}
