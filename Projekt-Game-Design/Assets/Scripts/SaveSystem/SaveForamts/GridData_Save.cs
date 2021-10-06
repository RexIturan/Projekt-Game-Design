using System;
using Grid;
using UnityEngine;

namespace SaveSystem.SaveForamts {
    [Serializable]
    public class GridData_Save {
        public int width;
        public int height;
        public float cellSize;
        public Vector3 originPosition;

        public void SetValues(GridDataSO globalGridData) {
            width = globalGridData.width;
            height = globalGridData.height;
            cellSize = globalGridData.cellSize;
            originPosition = globalGridData.originPosition;
        }

        public void GetValues(GridDataSO gridData) {
            gridData.height = height;
            gridData.width = width;
            gridData.cellSize = cellSize;
            gridData.originPosition = originPosition;
        }
    }
}