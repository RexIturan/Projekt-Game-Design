using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Graph;
using Util;
using UnityEngine;
using Util.VisualDebug;

namespace Pathfinding {
    public class Pathfinding {

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        public static Pathfinding Instance { get; private set; }
        
        private bool diagonal = true;
        private bool debug = false;
        
        private NodeGraph graph;
        private List<PathNode> openList;
        private List<PathNode> closedList;

        
        
        public Pathfinding(NodeGraph graph) {
            this.graph = graph;
        }

        /*Calculate list of all reachable nodes from start node using dijkstra
         maxDist is maximum movement distance
         returns list of all reachable nodes*/
        public List<PathNode> GetReachableNodes(int startX, int startY, int maxDist)
        {
            var startNode = graph.GetGridObject(startX, startY);

            //used to compare nodes by their distance from start node
            IComparer<PathNode> nodeComparer = new CompareNodeDist(); 
            
            List<PathNode> activeNodes = new List<PathNode>();
            
            List<PathNode> closedNodes = new List<PathNode>();

            for (int x = 0; x < graph.Width; x++) {
                for (int y = 0; y < graph.Height; y++) {
                    PathNode pathNode = graph.GetGridObject(x, y);
                    pathNode.dist = int.MaxValue;
                    pathNode.parentNode = null;
                }
            }

            startNode.dist = 0;
            
            activeNodes.Add(startNode);

            while (activeNodes.Any())
            {
                //list of active nodes is sorted by distance -> always handle first list element next
                var currentNode = activeNodes.First();
                activeNodes.RemoveAt(0);
                closedNodes.Add(currentNode);

                if (currentNode.Edges == null) {
                    continue;    
                }
                
                foreach (var edge in currentNode.Edges)
                {
                    if (!closedNodes.Contains(edge.Target) && currentNode.dist + edge.Cost <= maxDist)
                    {
                        if (currentNode.dist + edge.Cost < edge.Target.dist)
                        {
                            edge.Target.dist = currentNode.dist + edge.Cost;
                            edge.Target.parentNode = currentNode;
                        }

                        if (!activeNodes.Contains(edge.Target))
                        {
                            //get index for insertion
                            var index = activeNodes.BinarySearch(edge.Target, nodeComparer);
                            //if there is no exact match index is set to the binary complement of the index of the next element (invert to get index for insertion)
                            if (index < 0) index = ~index; 
                            activeNodes.Insert(index, edge.Target);
                            
                            //activeNodes.Add(edge.Target);
                            //activeNodes = activeNodes.OrderBy(x => x.dist).ToList();
                        }
                        
                    }
                }
            }

            return closedNodes;

        }
        

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY, bool ignoreIsWalkable = false) {
            var startNode = graph.GetGridObject(startX, startY);
            var endNode = graph.GetGridObject(endX, endY);
            if (endNode == null) return null;
            
            openList = new List<PathNode> { startNode };
            closedList = new List<PathNode>();

            for (int x = 0; x < graph.Width; x++) {
                for (int y = 0; y < graph.Height; y++) {
                    PathNode pathNode = graph.GetGridObject(x, y);
                    pathNode.gCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.parentNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            if (debug) {
                PathfindingStepsVisualDebug.Instance.ClearSnapshots();
                PathfindingStepsVisualDebug.Instance.TakeSnapshot(graph, startNode, openList, closedList);    
            }

            while (openList.Count > 0) {
                PathNode currentNode = GetLowestFCostNode(openList);
                if (currentNode == endNode) { 
                    // Reached Final Node
                    if (debug) {
                        PathfindingStepsVisualDebug.Instance.TakeSnapshot(graph, currentNode, openList, closedList);
                        PathfindingStepsVisualDebug.Instance.TakeSnapshotFinalPath(graph, CalculatePath(endNode));
                    }
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (var edge in currentNode.Edges) {
                    if(closedList.Contains(edge.Target)) continue;
                    if (!edge.Target.isWalkable && !ignoreIsWalkable) {
                        closedList.Add(edge.Target);
                        continue;
                    }
                    
                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, edge.Target);
                    if (tentativeGCost < edge.Target.gCost) {
                        edge.Target.parentNode = currentNode;
                        edge.Target.gCost = tentativeGCost;
                        edge.Target.hCost = CalculateDistanceCost(edge.Target, endNode);
                        edge.Target.CalculateFCost();

                        if (!openList.Contains(edge.Target)) {
                            openList.Add(edge.Target);
                        }
                    }

                    if (debug) {
                        PathfindingStepsVisualDebug.Instance.TakeSnapshot(graph, currentNode, openList, closedList);
                    }
                }
            }
            
            // Out of nodes on the openList
            return null;
        }

        private List<PathNode> CalculatePath(PathNode endNode) {
            List<PathNode> path = new List<PathNode>();
            path.Add(endNode);
            var currentNode = endNode;
            while (currentNode.parentNode != null) {
                path.Add(currentNode.parentNode);
                currentNode = currentNode.parentNode;
            }
            path.Reverse();
            return path;
        }
        
        private int CalculateDistanceCost(PathNode a, PathNode b) {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList) {
            PathNode lowestFCostNode = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++) {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost) 
                    lowestFCostNode = pathNodeList[i];
            }
            return lowestFCostNode;
        }
        
    }
}