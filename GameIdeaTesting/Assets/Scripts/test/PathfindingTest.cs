using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace DefaultNamespace.test {
    public class PathfindingTest : MonoBehaviour {

        private Util.Pathfinding pathfinding;

        private void Start() {
            pathfinding = new Util.Pathfinding(10, 10);
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                Vector3 mouseWorldPosition = Util.MousePosition.GetMouseWorldPosition();
                pathfinding.GetGrid.GetXY(mouseWorldPosition, out int x, out int y);
                List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
                if (path != null) {
                    for (int i = 0; i < path.Count-1; i++) {
                        var pathNode = path[i];
                        Debug.DrawLine(
                            new Vector3(pathNode.X, 0 ,pathNode.Y) + new Vector3(.5f,0, .5f), 
                            new Vector3(path[i+1].X, 0 ,path[i+1].Y) + new Vector3(.5f,0, .5f),
                            Color.green, 1);
                    }
                }
            }
        }
    }
}