using System.Collections.Generic;
using Ability;
using Events.ScriptableObjects;
using Grid;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;
using Util.Extensions;

namespace Pathfinding {
	public class PathfindingDrawer : MonoBehaviour {
		[Header("Recieving Events On")] [SerializeField]
		private NodeListEventChannelSO drawReachableTilesEC;

		[SerializeField] private VoidEventChannelSO clearReachableTilesEC;
		[SerializeField] private NodeListEventChannelSO drawPathEC;
		[SerializeField] private VoidEventChannelSO clearPathEC;
		[SerializeField] private NodeListEventChannelSO drawRangeEC;
		[SerializeField] private VoidEventChannelSO clearRangeEC;

		// TODO: Maybe use a tilemap drawer instead of this one?
		[SerializeField] private DrawPatternEventChannelSO drawPatternEC;
		[SerializeField] private VoidEventChannelSO clearPatternEC;

		[SerializeField] private DrawTargetTilesEventChannelSO drawTargetTilesEC;
		[SerializeField] private VoidEventChannelSO clearTargetTilesEC;

		[Header("SO References")] [SerializeField]
		private GridDataSO gridData;

		[Header("Visual References")] [SerializeField]
		private Tilemap previewTilemap;

		[SerializeField] private Tilemap previewPathTilemap;
		[SerializeField] private Tilemap previewPatternTilemap;
		[SerializeField] private Tilemap previewTargetTilemap;

		[SerializeField] private TileBase previewTile;
		[SerializeField] private TileBase previewPathTile;
		[SerializeField] private TileBase previewRangeTile;
		[SerializeField] private TileBase previewPatternTile;
		[SerializeField] private TileBase enemyTargetTile;
		[SerializeField] private TileBase neutralTargetTile;
		[SerializeField] private TileBase allyTargetTile;
		[SerializeField] private TileBase healTargetTile;

		private void Awake() {
			drawReachableTilesEC.OnEventRaised += DrawPreview;
			clearReachableTilesEC.OnEventRaised += ClearPreviewTilemap;

			drawPathEC.OnEventRaised += DrawPreviewPath;
			clearPathEC.OnEventRaised += ClearPreviewPathTilemap;

			drawRangeEC.OnEventRaised += DrawRange;
			clearRangeEC.OnEventRaised += ClearPreviewTilemap;

			drawPatternEC.OnEventRaised += DrawPattern;
			clearPatternEC.OnEventRaised += ClearPreviewPatternTilemap;

			drawTargetTilesEC.OnEventRaised += DrawTargetTiles;
			clearTargetTilesEC.OnEventRaised += ClearTargetTilesTilemap;
		}

		private void OnDisable() {
			drawReachableTilesEC.OnEventRaised -= DrawPreview;
			clearReachableTilesEC.OnEventRaised -= ClearPreviewTilemap;

			drawPathEC.OnEventRaised -= DrawPreviewPath;
			clearPathEC.OnEventRaised -= ClearPreviewPathTilemap;

			drawRangeEC.OnEventRaised -= DrawRange;
			clearRangeEC.OnEventRaised -= ClearPreviewTilemap;

			drawPatternEC.OnEventRaised -= DrawPattern;
			clearPatternEC.OnEventRaised -= ClearPreviewPatternTilemap;

			drawTargetTilesEC.OnEventRaised -= DrawTargetTiles;
			clearTargetTilesEC.OnEventRaised -= ClearTargetTilesTilemap;
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

		public void DrawRange(List<PathNode> nodes) {
			ClearPreviewPathTilemap();
			if ( nodes != null ) {
				foreach ( var node in nodes ) {
					previewTilemap.SetTile(gridData.GetTilePosFromGridPos(node.pos), previewRangeTile);
				}
			}
		}

		public void DrawPattern(Vector3Int gridPos, bool[,] pattern, Vector2Int anchor) {
			int patternWidth = pattern.GetLength(0);
			int patternHeight = pattern.GetLength(1);

			
			
			ClearPreviewPatternTilemap();
			for ( int y = 0; y < patternHeight; y++ ) {
				for ( int x = 0; x < patternWidth; x++ ) {
					if ( pattern[x, y] ) {
						var pos = gridPos - anchor.XZ();
						pos += new Vector3Int(x, 0, y);
						
						if ( gridData.IsIn3DGridBounds(pos) )
							previewPatternTilemap.SetTile(gridData.GetTilePosFromGridPos(pos),
								previewPatternTile);
					}
				}
			}
		}

		private void DrawTargetTiles(List<PathNode> allies, List<PathNode> neutrals,
			List<PathNode> enemies, bool damage) {
			ClearTargetTilesTilemap();

			if ( damage ) {
				foreach ( var node in allies ) {
					previewTargetTilemap.SetTile(gridData.GetTilePosFromGridPos(node.pos), allyTargetTile);
				}

				foreach ( var node in neutrals ) {
					previewTargetTilemap.SetTile(gridData.GetTilePosFromGridPos(node.pos), neutralTargetTile);
				}

				foreach ( var node in enemies ) {
					previewTargetTilemap.SetTile(gridData.GetTilePosFromGridPos(node.pos), enemyTargetTile);
				}
			}
			else {
				List<PathNode> allNodes = new List<PathNode>();
				allNodes.AddRange(allies);
				allNodes.AddRange(neutrals);
				allNodes.AddRange(enemies);

				foreach ( var node in allNodes ) {
					previewTargetTilemap.SetTile(gridData.GetTilePosFromGridPos(node.pos), healTargetTile);
				}
			}
		}

		private void ClearPreviewPathTilemap() {
			previewPathTilemap.ClearAllTiles();
		}

		public void ClearPreviewTilemap() {
			previewTilemap.ClearAllTiles();
		}

		private void ClearPreviewPatternTilemap() {
			previewPatternTilemap.ClearAllTiles();
		}

		private void ClearTargetTilesTilemap() {
			previewTargetTilemap.ClearAllTiles();
		}
	}
}