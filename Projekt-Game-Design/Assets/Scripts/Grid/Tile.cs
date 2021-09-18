using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Util;

namespace Grid {
    [System.Serializable]
    public class Tile {

        [HideInInspector] public string name = "Tile";

        public TileTypeSO type;

        public TileTypeSO Type => type;

        public GenericGrid1D<Tile> grid;
        public int x;
        public int y;

        public void SetTileType(TileTypeSO tileType) {
            type = tileType;
            // grid.TriggerGridObjectChanged(x, y);
        }
        
        public Tile(GenericGrid1D<Tile> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            name += $" {x}:{y}";
        }
        
        public override string ToString() {
            return Type.Name;    
        }
    }
}