using System;
using Input;
using UnityEngine;
using Util;

namespace DefaultNamespace.test {

    public enum TestUnit {
        ONE,
        TWO
    }
    
    public class SOTester : MonoBehaviour {
        public InputReader InputReader;
        public GenericGridSO gridSO;

        public Vector2Int where;
        public int value;
        public TestUnit unit;
        
        
        private void OnEnable() {
            gridSO.initGrid(5, 5);
            InputReader.leftClickEvent += HandleLeftClickEvent;
            // InputReader.rightClickEvent += HandleRightClickEvent;
        }

        private void OnDisable() {
            InputReader.leftClickEvent -= HandleLeftClickEvent;
            // InputReader.rightClickEvent -= HandleRightClickEvent;
        }

        private void HandleLeftClickEvent() {
            if (unit == TestUnit.ONE) {
                gridSO.setValue(where.x, where.y, value);    
            }

            if (unit == TestUnit.TWO) {
                gridSO.setValue(where.x, where.y, value);
            }
            
        }
        
        private void HandleRightClickEvent() {
            
        }
    }
}