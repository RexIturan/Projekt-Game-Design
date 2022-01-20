using UnityEngine;
using Util;

namespace Graph {
    [System.Serializable]
    public class NodeGraph : GenericGrid1D<PathNode> {
        
        public NodeGraph(
            int width, 
            int height,
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
                (grid, x, z) => new PathNode(x, height, z), 
                showDebug, 
                debugTextParent) { }
        
        public override string ToString() {
            var str = "";

            for (int z = Depth - 1; z >= 0; z--) {
                for (int x = 0; x < Width; x++) {
                    str += "[";
                    if (GetGridObject(x, z).edges != null) {
                        str += GetGridObject(x, z).ToString();                        
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