using System;
using Graph.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Graph {
    public class GraphDrawer : MonoBehaviour {
        [SerializeField] private GraphContainerSO GraphContainer;


        public bool drawGraph;
        
        public void DrawGraph() {
            drawGraph = true;
            //
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
                foreach (var graph in GraphContainer.basicMovementGraph)
                    for (var x = 0; x < graph.Width; x++)
                    for (var y = 0; y < graph.Height; y++) {
                        if (graph.GetGridObject(x, y).Edges != null) {
                            foreach (var edge in graph.GetGridObject(x, y).Edges) {
                                var cellOffset = graph.GetCellCenter();
                                var originOffset = graph.OriginPosition;
                                var offset = originOffset + cellOffset;
                                var nodePos = new Vector3(x, 0, y) + offset;
                                var tartgetPos = new Vector3(edge.Target.x, 0, edge.Target.y) + offset;
                                // Handles.DrawLine(nodePos, tartgetPos, 2f);
                                Debug.DrawLine(nodePos, tartgetPos, Color.red);
                            }    
                        }
                    }
            }
        }
    }
}