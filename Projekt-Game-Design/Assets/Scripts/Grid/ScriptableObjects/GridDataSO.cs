using UnityEngine;

namespace Grid {
    [System.Serializable]
    [CreateAssetMenu(fileName = "newGridData", menuName = "Grid/GridData", order = 0)]
    public class GridDataSO : ScriptableObject {

        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private float cellSize;
        [SerializeField] private Vector3 originPosition;
        
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
        public bool IsInBounds(int x, int y) {
            return x >= originPosition.x && 
                   y >= originPosition.z && 
                   x < width + originPosition.x && 
                   y < height + originPosition.z;
        }
        
        public bool IsInBounds(Vector2Int pos) {
            return IsInBounds(pos.x, pos.y);
        }

        public void InitValues(GridDataSO newData) {
            width = newData.Width;
            height = newData.Height;
            cellSize = newData.CellSize;
            originPosition = newData.OriginPosition;
        }
    }
}