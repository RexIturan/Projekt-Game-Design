using System;
using TMPro;
using UnityEngine;

namespace Util {
    public class GenericGrid<TGridObject> {
        public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
        public class OnGridObjectChangedEventArgs {
            public int x;
            public int y;
        }

        private int _width;
        private int _height;
        private float _cellSize;
        private Vector3 _originPosition;
        private TGridObject[,] _gridArray;

        public int Width => _width;
        public int Height => _height;
        public float CellSize => _cellSize;
        
        //debug
        private Transform _debugTextParent;
        private readonly bool _showDebug;

        public GenericGrid(int width, int height, float cellSize, Vector3 originPosition,
            Func<GenericGrid<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug, Transform debugTextParent = null) {
            this._showDebug = showDebug;
            this._width = width;
            this._height = height;
            this._cellSize = cellSize;
            this._originPosition = originPosition;
            this._debugTextParent = debugTextParent;

            _gridArray = new TGridObject[width, height];


            for (int x = 0; x < _gridArray.GetLength(0); x++) {
                for (int y = 0; y < _gridArray.GetLength(1); y++) {
                    _gridArray[x, y] = createGridObject(this, x, y);
                }
            }

            // todo expose as getter setter or event to change during runtime
            if (_showDebug) {
                CreateDebugDisplay();
            }
        }

        public void CreateDebugDisplay() {
            TextMeshPro[,] debugTextArray = new TextMeshPro[_width, _height];

            for (int x = 0; x < _gridArray.GetLength(0); x++) {
                for (int y = 0; y < _gridArray.GetLength(1); y++) {
                    debugTextArray[x, y] = Text.CreateWorldText(
                        _gridArray[x, y].ToString(),
                        _debugTextParent,
                        GetCellDimensions(),
                        GetWorldPosition(x, y) + GetCellCenter(),
                        GetWorldRotation(),
                        4,
                        Color.white);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
                }
            }

            Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100);

            OnGridObjectChanged +=
                (assemblyDefinitionReferenceAsset, eventArgs) => {
                    debugTextArray[eventArgs.x, eventArgs.y].text = _gridArray[eventArgs.x, eventArgs.y].ToString();
                };
        }

        private Vector3 GetWorldPosition(int x, int y) {
            // todo use grid axis / rotation
            return new Vector3(x, 0, y) * _cellSize + _originPosition;
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y) {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
            y = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
        }

        private Vector3 GetCellCenter() {
            // todo use grid axis / rotation
            return new Vector3(0.5f, 0, 0.5f) * _cellSize;
        }

        private Vector3 GetWorldRotation() {
            // todo get rotation from variable or enum
            // todo setup axis of the grid at the constuctor
            return new Vector3(90, 0, 0);
        }

        private Vector2 GetCellDimensions() {
            return new Vector2(_cellSize, _cellSize);
        }

        public bool IsInBounds(int x, int y) {
            return x >= 0 && y >= 0 && x < _width && y < _height;
        }

        public void TriggerGridObjectChanged(int x, int y) {
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs {x = x, y = y});
        }

        public void SetGridObject(int x, int y, TGridObject value) {
            if (IsInBounds(x, y)) {
                _gridArray[x, y] = value;
                TriggerGridObjectChanged(x, y);
            }
        }

        public void SetGridObject(Vector3 worldPosition, TGridObject value) {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }

        public TGridObject GetGridObject(int x, int y) {
            if (IsInBounds(x, y)) {
                return _gridArray[x, y];
            }

            return default(TGridObject);
        }

        public TGridObject GetGridObject(Vector3 worldPosition) {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }
    }
}