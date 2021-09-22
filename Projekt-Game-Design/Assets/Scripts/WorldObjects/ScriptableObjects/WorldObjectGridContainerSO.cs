using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace WorldObjects.ScriptableObjects {
    [CreateAssetMenu(fileName = "New WorldObjectGridContainer", menuName = "WorldObject/WorldObjectGridContainer")]
    public class WorldObjectGridContainerSO : ScriptableObject {
        public List<WorldObjectGrid> worldObjectGrids = new List<WorldObjectGrid>();
    }
}