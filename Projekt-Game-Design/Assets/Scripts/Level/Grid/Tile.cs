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
        public int z;
        
        public void SetTileType(int id) {
            tileTypeID = id;
            //todo grid ref or not??
            // grid.TriggerGridObjectChanged(x, y);
        }
        
        public Tile(int x, int z) {
            this.x = x;
            this.z = z;
            name += $" {x}:{z}";
        }
        
        public override string ToString() {
            return tileTypeID.ToString();    
        }
    }
}