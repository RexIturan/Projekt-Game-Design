using UnityEngine;
using Util;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "newTileType", menuName = "Tile Type", order = 0)]
    public class TileType : ScriptableObject {

        public TileFlags flags;
        public int movementCost;

    }
}