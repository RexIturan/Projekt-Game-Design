using UnityEngine;

namespace Grid {
    [CreateAssetMenu(fileName = "newTileTypeContainer", menuName = "Grid/TileTypeContainer", order = 0)]
    public class TileTypeContainerSO : ScriptableObject {
        public TileTypeSO[] tileTypes;
    }
}