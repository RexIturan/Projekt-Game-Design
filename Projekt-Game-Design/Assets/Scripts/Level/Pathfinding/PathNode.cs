using System.Collections.Generic;


namespace Util {
    [System.Serializable]
    public class PathNode {
        
        public struct Edge
        {
            public Edge(int cost, PathNode target)
            {
                this.cost = cost;
                this.target = target;
            }
            public readonly int cost;
            public readonly PathNode target;
        }

        private GenericGrid1D<PathNode> _grid;
        public int x;
        public int y;

        public int gCost;
        public int hCost;
        public int fCost;
        
        private const int MoveStraightCost = 10;
        private const int MoveDiagonalCost = 14;

        private int _costFactor = 2;

        public List<Edge> edges;

        public int dist;

        public bool isWalkable;
        public PathNode parentNode;
        
        public PathNode(GenericGrid1D<PathNode> grid, int x, int y) {
            this._grid = grid;
            this.x = x;
            this.y = y;
            isWalkable = true;
        }

        public void SetEdges(bool diagonal)
        {
            // if (!isWalkable) {
            //     return;
            // }
            
            edges = new List<Edge>();

                if (this.x - 1 >= 0)
                {
                    // Left
                    AddEdge(x - 1, y, MoveStraightCost);
                    if (diagonal) {
                        // Left Down
                        if (this.y - 1 >= 0)  AddEdge(this.x - 1, this.y - 1, MoveDiagonalCost);
                        // Left Up
                        if(this.y + 1 < _grid.Height) AddEdge(this.x - 1, this.y + 1, MoveDiagonalCost);  
                    }
                }

                if (this.x + 1 < _grid.Width) {
                    // Right
                    AddEdge(x + 1, y, MoveStraightCost);
                    if (diagonal) {
                        // Right Down
                        if (this.y - 1 >= 0)  AddEdge(this.x + 1, this.y - 1, MoveDiagonalCost);
                        // Right Up
                        if(this.y + 1 < _grid.Height) AddEdge(this.x + 1, this.y + 1, MoveDiagonalCost);      
                    }
                }
            
                // Down
                if(this.y - 1 >= 0) AddEdge(this.x, this.y - 1, MoveStraightCost);
                // Up
                if (this.y + 1 < _grid.Height) AddEdge(this.x, this.y + 1, MoveStraightCost);
        }

        private void AddEdge(int posX, int posY, int cost)
        {
            if (_grid.GetGridObject(posX, posY).isWalkable)
            {
                edges.Add(new Edge(cost * _costFactor, _grid.GetGridObject(posX, posY)));
            }
        }

        public void CalculateFCost() {
            fCost = gCost + hCost;
        }

        public void SetIsWalkable(bool value) {
            isWalkable = value;
            _grid.TriggerGridObjectChanged(x, y);
        }
        
        public override string ToString() {
            if (isWalkable) {
                return x + "," + y + " +";    
            }
            else {
                return x + "," + y + " -";
            }
        }
    }
}