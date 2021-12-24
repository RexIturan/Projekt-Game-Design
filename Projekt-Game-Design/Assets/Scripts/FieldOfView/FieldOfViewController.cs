using System;
using System.Collections.Generic;
using Events.ScriptableObjects.FieldOfView;
using Grid;
using Level.Grid;
using UnityEngine;
using Util;

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
				[SerializeField] private FOVCrossQueryEventChannelSO fieldOfViewCrossQueryEventChannel;

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
						fieldOfViewCrossQueryEventChannel.OnEventRaised += HandleCrossQueryEvent;
				}

        private FieldOfView InitFieldOfView() {
            return new FieldOfView(grid, tileTypeContainer, debug);
        }

        private void HandleQueryEvent(Vector3Int startPos, int range, TileProperties blocking, Action<bool[,]> callback) {
            var pos = globalGridData.GetGridPos2DFromGridPos3D(startPos);

            InitFieldOfViewAdam();
            _fieldOfViewAdam.Compute(pos, range);
            callback(_visible);
				}

				private void HandleCrossQueryEvent(Vector3Int startPos, TileProperties blocking, Action<bool[,]> callback)
				{
						var pos = globalGridData.GetGridPos2DFromGridPos3D(startPos);

						// get every surrounding tile
						InitFieldOfViewAdam();
						_fieldOfViewAdam.Compute(pos, 1);

						// remove the query origin position and the diagonals
						_visible[pos.x, pos.y] = false;
						if ( pos.x > 0 )
						{
								if( pos.y + 1 < _visible.GetLength(1) )
									_visible[pos.x - 1, pos.y + 1] = false;
								if( pos.y > 0 )
									_visible[pos.x - 1, pos.y - 1] = false;
						}
						if( pos.x + 1 < _visible.GetLength(0) )
						{
								if ( pos.y + 1 < _visible.GetLength(1) )
										_visible[pos.x + 1, pos.y + 1] = false;
								if ( pos.y > 0 )
										_visible[pos.x + 1, pos.y - 1] = false;
						}

						callback(_visible);
				}

				// debug
				public void GenerateVision() {
            // fieldOfView.GetVisibleTiles(visionRangeTest, startPosTest, ETileFlags.opaque);
            InitFieldOfViewAdam();
            _fieldOfViewAdam.Compute(posAdam, rangeAdam);

            // gen string
            string str = " \n";
            var width = globalGridData.Width;
            var depth = globalGridData.Depth;

            for (int y = depth-1; y >= 0; y--) {
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
            var depth = globalGridData.Depth;
            _visible = new bool[width, depth];
            _fieldOfViewAdam = new FieldOfView_Adam(
                (x, y) => BlocksLight(x, y, blocker: TileProperties.Opaque),
                SetVisible,
                GetDistance);
        }

        private bool BlocksLight(int x, int y, TileProperties blocker) {
            bool blocksLight = true;

            if (globalGridData.IsIn2DGridBounds(x, y)) {
                var type = grid.tileGrids[1].GetGridObject(x, y).tileTypeID;
                var flags = tileTypeContainer.tileTypes[type].properties;
                blocksLight = flags.HasFlag(flag: blocker);
            }

            return blocksLight;
        }

        void SetVisible(int x, int y) {
            if (globalGridData.IsIn2DGridBounds(x, y)) {
                _visible[x, y] = true;
            }
        }

        int GetDistance(int x, int y) {
            // (0|0) -> (x|y)
            var dist = new Vector2Int(x, y) - Vector2Int.zero;
            return Mathf.RoundToInt(dist.magnitude);
        }
				
				// TODO: move to somewhere else I guess?
				// TODO: add third dimension to visibility and also this function
				public static List<PathNode> VisibleTilesToPathNodeList(bool[,] visibleTiles)
				{
						List<PathNode> tilesInRange = new List<PathNode>();
						for ( int x = 0; x < visibleTiles.GetLength(0); x++ )
						{
								for ( int y = 0; y < visibleTiles.GetLength(1); y++ )
								{
										if ( visibleTiles[x, y] )
										{
												tilesInRange.Add(new PathNode(x, 1, y));
										}
								}
						}
						return tilesInRange;
				}
		}
}