using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VisualDebug;

namespace Util {
    public class Pathfinding {

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        public static Pathfinding Instance { get; private set; }
        
        private bool diagonal = true;
        private bool debug = false;
        
        private GenericGrid<PathNode> grid;
        private List<PathNode> openList;
        private List<PathNode> closedList;

        public GenericGrid<PathNode> GetGrid => grid;
        
        public Pathfinding(int width, int height, bool diagonal, bool debug = true) {
            Instance = this;
            this.debug = debug;
            this.diagonal = diagonal; 
            int cellSize = 1;
            grid = new GenericGrid<PathNode>(width, height, cellSize, Vector3.zero, (GenericGrid<PathNode> g, int x, int y) => new PathNode(g, x, y), debug);
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY, bool ignoreIsWalkable = false) {
            var startNode = grid.GetGridObject(startX, startY);
            var endNode = grid.GetGridObject(endX, endY);
            if (endNode == null) return null;
            
            openList = new List<PathNode> { startNode };
            closedList = new List<PathNode>();

            for (int x = 0; x < grid.Width; x++) {
                for (int y = 0; y < grid.Height; y++) {
                    PathNode pathNode = grid.GetGridObject(x, y);
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
                PathfindingStepsVisualDebug.Instance.TakeSnapshot(grid, startNode, openList, closedList);    
            }

            while (openList.Count > 0) {
                PathNode currentNode = GetLowestFCostNode(openList);
                if (currentNode == endNode) { 
                    // Reached Final Node
                    if (debug) {
                        PathfindingStepsVisualDebug.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
                        PathfindingStepsVisualDebug.Instance.TakeSnapshotFinalPath(grid, CalculatePath(endNode));
                    }
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode)) {
                    if(closedList.Contains(neighbourNode)) continue;
                    if (!neighbourNode.isWalkable && !ignoreIsWalkable) {
                        closedList.Add(neighbourNode);
                        continue;
                    }
                    
                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost) {
                        neighbourNode.parentNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode)) {
                            openList.Add(neighbourNode);
                        }
                    }

                    if (debug) {
                        PathfindingStepsVisualDebug.Instance.TakeSnapshot(grid, currentNode, openList, closedList);
                    }
                }
            }
            
            // Out of nodes on the openList
            return null;
        }

        private List<PathNode> GetNeighbourList(PathNode currentNode) {
            List<PathNode> neighbourList = new List<PathNode>();

            if (currentNode.x - 1 >= 0) {
                // Left
                neighbourList.Add(grid.GetGridObject(currentNode.x -1, currentNode.y));
                if (diagonal) {
                    // Left Down
                    if(currentNode.y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.x -1, currentNode.y -1));
                    // Left Up
                    if(currentNode.y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentNode.x -1, currentNode.y +1));    
                }
            }

            if (currentNode.x + 1 < grid.Width) {
                // Right
                neighbourList.Add(grid.GetGridObject(currentNode.x +1, currentNode.y));
                if (diagonal) {
                    // Right Down
                    if(currentNode.y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.x +1, currentNode.y -1));
                    // Right Up
                    if(currentNode.y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentNode.x +1, currentNode.y +1));    
                }
            }
            
            // Down
            if(currentNode.y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.x, currentNode.y -1));
            // Up
            if(currentNode.y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentNode.x, currentNode.y +1));

            return neighbourList;
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