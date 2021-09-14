using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Visual {
    public class TileMapDrawer : MonoBehaviour,  IMapDrawer {

        [System.Serializable]
        struct tileTypePair {
            public TileTypeSO tileType;
            public TileBase tile;
        }

        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO globalGridData;
        
        [SerializeField] private Tilemap gridTilemap;
        [SerializeField] private Tilemap cursorTilemap;

        [SerializeField] private tileTypePair[] tileTypeTileDict;
        [SerializeField] private TileBase cursor;
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
        
        public void DrawGrid() {
            gridTilemap.ClearAllTiles();

            var offset = new Vector2Int((int)globalGridData.OriginPosition.x, (int)globalGridData.OriginPosition.z); 
            
            for (int l = 0; l < gridContainer.tileGrids.Count; l++) {
                var tileGrid = gridContainer.tileGrids[l];
                for (int x = 0; x < tileGrid.Width; x++) {
                    for (int y = 0; y < tileGrid.Height; y++) {
                        var tile = tileGrid.GetGridObject(x, y).Type;
                        if (tile != null) {
                            gridTilemap.SetTile(
                                new Vector3Int(x + offset.x, y + offset.y, l),
                                GetTileFromTileType(tileGrid.GetGridObject(x, y).Type));    
                        }
                        else {
                            Debug.Log("error tile");
                            gridTilemap.SetTile(
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
    }
}