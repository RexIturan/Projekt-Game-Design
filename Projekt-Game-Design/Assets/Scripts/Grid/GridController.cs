using System;
using System.Collections.Generic;
using Input;
using UnityEngine;

namespace Grid {
    public class GridController : MonoBehaviour {

        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO defaultGridData;
        [SerializeField] private GridDataSO globalGridData;

        [SerializeField] private TileTypeSO[] tiles;

        [SerializeField] private GridViewer gridViewer;
        
        public void OnEnable() {

            globalGridData.InitValues(defaultGridData);
            
            gridContainer.tileGrids = new List<TileGrid>();
            
            gridContainer.tileGrids.Add(CreateNewTileGrid());
            gridContainer.tileGrids.Add(CreateNewTileGrid());
            
            foreach (var tileGrid in gridContainer.tileGrids) {
                FillGrid(gridContainer.tileGrids[0], tiles[1]);
                FillGrid(gridContainer.tileGrids[1], tiles[0]);
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
            
            if (!globalGridData.IsInBounds(x, y)) {
                // calculate difference

                Vector2Int from = new Vector2Int(
                    Mathf.Min(x, (int) Mathf.Floor(globalGridData.OriginPosition.x)), 
                    Mathf.Min(y,(int) Mathf.Floor(globalGridData.OriginPosition.y ))
                );

                Vector2Int to = new Vector2Int(
                    Mathf.Max(x, globalGridData.Width), 
                    Mathf.Max(y,globalGridData.Height)
                );
                
                IncreaseGrid(from, to);
            }
            
            gridContainer.tileGrids[level].GetGridObject(x,y).SetTileType(tileType);
            
            gridViewer.DrawGrid();
        }

        
        // from = -OO -> origin | to origin -> +OO
        public void AddMultipleTilesAt(Vector3Int start, Vector3Int end, TileTypeSO tileType) {
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
            }

            foreach (var tileGrid in gridContainer.tileGrids) {
                FillGrid(tileGrid, tileType);
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


            Vector2Int offset = from * -1;
            
            for (int i = 0; i < gridContainer.tileGrids.Count; i++) {
                
                TileGrid newTileGrid = CreateNewTileGrid();
                FillGrid(newTileGrid, tiles[0]);
                oldTileGrids[i].CopyTo(newTileGrid, offset);
                gridContainer.tileGrids[i] = newTileGrid;
            }
        }
        
        
        
        


    }
}