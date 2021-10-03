using System;
using System.Collections.Generic;
using Graph.ScriptableObjects;
using Grid;
using Input;
using TMPro;
using UnityEngine;

namespace Util.VisualDebug {
    public class PathfindingStepsVisualDebug : MonoBehaviour {
        
        public static PathfindingStepsVisualDebug Instance { get; private set; }
        
        [Header("Colors")]
        public Color inPathColor = Color.green;
        public Color currentColor = Color.green;
        public Color inOpenListColor = new Color32(0, 154, 255, 255);
        public Color inClosedListColor = Color.red;
        public Color defaultBackgroundColor = new Color32(99, 99, 99, 255);

        [Header("Settings")] 
        [SerializeField] private GraphContainerSO graphContainer;
        [SerializeField] private GridDataSO globalGridData;
        [SerializeField] private InputReader inputReader;
        [SerializeField] private Transform parent;
        [SerializeField] private Transform prefab;

        private bool active;
        private bool autoShowSnapshots;
        private float autoShowSnapshotsTimer;
        
        private List<GridSnapshotAction> gridSnapshotActionList;
        private PathfindingDebugTileData[,] visualNodes;


        private void Awake() {
            Instance = this;
            // visualNodeList = new List<Transform>();
            gridSnapshotActionList = new List<GridSnapshotAction>();
            inputReader.stepEvent += HandleOnStepEvent;
            inputReader.showFullPathEvent += HandleOnShowFullPathEvent;
        }

        private void Start() {
            Setup(graphContainer.basicMovementGraph[1]);
        }

        private void OnDestroy() {
            inputReader.stepEvent -= HandleOnStepEvent;
            inputReader.showFullPathEvent -= HandleOnShowFullPathEvent;
        }

        public void Setup(GenericGrid1D<PathNode> grid) {
            visualNodes = new PathfindingDebugTileData[grid.Width, grid.Height];

            for (int x = 0; x < grid.Width; x++) {
                for (int y = 0; y < grid.Height; y++) {
                    Vector3 gridPosition = new Vector3(x, 0.01f, y) * grid.CellSize;// + new Vector3(0.5f, 0, 0.5f) * grid.CellSize;// + Vector3.one * grid.CellSize * .5f;
                    gridPosition += new Vector3(0.5f, 0, 0.5f) * grid.CellSize;
                    gridPosition += globalGridData.OriginPosition;
                    gridPosition += Vector3.up;
                    Transform visualNode = CreateVisualNode(gridPosition);
                    visualNodes[x, y] = visualNode.GetComponent<PathfindingDebugTileData>();
                    // visualNodeArray[x, y] = visualNode;
                }
            }
            HideNodeVisuals();
        }
        
        private void Update() {
            if (autoShowSnapshots) {
                float autoShowSnapshotsTimerMax = .05f;
                autoShowSnapshotsTimer -= Time.deltaTime;
                if (autoShowSnapshotsTimer <= 0f) {
                    autoShowSnapshotsTimer += autoShowSnapshotsTimerMax;
                    ShowNextSnapshot();
                    if (gridSnapshotActionList.Count == 0) {
                        autoShowSnapshots = false;
                    }
                }
            }
        }

        private void HandleOnStepEvent() {
            ShowNextSnapshot();
        }
        
        private void HandleOnShowFullPathEvent() {
            autoShowSnapshots = true;
        }
        
        private void ShowNextSnapshot() {
            if (gridSnapshotActionList.Count > 0) {
                GridSnapshotAction gridSnapshotAction = gridSnapshotActionList[0];
                gridSnapshotActionList.RemoveAt(0);
                gridSnapshotAction.TriggerAction();
            }
        }
        
