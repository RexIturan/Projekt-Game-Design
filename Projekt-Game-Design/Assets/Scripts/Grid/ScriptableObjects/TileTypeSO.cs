using System;
using UnityEngine;

namespace Grid {
    [CreateAssetMenu(fileName = "newTileType", menuName = "Grid/TileType", order = 0)]
    public class TileTypeSO : ScriptableObject {
        public string name;
        public ETileFlags flags;
        public int movementCost;
        public Color color;


        public string Name => name;
        public ETileFlags Flags => flags;
        public int MovementCost => movementCost;
        public Color Color => color;
    }
}