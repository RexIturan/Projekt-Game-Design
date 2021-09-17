using System;
using System.Linq;
using UnityEngine;
using Util;

namespace Grid {
    [System.Serializable]
    public class TileGrid : GenericGrid<Tile> {

        [SerializeField] private Tile[] testTiles;

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
                (GenericGrid<Tile> g, int x, int y) => new Tile(g, x, y),
                showDebug,
                debugTextParent) {

            testTiles = new Tile[width * height];
            Debug.Log($"{width * height} {Coord2DToIndex(new Vector2Int(width-1, height-1), width, height)}");
            
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    var index = Coord2DToIndex(new Vector2Int(x, y), width, height);
                    Debug.Log(index);
                    testTiles[index] = new Tile(null, x, y);
                }
            }
        }

        public int Coord2DToIndex(Vector2Int coord, int width, int height) {
            return coord.x + coord.y * width;
        }

        public Vector2Int IndexToCoord2D(int index, int width, int height) {
            Vector2Int coord = new Vector2Int(
                x: index % width,
                y: index / width);
            return coord;
        }
        
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