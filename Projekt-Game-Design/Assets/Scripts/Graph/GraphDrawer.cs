using Graph.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Graph {
    public class GraphDrawer : MonoBehaviour {
        [SerializeField] private GraphContainerSO GraphContainer;

        public void DrawGraph() {
            
            // foreach (var graph in GraphContainer.basicMovementGraph)
            // for (var x = 0; x < graph.Width; x++)
            // for (var y = 0; y < graph.Height; y++)
            // foreach (var edge in graph.GetGridObject(x, y).Edges) {
            //     var nodePos = new Vector3(x, 0, y);
            //     var tartgetPos = new Vector3(edge.Target.x, 0, edge.Target.y);
            //     Handles.DrawLine(nodePos, tartgetPos, 2f);
            //     Debug.DrawLine(nodePos, tartgetPos, Color.red, 1000);
            // }
        }
    }
}