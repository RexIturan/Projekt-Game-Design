﻿using System.Collections.Generic;
using Graph.ScriptableObjects;
using Grid;
using Level.Grid;
using UnityEngine;

namespace Graph {
    public class GraphGenerator : MonoBehaviour {
        [Header("Scene References")]
        [SerializeField] private GraphDrawer graphDrawer;

        [Header("SO References")] 
        public CharacterList characterList;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;
        [SerializeField] private GraphContainerSO graphContainer;
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO globalGridData;
        
        [Header("Settings")]
        [SerializeField] private bool diagonal;
        
        public void GenerateGraphFromGrids() {
            characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
            // characterContainer.FillContainer();
            
            graphContainer.basicMovementGraph = new List<NodeGraph>();
            
            foreach (var grid in gridContainer.tileGrids) {
                var graph = CreateNewGraph();

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
                for (int y = 0; y < ground.Height; y++) {
                    var type = tileTypeContainer.tileTypes[ground.GetGridObject(x, y).tileTypeID];
                    var walkable = !type.Properties.HasFlag(TileProperties.Solid);// &&
                    //     !current.GetGridObject(x, y).Type.Flags.HasFlag(ETileFlags.solid);
                    graph.GetGridObject(x, y).SetIsWalkable(walkable);
                }
            }
            
            // todo refactor
            // sets the player and enemy pos to not walkable
            // foreach (var player in characterContainer.playerContainer) {
            //     var pos = globalGridData.GridPos3DToGridPos2D(player.gridPosition);
            //     graph.GetGridObject(pos).SetIsWalkable(false);
            // }
            
            foreach (var enemy in characterList.enemyContainer) {
                var pos = globalGridData.GridPos3DToGridPos2D(enemy.GetComponent<EnemyCharacterSC>().gridPosition);
                graph.GetGridObject(pos).SetIsWalkable(false);
            }    
            
            for (int x = 0; x < graph.Width; x++) {
                for (int y = 0; y < graph.Height; y++) {
                    graph.GetGridObject(x, y).SetEdges(diagonal, graph);
                }
            }
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