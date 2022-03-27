using System.Collections.Generic;
using SaveSystem.SaveFormats;
using UnityEditor;
using UnityEngine;
using Util.Extensions;

namespace Grid {
	[System.Serializable]
	[CreateAssetMenu(fileName = "newGridData", menuName = "Grid/GridData", order = 0)]
	public class GridDataSO : ScriptableObject {

		[SerializeField] private string levelName;
		[Header("Data")] 
		[SerializeField] private int width; //world x
		[SerializeField] private int height; //world y
		[SerializeField] private int depth; //world z

		//todo use?
		// [SerializeField] private Vector3Int size;
		[SerializeField] private float cellSize;

		//offset to word space
		[SerializeField] private Vector3 originPosition;
		[SerializeField] private Vector3 _cellCenter;

		//grid bounds
		private Vector3Int maxPosition = new Vector3Int(100, 1, 100);
		private Vector3Int minPosition = new Vector3Int(-100, 0, -100);
		
		//grid container
		private List<TileGrid> tileGrids = new List<TileGrid>();
		public List<TileGrid> TileGrids => tileGrids;

		#region Setter / Getter

		public string LevelName => levelName ??= "No-Name";
		public int Width => width;
		public int Height => height;
		public int Depth => depth;
		public float CellSize => cellSize;

		public Vector3 OriginPosition {
			get => originPosition;
			set => originPosition = value;
		}

		#endregion
		
		public void InitGrids(GridDataSO gridData) {
			var layerNum = gridData.Height;

			tileGrids = new List<TileGrid>();

			for ( int i = 0; i < layerNum; i++ ) {
				tileGrids.Add(CreateNewTileGrid(gridData));
			}
		}

		#region Copy

		public GridDataSO Copy() {
			var newGridData = ScriptableObject.CreateInstance<GridDataSO>();

#if UNITY_EDITOR
			// AssetDatabase.CreateAsset(newGridData, "Assets/newGridData.asset");
#endif
			
			newGridData.InitGrids(this);
			newGridData.InitValues(this);
			
			newGridData.InitValues(this);
			newGridData.InitGrids(this);
			for ( int i = 0; i < TileGrids.Count; i++ ) {
				TileGrids[i].CopyTo(newGridData.TileGrids[i], Vector2Int.zero);
			}

			return newGridData;
		}
		
		public void CopyAllGrids(Vector2Int originOffset, GridDataSO gridData) {
			CopyTileGrid(originOffset, gridData);
		}

		private void CopyTileGrid(Vector2Int originOffset, GridDataSO gridData) {
			var oldTileGrids = tileGrids;
			for ( int i = 0; i < tileGrids.Count; i++ ) {
				TileGrid newTileGrid = CreateNewTileGrid(gridData);

				// if ( i == 0 ) {
				//  FillTileGrid(newTileGrid, tileTypesContainer.tileTypes[1].id);
				// }
				// else {
				//  FillTileGrid(newTileGrid, tileTypesContainer.tileTypes[0].id);
				// }

				oldTileGrids[i].CopyTo(newTileGrid, originOffset * -1);
				tileGrids[i] = newTileGrid;
			}
		}

		#endregion

		#region Create New

		private TileGrid CreateNewTileGrid(GridDataSO gridData) {
			return new TileGrid(
				gridData.Width,
				gridData.Depth,
				gridData.CellSize,
				gridData.OriginPosition
			);
		}

		#endregion
		
		public void InitValues(GridDataSO newData) {
			levelName = newData.LevelName;
			width = newData.Width;
			height = newData.Height;
			depth = newData.Depth;
			cellSize = newData.CellSize;
			originPosition = newData.OriginPosition;

			_cellCenter = new Vector3(cellSize, 0, cellSize) * 0.5f;
		}
		
		public void InitFromSaveValues(GridData_Save gridDataSave, string filename) {
			levelName = filename;
			width = gridDataSave.width;
			height = gridDataSave.height;
			depth = gridDataSave.depth;
			cellSize = gridDataSave.cellSize;
			originPosition = gridDataSave.originPosition;

			_cellCenter = new Vector3(cellSize, 0, cellSize) * 0.5f;
		}
		
		public void Reset() {
			height = 2;
			width = 1;
			depth = 1;
			originPosition = new Vector3(0, 0, 0);
			cellSize = 1;
		}