        public void TakeSnapshot( GenericGrid1D<PathNode> grid, PathNode current, 
            List<PathNode> openList, List<PathNode> closedList) {
            
            Debug.Log("screenshot");
            
            GridSnapshotAction gridSnapshotAction = new GridSnapshotAction();
            gridSnapshotAction.AddAction(HideNodeVisuals);

            for (int x = 0; x < grid.Width; x++) {
                for (int y = 0; y < grid.Height; y++) {
                    PathNode pathNode = grid.GetGridObject(x, y);

                    int gCost = pathNode.gCost;
                    int hCost = pathNode.hCost;
                    int fCost = pathNode.fCost;
                    // Vector3 gridPosition = new Vector3(pathNode.x, 0, pathNode.y) * grid.CellSize;
                    bool isCurrent = pathNode == current;
                    bool isInOpenList = openList.Contains(pathNode);
                    bool isInClosedList = closedList.Contains(pathNode);
                    int tmpX = x;
                    int tmpY = y;

                    gridSnapshotAction.AddAction(() => {
                        var visualNode = visualNodes[tmpX, tmpY];
                        SetupVisualNode(visualNode, gCost, hCost, fCost);

                        Color backgroundColor = defaultBackgroundColor;

                        if (isInClosedList) {
                            backgroundColor = inClosedListColor;
                        }

                        if (isInOpenList) {
                            backgroundColor = inOpenListColor;
                        }

                        if (isCurrent) {
                            backgroundColor = currentColor;
                        }

                        visualNode.background.color = backgroundColor;
                    });
                }
            }

            gridSnapshotActionList.Add(gridSnapshotAction);
        }

        public void TakeSnapshotFinalPath(GenericGrid1D<PathNode> grid, List<PathNode> path) {
            GridSnapshotAction gridSnapshotAction = new GridSnapshotAction();
            gridSnapshotAction.AddAction(HideNodeVisuals);

            for (int x = 0; x < grid.Width; x++) {
                for (int y = 0; y < grid.Height; y++) {
                    PathNode pathNode = grid.GetGridObject(x, y);

                    int gCost = pathNode.gCost;
                    int hCost = pathNode.hCost;
                    int fCost = pathNode.fCost;
                    // Vector3 gridPosition = new Vector3(pathNode.x, pathNode.y) * grid.CellSize;
                    bool isInPath = path.Contains(pathNode);
                    int tmpX = x;
                    int tmpY = y;

                    gridSnapshotAction.AddAction(() => {
                        var visualNode = visualNodes[tmpX, tmpY];
                        SetupVisualNode(visualNode, gCost, hCost, fCost);

                        Color backgroundColor;

                        if (isInPath) {
                            backgroundColor = inPathColor;
                        }
                        else {
                            backgroundColor = defaultBackgroundColor;
                        }

                        visualNode.background.color = backgroundColor;
                    });
                }
            }

            gridSnapshotActionList.Add(gridSnapshotAction);
        }
        
        private void HideNodeVisuals() {
            foreach (var node in visualNodes) {
                ResetVisualNode(node);
            }
        }

        private void ResetVisualNode(PathfindingDebugTileData debugTileData) {
            debugTileData.gCostText.SetText("");
            debugTileData.hCostText.SetText("");
            debugTileData.fCostText.SetText("");
        }

        private void SetupVisualNode(PathfindingDebugTileData debugTileData, int gCost, int hCost, int fCost) {
            debugTileData.gCostText.SetText(gCost == int.MaxValue ? "∞" : gCost.ToString() );
            debugTileData.hCostText.SetText(gCost == int.MaxValue ? "∞" : hCost.ToString());
            debugTileData.fCostText.SetText(gCost == int.MaxValue ? "∞" : fCost.ToString());
        }

        private Transform CreateVisualNode(Vector3 position) {
            Transform visualNodeTransform = Instantiate(prefab, position, Quaternion.identity, parent);
            return visualNodeTransform;
        }

        public void ClearSnapshots() {
            gridSnapshotActionList.Clear();
        }
        
        private struct DebugTile {
            public TextMeshPro gCostText;
            public TextMeshPro hCostText;
            public TextMeshPro fCostText;
            public Sprite background;
        }

        private class GridSnapshotAction {
            private Action action;

            public GridSnapshotAction() {
                action = () => { };
            }

            public void AddAction(Action action) {
                this.action += action;
            }

            public void TriggerAction() {
                action();
            }
        }
    }
}