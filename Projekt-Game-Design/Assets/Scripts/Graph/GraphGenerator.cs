using System.Collections.Generic;
using Graph.ScriptableObjects;
using Grid;
using UnityEngine;

namespace Graph {
    public class GraphGenerator : MonoBehaviour {
        [SerializeField] private GraphContainerSO graphContainer;
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO globalGridData;
        
        
        [SerializeField] private bool diagonal;
        
        public void GenerateGraphFromGrids() {
            graphContainer.basicMovementGraph = new List<NodeGraph>();
            
            foreach (var grid in gridContainer.tileGrids) {
                var graph = CreateNewGraph();
                
                
                
                graphContainer.basicMovementGraph.Add(graph);
            }
        }

        public void GenerateGraph(TileGrid tileGrid) {
            // go through each tile in the grid
            // and generate a pathnode for that
            // and then set the isWalkableFlag 
        }
        
        public NodeGraph CreateNewGraph() {
            NodeGraph graph = new NodeGraph(
                width: globalGridData.Width,
                height: globalGridData.Height,
                cellSize: globalGridData.CellSize,
                originPosition: globalGridData.OriginPosition);
            return graph;
        }
    }
}