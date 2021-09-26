﻿using System;
using UnityEngine;
using Util;

namespace WorldObjects {
    [System.Serializable]
    public class WorldObjectGrid : GenericGrid1D<WorldObject> {
        public WorldObjectGrid(
            int width, int height, float cellSize, Vector3 originPosition) :
            base(width, height, cellSize, originPosition,
                (GenericGrid1D<WorldObject> g, int x, int y) => new WorldObject(g, x, y), false) {
            
        }
        
        public void CopyTo(WorldObjectGrid worldObjectGrid, Vector2Int offset) {
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    worldObjectGrid.SetGridObject(x + offset.x, y + offset.y, GetGridObject(x, y));
                }
            }
        }
        
    }
}