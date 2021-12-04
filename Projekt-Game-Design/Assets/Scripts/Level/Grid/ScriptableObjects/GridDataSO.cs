using UnityEngine;
using Util.Extensions;

namespace Grid {
    [System.Serializable]
    [CreateAssetMenu(fileName = "newGridData", menuName = "Grid/GridData", order = 0)]
    public class GridDataSO : ScriptableObject {

	    //todo make private
        private int width;  //world x
        private int height; //world y
        private int depth;  //world z
        private float cellSize;
        //offset to word space
        private Vector3 originPosition;

        #region Setter / Getter

        public int Width => width;
        public int Height => height;
        public int Depth => depth;
        public float CellSize => cellSize;

        public Vector3 OriginPosition {
	        get => originPosition;
	        set => originPosition = value;
        }

        #endregion

        private Vector3 _cellCenter;
        
        
        public void InitValues(GridDataSO newData) {
            width = newData.Width;
            height = newData.Height;
            depth = newData.Depth;
            cellSize = newData.CellSize;
            originPosition = newData.OriginPosition;
            
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
	        return new Vector2Int(width-1, depth-1);
        }
        
        public Vector2Int GetNewDimensions(Vector2Int gridPos2D, Vector2Int offset) {
	        //todo increase, decrese ??
	        var upper = GetUpperBounds();
	        return new Vector2Int(
		        Mathf.Max(gridPos2D.x, upper.x) + 1 ,
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
	        return GetGridPos2DFromGridPos3D( GetGridPos3DFromWorldPos( worldPos ) );
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

        public Vector3Int GetGridPosWithoutOffsetFromWorldPos(Vector3 worldPos) {
	        return Vector3Int.FloorToInt(worldPos);
        }

        public Vector3 GetCellPositionFromWorldPos(Vector3 worldPos) {
	        return GetGridPosWithoutOffsetFromWorldPos(worldPos) + GetCellCenter();
        }
        
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
        
        private Vector2Int GetGridPosFromTilePos(Vector2Int pos, Vector2Int lowerBounds) {
	        // shift pos into grid space
	        return pos + lowerBounds.Abs();
        }
        
        public Vector3Int GetWorldPosFromGridPos(int x, int y, int z) {
	        var lowerBounds = Vector3Int.FloorToInt(OriginPosition);
	        return new Vector3Int(x, y, z) + lowerBounds;
        }
        
        #endregion

        #region Bound Check
        // bounds are inclusive

        #region 2D bounds check
        
        public bool IsIn2DGridBounds(Vector2Int gridPos) {
	        return IsIn2DGridBounds(gridPos.x, gridPos.y);
        }
        
        public bool IsIn2DGridBounds(int x, int y) {
	        return IsIn2DGridBounds(x, y, Vector2Int.zero, new Vector2Int(width-1, depth-1));
        }
        
        // inclusive border
        public bool IsIn2DGridBounds(Vector2Int gridPos, Vector2Int lowerBounds, Vector2Int upperBounds) {
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

        
    }
}