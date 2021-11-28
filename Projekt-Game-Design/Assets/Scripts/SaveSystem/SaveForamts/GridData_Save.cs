using System;
using Grid;
using UnityEngine;

namespace SaveSystem.SaveFormats {
    [Serializable]
    public class GridData_Save {
        public int width;
        public int height;
        public int depth;
        public float cellSize;
        public Vector3 originPosition;
    }
}