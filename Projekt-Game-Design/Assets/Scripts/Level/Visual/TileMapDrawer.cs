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

        [SerializeField] private TileTypePair[] tileTypeTileDict;
        [SerializeField] private TileBase cursor;
        [SerializeField] private TileBase errorTile;

        private void Awake() {
	        Debug.Log(gameObject.GetInstanceID());
        }

        public TileBase GetTileFromTileType(TileTypeSO tileType) {
            TileBase tile = errorTile;
            foreach (var pair in tileTypeTileDict) {
                if (pair.tileType == tileType) {
                    tile = pair.tile;
                }
            }
            return tile;
        }
        
        public void DrawGrid() {
            foreach (var tilemap in gridTilemap) {
                tilemap.ClearAllTiles();    
            }

            var offset = new Vector2Int((int)globalGridData.OriginPosition.x, (int)globalGridData.OriginPosition.z); 
            
            for (int l = 0; l < gridContainer.tileGrids.Count; l++) {
                var tileGrid = gridContainer.tileGrids[l];
                for (int x = 0; x < tileGrid.Width; x++) {
                    for (int y = 0; y < tileGrid.Height; y++) {
                        var tile = tileGrid.GetGridObject(x, y);
                        var type = tileTypeContainer.tileTypes[tile.tileTypeID];
                        if (tile != null) {
                            gridTilemap[l].SetTile(
                                new Vector3Int(x + offset.x, y + offset.y, l),
                                GetTileFromTileType(type));    
                        }
                        else {
                            Debug.Log("error tile");
                            gridTilemap[l].SetTile(
                                new Vector3Int(x + offset.x, y + offset.y, l),
                                errorTile);    
                        }
                    }
                }
            }
        }

        public void DrawBoxCursorAt(Vector3 start, Vector3 end) {
            cursorTilemap.ClearAllTiles();

            var startPos = WorldPosToTileMapPos(start);
            var endPos = WorldPosToTileMapPos(end);
            
            int xMin = Mathf.Min(startPos.x, endPos.x);
            int yMin = Mathf.Min(startPos.y, endPos.y);
            int xMax = Mathf.Max(startPos.x, endPos.x);
            int yMax = Mathf.Max(startPos.y, endPos.y);
        
            for (int x = xMin; x <= xMax; x++) {
                for (int y = yMin; y <= yMax; y++) {
                    Vector3Int tilePos = new Vector3Int(x, y, 0);
                    cursorTilemap.SetTile(tilePos, cursor);
                }
            }    
        }
        
        public void DrawCursorAt(Vector3 pos) {
            cursorTilemap.ClearAllTiles();
            cursorTilemap.SetTile(WorldPosToTileMapPos(pos), cursor);
        }
        
        public Vector3Int WorldPosToTileMapPos(Vector3 pos) {
            Vector3Int flooredPos = Vector3Int.FloorToInt(pos);
            return new Vector3Int(flooredPos.x, flooredPos.z, 0);
        }

        public void ClearCursor() {
            cursorTilemap.ClearAllTiles();
        }
    }
}