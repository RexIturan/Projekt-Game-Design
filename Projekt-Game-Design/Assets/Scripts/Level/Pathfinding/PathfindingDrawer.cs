using System.Collections.Generic;
using Ability;
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

				// TODO: Maybe use a tilemap drawer instead of this one?
				[SerializeField] private DrawPatternEventChannelSO drawPatternEC;
				[SerializeField] private VoidEventChannelSO clearPatternEC;

				[Header("SO References")]
				[SerializeField] private GridDataSO gridData;

				[Header("Visual References")]
				[SerializeField] private Tilemap previewTilemap;
				[SerializeField] private Tilemap previewPathTilemap;
				[SerializeField] private Tilemap previewPatternTilemap;

				[SerializeField] private TileBase previewTile;
				[SerializeField] private TileBase previewPathTile;
				[SerializeField] private TileBase previewPatternTile;

				private void Awake() {
						drawReachableTilesEC.OnEventRaised += DrawPreview;
						clearReachableTilesEC.OnEventRaised += ClearPreviewTilemap;

						drawPathEC.OnEventRaised += DrawPreviewPath;
						clearPathEC.OnEventRaised += ClearPreviewPathTilemap;

						drawPatternEC.OnEventRaised += DrawPattern;
						clearPatternEC.OnEventRaised += ClearPreviewPatternTilemap;
				}

				private void OnDisable() {
						drawReachableTilesEC.OnEventRaised -= DrawPreview;
						clearReachableTilesEC.OnEventRaised -= ClearPreviewTilemap;

						drawPathEC.OnEventRaised -= DrawPreviewPath;
						clearPathEC.OnEventRaised -= ClearPreviewPathTilemap;

						drawPatternEC.OnEventRaised -= DrawPattern;
						clearPatternEC.OnEventRaised -= ClearPreviewPatternTilemap;
				}

				public void DrawPreview(List<PathNode> nodes) {
						ClearPreviewTilemap();
						
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

				public void DrawPattern(Vector3Int gridPos, bool[][] pattern, Vector2Int anchor)
				{
						ClearPreviewPatternTilemap();

						for ( int x = 0; x < pattern.Length; x++ )
						{
								for ( int y = 0; y < pattern[x].Length; y++ )
								{
										if(pattern[x][y])
										{
												Vector3Int patternTileGridPos = gridPos - new Vector3Int(anchor.x, 0, anchor.y) + new Vector3Int(x, 0, y);
												if ( gridData.IsIn3DGridBounds(patternTileGridPos) )
														previewPatternTilemap.SetTile(gridData.GetTilePosFromGridPos(patternTileGridPos), previewPatternTile);
										}
								}
						}
				}

				private void ClearPreviewPathTilemap() {
						previewPathTilemap.ClearAllTiles();
				}

				public void ClearPreviewTilemap() {
						previewTilemap.ClearAllTiles();
				}

				private void ClearPreviewPatternTilemap()
				{
						previewPatternTilemap.ClearAllTiles();
				}
		}
}