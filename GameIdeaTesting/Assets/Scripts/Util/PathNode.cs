namespace Util {
    public class PathNode {

        private GenericGrid<PathNode> genericGrid;
        private int x;
        private int y;

        public int X => x;
        public int Y => y;

        public int gCost;
        public int hCost;
        public int fCost;

        public PathNode parentNode;
        
        public PathNode(GenericGrid<PathNode> genericGrid, int x, int y) {
            this.genericGrid = genericGrid;
            this.x = x;
            this.y = y;
        }

        public override string ToString() {
            return x + "," + y;
        }

        public void CalculateFCost() {
            fCost = gCost + hCost;
        }
    }
}