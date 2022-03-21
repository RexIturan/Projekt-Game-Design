using UnityEngine;
using Util;

namespace Grid {
    [System.Serializable]
    public class TileGrid : GenericGrid1D<Tile> {
        
        public TileGrid(
            int width,
            int depth,
            float cellSize,
            Vector3 originPosition,
            bool showDebug = false,
            Transform debugTextParent = null) :
            base(
                width,
                depth,
                cellSize,
                originPosition,
                (grid, x, z) => new Tile(x, z),
                showDebug,
                debugTextParent) { }

        

        public override string ToString() {
            var str = "";

            for (int z = Depth - 1; z >= 0; z--) {
                for (int x = 0; x < Width; x++) {
                    str += "[";
                    // if (GetGridObject(x, y).tileTypeID >= 0) {
                    //     str += GetGridObject(x, y).ToString();                        
                    // }
                    // else {
                    //     str += " ";
                    // }
                    str += GetGridObject(x, z).ToString();
                    str += "] ";
                }

                str += "\n";
            }

            return str;
        }
    }
}