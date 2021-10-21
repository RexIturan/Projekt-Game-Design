using System.Collections.Generic;
using System.Linq;
using Graph;
using Util;
using UnityEngine;
using Util.VisualDebug;

namespace Pathfinding {
    public class Pathfinding {

        private const int MoveStraightCost = 10;
        private const int MoveDiagonalCost = 14;

        // todo(vincent) set in constructor, or make it changeable at runtime
        private const bool Debug = true;

        private readonly NodeGraph _graph;
        private List<PathNode> _openList;
        private List<PathNode> _closedList;
        
        public Pathfinding(NodeGraph graph) {
            this._graph = graph;
        }

        /*Calculate list of all reachable nodes from start node using dijkstra
         maxDist is maximum movement distance
         returns list of all reachable nodes*/
        public List<PathNode> GetReachableNodes(int startX, int startY, int maxDist)
        {
            var startNode = _graph.GetGridObject(startX, startY);

            //used to compare nodes by their distance from start node
            IComparer<PathNode> nodeComparer = new CompareNodeDist(); 
            
            List<PathNode> activeNodes = new List<PathNode>();
            
            List<PathNode> closedNodes = new List<PathNode>();

            for (int x = 0; x < _graph.Width; x++) {
                for (int y = 0; y < _graph.Height; y++) {
                    PathNode pathNode = _graph.GetGridObject(x, y);
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

                if (currentNode.edges == null) {
                    continue;    
                }
                
                foreach (var edge in currentNode.edges)
                {
                    if (!closedNodes.Contains(edge.target) && currentNode.dist + edge.cost <= maxDist)
                    {
                        if (currentNode.dist + edge.cost < edge.target.dist)
                        {
                            edge.target.dist = currentNode.dist + edge.cost;
                            edge.target.parentNode = currentNode;
                        }

                        if (!activeNodes.Contains(edge.target))
                        {
                            //get index for insertion
                            var index = activeNodes.BinarySearch(edge.target, nodeComparer);
                            //if there is no exact match index is set to the binary complement of the index of the next element (invert to get index for insertion)
                            if (index < 0) index = ~index; 
                            activeNodes.Insert(index, edge.target);
                            
                            //activeNodes.Add(edge.Target);
                            //activeNodes = activeNodes.OrderBy(x => x.dist).ToList();
                        }
                        
                    }
                }
            }

            return closedNodes;

        }
        

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY, bool ignoreIsWalkable = false) {
            var startNode = _graph.GetGridObject(startX, startY);
            var endNode = _graph.GetGridObject(endX, endY);
            if (endNode == null) return null;
            
            _openList = new List<PathNode> { startNode };
            _closedList = new List<PathNode>();

            for (int x = 0; x < _graph.Width; x++) {
                for (int y = 0; y < _graph.Height; y++) {
                    PathNode pathNode = _graph.GetGridObject(x, y);
                    pathNode.gCost = int.MaxValue;
                    pathNode.hCost = int.MaxValue;
                    pathNode.CalculateFCost();
                    pathNode.parentNode = null;
                }
            }

            startNode.gCost = 0;
            startNode.hCost = CalculateDistanceCost(startNode, endNode);
            startNode.CalculateFCost();

            if (Debug) {
                PathfindingStepsVisualDebug.Instance.ClearSnapshots();
                PathfindingStepsVisualDebug.Instance.TakeSnapshot(_graph, startNode, _openList, _closedList);    
            }

            while (_openList.Count > 0) {
                PathNode currentNode = GetLowestFCostNode(_openList);
                if (currentNode == endNode) { 
                    // Reached Final Node
                    if (Debug) {
                        PathfindingStepsVisualDebug.Instance.TakeSnapshot(_graph, currentNode, _openList, _closedList);
                        PathfindingStepsVisualDebug.Instance.TakeSnapshotFinalPath(_graph, CalculatePath(endNode));
                    }
                    return CalculatePath(endNode);
                }

                _openList.Remove(currentNode);
                _closedList.Add(currentNode);

                foreach (var edge in currentNode.edges) {
                    if(_closedList.Contains(edge.target)) continue;
                    if (!edge.target.isWalkable && !ignoreIsWalkable) {
                        _closedList.Add(edge.target);
                        continue;
                    }
                    
                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, edge.target);
                    if (tentativeGCost < edge.target.gCost) {
                        edge.target.parentNode = currentNode;
                        edge.target.gCost = tentativeGCost;
                        edge.target.hCost = CalculateDistanceCost(edge.target, endNode);
                        edge.target.CalculateFCost();

                        if (!_openList.Contains(edge.target)) {
                            _openList.Add(edge.target);
                        }
                    }

                    if (Debug) {
                        PathfindingStepsVisualDebug.Instance.TakeSnapshot(_graph, currentNode, _openList, _closedList);
                    }
                }
            }
            
            // Out of nodes on the openList
            return null;
        }

        public List<PathNode> CalculatePath(PathNode endNode) {
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
            return MoveDiagonalCost * Mathf.Min(xDistance, yDistance) + MoveStraightCost * remaining;
        }

        private PathNode GetLowestFCostNode(List<PathNode> pathNodeList) {
            PathNode lowestFCostNode = pathNodeList[0];
            for (int i = 1; i < pathNodeList.Count; i++) {
                if (pathNodeList[i].fCost < lowestFCostNode.fCost) 
                    lowestFCostNode = pathNodeList[i];
            }
            return lowestFCostNode;
        }
        
        // are coordinates part of the graph
        //
        public bool IsInBounds(int x, int y)
        {
            return _graph.IsInBounds(x, y);
        }
    }
}