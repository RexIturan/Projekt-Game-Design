using UnityEngine;
using Util.Extensions;

namespace Grid {
	//todo rename -> GridEditor??
	public class GridController : MonoBehaviour {
		[SerializeField] private GridDataSO gridData;

		[SerializeField] private TileTypeContainerSO tileTypesContainer;

		#region private functions
		
		private void FillTileGrid(TileGrid tileGrid, int tileTypeID) {
			for ( int x = 0; x < tileGrid.Width; x++ ) {
				for ( int y = 0; y < tileGrid.Depth; y++ ) {
					tileGrid.GetGridObject(x, y).SetTileType(tileTypeID);
				}
			}
		}

		//todo use vec3
		private void ResizeGrids(Vector2Int gridPos, out Vector2Int newGridPos) {
			newGridPos = gridPos;
			if ( !gridData.IsIn2DGridBounds(gridPos) ) {
			
				// calc new offset & new dimensions
				// 0 or something negative
				Vector2Int originOffset = gridData.GetOriginOffset(gridPos);
				// bigger then before??
				Vector2Int newDimensions = gridData.GetNewDimensions(gridPos, originOffset);
				
				newGridPos = gridPos + originOffset.Abs();

				Debug.Log($"pos:{gridPos}| offset{originOffset} new dim{newDimensions}| oldDim {gridData.Width}|{gridData.Depth}");
				
				// resize grid
				ResizeGrid(originOffset, newDimensions, gridData);
			}
		}

		private void ResizeGrids(Vector2Int min2D, Vector2Int max2D, out Vector2Int newMin2D, out Vector2Int newMax2D) {

			newMin2D = min2D;
			newMax2D = max2D;
			
			if ( !gridData.IsIn2DGridBounds(min2D) || !gridData.IsIn2DGridBounds(max2D) ) {

				// calc new offset & new dimensions
				// 0 or something negative
				Vector2Int originOffset = gridData.GetOriginOffset(min2D);
				// bigger then before??
				Vector2Int newDimensions = gridData.GetNewDimensions(max2D, originOffset);
				
				newMin2D = min2D + originOffset.Abs();
				newMax2D = max2D + originOffset.Abs();
				
				Debug.Log($"min:{min2D} max:{max2D} | offset{originOffset} new dim{newDimensions}| oldDim {gridData.Width}|{gridData.Depth}");
				
				ResizeGrid(originOffset, newDimensions, gridData);				
			}
		}
		
		//todo maybe move to grid data or gridContainer??
		private void ResizeGrid(Vector2Int originOffset, Vector2Int newDimensions, GridDataSO gridDataSO) {
			
			gridDataSO.ChangeBounds(newDimensions.x, newDimensions.y, originOffset);
			
			//copy grids into the new grids
			gridData.CopyAllGrids(originOffset, gridDataSO);
		}
		
		#endregion
		
		#region Get
		
		/// <summary>
		/// Returns the tile type at given position. 
		/// </summary>
		/// <param name="gridPos">Position within the grid </param>
		/// <returns>Type of corresponding tile </returns>
		public TileTypeSO GetTileAt(Vector3Int gridPos) {
			int id = -1;
			if( gridPos.y < gridData.TileGrids.Count ) {
				Tile tile = gridData.TileGrids[gridPos.y].GetGridObject(gridPos.x, gridPos.z);

				if (tile != null) { 
					id = tile.tileTypeID;
				}
			}

			return id >= 0 ? tileTypesContainer.tileTypes[id] : null;
		}

		#endregion

		#region Add One

		public void AddTileAt(Vector3 pos, TileTypeSO tileType) {
			AddTileAt(gridData.GetGridPos3DFromWorldPos(pos), tileType.id);
		}
		
		private void AddTileAt(Vector3Int gridPos, int tileTypeID) {
			var posInBounds = gridData.GetGridPosInBounds(gridPos);
			var gridPos2D = gridData.GetGridPos2DFromGridPos3D(posInBounds);

			ResizeGrids(gridPos2D, out var finalPos);

			SetTileAt(finalPos.x, posInBounds.y, finalPos.y, tileTypeID);
		}

		private void SetTileAt(int x, int y, int z, int tileTypeID) {
			
			if ( gridData.TileGrids != null ) {
				if ( gridData.TileGrids.Count > y ) {
					if ( gridData.TileGrids[y] != null ) {
						gridData.TileGrids[y].GetGridObject(x, z)
							.SetTileType(tileTypeID);
					}
				}
			}
		}

		#endregion

		#region Remove One
		
		public void RemoveTileAt(Vector3 worldPos) {
			AddTileAt(gridData.GetGridPos3DFromWorldPos(worldPos), 0);
		}

		#endregion

		#region Add Many

		public void AddMultipleTilesAt(Vector3 start, Vector3 end, int tileTypeID) {
			AddMultipleTilesAt(
				gridPos3DStart: gridData.GetGridPos3DFromWorldPos(start),
				gridPos3DEnd: gridData.GetGridPos3DFromWorldPos(end), 
				tileTypeID);
		}

		private void AddMultipleTilesAt(Vector3Int gridPos3DStart, Vector3Int gridPos3DEnd, int tileTypeID) {

			var startInBounds = gridData.GetGridPosInBounds(gridPos3DStart);
			var endInBounds = gridData.GetGridPosInBounds(gridPos3DEnd);
			
			var min3D = Vector3Int.Min(startInBounds, endInBounds);
			var max3D = Vector3Int.Max(startInBounds, endInBounds);

			var min2D = gridData.GetGridPos2DFromGridPos3D(min3D);
			var max2D = gridData.GetGridPos2DFromGridPos3D(max3D);

			ResizeGrids(min2D, max2D, out var newMin2D, out var newMax2D);

			for ( int y = min3D.y; y <= max3D.y; y++ ) {
				var tileGrid = gridData.TileGrids[y];
				for ( int x = newMin2D.x; x <= newMax2D.x; x++ ) {
					for ( int z = newMin2D.y; z <= newMax2D.y; z++ ) {
						tileGrid.GetGridObject(x, z).SetTileType(tileTypeID);
					}
				}
			}
		}
		
		#endregion

		#region Remove many

		public void RemoveMultipleTilesAt(Vector3 start, Vector3 end) {
			AddMultipleTilesAt(start, end, 0);
		}
		
		#endregion

		public void ResetGrid() {
			gridData.Reset();

			gridData.InitGrids(gridData);
			FillTileGrid(gridData.TileGrids[0], tileTypesContainer.tileTypes[1].id);
			FillTileGrid(gridData.TileGrids[1], tileTypesContainer.tileTypes[0].id);
		}
				
		public static GridController FindGridController() {
			GameObject gridControllerGameObject = GameObject.Find("GridController");
			if ( gridControllerGameObject )
				return gridControllerGameObject.GetComponent<GridController>();
			else
				return null;
		}
	}
}