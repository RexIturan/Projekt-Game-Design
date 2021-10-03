using System;
using Events.ScriptableObjects;
using Events.ScriptableObjects.FieldOfView;
using Grid;
using UnityEngine;

namespace FieldOfView {
    public class FieldOfViewController : MonoBehaviour {
        [SerializeField] private GridContainerSO grid;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;
        [SerializeField] private bool debug = false;
        [SerializeField] private int visionRangeTest;
        [SerializeField] private Vector2Int startPosTest;
        [SerializeField] private GridDataSO globalGridData;

        [SerializeField] private FieldOfViewQueryEventChannelSO fieldOfViewQueryEventChannel;

        // algorythm
        private FieldOfView_Adam fieldOfViewAdam;
        private FieldOfView fieldOfView;

        //adamFOV settings
        [SerializeField] private Vector2Int posAdam;
        [SerializeField] private int rangeAdam;
        private bool[,] visible;

        public void Awake() {
            fieldOfView = InitFieldOfView();
            fieldOfViewQueryEventChannel.OnEventRaised += handleQueryEvent;
        }

        private FieldOfView InitFieldOfView() {
            return new FieldOfView(grid, tileTypeContainer, debug);
        }

        private void handleQueryEvent(Vector3Int startpos, int range, ETileFlags blocking, Action<bool[,]> callback) {
            var pos = globalGridData.GridPos3DToGridPos2D(startpos);
            // callback(fieldOfView.GetVisibleTiles(range, pos, blocking));

            initFieldOfViewAdam();
            fieldOfViewAdam.Compute(pos, range);
            callback(visible);
        }

        // debug
        public void GenerateVision() {
            // fieldOfView.GetVisibleTiles(visionRangeTest, startPosTest, ETileFlags.opaque);
            initFieldOfViewAdam();
            fieldOfViewAdam.Compute(posAdam, rangeAdam);

            // gen string
            string str = " \n";
            var width = globalGridData.Height;
            var height = globalGridData.Height;

            for (int y = height-1; y >= 0; y--) {
                for (int x = 0; x < width; x++) {
                    if (visible[x, y]) {
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

        void initFieldOfViewAdam() {
            var width = globalGridData.Width;
            var height = globalGridData.Height;
            visible = new bool[width, height];
            fieldOfViewAdam = new FieldOfView_Adam(
                (x, y) => BlocksLight(x, y, blocker: ETileFlags.opaque),
                SetVisible,
                GetDistance);
        }

        bool BlocksLight(int x, int y, ETileFlags blocker) {
            bool blocksLight = true;

            if (globalGridData.IsInGridBounds(x, y)) {
                var type = grid.tileGrids[1].GetGridObject(x, y).tileTypeID;
                var flags = tileTypeContainer.tileTypes[type].flags;
                blocksLight = flags.HasFlag(blocker);
            }

            return blocksLight;
        }

        void SetVisible(int x, int y) {
            if (globalGridData.IsInGridBounds(x, y)) {
                visible[x, y] = true;
            }
        }

        int GetDistance(int x, int y) {
            // (0|0) -> (x|y)
            var dist = new Vector2Int(x, y) - Vector2Int.zero;
            return Mathf.RoundToInt(dist.magnitude);
        }
    }
}