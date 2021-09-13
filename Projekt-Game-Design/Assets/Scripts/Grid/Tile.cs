using UnityEngine;
using Util;

namespace Grid {
    [System.Serializable]
    public class Tile {
        
        private TileTypeSO type;

        public TileTypeSO Type => type;

        private GenericGrid<Tile> grid;
        public int x;
        public int y;

        public void SetTileType(TileTypeSO tileType) {
            type = tileType;
            grid.TriggerGridObjectChanged(x, y);
        }
        
        public Tile(GenericGrid<Tile> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
        
        public override string ToString() {
            return type.Name != null ? type.Name : "-";    
        }
    }
}