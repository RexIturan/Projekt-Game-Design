using System;
using System.Collections.Generic;
using Graph.ScriptableObjects;
using Grid;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;
using Events.ScriptableObjects;
using Events.ScriptableObjects.Pathfinding;
using Graph;

namespace Pathfinding {
    public class PathfindingController : MonoBehaviour {
        private Pathfinding _pathfinding;
        
        [Header("SO References")]
        [SerializeField] private GridDataSO globalGridData;
        [SerializeField] private GraphContainerSO graphContainer;
        
        [Header("References")]
        [SerializeField] private PathfindingDrawer drawer;
        [SerializeField] private GraphGenerator graphGenerator;

        [Header("Settings")]
        public int dist;
        public bool test;

        [Header("Receiving events on")]
        [SerializeField] private PathfindingQueryEventChannelSO pathfindingAllNodesQueryEventChannel;
        [SerializeField] private PathFindingPathQueryEventChannelSO pathfindingPathQueryEventChannel;
        [SerializeField] private FindPathBatchEventChannelSO findPathBatchEC;
        
        private Vector2Int _clickedPos = Vector2Int.zero;
        private List<PathNode> _reachableNodes;

        private void Awake() {
            pathfindingAllNodesQueryEventChannel.OnEventRaised += HandlePathfindingQueryEvent;
            pathfindingPathQueryEventChannel.OnEventRaised += HandlePathfindingPathQueryEvent;
            findPathBatchEC.OnEventRaised += HandleFindPathBatch;
        }

        private void Update() {
            if (test) {
                var pos = MousePosition.GetMouseWorldPosition();
            
                if (_clickedPos != null && _reachableNodes != null && _reachableNodes.Count > 0) {
                    var gridPos = WorldPosToGridPos(pos);
                    bool reachable = false;
                    PathNode currentNode = null;
                    foreach (var node in _reachableNodes) {
                        if (node.x == gridPos.x && node.y == gridPos.y) {
                            reachable = true;
                            currentNode = node;
                        }
                    }

                    if (reachable) {
                        var path = _pathfinding.CalculatePath(currentNode);
                        drawer.DrawPreviewPath(path);
                    }
                }
            
                if (Mouse.current.leftButton.wasPressedThisFrame) {
                    // var pos = MousePosition.GetMouseWorldPosition();
                    // Debug.Log($"{pos}");
            
                    _reachableNodes = GetReachableNodes(pos, dist);
                    drawer.DrawPreview(_reachableNodes);
                    // var nodeList = "";
                    // foreach (var node in reachableNodes) {
                    //     nodeList += $" {node.ToString()}";
                    // }
                }
                // else{
                //     Debug.LogWarning("Click Position is not inside the Graph");
                // }
            }
        }

        public void InitialisePathfinding() {
            // todo give the pathfinding all graphs
            graphGenerator.GenerateGraphFromGrids();
            _pathfinding = new Pathfinding(graphContainer.basicMovementGraph[1]);
        }

        public List<PathNode> GetReachableNodes(Vector3 pos3d, int maxDist) {
            
            var pos = WorldPosToGridPos(pos3d);
            return GetReachableNodes(pos, maxDist);
        }
        
        public List<PathNode> GetReachableNodes(Vector2Int pos, int maxDist) {
            InitialisePathfinding();
            return _pathfinding.GetReachableNodes(pos.x, pos.y, maxDist);
        }

        public List<PathNode> GetPath(Vector3 start3d, Vector3 end3d, int maxDist) {
            InitialisePathfinding();
            Vector2Int start = WorldPosToGridPos(start3d);
            Vector2Int end = WorldPosToGridPos(end3d);
            return _pathfinding.FindPath(start.x, start.y, end.x, end.y);
        }

        public List<PathNode> GetPath(Vector3Int start, Vector3Int end)
        {
            // Debug.Log($"PathfindingC: get Path from {start} to {end}");
            InitialisePathfinding();
            var path = _pathfinding.FindPath(start.x, start.y, end.x, end.y);
            if(path is null) Debug.Log("no path found");
            return path;
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

        // calculate all reachable nodes and call the given method
        //
        private void HandlePathfindingQueryEvent(Vector3Int startNode, int distance, Action<List<PathNode>> callback) {
            Vector2Int gridPos = new Vector2Int(startNode.x, startNode.z);
            
            callback(GetReachableNodes(gridPos, distance));
        }

        // calculate path nodes and call the given method
        //
        private void HandlePathfindingPathQueryEvent(Vector3Int startNode, Vector3Int endNode, Action<List<PathNode>> callback)
        {
            callback(GetPath(startNode, endNode));
        }
        
        private void HandleFindPathBatch(List<Tuple<Vector3Int, Vector3Int>> input, Action<List<List<PathNode>>> callback) {
            List<List<PathNode>> foundPaths = new List<List<PathNode>>();
            
            foreach (var startEnd in input) {
                foundPaths.Add(GetPath(startEnd.Item1, startEnd.Item2));
            }
            callback(foundPaths);
        }
    }
}