using UnityEngine;
using Util;

namespace Grid {
    [System.Serializable]
    public class Tile {

        [HideInInspector] public string name = "Tile";

        // serialisable field used for saving a tile
        public int tileTypeID;
        //todo could possibly be private  
        public int x;
        public int y;
        
        private GenericGrid1D<Tile> grid;
        
        public void SetTileType(int id) {
            tileTypeID = id;
            //todo grid ref or not??
            // grid.TriggerGridObjectChanged(x, y);
        }
        
        public Tile(GenericGrid1D<Tile> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            name += $" {x}:{y}";
        }
        
        public override string ToString() {
            return tileTypeID.ToString();    
        }
    }
}