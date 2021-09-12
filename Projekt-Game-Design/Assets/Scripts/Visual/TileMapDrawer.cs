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
        
        [SerializeField] private Tilemap gridTilemap;
        [SerializeField] private Tilemap cursorTilemap;

        [SerializeField] private tileTypePair[] tileTypeTileDict;
        [SerializeField] private TileBase cursor;
        [SerializeField] private TileBase erroeTile;

        public TileBase GetTileFromTileType(TileTypeSO tileType) {
            TileBase tile = erroeTile;
            foreach (var pair in tileTypeTileDict) {
                if (pair.tileType == tileType) {
                    tile = pair.tile;
                }
            }
            return tile;
        }
        
        public void DrawGrid() {
            gridTilemap.ClearAllTiles();
            
            for (int l = 0; l < gridContainer.tileGrids.Count; l++) {
                var tileGrid = gridContainer.tileGrids[l];
                for (int x = 0; x < tileGrid.Width; x++) {
                    for (int y = 0; y < tileGrid.Height; y++) {
                        gridTilemap.SetTile(
                            new Vector3Int(x, y, l),
                            GetTileFromTileType(tileGrid.GetGridObject(x,y).Type));
                    }
                }
            }
        }

        public void DrawBoxCursorAt(Vector3 start, Vector3 end) {
            
        }
        
        public void DrawCursorAt(Vector3 pos) {
            
        }
    }
}