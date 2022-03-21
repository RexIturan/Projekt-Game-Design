using System.Collections.Generic;
using UnityEngine;

namespace Graph.ScriptableObjects {
    [CreateAssetMenu(fileName = "newGraphContainer", menuName = "Grid/GraphContainer", order = 0)]
    public class GraphContainerSO : ScriptableObject {
        public List<NodeGraph> basicMovementGraph;
    }
}