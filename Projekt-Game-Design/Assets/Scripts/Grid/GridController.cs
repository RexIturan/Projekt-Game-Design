﻿using System;
using System.Collections.Generic;
using Input;
using UnityEngine;
using Visual;

namespace Grid {
    public class GridController : MonoBehaviour {

        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO defaultGridData;
        [SerializeField] private GridDataSO globalGridData;

        [SerializeField] private TileTypeContainerSO tileTypesContainer;
        [SerializeField] private TileMapDrawer drawer;
        
        public void OnEnable() {

            globalGridData.InitValues(defaultGridData);
            
            gridContainer.tileGrids = new List<TileGrid>();
            gridContainer.tileGrids.Add(CreateNewTileGrid());
            FillGrid(gridContainer.tileGrids[0], tileTypesContainer.tileTypes[0]);
            // IncreaseGrid(new Vector2Int(-3,-3), new Vector2Int(globalGridData.Width + 3, globalGridData.Height +3));
            drawer.DrawGrid();
            
            
            //
            // gridContainer.tileGrids = new List<TileGrid>();
            //
            // gridContainer.tileGrids.Add(CreateNewTileGrid());
            // // gridContainer.tileGrids.Add(CreateNewTileGrid());
            //
            // foreach (var tileGrid in gridContainer.tileGrids) {
            //     FillGrid(gridContainer.tileGrids[0], tileTypesContainer.tileTypes[1]);
            //     // FillGrid(gridContainer.tileGrids[1], tileTypesContainer.tileTypes[0]);
            //     Debug.Log(tileGrid.ToString());
            //     drawer.DrawGrid();
            // }
            //
            // IncreaseGrid(new Vector2Int(-3,-3), new Vector2Int(globalGridData.Width +3, globalGridData.Height +4));
            //
            // foreach (var tileGrid in gridContainer.tileGrids) {
            //     Debug.Log(tileGrid.ToString());
            //     drawer.DrawGrid();
            // }
        }

        // int level 0 - 1 
        public TileGrid CreateNewTileGrid() {

            return new TileGrid(
                globalGridData.Width,
                globalGridData.Height,
                globalGridData.CellSize,
                globalGridData.OriginPosition,
                false
                );
        }

        // TODO move to grid data 
        // bounds are inclusive
        public bool IsInBounds(int x, int y, Vector2Int lowerBounds, Vector2Int upperBounds) {
            return x >= lowerBounds.x && 
                   y >= lowerBounds.y && 
                   x <= upperBounds.x && 
                   y <= upperBounds.y ;
        }
        
        public bool IsInBounds(Vector2Int pos, Vector2Int lowerBounds, Vector2Int upperBounds) {
            return IsInBounds(pos.x, pos.y, lowerBounds, upperBounds);
        }

        // pos in -/- direction
        // returns inclusive bound
        public Vector2Int GetLowerBounds(Vector3 pos) {
            return WorldPosToGridPos(pos);
        }
        
        // pos in -/- direction
        // returns inclusive bound
        public Vector2Int GetUpperBounds(Vector2Int pos, int width, int height) {
            return new Vector2Int(width - 1 + pos.x, height - 1 + pos.y);
        }

        // y == layer
        public Vector2Int WorldPosToGridPos(Vector3 worldPos) {
            var flooredPos = Vector3Int.FloorToInt(worldPos);
            return new Vector2Int(flooredPos.x, flooredPos.z);
        }
        
        private Vector2Int TilePosToGridPos(Vector2Int pos, Vector2Int lowerBounds) {
            // shift pos into grid space
            return new Vector2Int(
                x: pos.x + Mathf.Abs(lowerBounds.x),
                y: pos.y + Mathf.Abs(lowerBounds.y));
        }
        
        public void AddTileAt(Vector3 pos, TileTypeSO tileType) {
            AddTileAt(WorldPosToGridPos(pos), 0 ,tileType);
        }
        
        public void AddTileAt(Vector2Int pos, int level, TileTypeSO tileType) {

            var lowerBounds = GetLowerBounds(globalGridData.OriginPosition);
            var upperBounds = GetUpperBounds(
                WorldPosToGridPos(globalGridData.OriginPosition), 
                globalGridData.Width, 
                globalGridData.Height);

            Vector2Int newLowerBounds = lowerBounds;
            Vector2Int newUpperBounds = upperBounds;
            
            if (!IsInBounds(pos.x, pos.y, lowerBounds, upperBounds)) {
                
                Debug.Log("Out of Bounds");
                
                newLowerBounds = new Vector2Int(
                    Mathf.Min(pos.x, lowerBounds.x), 
                    Mathf.Min(pos.y, lowerBounds.y)
                );

                newUpperBounds = new Vector2Int(
                    Mathf.Max(pos.x, upperBounds.x), 
                    Mathf.Max(pos.y, upperBounds.y)
                );
                
                IncreaseGrid(lowerBounds, newLowerBounds, newUpperBounds);

                // TODO newPos?
                
                Debug.Log($"pos:{pos}| lower{lowerBounds} upper{upperBounds}| newLower{newLowerBounds} newUpper{newUpperBounds}");
            }
            else {
                // Debug.Log("In Bounds");
                // Debug.Log($"pos:{pos}| lower{lowerBounds} upper{upperBounds}|");
            }

            var newPos = TilePosToGridPos(pos, newLowerBounds);
            
            // Debug.Log($"tilePosOffsetted {x} {y}");
            
            gridContainer.tileGrids[level].GetGridObject(newPos.x, newPos.y).SetTileType(tileType);
            
            drawer.DrawGrid();
        }

