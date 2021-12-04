using System.Collections.Generic;
using Characters;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.PlayerCharacter.ScriptableObjects;
using Level.Grid;
using Level.Grid.CharacterGrid;
using Level.Grid.ItemGrid;
using Level.Grid.ObjectGrid;
using UnityEngine;
using Util.Extensions;

namespace Grid {
	//todo rename -> GridEditor??
	public class GridController : MonoBehaviour {
		[SerializeField] private GridContainerSO gridContainer;
		[SerializeField] private GridDataSO gridData;

		[SerializeField] private TileTypeContainerSO tileTypesContainer;

		#region private functions
		
		private void FillTileGrid(TileGrid tileGrid, int tileTypeID) {
			for ( int x = 0; x < tileGrid.Width; x++ ) {
				for ( int y = 0; y < tileGrid.Height; y++ ) {
					tileGrid.GetGridObject(x, y).SetTileType(tileTypeID);
				}
			}
		}

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
			gridContainer.CopyAllGrids(originOffset, gridDataSO);
		}
		
		#endregion
		
		#region Add One

		public void AddItemAt(Vector3 pos, int itemId) {
			AddItemAt(gridData.GetGridPos3DFromWorldPos(pos), itemId);
		}
		
		// public void AddCharacterAt(Vector3 pos, Faction faction) {
		// 	AddCharacterAt(gridData.GetGridPos3DFromWorldPos(pos), faction);
		// }
		
		public void AddEnemyCharacterAt(Vector3 pos, Faction faction, EnemySpawnDataSO enemySpawnDataSO, EnemyTypeSO enemyTypeSO) {
			
			AddCharacterAt(gridData.GetGridPos3DFromWorldPos(pos), faction, null, null);
		}
		
		public void AddPlayerCharacterAt(Vector3 pos, Faction faction, PlayerSpawnDataSO playerSpawnDataSO, PlayerTypeSO playerTypeSO) {
			AddCharacterAt(gridData.GetGridPos3DFromWorldPos(pos), faction, null, null);
		}
		
		public void AddTileAt(Vector3 pos, int tileTypeID) {
			AddTileAt(gridData.GetGridPos3DFromWorldPos(pos), tileTypeID);
		}

		private void AddCharacterAt(Vector3Int gridPos, Faction faction, PlayerCharacterSC playerCharacterSC, EnemyCharacterSC enemyCharacterSC) {
			
			var layer = gridPos.y;
			var gridPos2D = gridData.GetGridPos2DFromGridPos3D(gridPos);

			ResizeGrids(gridPos2D, out var finalPos);
			
			SetCharacterAt(finalPos.x, layer, finalPos.y, faction, playerCharacterSC, enemyCharacterSC);
		}

		private void AddItemAt(Vector3Int gridPos, int itemId) {
			var layer = gridPos.y;
			var gridPos2D = gridData.GetGridPos2DFromGridPos3D(gridPos);

			ResizeGrids(gridPos2D, out var finalPos);
			
			SetItemAt(finalPos, layer, itemId);
		}
		
		private void AddTileAt(Vector3Int gridPos, int tileTypeID) {

			var layer = gridPos.y;
			var gridPos2D = gridData.GetGridPos2DFromGridPos3D(gridPos);

			ResizeGrids(gridPos2D, out var finalPos);
			
			if ( gridContainer.tileGrids != null ) {
				if ( gridContainer.tileGrids.Count > layer ) {
					if ( gridContainer.tileGrids[layer] != null ) {
						gridContainer.tileGrids[layer].GetGridObject(finalPos.x, finalPos.y)
							.SetTileType(tileTypeID);
					}
				}
			}
		}
		
		private void SetCharacterAt(int x, int y, int z, Faction faction, PlayerCharacterSC playerCharacterSC, EnemyCharacterSC enemyCharacterSC) {
			//todo char data
			if ( gridContainer.characters != null ) {
				if ( gridContainer.characters.Length > y ) {
					if ( gridContainer.characters[y] != null ) {
						gridContainer.characters[y].GetGridObject(x, z).SetCharData(faction, playerCharacterSC, enemyCharacterSC);
					}
				}
			}
		}

		private void SetItemAt(Vector2Int pos, int layer, int itemId) {
			Debug.Log($"SetItemAt {pos} {itemId} |{gridContainer.items[layer].Width}, {layer}, {gridContainer.items[layer].Height}");

			var tileId = gridContainer.tileGrids[layer].GetGridObject(pos).tileTypeID;
			var tile = tileTypesContainer.tileTypes[tileId];
			if ( ! tile.properties.HasFlag(TileProperties.Solid | TileProperties.Opaque) ) {
				gridContainer.items[layer].GetGridObject(pos).SetId(itemId);	
			}
		}
		
		#endregion

		#region Remove One

		// public void RemoveTileAt()

		public void RemoveItemAt(Vector3 worldPos) {
			AddItemAt(worldPos, -1);
		}
		
		#endregion

		#region Add Many

		public void AddMultipleTilesAt(Vector3 start, Vector3 end, int tileTypeID) {
			// AddMultipleTilesAt(WorldPosToTilePos(start), WorldPosToTilePos(end), tileTypeID);
			AddMultipleTilesAt(
				gridPos3DStart: gridData.GetGridPos3DFromWorldPos(start),
				gridPos3DEnd: gridData.GetGridPos3DFromWorldPos(end), 
				tileTypeID);
		}

		private void AddMultipleTilesAt(Vector3Int gridPos3DStart, Vector3Int gridPos3DEnd, int tileTypeID) {

			var min3D = Vector3Int.Min(gridPos3DStart, gridPos3DEnd);
			var max3D = Vector3Int.Max(gridPos3DStart, gridPos3DEnd);

			var min2D = gridData.GetGridPos2DFromGridPos3D(min3D);
			var max2D = gridData.GetGridPos2DFromGridPos3D(max3D);

			ResizeGrids(min2D, max2D, out var newMin2D, out var newMax2D);

			for ( int y = min3D.y; y <= max3D.y; y++ ) {
				var tileGrid = gridContainer.tileGrids[y];
				for ( int x = newMin2D.x; x <= newMax2D.x; x++ ) {
					for ( int z = newMin2D.y; z <= newMax2D.y; z++ ) {
						tileGrid.GetGridObject(x, z).SetTileType(tileTypeID);
					}
				}
			}
		}
		
		#endregion

		public void ResetGrid() {
			gridData.Reset();

			gridContainer.InitGrids(gridData);
			FillTileGrid(gridContainer.tileGrids[0], tileTypesContainer.tileTypes[1].id);
			FillTileGrid(gridContainer.tileGrids[1], tileTypesContainer.tileTypes[0].id);
		}
	}
}