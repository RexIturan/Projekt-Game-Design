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


        public string Name => name;
        public TileProperties Properties => properties;
        public int MovementCost => movementCost;
        public Color Color => color;
    }
}