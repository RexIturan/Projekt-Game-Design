using System.Collections;
using System.Collections.Generic;
using Grid;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Grid {
    public class generic_grid_1D {
        // A Test behaves as an ordinary method
        [Test]
        public void coord2DToIndex_test_5x2() {
            var grid = new TileGrid(5, 2, 1, Vector3.zero);

            var expected = new int[10];
            var actual = new List<int>();
            // for

            for (int i = 0; i < 10; i++) {
                expected[i] = i;
            }

            for (int y = 0; y < grid.Height; y++) {
                for (int x = 0; x < grid.Width; x++) {
                    actual.Add(grid.Coord2DToIndex(x, y, grid.Width));
                }
            }
            
            // Use the Assert class to test conditions
            Assert.AreEqual(expected, actual);
        }
        
        
        
        //
        // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator tile_gridWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }        
    }
}


