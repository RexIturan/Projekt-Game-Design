using Graph.ScriptableObjects;
using UnityEngine;

namespace Graph {
    public class GraphDrawer : MonoBehaviour {
        [SerializeField] private GraphContainerSO graphContainer;


        public bool drawGraph;
        
        public void DrawGraph() {
            drawGraph = true;
            // foreach (var graph in GraphContainer.basicMovementGraph)
            // for (var x = 0; x < graph.Width; x++)
            // for (var y = 0; y < graph.Height; y++)
            // foreach (var edge in graph.GetGridObject(x, y).Edges) {
            //     var cellOffset = graph.GetCellCenter();
            //     var originOffset = graph.OriginPosition;
            //     var offset = originOffset + cellOffset;
            //     var nodePos = new Vector3(x, 0, y) + offset;
            //     var tartgetPos = new Vector3(edge.Target.x, 0, edge.Target.y) + offset;
            //     Handles.DrawLine(nodePos, tartgetPos, 2f);
            // }
        }

        private void OnDrawGizmos() {
            if (drawGraph) {
                foreach (var graph in graphContainer.basicMovementGraph)
                    for (var x = 0; x < graph.Width; x++)
                    for (var z = 0; z < graph.Depth; z++) {
                        if (graph.GetGridObject(x, z).edges != null) {
                            foreach (var edge in graph.GetGridObject(x, z).edges) {
                                var cellOffset = graph.GetCellCenter();
                                var originOffset = graph.OriginPosition;
                                var offset = originOffset + cellOffset;
                                var nodePos = new Vector3(x, 1, z) + offset;
                                var tartgetPos = new Vector3(edge.target.pos.x, 1, edge.target.pos.y) + offset;
                                // Handles.DrawLine(nodePos, tartgetPos, 2f);
                                Debug.DrawLine(nodePos, tartgetPos, Color.red);
                            }    
                        }
                    }
            }
        }
    }
}