using System;
using System.Collections.Generic;
using Input;
using UnityEngine;
using Util;
using VisualDebug;

namespace DefaultNamespace.test {
    public class PathfindingTest : MonoBehaviour {

        [SerializeField] private InputReader inputReader;
        public bool diagonal;
        [SerializeField] private PathfindingStepsVisualDebug stepsVisualDebug;
        
        private Util.Pathfinding pathfinding;

        private void Awake() {
            inputReader.leftClickEvent += HandleLeftClickEvent;
            inputReader.rightClickEvent += HandleRightClickEvent;
        }

        private void OnDestroy() {
            inputReader.leftClickEvent -= HandleLeftClickEvent;
            inputReader.rightClickEvent -= HandleRightClickEvent;
        }

        private void Start() {
            pathfinding = new Util.Pathfinding(10, 10, diagonal);
            stepsVisualDebug.Setup(pathfinding.GetGrid);
        }

        private void HandleLeftClickEvent() {
            Vector3 mouseWorldPosition = Util.MousePosition.GetMouseWorldPosition();
            pathfinding.GetGrid.GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i = 0; i < path.Count-1; i++) {
                    var pathNode = path[i];
                    Debug.DrawLine(
                        new Vector3(pathNode.x, 0 ,pathNode.y) + new Vector3(.5f,0, .5f), 
                        new Vector3(path[i+1].x, 0 ,path[i+1].y) + new Vector3(.5f,0, .5f),
                        Color.green, 1);
                }
            }
        }

        private void HandleRightClickEvent() {
            Vector3 mouseWorldPosition = Util.MousePosition.GetMouseWorldPosition();
            pathfinding.GetGrid.GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetGrid.GetGridObject(x, y).SetIsWalkable(!pathfinding.GetGrid.GetGridObject(x, y).isWalkable);
        }
    }
}