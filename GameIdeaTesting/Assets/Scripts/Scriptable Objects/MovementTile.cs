using UnityEngine;

namespace ScriptableObjects {
    [CreateAssetMenu(fileName = "new MovementTile", menuName = "Movement Tile", order = 0)]
    public class MovementTile : ScriptableObject {
        public Material material;
        public Texture sprite;

    }
}