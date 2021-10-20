using Util;

// script attached to each world object
// contains data for current stats and type
//
namespace WorldObjects {
    public class WorldObject {
        // [HideInInspector] public string name = "WorldObject";

        // basic Grid data
        public GenericGrid1D<WorldObject> grid;
        public int x;
        public int y;

        public WorldObjectTypeSO type;

        // public EWorldObjectFlags currentProperties;
        // public int hitPoints;

        public void SetWorldObjectType(WorldObjectTypeSO worldObjectType) {
            type = worldObjectType;
            // grid.TriggerGridObjectChanged(x, y);
        }

        public WorldObject(GenericGrid1D<WorldObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            // name += $" {x}:{y}";
        }

        public override string ToString() {
            return type.name;
        }
    }
}