		public Vector3 GetCellCenter() {
			//todo(vincent) init this better, this is a fix for the level editor
			_cellCenter = new Vector3(cellSize, 0, cellSize) * 0.5f;
			return _cellCenter;
		}

		public Vector3 GetCellCenter3D() {
			//todo(vincent) init this better, this is a fix for the level editor
			_cellCenter = new Vector3(cellSize, cellSize, cellSize) * 0.5f;
			return _cellCenter;
		}

		#region Get Bounds

		// pos in -/- direction
		// returns inclusive bound
		public Vector2Int GetLowerBounds() {
			return GetTilePosFromWorldPos(OriginPosition);
		}

		// pos in -/- direction
		// returns inclusive bound
		public Vector2Int GetUpperBounds() {
			//todo check if corect
			return new Vector2Int(width - 1, depth - 1);
		}

		public Vector2Int GetNewDimensions(Vector2Int gridPos2D, Vector2Int offset) {
			//todo increase, decrese ??
			var upper = GetUpperBounds();
			return new Vector2Int(
				Mathf.Max(gridPos2D.x, upper.x) + 1,
				Mathf.Max(gridPos2D.y, upper.y) + 1
			) + offset.Abs();
		}

		// works on grid positions
		/// <summary>
		/// 
		/// </summary>
		/// <param name="gridPos2D"></param>
		/// <returns>([0 | -x], [0 | -y])</returns>
		public Vector2Int GetOriginOffset(Vector2Int gridPos2D) {
			//todo increase, decrease
			var lower = Vector2Int.zero;
			return Vector2Int.Min(gridPos2D, lower);
		}

		public void ChangeBounds(int newWidth, int newDepth, Vector2Int originOffset) {
			this.width = newWidth;
			this.depth = newDepth;
			this.originPosition = new Vector3(
				x: OriginPosition.x + originOffset.x,
				y: OriginPosition.y,
				z: OriginPosition.z + originOffset.y);
		}

		#endregion

		#region Exchange Positions

		/// <summary>
		/// Calculates the 2D Grid Pos for a World Position
		/// The Y value gets lost
		/// </summary>
		/// <param name="worldPos"></param>
		/// <returns></returns>
		public Vector2Int GetGridPos2DFromWorldPos(Vector3 worldPos) {
			return GetGridPos2DFromGridPos3D(GetGridPos3DFromWorldPos(worldPos));
		}

		/// <summary>
		/// Calculates the 3D Grid Pos for a World Position
		/// The Y value gets lost
		/// </summary>
		/// <param name="worldPos"></param>
		/// <returns></returns>
		public Vector3Int GetGridPos3DFromWorldPos(Vector3 worldPos) {
			var lowerBounds = Vector3Int.FloorToInt(OriginPosition);
			var flooredPos = Vector3Int.FloorToInt(worldPos);

			return flooredPos + lowerBounds.Abs();
		}

		// public Vector3Int GetGridPosWithoutOffsetFromWorldPos(Vector3 worldPos) {
		//  return Vector3Int.FloorToInt(worldPos);
		// }


		/// <summary>
		/// Converts a 3D Grid Position into a 2D Grid Pos 
		/// </summary>
		/// <param name="gridPos3D"> valid 3D Grid Pos </param>
		/// <returns> valid 2D Grid Pos </returns>
		public Vector2Int GetGridPos2DFromGridPos3D(Vector3Int gridPos3D) {
			return new Vector2Int(
				x: gridPos3D.x,
				y: gridPos3D.z);
		}

		public Vector2Int GetTilePosFromWorldPos(Vector3 worldPos) {
			var flooredPos = Vector3Int.FloorToInt(worldPos);
			return new Vector2Int(flooredPos.x, flooredPos.z);
		}

		public Vector3Int GetTilePos3DFromWorldPos(Vector3 worldPos) {
			return Vector3Int.FloorToInt(worldPos);
		}

		/* todo: is this right? */
		public Vector3Int GetTilePosFromGridPos(Vector3Int gridPos) {
			var lowerBounds = Vector3Int.FloorToInt(OriginPosition);
			return new Vector3Int(gridPos.x, gridPos.z, gridPos.y) + 
								new Vector3Int(lowerBounds.x, lowerBounds.z, lowerBounds.y);
		}

