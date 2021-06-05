
using System;
using UnityEngine;
using Util;


namespace DefaultNamespace.test {
    public class GridTesting : MonoBehaviour {
        
        [SerializeField] private int cellSize;
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Vector3 origin;
        private GenericGrid<TestGridObject> genericGrid;
        
        private void Start() {
            genericGrid = new GenericGrid<TestGridObject>(gridSize.x, gridSize.y, cellSize, origin, (GenericGrid<TestGridObject> grid, int x, int y) => new TestGridObject(grid, x, y));
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                var pos = MousePosition.GetMouseWorldPosition();
                var obj = genericGrid.GetGridObject(pos);
                obj?.AddValue(5);
            }
            
            if (Input.GetMouseButtonDown(1)) {
                Debug.Log(genericGrid.GetGridObject(MousePosition.GetMouseWorldPosition()));
            }
        }
    }

    public class TestGridObject {
        private int MIN = 0;
        private int MAX = 100;

        private GenericGrid<TestGridObject> genericGrid;
        private int x;
        private int y;
        private int value;

        public TestGridObject(GenericGrid<TestGridObject> genericGrid, int x, int y) {
            this.genericGrid = genericGrid;
            this.x = x;
            this.y = y;
        }
        
        public void AddValue(int addValue) {
            value += addValue;
            value = Mathf.Clamp(value, MIN, MAX);
            genericGrid.TriggerGridObjectChanged(x, y);
        }

        public float GetValueNormalized() {
            return (float) value / MAX;
        }

        public override string ToString() {
            return value.ToString();
        }
    }
    
}