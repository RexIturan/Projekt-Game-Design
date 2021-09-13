using System;
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
            FillGrid(gridContainer.tileGrids[0], tileTypesContainer.tileTypes[1]);
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

        public void AddTileAt(Vector3 pos, TileTypeSO tileType) {
            
            // wordPostoGridPos
            // vector3.floor -> pos.x, pos.z
            var flooredPos = Vector3Int.FloorToInt(pos);
            Vector2Int arrayPos = new Vector2Int(flooredPos.x, flooredPos.z);

            
            
            
            
            AddTileAt(arrayPos, 0 ,tileType);
        }
        
        public void AddTileAt(Vector2Int pos, int level, TileTypeSO tileType) {

            var tilePos = pos;
            
            if (!globalGridData.IsInBounds(pos)) {

                Debug.Log("Out of Bounds");
                
                Vector2Int from = new Vector2Int(
                    Mathf.Min(tilePos.x, (int) Mathf.Floor(globalGridData.OriginPosition.x)), 
                    Mathf.Min(tilePos.y, (int) Mathf.Floor(globalGridData.OriginPosition.z))
                );

                from.x -= (int) Mathf.Floor(globalGridData.OriginPosition.x);
                from.y -= (int) Mathf.Floor(globalGridData.OriginPosition.z);
                
                Vector2Int to = new Vector2Int(
                    Mathf.Max(tilePos.x + 1 - (int) Mathf.Floor(globalGridData.OriginPosition.x), globalGridData.Width), 
                    Mathf.Max(tilePos.y + 1 - (int) Mathf.Floor(globalGridData.OriginPosition.z), globalGridData.Height)
                );
                
                IncreaseGrid(from, to);

                // tilePos.x = tilePos.x + Mathf.Abs(from.x);
                // tilePos.y = tilePos.y + Mathf.Abs(from.y);

                Debug.Log($"pos:{pos} from:{from} to:{to} tilePos:{tilePos}");
            }

            // Debug.Log($"tPos {tilePos} OPos {globalGridData.OriginPosition}");
            
            // bring input -> grid space
            int x = tilePos.x + (int) Mathf.Abs(globalGridData.OriginPosition.x);
            int y = tilePos.y + (int) Mathf.Abs(globalGridData.OriginPosition.z);
            
            // Debug.Log($"tilePosOffsetted {x} {y}");
            
            gridContainer.tileGrids[level].GetGridObject(x, y).SetTileType(tileType);
            
            drawer.DrawGrid();
        }

        public void AddMultipleTilesAt(Vector3 start, Vector3 end, TileTypeSO tileType) {
            
            
            var flooredStartPos = Vector3Int.FloorToInt(start);
            Vector2Int arrayPos = new Vector2Int(flooredStartPos.x, flooredStartPos.z);
            
            
            // worldPosToGridPos
            // floor
            // to int
            // shift -> +
            
            
            
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
            
            drawer.DrawGrid();
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
            var oldTileGrids = gridContainer.tileGrids;

            globalGridData.Width = to.x + Mathf.Abs(from.x);
            globalGridData.Height = to.y + Mathf.Abs(from.y);
            
            Debug.Log($"increaseGrid {from} {to}");
            
            globalGridData.OriginPosition = new Vector3(
                globalGridData.OriginPosition.x + from.x, 
                globalGridData.OriginPosition.y, 
                globalGridData.OriginPosition.z + from.y); 

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