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
				[SerializeField] private NodeListEventChannelSO drawPathEC;
				[SerializeField] private VoidEventChannelSO clearPathEC;

				[Header("SO References")]
				[SerializeField] private GridDataSO gridData;

				[Header("Visual References")]
				[SerializeField] private Tilemap previewTilemap;
				[SerializeField] private Tilemap previewPathTilemap;

				[SerializeField] private TileBase previewTile;
				[SerializeField] private TileBase previewPathTile;

				private void Awake() {
						drawReachableTilesEC.OnEventRaised += DrawPreview;
						clearReachableTilesEC.OnEventRaised += ClearPreviewTilemap;
						drawPathEC.OnEventRaised += DrawPreviewPath;
						clearPathEC.OnEventRaised += ClearPreviewPathTilemap;
				}

				private void OnDisable() {
						drawReachableTilesEC.OnEventRaised -= DrawPreview;
						clearReachableTilesEC.OnEventRaised -= ClearPreviewTilemap;
						drawPathEC.OnEventRaised -= DrawPreviewPath;
						clearPathEC.OnEventRaised -= ClearPreviewPathTilemap;
				}

				public void DrawPreview(List<PathNode> nodes) {
						ClearPreviewTilemap();

						Debug.Log("Draw Preview Tilemap, inside drawer");
						foreach ( var node in nodes ) {
								previewTilemap.SetTile(gridData.GetTilePosFromGridPos(node.pos), previewTile);
						}
				}

				public void DrawPreviewPath(List<PathNode> nodes) {
						ClearPreviewPathTilemap();
						if ( nodes != null ) {
								foreach ( var node in nodes ) {
										previewPathTilemap.SetTile(gridData.GetTilePosFromGridPos(node.pos), previewPathTile);
								}
						}
				}

				private void ClearPreviewPathTilemap() {
						previewPathTilemap.ClearAllTiles();
				}

				public void ClearPreviewTilemap() {
						// Debug.Log("Clear Preview Tilemap, inside drawer");
						previewTilemap.ClearAllTiles();
				}
    }
}