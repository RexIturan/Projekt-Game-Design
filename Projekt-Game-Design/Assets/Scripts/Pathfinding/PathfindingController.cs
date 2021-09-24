using System;
using System.Collections.Generic;
using Graph.ScriptableObjects;
using Grid;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;

namespace Pathfinding {
    public class PathfindingController : MonoBehaviour {
        private Pathfinding pathfinding;
        
        [Header("References")]
        [SerializeField] private GridDataSO globalGridData;
        [SerializeField] private GraphContainerSO graphContainer;
        [SerializeField] private PathfindingDrawer drawer;

        [Header("Settings")]
        [SerializeField] private int dist;

        private Vector2Int clickedPos;
        private List<PathNode> reachableNodes;

        private void Awake() {
            InitialisePathfinding();            
        }

        private void Update() {
            var pos = MousePosition.GetMouseWorldPosition();
            
            if (clickedPos != null && reachableNodes != null && reachableNodes.Count > 0) {
                var gridPos = WorldPosToGridPos(pos);
                bool reachable = false;
                PathNode currentNode = null;
                foreach (var node in reachableNodes) {
                    if (node.x == gridPos.x && node.y == gridPos.y) {
                        reachable = true;
                        currentNode = node;
                    }
                }

                if (reachable) {
                    var path = pathfinding.CalculatePath(currentNode);
                    drawer.DrawPreviewPath(path);
                }
            }
            
            if (Mouse.current.leftButton.wasPressedThisFrame) {
                // var pos = MousePosition.GetMouseWorldPosition();
                // Debug.Log($"{pos}");
                
                reachableNodes = GetReachableNodes(pos, dist);
                // var nodeList = "";
                // foreach (var node in reachableNodes) {
                //     nodeList += $" {node.ToString()}";
                // }
                
                // Debug.Log($"{pos} {nodeList}");
                
                drawer.DrawPreview(reachableNodes);
            }
        }

        public void InitialisePathfinding() {
            pathfinding = new Pathfinding(graphContainer.basicMovementGraph[0]);
        }

        public List<PathNode> GetReachableNodes(Vector3 pos3d, int maxDist) {
            
            var pos = WorldPosToGridPos(pos3d);
            return GetReachableNodes(pos, maxDist);
        }
        
        public List<PathNode> GetReachableNodes(Vector2Int pos, int maxDist) {
            InitialisePathfinding();
            return pathfinding.GetReachableNodes(pos.x, pos.y, maxDist);
        }

        public List<PathNode> GetPath(Vector3 start3d, Vector3 end3d, int maxDist) {
            InitialisePathfinding();
            Vector2Int start = WorldPosToGridPos(start3d);
            Vector2Int end = WorldPosToGridPos(end3d);
            return pathfinding.FindPath(start.x, start.y, end.x, end.y);
        }
        
        // todo Umrechnung zentraler globalgrid data vlt??
        // private Vector2Int WorldToGridCoords(Vector3 pos3d) {
        //     var offset = new Vector2Int((int) globalGridData.OriginPosition.x, (int) globalGridData.OriginPosition.y);
        //     var pos = new Vector2Int((int) pos3d.x, (int) pos3d.z) + offset;
        //     return pos;
        // }
        
        // todo Umrechnung zentraler globalgrid data vlt??
        public Vector2Int WorldPosToGridPos(Vector3 worldPos) {
            var lowerBounds = Vector3Int.FloorToInt(globalGridData.OriginPosition);
            var flooredPos = Vector3Int.FloorToInt(worldPos);
            return new Vector2Int(
                x: flooredPos.x + Mathf.Abs(lowerBounds.x),
                y: flooredPos.z + Mathf.Abs(lowerBounds.z));
        }
    }
}