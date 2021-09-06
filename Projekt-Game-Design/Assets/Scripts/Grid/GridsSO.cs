using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Grid {
    [CreateAssetMenu(fileName = "Grids", menuName = "Grid/Grids", order = 0)]
    public class GridsSO : ScriptableObject {
        public List<GenericGrid<Tile>> grids = new List<GenericGrid<Tile>>();
    }
}