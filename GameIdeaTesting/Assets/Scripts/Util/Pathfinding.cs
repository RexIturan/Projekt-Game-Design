using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util {
    public class Pathfinding {

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;
        
        private GenericGrid<PathNode> grid;
        private List<PathNode> openList;
        private List<PathNode> closedList;

        public GenericGrid<PathNode> GetGrid => grid;
        
        public Pathfinding(int width, int height) {
            int cellSize = 1;
            grid = new GenericGrid<PathNode>(width, height, cellSize, Vector3.zero, (GenericGrid<PathNode> g, int x, int y) => new PathNode(g, x, y));
        }

        public List<PathNode> FindPath(int startX, int startY, int endX, int endY) {
            var startNode = grid.GetGridObject(startX, startY);
            var endNode = grid.GetGridObject(endX, endY);
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

            while (openList.Count > 0) {
                PathNode currentNode = GetLowestFCostNode(openList);
                if (currentNode == endNode) { 
                    // Reached Final Node
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode)) {
                    if(closedList.Contains(neighbourNode)) continue;
                    
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
                }
            }
            
            // Out of nodes on the openList
            return null;
        }

        private List<PathNode> GetNeighbourList(PathNode currentNode) {
            List<PathNode> neighbourList = new List<PathNode>();

            if (currentNode.X - 1 >= 0) {
                // Left
                neighbourList.Add(grid.GetGridObject(currentNode.X -1, currentNode.Y));
                // Left Down
                if(currentNode.Y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.X -1, currentNode.Y -1));
                // Left Up
                if(currentNode.Y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentNode.X -1, currentNode.Y +1));
            }

            if (currentNode.X + 1 < grid.Width) {
                // Right
                neighbourList.Add(grid.GetGridObject(currentNode.X +1, currentNode.Y));
                // Right Down
                if(currentNode.Y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.X +1, currentNode.Y -1));
                // Right Up
                if(currentNode.Y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentNode.X +1, currentNode.Y +1));
            }
            
            // Down
            if(currentNode.Y - 1 >= 0) neighbourList.Add(grid.GetGridObject(currentNode.X, currentNode.Y -1));
            // Up
            if(currentNode.Y + 1 < grid.Height) neighbourList.Add(grid.GetGridObject(currentNode.X, currentNode.Y +1));

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
            int xDistance = Mathf.Abs(a.X - b.X);
            int yDistance = Mathf.Abs(a.Y - b.Y);
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