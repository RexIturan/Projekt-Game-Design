using System;
using System.Linq;
using UnityEngine;
using GDP01.Util;

namespace Grid {
    [System.Serializable]
    public class TileGrid : GenericGrid1D<Tile> {

        public TileGrid(
            int width,
            int height,
            float cellSize,
            Vector3 originPosition,
            bool showDebug = false,
            Transform debugTextParent = null) :
            base(
                width,
                height,
                cellSize,
                originPosition,
                (GenericGrid1D<Tile> g, int x, int y) => new Tile(g, x, y),
                showDebug,
                debugTextParent) { }

        
        
        public void CopyTo(TileGrid tileGrid, Vector2Int offset) {
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    tileGrid.SetGridObject(x + offset.x, y + offset.y, GetGridObject(x, y));
                }
            }
        }

        public override string ToString() {
            var str = "";

            for (int y = Height - 1; y >= 0; y--) {
                for (int x = 0; x < Width; x++) {
                    str += "[";
                    if (GetGridObject(x, y).Type != null) {
                        str += GetGridObject(x, y).ToString();                        
                    }
                    else {
                        str += " ";
                    }
                    str += "] ";
                }

                str += "\n";
            }

            return str;
        }
    }
}