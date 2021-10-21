using UnityEngine;

namespace Grid {
    [System.Serializable]
    [CreateAssetMenu(fileName = "newGridData", menuName = "Grid/GridData", order = 0)]
    public class GridDataSO : ScriptableObject {

        public int width;
        public int height;
        public float cellSize;
        public Vector3 originPosition;
        
        public int Width {
            get => width;
            set => width = value;
        }
        
        public int Height {
            get => height;
            set => height = value;
        }

        public float CellSize => cellSize;

        public Vector3 OriginPosition {
            get => originPosition;
            set => originPosition = value;
        }

        // TODO refactor to util class
        // public bool IsInBounds(int x, int y) {
        //     return x >= 0 && 
        //            y >= 0 && 
        //            x < width && 
        //            y < height ;
        // }
        //
        // public bool IsInBounds(Vector2Int pos) {
        //     return IsInBounds(pos.x, pos.y);
        // }

        public void InitValues(GridDataSO newData) {
            width = newData.Width;
            height = newData.Height;
            cellSize = newData.CellSize;
            originPosition = newData.OriginPosition;
        }
        
        public Vector2Int WorldPosToGridPos(Vector3 worldPos)
        {
            var lowerBounds = Vector3Int.FloorToInt(OriginPosition);
            var flooredPos = Vector3Int.FloorToInt(worldPos);
            return new Vector2Int(
                x: flooredPos.x + Mathf.Abs(lowerBounds.x),
                y: flooredPos.z + Mathf.Abs(lowerBounds.z));
        }
        
        //todo renaming
        public Vector2Int GridPos3DToGridPos2D(Vector3Int gridPos3D)
        {
            return new Vector2Int(
                x: gridPos3D.x,
                y: gridPos3D.z);
        }

        public Vector3 GetCellCenter() {
            return new Vector3(cellSize, 0, cellSize) * 0.5f;
        }

        public bool IsInGridBounds(Vector2Int pos) {
            return IsInGridBounds(pos.x, pos.y);
        }
        
        public bool IsInGridBounds(int x, int y) {
            return x >= 0 && y >= 0 && x < width && y < height;
        }
    }
}