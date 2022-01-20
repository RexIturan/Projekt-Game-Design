namespace Util {
    public abstract class GridObject {
	    public string name = "GridObject";

            public GenericGrid1D<GridObject> grid;
            public int x;
            public int y;

        
            public GridObject(GenericGrid1D<GridObject> grid, int x, int y) {
                this.grid = grid;
                this.x = x;
                this.y = y;
                name += $" {x}:{y}";
            }
        }
    }
