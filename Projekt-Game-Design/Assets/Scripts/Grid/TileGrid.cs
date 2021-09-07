using System;
using UnityEngine;
using Util;

namespace Grid {
    [System.Serializable]
    public class TileGrid : GenericGrid<Tile> {
        public TileGrid(
            int width, 
            int height, 
            float cellSize, 
            Vector3 originPosition, 
            Func<GenericGrid<Tile>, int, int, Tile> createGridObject, 
            bool showDebug, 
            Transform debugTextParent = null) : 
            base(
                width, 
                height, 
                cellSize, 
                originPosition, 
                createGridObject, 
                showDebug, 
                debugTextParent) { }
        
        
    }
}