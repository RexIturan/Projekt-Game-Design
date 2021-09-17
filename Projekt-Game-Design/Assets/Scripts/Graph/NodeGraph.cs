using System;
using UnityEngine;
using GDP01.Util;

namespace Graph {
    [System.Serializable]
    public class NodeGraph : GenericGrid1D<PathNode> {
        
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
                (GenericGrid1D<PathNode> g, int x, int y) => new PathNode(g, x, y), 
                showDebug, 
                debugTextParent) { }
    }
}