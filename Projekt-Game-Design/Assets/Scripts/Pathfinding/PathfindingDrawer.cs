using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;

namespace Pathfinding {
    public class PathfindingDrawer : MonoBehaviour {
        [SerializeField] private GridDataSO globalGridData;
        
        [SerializeField] private Tilemap previewTilemap;
        [SerializeField] private Tilemap previewPathTilemap;

        [SerializeField] private TileBase previewTile;
        [SerializeField] private TileBase previewPathTile;
        
        public void DrawPreview(List<PathNode> nodes) {

            // ClearPreviewTilemap();
            
            foreach (var node in nodes) {
                Vector2Int pos = GridPosToTilePos(node.x, node.y);
                previewTilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), previewTile);                

            }
        }

        public void DrawPreviewPath(List<PathNode> nodes) {
            ClearPreviewPathTilemap();
            if (nodes != null) {
                foreach (var node in nodes) {
                    Vector2Int pos = GridPosToTilePos(node.x, node.y);
                    previewPathTilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), previewPathTile);                
                }    
            }
        }

        private void ClearPreviewPathTilemap() {
            previewPathTilemap.ClearAllTiles();
        }

        public void ClearPreviewTilemap() {
            previewTilemap.ClearAllTiles();
        }
        
        // todo move to globla grid data or so
        private Vector2Int GridPosToTilePos(int x, int y) {
            var offset = Vector3Int.FloorToInt(globalGridData.OriginPosition);
            return new Vector2Int(x + offset.x, y + offset.z);
        }
    }
}