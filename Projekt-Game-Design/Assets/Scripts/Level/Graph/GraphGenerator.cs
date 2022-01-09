using System.Collections.Generic;
using Characters;
using Graph.ScriptableObjects;
using Grid;
using Level.Grid;
using UnityEngine;
using WorldObjects;

namespace Graph {
    public class GraphGenerator : MonoBehaviour {
        [Header("Scene References")]
        [SerializeField] private GraphDrawer graphDrawer;

        [Header("SO References")] 
        public CharacterList characterList;
				public WorldObjectList worldObjectList;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;
        [SerializeField] private GraphContainerSO graphContainer;
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO globalGridData;
        
        [Header("Settings")]
        [SerializeField] private bool diagonal;
        
        public void GenerateGraphFromGrids() {
            characterList = CharacterList.FindInstant();
						worldObjectList = WorldObjectList.FindWorldObjectList();
            // characterContainer.FillContainer();
            
            graphContainer.basicMovementGraph = new List<NodeGraph>();
            
            foreach (var grid in gridContainer.tileGrids) {
								// TODO: check if this assumption is correct
								// the y-position of the grid/graph is assumed to be the index within the grid container
                var graph = CreateNewGraph(gridContainer.tileGrids.IndexOf(grid));

                GenerateGraph(grid, graph);
                
                graphContainer.basicMovementGraph.Add(graph);
            }
            
            graphDrawer.DrawGraph();
        }

        public void GenerateGraph(TileGrid ground, NodeGraph graph) {
            // go through each tile in the grid
            // and generate a pathnode for that
            // and then set the isWalkableFlag 
            for (int x = 0; x < ground.Width; x++) {
                for (int z = 0; z < ground.Depth; z++) {
                    var type = tileTypeContainer.tileTypes[ground.GetGridObject(x, z).tileTypeID];
                    var walkable = !type.Properties.HasFlag(TileProperties.Solid);// &&
                    //     !current.GetGridObject(x, y).Type.Flags.HasFlag(ETileFlags.solid);
                    graph.GetGridObject(x, z).SetIsWalkable(walkable);
                }
            }
            
            // todo refactor
            // sets the player and enemy pos to not walkable
            // foreach (var player in characterContainer.playerContainer) {
            //     var pos = globalGridData.GridPos3DToGridPos2D(player.gridPosition);
            //     graph.GetGridObject(pos).SetIsWalkable(false);
            // }
            
            foreach (var enemy in characterList.enemyContainer) {
                var pos = globalGridData.GetGridPos2DFromGridPos3D(enemy.GetComponent<GridTransform>().gridPosition);
                graph.GetGridObject(pos).SetIsWalkable(false);
						}

						foreach ( var player in characterList.playerContainer ) {
								var pos = globalGridData.GetGridPos2DFromGridPos3D(player.GetComponent<GridTransform>().gridPosition);
								graph.GetGridObject(pos).SetIsWalkable(false);
						}

						foreach ( var door in worldObjectList.doors )
						{
								if(!door.GetComponent<Door>().open)
								{
										var pos = globalGridData.GetGridPos2DFromGridPos3D(door.GetComponent<GridTransform>().gridPosition);
										graph.GetGridObject(pos).SetIsWalkable(false);
								}
						}

						for (int x = 0; x < graph.Width; x++) {
                for (int z = 0; z < graph.Depth; z++) {
                    graph.GetGridObject(x, z).SetEdges(diagonal, graph);
                }
            }
        }
        
        public NodeGraph CreateNewGraph(int height) {
            NodeGraph graph = new NodeGraph(
                width: globalGridData.Width,
                height: height,
                depth: globalGridData.Depth,
                cellSize: globalGridData.CellSize,
                originPosition: globalGridData.OriginPosition);
            return graph;
        }
    }
}