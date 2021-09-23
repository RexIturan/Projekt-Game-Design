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

        private void Awake() {
            InitialisePathfinding();            
        }

        private void Update() {
            if (Mouse.current.leftButton.wasPressedThisFrame) {
                var pos = MousePosition.GetMouseWorldPosition();
                // Debug.Log($"{pos}");
                
                var reach = GetReachableNodes(pos, dist);
                var nodeList = "";
                foreach (var node in reach) {
                    nodeList += $" {node.ToString()}";
                }
                
                Debug.Log($"{pos} {nodeList}");
                
                drawer.DrawPreview(reach);
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
            return pathfinding.GetReachableNodes(pos.x, pos.y, maxDist);
        }

        public List<PathNode> GetPath(Vector3 start3d, Vector3 end3d, int maxDist) {
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