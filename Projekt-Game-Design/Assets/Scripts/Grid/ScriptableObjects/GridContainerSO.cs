using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Grid {
    [CreateAssetMenu(fileName = "newGridContainer", menuName = "Grid/GridContainer", order = 0)]
    public class GridContainerSO : ScriptableObject {
        public GridDataSO globalGridData;
        public List<TileGrid> tileGrids = new List<TileGrid>();
    }
}