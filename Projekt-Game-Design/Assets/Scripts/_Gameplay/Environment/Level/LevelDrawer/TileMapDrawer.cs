using System;
using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Visual {
    public class TileMapDrawer : MonoBehaviour,  IMapDrawer {

        [System.Serializable]
        private struct TileTypePair {
            public TileTypeSO tileType;
            public TileBase tile;
        }

        [Header("References")]
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO globalGridData;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;
        
        [Header("Visuals")]
        [SerializeField] private Tilemap[] gridTilemap;
        [SerializeField] private Tilemap cursorTilemap;
        [SerializeField] private Tilemap bottomTilemap;

        [SerializeField] private TileTypePair[] tileTypeTileDict;
        [SerializeField] private TileBase cursor;
        [SerializeField] private TileBase bottomTile;
        [SerializeField] private TileBase errorTile;

        public TileBase GetTileFromTileType(TileTypeSO tileType) {
            TileBase tile = errorTile;
            foreach (var pair in tileTypeTileDict) {
                if (pair.tileType == tileType) {
                    tile = pair.tile;
                }
            }
            return tile;
        }

        public void DrawGridLayout() {
	        bottomTilemap.ClearAllTiles();

	        var width = globalGridData.Width;
	        var depth = globalGridData.Depth;
	        var offset = globalGridData.GetLowerBounds();
	        
	        for ( int x = 0; x < width; x++ ) {
		        for ( int z = 0; z < depth; z++ ) {
							bottomTilemap.SetTile(new Vector3Int(x + offset.x, z + offset.y, 0), bottomTile);			        
		        }
	        }
        }
        
        public void DrawGrid() {
            foreach (var tilemap in gridTilemap) {
                tilemap.ClearAllTiles();    
            }

            for (int l = 0; l < globalGridData.Height; l++) {
                var tileGrid = gridContainer.tileGrids[l];
                for (int x = 0; x < tileGrid.Width; x++) {
                    for (int y = 0; y < tileGrid.Depth; y++) {
                        var tile = tileGrid.GetGridObject(x, y);
                        var type = tileTypeContainer.tileTypes[tile.tileTypeID];
                        if (tile != null) {
                            gridTilemap[l].SetTile(
                                new Vector3Int(x, y, l),
                                GetTileFromTileType(type));    
                        }
                        else {
                            Debug.Log("error tile");
                            gridTilemap[l].SetTile(
                                new Vector3Int(x, y, l),
                                errorTile);    
                        }
                    }
                }
            }
        }

        public void DrawBoxCursorAt(Vector3 start, Vector3 end) {
            cursorTilemap.ClearAllTiles();

            Vector3Int min = Vector3Int.FloorToInt(Vector3.Min(start, end));
            Vector3Int max = Vector3Int.FloorToInt(Vector3.Max(start, end));
        
            for (int x = min.x; x <= max.x; x++) {
                for (int y = min.y; y <= max.y; y++) {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    cursorTilemap.SetTile(tilePos, cursor);
                }
            }    
        }
        
        public void DrawCursorAt(Vector3 pos) {
            cursorTilemap.ClearAllTiles();
            cursorTilemap.SetTile(Vector3Int.FloorToInt(pos), cursor);
        }
        
        public void ClearCursor() {
            cursorTilemap.ClearAllTiles();
        }
    }
}