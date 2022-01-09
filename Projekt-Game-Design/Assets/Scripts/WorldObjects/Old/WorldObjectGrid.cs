using UnityEngine;
using Util;

namespace WorldObjects {
    [System.Serializable]
    public class WorldObjectGrid : GenericGrid1D<WorldObject> {
        public WorldObjectGrid(
            int width, int depth, float cellSize, Vector3 originPosition) :
            base(width, depth, cellSize, originPosition,
                (g, x, y) => new WorldObject(g, x, y), false) {
            
        }
        
        public void CopyTo(WorldObjectGrid worldObjectGrid, Vector2Int offset) {
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Depth; y++) {
                    worldObjectGrid.SetGridObject(x + offset.x, y + offset.y, GetGridObject(x, y));
                }
            }
        }
        
    }
}