using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Util;

namespace Grid {
    [System.Serializable]
    public class Tile {

        [System.Serializable]
        public struct TestStruct {
            public string name;
            public ETileFlags flags;
        }
        
        public class testSO : ScriptableObject {
            public string name;
        }

        public testSO testScriptableObject = new testSO();
        
        
        public TestStruct strcut = new TestStruct {
            name = "tile",
            flags = ETileFlags.destructible | ETileFlags.opaque
        };

        public ETileFlags flags = ETileFlags.solid;
        
        public int typeID;

        
        public TileTypeSO type;

        public TileTypeSO Type => type;

        private GenericGrid<Tile> grid;
        public int x;
        public int y;

        public void SetTileType(TileTypeSO tileType) {
            type = tileType;
            typeID = tileType.movementCost;
            grid.TriggerGridObjectChanged(x, y);
        }
        
        public Tile(GenericGrid<Tile> grid, int x, int y) {
            testScriptableObject.name = "testSO";
            this.grid = grid;
            this.x = x;
            this.y = y;
        }
        
        public override string ToString() {
            return Type.Name;    
        }
    }
}