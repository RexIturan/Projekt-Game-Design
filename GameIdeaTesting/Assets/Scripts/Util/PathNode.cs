using DefaultNamespace;

namespace Util {
    public class PathNode {

        private GenericGrid<PathNode> grid;
        public int x;
        public int y;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool isWalkable;
        public PathNode parentNode;
        
        // todo remove this is just a hack
        public GridUnit unit;
        
        public PathNode(GenericGrid<PathNode> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            isWalkable = true;
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