		public Vector3 GetTileCenter3DFromWorldPos(Vector3 worldPos) {
			var flooredPos = GetTilePos3DFromWorldPos(worldPos);
			return GetTilePos3DFromWorldPos(worldPos) + GetCellCenter3D();
		}

		public Vector3 GetTileCenter2DFromWorldPos(Vector3 worldPos) {
			return GetTilePos3DFromWorldPos(worldPos) + GetCellCenter();
		}

		private Vector2Int GetGridPosFromTilePos(Vector2Int pos, Vector2Int lowerBounds) {
			// shift pos into grid space
			return pos + lowerBounds.Abs();
		}

		public Vector3Int GetWorldPosFromGridPos(int x, int y, int z) {
			var lowerBounds = Vector3Int.FloorToInt(OriginPosition);
			return new Vector3Int(x, y, z) + lowerBounds;
		}
		
		public Vector3 GetWorldPosFromGridPos(Vector3Int gridPos) {
			
			var pos = gridPos + GetCellCenter();
			pos *= CellSize;
			pos += OriginPosition;

			return pos;
		}

		public Vector3Int GetGridPosInBounds(Vector3Int gridPos3D) {
			var newX = Mathf.Clamp(gridPos3D.x, minPosition.x, maxPosition.x); 
			var newY = Mathf.Clamp(gridPos3D.y, minPosition.y, maxPosition.y);
			var newZ = Mathf.Clamp(gridPos3D.z, minPosition.z, maxPosition.z);
			
			return new Vector3Int {
				x = newX, y =newY, z = newZ
			};
		}
		
		#endregion

		#region Bound Check

		// bounds are inclusive
		
		#region 3D bounds check
		
		public bool IsIn3DGridBounds(Vector3 worldPos) {
			return IsIn3DGridBounds(GetGridPos3DFromWorldPos(worldPos));
		}

		public bool IsIn3DGridBounds(Vector3Int gridPos) {
			return IsIn3DGridBounds(gridPos.x, gridPos.y, gridPos.z);
		}

		public bool IsIn3DGridBounds(int x, int y, int z) {
			return IsIn3DGridBounds(x, y, z, Vector3Int.zero, new Vector3Int(width - 1, height - 1, depth - 1));
		}

		// inclusive border
		public bool IsIn3DGridBounds(Vector3Int gridPos, Vector3Int lowerBounds,
			Vector3Int upperBounds) {
			return IsIn3DGridBounds(gridPos.x, gridPos.y, gridPos.z, lowerBounds, upperBounds);
		}
		
		public bool IsIn3DGridBounds(int x, int y, int z, Vector3Int lowerBounds, Vector3Int upperBounds) {
			return x >= lowerBounds.x &&
			       y >= lowerBounds.y &&
			       z >= lowerBounds.z &&
			       x <= upperBounds.x &&
			       y <= upperBounds.y &&
			       z <= upperBounds.z;
		}
		#endregion
		
		#region 2D bounds check

		public bool IsIn2DGridBounds(Vector2Int gridPos) {
			return IsIn2DGridBounds(gridPos.x, gridPos.y);
		}

		public bool IsIn2DGridBounds(int x, int y) {
			return IsIn2DGridBounds(x, y, Vector2Int.zero, new Vector2Int(width - 1, depth - 1));
		}

		// inclusive border
		public bool IsIn2DGridBounds(Vector2Int gridPos, Vector2Int lowerBounds,
			Vector2Int upperBounds) {
			return IsIn2DGridBounds(gridPos.x, gridPos.y, lowerBounds, upperBounds);
		}

		// inclusive border
		public bool IsIn2DGridBounds(int x, int y, Vector2Int lowerBounds, Vector2Int upperBounds) {
			return x >= lowerBounds.x &&
			       y >= lowerBounds.y &&
			       x <= upperBounds.x &&
			       y <= upperBounds.y;
		}

		#endregion

		#endregion

		#if UNITY_EDITOR
		#endif
		
		[ContextMenu("Save GridData As new Asset")]
		public void SaveGridDataAs() {
			//clone	
			var newGridData = Copy();
			newGridData.name = LevelName;
			
#if UNITY_EDITOR
			AssetDatabase.CreateAsset(newGridData, "Assets/testGridData_2.asset");
			// AssetDatabase.SaveAssets();
#endif
		}

		
	}
}