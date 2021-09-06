using UnityEngine;
using Util;

namespace Grid {
    [System.Serializable]
    public class Tile : MonoBehaviour {
        
        private TileTypeSO type;
        
        private GenericGrid<Tile> grid;
        public int x;
        public int y;

        public Tile(GenericGrid<Tile> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
        
        public override string ToString() {
            return x + "," + y;    
        }
    }
}