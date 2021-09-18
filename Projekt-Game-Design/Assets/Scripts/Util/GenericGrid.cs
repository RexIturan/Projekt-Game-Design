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

        [SerializeField] private bool showDebug { get; set; }
        
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private Vector3 originPosition;
        [SerializeField] private TGridObject[,] gridArray;

        public int Width => width;
        public int Height => height;
        public float CellSize => cellSize;
        
        
        //debug
        [SerializeField] private Transform debugTextParent;
        

        public GenericGrid(int width, int height, float cellSize, Vector3 originPosition,
            Func<GenericGrid<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug, Transform debugTextParent = null) {
            this.showDebug = showDebug;
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.debugTextParent = debugTextParent;

            gridArray = new TGridObject[width, height];


            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    gridArray[x, y] = createGridObject(this, x, y);
                }
            }

            // todo expose as getter setter or event to change during runtime
            if (showDebug) {
                CreateDebugDisplay();
            }
        }

        public void CreateDebugDisplay() {
            TextMeshPro[,] debugTextArray = new TextMeshPro[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    debugTextArray[x, y] = Util.Text.CreateWorldText(
                        gridArray[x, y].ToString(),
                        debugTextParent,
                        GetCellDimensions(),
                        GetWorldPosition(x, y) + GetCellCenter(),
                        GetWorldRotation(),
                        4,
                        Color.white);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
                }
            }

            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);

            OnGridObjectChanged +=
                (object AssemblyDefinitionReferenceAsset, OnGridObjectChangedEventArgs eventArgs) => {
                    debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y].ToString();
                };
        }

        private Vector3 GetWorldPosition(int x, int y) {
            // todo use grid axis / rotation
            return new Vector3(x, 0, y) * cellSize + originPosition;
        }

        public void GetXY(Vector3 worldPosition, out int x, out int y) {
            x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
        }

        private Vector3 GetCellCenter() {
            // todo use grid axis / rotation
            return new Vector3(0.5f, 0, 0.5f) * cellSize;
        }

        private Vector3 GetWorldRotation() {
            // todo get rotation from variable or enum
            // todo setup axis of the grid at the constuctor
            return new Vector3(90, 0, 0);
        }

        private Vector2 GetCellDimensions() {
            return new Vector2(cellSize, cellSize);
        }

        public bool IsInBounds(int x, int y) {
            return x >= 0 && y >= 0 && x < width && y < height;
        }

        public void TriggerGridObjectChanged(int x, int y) {
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs {x = x, y = y});
        }

        public void SetGridObject(int x, int y, TGridObject value) {
            if (IsInBounds(x, y)) {
                gridArray[x, y] = value;
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
                return gridArray[x, y];
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