using System;
using UnityEngine;

namespace Grid {
    [CreateAssetMenu(fileName = "newTileType", menuName = "Grid/TileType", order = 0)]
    public class TileTypeSO : ScriptableObject {
        [SerializeField] private string name;
        [SerializeField] private ETileFlags flags;
        [SerializeField] private int movementCost;
        [SerializeField] private Color color;


        public string Name => name;
        public ETileFlags Flags => flags;
        public int MovementCost => movementCost;
        public Color Color => color;
    }
}