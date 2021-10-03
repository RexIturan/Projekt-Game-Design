﻿using System;
using UnityEngine;
using Util;

namespace Graph {
    [System.Serializable]
    public class NodeGraph : GenericGrid1D<PathNode> {
        
        public NodeGraph(
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
                (GenericGrid1D<PathNode> g, int x, int y) => new PathNode(g, x, y), 
                showDebug, 
                debugTextParent) { }
        
        public override string ToString() {
            var str = "";

            for (int y = Height - 1; y >= 0; y--) {
                for (int x = 0; x < Width; x++) {
                    str += "[";
                    if (GetGridObject(x, y).Edges != null) {
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