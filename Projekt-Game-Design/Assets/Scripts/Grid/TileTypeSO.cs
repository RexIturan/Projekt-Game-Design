using System;
using UnityEngine;

namespace Grid {
    [CreateAssetMenu(fileName = "newTileType", menuName = "Grid/TileType", order = 0)]
    public class TileTypeSO : ScriptableObject {
        [SerializeField] private TileFlags flags;
        [SerializeField] private int movementCost;
        [SerializeField] private Color color;
    }

    [System.Serializable, System.Flags]
    public enum TileFlags {
        walkable = 0x1,
        opaque = 0x2,
        destructible = 0x4,
        shootTrough = 0x8,
    }
}