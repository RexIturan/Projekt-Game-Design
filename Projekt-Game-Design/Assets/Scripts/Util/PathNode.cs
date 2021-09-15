using System.Collections.Generic;


namespace Util {
    public class PathNode {
        
        public struct Edge
        {
            public Edge(int cost, PathNode target)
            {
                this.Cost = cost;
                this.Target = target;
            }
            public int Cost;
            public PathNode Target;
        }

        private GenericGrid<PathNode> grid;
        public int x;
        public int y;

        public int gCost;
        public int hCost;
        public int fCost;
        
        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private int CostFactor = 2;

        public List<Edge> Edges;

        public int dist;

        public bool isWalkable;
        public PathNode parentNode;
        
        public PathNode(GenericGrid<PathNode> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            isWalkable = true;
        }

        public void SetEdges(bool diagonal)
        {
            Edges = new List<Edge>();

                if (this.x - 1 >= 0)
                {
                    // Left
                    AddEdge(x - 1, y, MOVE_STRAIGHT_COST);
                    if (diagonal) {
                        // Left Down
                        if (this.y - 1 >= 0)  AddEdge(this.x - 1, this.y - 1, MOVE_DIAGONAL_COST);
                        // Left Up
                        if(this.y + 1 < grid.Height) AddEdge(this.x - 1, this.y + 1, MOVE_DIAGONAL_COST);  
                    }
                }

                if (this.x + 1 < grid.Width) {
                    // Right
                    AddEdge(x + 1, y, MOVE_STRAIGHT_COST);
                    if (diagonal) {
                        // Right Down
                        if (this.y - 1 >= 0)  AddEdge(this.x + 1, this.y - 1, MOVE_DIAGONAL_COST);
                        // Right Up
                        if(this.y + 1 < grid.Height) AddEdge(this.x + 1, this.y + 1, MOVE_DIAGONAL_COST);      
                    }
                }
            
                // Down
                if(this.y - 1 >= 0) AddEdge(this.x, this.y - 1, MOVE_STRAIGHT_COST);
                // Up
                if(this.y + 1 < grid.Height) AddEdge(this.x, this.y + 1, MOVE_STRAIGHT_COST);
                
            
        }

        private void AddEdge(int x, int y, int cost)
        {
            if (grid.GetGridObject(x, y).isWalkable)
            {
                Edges.Add(new Edge(cost * CostFactor, grid.GetGridObject(x, y)));
            }
        }

        public void CalculateFCost() {
            fCost = gCost + hCost;
        }

        public void SetIsWalkable(bool value) {
            isWalkable = value;
            grid.TriggerGridObjectChanged(x, y);
        }
        
        public override string ToString() {
            if (isWalkable) {
                return x + "," + y;    
            }
            else {
                return "-";
            }
        }
    }
}