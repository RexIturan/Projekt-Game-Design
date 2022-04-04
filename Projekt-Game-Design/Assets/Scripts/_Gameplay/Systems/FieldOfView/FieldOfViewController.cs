using System;
using System.Collections.Generic;
using Characters;
using Combat;
using Events.ScriptableObjects.FieldOfView;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using Grid;
using Level.Grid;
using UnityEngine;
using UnityEngine.Tilemaps;
using Util;
using WorldObjects;

namespace FieldOfView {
    public class FieldOfViewController : MonoBehaviour {
        [SerializeField] private TileTypeContainerSO tileTypeContainer;
        [SerializeField] private bool debug;
        // [SerializeField] private int visionRangeTest;
        // [SerializeField] private Vector2Int startPosTest;
        [SerializeField] private GridDataSO gridData;
        
        [Header("Receiving Event On")]
        [SerializeField] private FOVQueryEventChannelSO fieldOfViewQueryEventChannel;
				[SerializeField] private FOVCrossQueryEventChannelSO fieldOfViewCrossQueryEventChannel;
				[SerializeField] private VoidEventChannelSO fov_PlayerCharViewUpdateEC;

				[Header("Sending Event On"), SerializeField]
				private FOV_ViewEventChannelSO updatePlayerViewEC;
				
				// fov algorithms
				private FieldOfView_Adam _fieldOfViewAdam;
        // private FieldOfView _fieldOfView;

        [Header("AdamFOV Settings")]
        [SerializeField] private Vector2Int posAdam;
        [SerializeField] private int rangeAdam;
        
        [SerializeField] private Tilemap viewTilemap;
        [SerializeField] private TileBase viewTile;
        
        private bool[,] _visible;
        

        public void OnEnable() {
            // _fieldOfView = InitFieldOfView();
            fov_PlayerCharViewUpdateEC.OnEventRaised += GeneratePlayerCharacterVision;
            fieldOfViewQueryEventChannel.OnEventRaised += HandleQueryEvent;
						fieldOfViewCrossQueryEventChannel.OnEventRaised += HandleCrossQueryEvent;
				}

        private void OnDisable() {
	        fov_PlayerCharViewUpdateEC.OnEventRaised -= GeneratePlayerCharacterVision;
	        fieldOfViewQueryEventChannel.OnEventRaised -= HandleQueryEvent;
	        fieldOfViewCrossQueryEventChannel.OnEventRaised -= HandleCrossQueryEvent;
        }

        private FieldOfView InitFieldOfView() {
            return new FieldOfView(gridData, tileTypeContainer, debug);
        }

        private void HandleQueryEvent(Vector3Int startPos, int range, TileProperties blocking, Action<bool[,]> callback) {
            var pos = gridData.GetGridPos2DFromGridPos3D(startPos);

            InitFieldOfViewAdam();
            _fieldOfViewAdam.Compute(pos, range);
            callback(_visible);
				}

				private void HandleCrossQueryEvent(Vector3Int startPos, TileProperties blocking, Action<bool[,]> callback)
				{
						var pos = gridData.GetGridPos2DFromGridPos3D(startPos);

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
            var width = gridData.Width;
            var depth = gridData.Depth;

            viewTilemap.ClearAllTiles();
            
            for (int y = depth-1; y >= 0; y--) {
                for (int x = 0; x < width; x++) {
                    if (_visible[x, y]) {
                        str += "+";
                        viewTilemap.SetTile(new Vector3Int(x, y, 0), viewTile);
                    }
                    else {
                        str += "-";
                    }
                }

                str += "\n";
            }

            Debug.Log(str);
        }

				public void GeneratePlayerCharacterVision() {
					
					//todo should work on list<pos, range> 
					bool[,] playerVision = new bool[gridData.Width, gridData.Depth];
					
					if ( GameplayProvider.Current.CharacterManager != null ) {
						var playerList = GameplayProvider.Current.CharacterManager.GetPlayerCharacters();

						viewTilemap.ClearAllTiles();
						viewTilemap.transform.position = gridData.OriginPosition;
						
						foreach ( var playerObj in playerList ) {
							var target = playerObj.GetComponent<Targetable>();
							if ( target is { IsAlive: true }) {
								var gridTransform = playerObj.GetComponent<GridTransform>();
								var statistics = playerObj.GetComponent<Statistics>();	
								var pos = gridTransform.gridPosition;
								InitFieldOfViewAdam();
							
								_fieldOfViewAdam.Compute(gridData.GetGridPos2DFromGridPos3D(pos), statistics.StatusValues.ViewDistance.Value);
							
								UniteVision(playerVision, _visible);
							}
						}
					}

					AddVisionToTilemap(playerVision);
					updatePlayerViewEC.RaiseEvent(playerVision);
				}

				private void UniteVision(bool[,] source, bool[,] newVision) {
					var width = gridData.Width;
					var depth = gridData.Depth;
					
					for ( int z = 0; z < depth; z++ ) {
						for ( int x = 0; x < width; x++ ) {
							if ( newVision[x, z] ) {
								source[x, z] = true;
							}
						}
					}
				} 
				
				private void AddVisionToTilemap(bool[,] source) {
					var width = gridData.Width;
					var depth = gridData.Depth;
					
					for (int z = 0; z < depth; z++) {
						for (int x = 0; x < width; x++) {
							if (source[x, z]) {
								//todo grid offset
								viewTilemap.SetTile(new Vector3Int(x, z, 0), viewTile);
							}
						}
					}
				}
				
        private void InitFieldOfViewAdam() {
            var width = gridData.Width;
            var depth = gridData.Depth;
            _visible = new bool[width, depth];
            _fieldOfViewAdam = new FieldOfView_Adam(
                (x, y) => BlocksLight(x, y, blocker: TileProperties.Opaque),
                SetVisible,
                GetDistance);
        }

        private bool BlocksLight(int x, int y, TileProperties blocker) {
            bool blocksLight = true;

            if (gridData.IsIn2DGridBounds(x, y)) {
                var type = gridData.TileGrids[1].GetGridObject(x, y).tileTypeID;
                var flags = tileTypeContainer.tileTypes[type].properties;
                blocksLight = flags.HasFlag(flag: blocker);
            }

						// world objects
						if ( !blocksLight ) {
								// block light with doors 
								WorldObjectManager worldObjectManager = FindObjectOfType<WorldObjectManager>();
								if ( worldObjectManager )
								{
										Door door = worldObjectManager.GetDoorAt(new Vector3Int(x, 1, y));
										if ( door && !door.IsBroken && !door.IsOpen )
												blocksLight = true;

										Junk junk = worldObjectManager.GetJunkAt(new Vector3Int(x, 1, y));
										if ( junk && !junk.IsBroken && junk.Type.opaque )
												blocksLight = true;
								}
						}

            return blocksLight;
        }

        void SetVisible(int x, int y) {
            if (gridData.IsIn2DGridBounds(x, y)) {
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
				public static List<PathNode> VisibleTilesToPathNodeList(bool[,] visibleTiles) {
					int width = visibleTiles.GetLength(0);
					int height = visibleTiles.GetLength(1);
					
						List<PathNode> tilesInRange = new List<PathNode>();
						for ( int x = 0; x < width; x++ )
						{
								for ( int y = 0; y < height; y++ )
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