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
        [SerializeField] private GridDataSO gridData;
        
        [Header("Settings")]
        [SerializeField] private bool diagonal;
        
        public void GenerateGraphFromGrids() {
            characterList = CharacterList.FindInstant();
						worldObjectList = WorldObjectList.FindInstant();
            // characterContainer.FillContainer();
            
            graphContainer.basicMovementGraph = new List<NodeGraph>();
            
        //     foreach (var grid in gridContainer.tileGrids) {
								// // TODO: check if this assumption is correct
								// // the y-position of the grid/graph is assumed to be the index within the grid container
        //     }
        
        //TODO make graph for each layer
            var graph = CreateNewGraph(1);
            GenerateGraph(gridData.TileGrids[0], gridData.TileGrids[1], graph);
            graphContainer.basicMovementGraph.Add(graph);
            
            graphDrawer.DrawGraph();
        }

        public void GenerateGraph(TileGrid ground, TileGrid current, NodeGraph graph) {
            // go through each tile in the grid
            // and generate a pathnode for that
            // and then set the isWalkableFlag 
            for (int x = 0; x < ground.Width; x++) {
                for (int z = 0; z < ground.Depth; z++) {
                    var groundType = tileTypeContainer.tileTypes[ground.GetGridObject(x, z).tileTypeID];
                    var currentType = tileTypeContainer.tileTypes[current.GetGridObject(x, z).tileTypeID];
                    var walkable = groundType.properties.HasFlag(TileProperties.Solid) &&
                                   !currentType.properties.HasFlag(TileProperties.Solid);
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
                var pos = gridData.GetGridPos2DFromGridPos3D(enemy.GetComponent<GridTransform>().gridPosition);
                graph.GetGridObject(pos).SetIsWalkable(false);
						}

						foreach ( var player in characterList.playerContainer ) {
								var pos = gridData.GetGridPos2DFromGridPos3D(player.GetComponent<GridTransform>().gridPosition);
								graph.GetGridObject(pos).SetIsWalkable(false);
						}

						foreach ( var npc in characterList.friendlyContainer )
						{
								var pos = gridData.GetGridPos2DFromGridPos3D(npc.GetComponent<GridTransform>().gridPosition);
								graph.GetGridObject(pos).SetIsWalkable(false);
						}

						foreach ( var door in worldObjectList.doors )
						{
								if(!door.GetComponent<Door>().IsOpen)
								{
										var pos = gridData.GetGridPos2DFromGridPos3D(door.GetComponent<GridTransform>().gridPosition);
										graph.GetGridObject(pos).SetIsWalkable(false);
								}
						}

						foreach ( var switchComponent in worldObjectList.switches )
						{
								if(!switchComponent.GetComponent<SwitchComponent>().Type.walkThrough)
								{
										var pos = gridData.GetGridPos2DFromGridPos3D(switchComponent.GetComponent<GridTransform>().gridPosition);
										graph.GetGridObject(pos).SetIsWalkable(false);
								}
						}

						foreach ( var junk in worldObjectList.junks )
						{
								if ( !junk.GetComponent<Junk>().junkType.walkThrough && !junk.GetComponent<Junk>().broken)
								{
										var pos = gridData.GetGridPos2DFromGridPos3D(junk.GetComponent<GridTransform>().gridPosition);
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
                width: gridData.Width,
                height: height,
                depth: gridData.Depth,
                cellSize: gridData.CellSize,
                originPosition: gridData.OriginPosition);
            return graph;
        }
    }
}