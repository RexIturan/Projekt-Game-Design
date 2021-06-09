using System;
using UnityEngine;

namespace Util {
    [System.Serializable]
    [CreateAssetMenu(fileName = "GenericGridSO", menuName = "Grid/GenericGridSO", order = 0)]
    public class GenericGridSO : ScriptableObject {
        // public bool debug;
        [SerializeField] public int[,] testGrid;
        
        [HideInInspector] public int width { get; private set; }
        [HideInInspector] public int height { get; private set; }
        
        // public GenericGrid<int> grid;

        private void OnEnable() {
            // grid = new GenericGrid<int>(10, 10, 1, Vector3.zero, (g, x, y) => 0, debug);
        }

        public void ToggleShowDebug() {
            string s = "";
            for (int x = 0; x < testGrid.GetLength(0); x++) {
                for (int y = 0; y < testGrid.GetLength(1); y++) {
                    s += getValue(x, y) + " ";
                }
                s += "\n";
            }
            
            Debug.Log(s);
        }

        public void initGrid(int width, int height) {
            this.width = width;
            this.height = height;
            testGrid = new int[width, height];
        }

        public bool IsInGrid(int x, int y) {
            return testGrid != null && x >= 0 && y >= 0 && x < width && y < height;
        }
        
        public void setValue(int x, int y, int value) {
            if(IsInGrid(x, y))
                testGrid[x, y] = value;
        }

        public int getValue(int x, int y) {
            if (IsInGrid(x, y)) {
                return testGrid[x, y];
            }

            return -1;
        }
    }
}