        public void AddMultipleTilesAt(Vector3 start, Vector3 end, TileTypeSO tileType) {
            AddMultipleTilesAt(WorldPosToGridPos(start), WorldPosToGridPos(end), tileType);
        }
        
        // from = -OO -> origin | to origin -> +OO
        public void AddMultipleTilesAt(Vector2Int start, Vector2Int end, TileTypeSO tileType) {
            
            var minXY = new Vector2Int(Mathf.Min(start.x, end.x), Mathf.Min(start.y, end.y));
            var maxXY = new Vector2Int(Mathf.Max(start.x, end.x), Mathf.Max(start.y, end.y));
            
            var lowerBounds = GetLowerBounds(globalGridData.OriginPosition);
            var upperBounds = GetUpperBounds(
                WorldPosToGridPos(globalGridData.OriginPosition), 
                globalGridData.Width, 
                globalGridData.Height);

            Vector2Int newLowerBounds = lowerBounds;
            Vector2Int newUpperBounds = upperBounds;

            if (!IsInBounds(start, lowerBounds, upperBounds) || !IsInBounds(end, lowerBounds, upperBounds)) {
                
                Debug.Log("Out of Bounds");
                
                newLowerBounds = new Vector2Int(
                    Mathf.Min(start.x, end.x, lowerBounds.x), 
                    Mathf.Min(start.y, end.y, lowerBounds.y)
                );

                newUpperBounds = new Vector2Int(
                    Mathf.Max(start.x, end.x, upperBounds.x), 
                    Mathf.Max(start.y, end.y, upperBounds.y)
                );
                
                IncreaseGrid(lowerBounds, newLowerBounds, newUpperBounds);

                Debug.Log($"start:{start} end:{start}| lower{lowerBounds} upper{upperBounds}| newLower{newLowerBounds} newUpper{newUpperBounds}");
            }

            minXY = TilePosToGridPos(minXY, newLowerBounds);
            maxXY = TilePosToGridPos(maxXY, newLowerBounds);
            
            foreach (var tileGrid in gridContainer.tileGrids) {
                for (int x = minXY.x; x <= maxXY.x; x++) {
                    for (int y = minXY.y; y <= maxXY.y; y++) {
                        tileGrid.GetGridObject(x,y).SetTileType(tileType);
                    }
                }
            }
            
            drawer.DrawGrid();
        }

        public void FillGrid(TileGrid tileGrid, TileTypeSO tileType) {
            Debug.Log($"{tileGrid.Width} {tileGrid.Height}");
            for (int x = 0; x < tileGrid.Width; x++) {
                for (int y = 0; y < tileGrid.Height; y++) {
                    tileGrid.GetGridObject(x, y).SetTileType(tileType);
                }
            }
        }
        
        
        // TODO wording
        // TODO maybe => from -> negChange && to -> positiveChange 
        // from = -OO -> origin | to = origin -> +OO
        public void IncreaseGrid(Vector2Int lowerBounds, Vector2Int newLowerBounds, Vector2Int newUpperBounds) {
            var oldTileGrids = gridContainer.tileGrids;

            Debug.Log($"increaseGrid {newLowerBounds} {newUpperBounds}");
            
            //TODO get change easier
            var offset = TilePosToGridPos(newLowerBounds, lowerBounds) * -1;
            
            ChangeBounds(newLowerBounds, newUpperBounds);

            Debug.Log($"{globalGridData.Width} {globalGridData.Height}");
            
            
            
            for (int i = 0; i < gridContainer.tileGrids.Count; i++) {
                
                TileGrid newTileGrid = CreateNewTileGrid();
            //     //TODO default fill
            FillGrid(newTileGrid, tileTypesContainer.tileTypes[0]);
            oldTileGrids[i].CopyTo(newTileGrid, offset);
            gridContainer.tileGrids[i] = newTileGrid;
            }
        }

        private void ChangeBounds(Vector2Int newLowerBounds, Vector2Int newUpperBounds) {
            globalGridData.Width = newUpperBounds.x + Mathf.Abs(newLowerBounds.x) + 1;
            globalGridData.Height = newUpperBounds.y + Mathf.Abs(newLowerBounds.y) + 1;
            globalGridData.OriginPosition = new Vector3(
                x: newLowerBounds.x, 
                y: globalGridData.OriginPosition.y, 
                z: newLowerBounds.y);
        }
    }
}