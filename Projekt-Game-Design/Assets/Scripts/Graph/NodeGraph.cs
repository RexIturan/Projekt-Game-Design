using System;
using UnityEngine;
using Util;

namespace Graph {
    [System.Serializable]
    public class NodeGraph : GenericGrid<PathNode> {
        
        public NodeGraph(
                int width, int height, 
                float cellSize, Vector3 originPosition, 
                bool showDebug = false, 
                Transform debugTextParent = null) : 
            base(
                width, 
                height, 
                cellSize, 
                originPosition, 
                (GenericGrid<PathNode> g, int x, int y) => new PathNode(g, x, y), 
                showDebug, 
                debugTextParent) { }
    }
}