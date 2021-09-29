using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;

namespace Pathfinding {
    public class PathfindingDrawer : MonoBehaviour {
        [Header("Recieving Events On")]
        [SerializeField] private NodeListEventChannelSO drawReachableTilesEC;
        [SerializeField] private VoidEventChannelSO clearReachableTilesEC;
        
        [Header("SO References")]
        [SerializeField] private GridDataSO globalGridData;
        
        [Header("Visual References")]
        [SerializeField] private Tilemap previewTilemap;
        [SerializeField] private Tilemap previewPathTilemap;

        [SerializeField] private TileBase previewTile;
        [SerializeField] private TileBase previewPathTile;

        private void Awake() {
            drawReachableTilesEC.OnEventRaised += DrawPreview;
            clearReachableTilesEC.OnEventRaised += ClearPreviewTilemap;
        }

        public void DrawPreview(List<PathNode> nodes) {
            ClearPreviewTilemap();
            
            Debug.Log("Draw Preview Tilemap, inside drawer");
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
            Debug.Log("Clear Preview Tilemap, inside drawer");
            previewTilemap.ClearAllTiles();
        }
        
        // todo move to globla grid data or so
        private Vector2Int GridPosToTilePos(int x, int y) {
            var offset = Vector3Int.FloorToInt(globalGridData.OriginPosition);
            return new Vector2Int(x + offset.x, y + offset.z);
        }
    }
}