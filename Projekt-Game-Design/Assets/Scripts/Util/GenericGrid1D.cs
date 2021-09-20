﻿using System;
using TMPro;
using UnityEngine;

namespace Util {
    public class GenericGrid1D<TGridObject> {
        
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private Vector3 originPosition;
        [SerializeField] private TGridObject[] grid1DArray;
        
        //debug
        [SerializeField] private readonly Transform debugTextParent;
        [SerializeField] private bool showDebug { get; }

        public int Width => width;
        public int Height => height;
        public float CellSize => cellSize;
        public Vector3 OriginPosition => originPosition;
        
        public event EventHandler<OnGridObjectChangedGrid1DEventArgs> OnGridObjectChanged;

        public GenericGrid1D(int width, int height, float cellSize, Vector3 originPosition,
            Func<GenericGrid1D<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug,
            Transform debugTextParent = null) {
            this.showDebug = showDebug;
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;
            this.debugTextParent = debugTextParent;

            grid1DArray = new TGridObject[width * height];

            for (var y = 0; y < this.height; y++)
            for (var x = 0; x < this.width; x++)
                grid1DArray[Coord2DToIndex(x, y, width)] = createGridObject(this, x, y);

            // todo expose as getter setter or event to change during runtime
            if (showDebug) CreateDebugDisplay();
        }

        public void CreateDebugDisplay() {
            var debugTextArray = new TextMeshPro[width, height];

            for (var y = 0; y < height; y++)
            for (var x = 0; x < width; x++) {
                debugTextArray[x, y] = Text.CreateWorldText(
                    grid1DArray[Coord2DToIndex(x, y, width)].ToString(),
                    debugTextParent,
                    GetCellDimensions(),
                    GetWorldPosition(x, y) + GetCellCenter(),
                    GetWorldRotation(),
                    4,
                    Color.white);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100);
            }

            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100);

            OnGridObjectChanged +=
                (AssemblyDefinitionReferenceAsset, eventArgs) => {
                    debugTextArray[eventArgs.x, eventArgs.y].text =
                        grid1DArray[Coord2DToIndex(eventArgs.x, eventArgs.y, width)].ToString();
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
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedGrid1DEventArgs {x = x, y = y});
        }

        public void SetGridObject(int x, int y, TGridObject value) {
            if (IsInBounds(x, y)) {
                grid1DArray[Coord2DToIndex(x, y, width)] = value;
                TriggerGridObjectChanged(x, y);
            }
        }

        public void SetGridObject(Vector3 worldPosition, TGridObject value) {
            int x, y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }

        public TGridObject GetGridObject(int x, int y) {
            if (IsInBounds(x, y)) return grid1DArray[Coord2DToIndex(x, y, width)];

            return default;
        }

        public TGridObject GetGridObject(Vector3 worldPosition) {
            int x, y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }

        public int Coord2DToIndex(Vector2Int coord, int width) {
            return Coord2DToIndex(coord.x, coord.y, width);
        }

        public int Coord2DToIndex(int x, int y, int width) {
            return x + y * width;
        }

        public void IndexToCoord(int index, int width, out int x, out int y) {
            x = index % width;
            y = index / width;
        }
        
        public Vector2Int IndexToCoord2D(int index, int width, int height) {
            int x, y;
            IndexToCoord(index, width, out x, out y);
            return new Vector2Int(x, y);
        }

        public class OnGridObjectChangedGrid1DEventArgs {
            public int x;
            public int y;
        }
    }
}