using System;
using System.Collections.Generic;
using Input;
using UnityEngine;

namespace Grid {
    public class GridController : MonoBehaviour {

        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO defaultGridData;
        [SerializeField] private GridDataSO globalGridData;

        [SerializeField] private TileTypeContainerSO tileTypesContainer;

        [SerializeField] private GridViewer gridViewer;
        
        public void OnEnable() {

            globalGridData.InitValues(defaultGridData);
            
            gridContainer.tileGrids = new List<TileGrid>();
            
            gridContainer.tileGrids.Add(CreateNewTileGrid());
            // gridContainer.tileGrids.Add(CreateNewTileGrid());
            
            foreach (var tileGrid in gridContainer.tileGrids) {
                FillGrid(gridContainer.tileGrids[0], tileTypesContainer.tileTypes[1]);
                // FillGrid(gridContainer.tileGrids[1], tileTypesContainer.tileTypes[0]);
                Debug.Log(tileGrid.ToString());
                gridViewer.DrawGrid();
            }
            
            IncreaseGrid(new Vector2Int(-3,-3), new Vector2Int(globalGridData.Width +3, globalGridData.Height +4));
            
            foreach (var tileGrid in gridContainer.tileGrids) {
                Debug.Log(tileGrid.ToString());
                gridViewer.DrawGrid();
            }
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

        
        
        public void AddTileAt(int x, int y, int level, TileTypeSO tileType) {

            Vector2Int tileGridCoords = new Vector2Int(x, y);
            
            if (!globalGridData.IsInBounds(x, y)) {
                // calculate difference

                Vector2Int from = new Vector2Int(
                    Mathf.Min(x, (int) Mathf.Floor(globalGridData.OriginPosition.x)), 
                    Mathf.Min(y,(int) Mathf.Floor(globalGridData.OriginPosition.y ))
                );

                Vector2Int to = new Vector2Int(
                    Mathf.Max(x+1, globalGridData.Width), 
                    Mathf.Max(y+1,globalGridData.Height)
                );
                
                IncreaseGrid(from, to);

                tileGridCoords.x = x + Mathf.Abs(from.x);
                tileGridCoords.y = y + Mathf.Abs(from.y);
            }
            
            Debug.Log(tileGridCoords);
            
            gridContainer.tileGrids[level].GetGridObject(tileGridCoords.x, tileGridCoords.y).SetTileType(tileType);
            
            gridViewer.DrawGrid();
        }

        
        // from = -OO -> origin | to origin -> +OO
        public void AddMultipleTilesAt(Vector3Int start, Vector3Int end, TileTypeSO tileType) {
            
            var minXY = new Vector2Int(Mathf.Min(start.x, end.x), Mathf.Min(start.y, end.y));
            var maxXY = new Vector2Int(Mathf.Max(start.x, end.x), Mathf.Max(start.y, end.y));
            
            if (!globalGridData.IsInBounds(start.x, start.y) || !globalGridData.IsInBounds(end.x, end.y)) {

                Vector2Int from = new Vector2Int(
                    Mathf.Min(start.x, end.x, (int) Mathf.Floor(globalGridData.OriginPosition.x)),
                    Mathf.Min(start.y, end.y, (int) Mathf.Floor(globalGridData.OriginPosition.y))
                );

                Vector2Int to = new Vector2Int(
                    Mathf.Max(start.x, end.x, globalGridData.Width),
                    Mathf.Max(start.y, end.y, globalGridData.Height)
                );
                
                IncreaseGrid(from, to);
                
                minXY.x += Math.Abs(from.x);
                minXY.y += Math.Abs(from.y);
                maxXY.x += Math.Abs(from.x);
                maxXY.y += Math.Abs(from.y);
            }
            
            foreach (var tileGrid in gridContainer.tileGrids) {
                
                for (int x = minXY.x; x <= maxXY.x; x++) {
                    for (int y = minXY.y; y <= maxXY.y; y++) {
                        tileGrid.GetGridObject(x,y).SetTileType(tileType);
                    }
                }
            }
            
            gridViewer.DrawGrid();
        }

        public void FillGrid(TileGrid tileGrid, TileTypeSO tileType) {

            for (int x = 0; x < tileGrid.Width; x++) {
                for (int y = 0; y < tileGrid.Height; y++) {
                    tileGrid.GetGridObject(x,y).SetTileType(tileType);
                }
            }
        }
        
        
        // TODO wording
        // TODO maybe => from -> negChange && to -> positiveChange 
        // from = -OO -> origin | to = origin -> +OO
        public void IncreaseGrid(Vector2Int from, Vector2Int to) {
            // 
            var oldTileGrids = gridContainer.tileGrids;

            globalGridData.Width = to.x + (-from.x);
            globalGridData.Height = to.y + (-from.y);

            
            // Debug.Log($"increase with {from} {to}");
            

            Vector2Int offset = from * -1;
            
            for (int i = 0; i < gridContainer.tileGrids.Count; i++) {
                
                TileGrid newTileGrid = CreateNewTileGrid();
                FillGrid(newTileGrid, tileTypesContainer.tileTypes[0]);
                oldTileGrids[i].CopyTo(newTileGrid, offset);
                gridContainer.tileGrids[i] = newTileGrid;
            }
        }
        
        
        
        


    }
}