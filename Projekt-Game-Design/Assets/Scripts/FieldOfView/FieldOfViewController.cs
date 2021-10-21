using System;
using Events.ScriptableObjects.FieldOfView;
using Grid;
using Level.Grid;
using UnityEngine;

namespace FieldOfView {
    public class FieldOfViewController : MonoBehaviour {
        [SerializeField] private GridContainerSO grid;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;
        [SerializeField] private bool debug;
        // [SerializeField] private int visionRangeTest;
        // [SerializeField] private Vector2Int startPosTest;
        [SerializeField] private GridDataSO globalGridData;
        
        [Header("Receiving Event On")]
        [SerializeField] private FOVQueryEventChannelSO fieldOfViewQueryEventChannel;

        // fov algorithms
        private FieldOfView_Adam _fieldOfViewAdam;
        // private FieldOfView _fieldOfView;

        [Header("AdamFOV Settings")]
        [SerializeField] private Vector2Int posAdam;
        [SerializeField] private int rangeAdam;
        
        private bool[,] _visible;

        public void Awake() {
            // _fieldOfView = InitFieldOfView();
            fieldOfViewQueryEventChannel.OnEventRaised += HandleQueryEvent;
        }

        private FieldOfView InitFieldOfView() {
            return new FieldOfView(grid, tileTypeContainer, debug);
        }

        private void HandleQueryEvent(Vector3Int startPos, int range, TileProperties blocking, Action<bool[,]> callback) {
            var pos = globalGridData.GridPos3DToGridPos2D(startPos);

            InitFieldOfViewAdam();
            _fieldOfViewAdam.Compute(pos, range);
            callback(_visible);
        }

        // debug
        public void GenerateVision() {
            // fieldOfView.GetVisibleTiles(visionRangeTest, startPosTest, ETileFlags.opaque);
            InitFieldOfViewAdam();
            _fieldOfViewAdam.Compute(posAdam, rangeAdam);

            // gen string
            string str = " \n";
            var width = globalGridData.Height;
            var height = globalGridData.Height;

            for (int y = height-1; y >= 0; y--) {
                for (int x = 0; x < width; x++) {
                    if (_visible[x, y]) {
                        str += "+";
                    }
                    else {
                        str += "-";
                    }
                }

                str += "\n";
            }

            Debug.Log(str);
        }

        private void InitFieldOfViewAdam() {
            var width = globalGridData.Width;
            var height = globalGridData.Height;
            _visible = new bool[width, height];
            _fieldOfViewAdam = new FieldOfView_Adam(
                (x, y) => BlocksLight(x, y, blocker: TileProperties.Opaque),
                SetVisible,
                GetDistance);
        }

        private bool BlocksLight(int x, int y, TileProperties blocker) {
            bool blocksLight = true;

            if (globalGridData.IsInGridBounds(x, y)) {
                var type = grid.tileGrids[1].GetGridObject(x, y).tileTypeID;
                var flags = tileTypeContainer.tileTypes[type].properties;
                blocksLight = flags.HasFlag(flag: blocker);
            }

            return blocksLight;
        }

        void SetVisible(int x, int y) {
            if (globalGridData.IsInGridBounds(x, y)) {
                _visible[x, y] = true;
            }
        }

        int GetDistance(int x, int y) {
            // (0|0) -> (x|y)
            var dist = new Vector2Int(x, y) - Vector2Int.zero;
            return Mathf.RoundToInt(dist.magnitude);
        }
    }
}