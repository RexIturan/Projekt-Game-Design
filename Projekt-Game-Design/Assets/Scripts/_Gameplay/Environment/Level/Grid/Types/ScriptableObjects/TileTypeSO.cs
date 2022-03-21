using Level.Grid;
using UnityEngine;

namespace Grid {
    [CreateAssetMenu(fileName = "newTileType", menuName = "Grid/TileType", order = 0)]
    public class TileTypeSO : ScriptableObject {
        public new string name;
        public int id;
        public TileProperties properties;
        public int movementCost;
        public Color color;
    }
}