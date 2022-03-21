using System;
using System.Collections.Generic;
using Characters.Types;
using Events.ScriptableObjects;
using Grid;
using Input;
using UnityEngine;
using Visual;

namespace LevelEditor {
	public class LevelEditor : MonoBehaviour {
		//todo(vincent) rename
		public enum EditType {
			Select,
			Paint,
			Box,
			Fill,
		}

		//todo rename
		public enum EditLayer {
			Terrain,
			Item,
			Character,
			Objects,
		}

		[Header("Debug")]
		public bool showCenterPos;
		public bool showMouseHit;
		
		[Header("Receiving Events On")] [SerializeField]
		private VoidEventChannelSO levelLoaded;

		[SerializeField] private IntEventChannelSO setModeEC;

		[Header("Sending Events On")] [SerializeField]
		private VoidEventChannelSO redrawLevelEC;

		[Header("scene References")] 
		[SerializeField] private TileMapDrawer gridDrawer;
		[SerializeField] private CursorDrawer cursorDrawer;

		[SerializeField] private GridController gridController;

		[Header("Input")] [SerializeField] private InputReader inputReader;
		[SerializeField] private InputCache inputCache;

		[Header("SO References")] 
		[SerializeField] private TileTypeContainerSO tileTypesContainer;
		[SerializeField] private GridDataSO gridDataSO;

		[Header("Settings")] 
		[SerializeField] private EditType mode = EditType.Paint;
		[SerializeField] private EditLayer editMode = EditLayer.Terrain;
		[SerializeField] private Faction characterFaction = Faction.Player;
		// [SerializeField] private PlayerSpawnDataSO playerSpawnDataSO;
		[SerializeField] private PlayerTypeSO playerTypeSO;
		// [SerializeField] private EnemySpawnDataSO enemySpawnDataSO;
		[SerializeField] private EnemyTypeSO enemyTypeSO;
		[SerializeField] private TileTypeSO selectedTileType;

/////////////////////////////////////// Properties ////////////////////////////////////////////

		public EditType Mode {
			get => mode;
			set => mode = value;
		}

		public EditLayer EditMode {
			get => editMode;
			set => editMode = value;
		}

/////////////////////////////////////// Local Variables ////////////////////////////////////////////

		//todo move to InputCache
		// runtime input data data
		// mouse input

		private bool rightMouseWasReleased = false;
		private bool leftMouseWasReleased = false;
		private bool leftMouseIsPressed = false;
		private bool rightMouseIsPressed = false;

		private bool _leftClicked;
		private bool _rightClicked;
		private bool _dragEnd;

		private bool remove = false;

		private List<Vector3> _clickedAbovePos;
		private List<Vector3> _clickedSelectedPos;

/////////////////////////////////////// Local Functions ////////////////////////////////////////////

		private void SetMode(int newMode) {
			var len = Enum.GetNames(typeof(EditType)).Length;

			if ( newMode >= len ) {
				editMode = ( EditLayer )( newMode - len );
			}

			if ( newMode >= 0 && newMode < len ) {
				Mode = ( EditType )newMode;
			}
			
			Debug.Log($"Level Editor > Set Mode\n {mode} {editMode}");
		}

		#region MonoBehaviour

		private void Awake() {
			_clickedAbovePos = new List<Vector3>();
			_clickedSelectedPos = new List<Vector3>();

			selectedTileType = tileTypesContainer.tileTypes[1];
			inputReader.ResetEditorLevelEvent += ResetLevel;
			levelLoaded.OnEventRaised += RedrawLevel;
			setModeEC.OnEventRaised += SetMode;
		}

		private void OnDestroy() {
			inputReader.ResetEditorLevelEvent -= ResetLevel;
			levelLoaded.OnEventRaised -= RedrawLevel;
			setModeEC.OnEventRaised -= SetMode;
		}

		private void Update() {
			if ( !inputReader.GameInput.Gameplay.enabled || inputCache.IsMouseOutsideWindow() ) {
				return;
			}

			inputCache.UpdateMouseInput(gridDataSO);
			
			//todo(vincent) refactor #input cache

			#region refactor to mouse input / input cache

			// Debug.Log(inputCache.IsMouseOverUI);
			
			if ( !inputCache.IsMouseOverUI ) {
				rightMouseWasReleased = inputCache.rightButton.wasRelesed;
				leftMouseWasReleased  = inputCache.leftButton.wasRelesed;
				leftMouseIsPressed    = inputCache.leftButton.isPressed;
				rightMouseIsPressed   = inputCache.rightButton.isPressed;
				_rightClicked = inputCache.rightButton.isPressed;
				_leftClicked  = inputCache.leftButton.isPressed;
				_dragEnd = inputCache.DragEnded();
			}
			else {
				rightMouseWasReleased = false;
				leftMouseWasReleased = false;
				leftMouseIsPressed = false;
				rightMouseIsPressed = false;
			}

			Vector3 selectedPosition = inputCache.cursor.selectedPos.tilePos;
			Vector3 positionAbove = inputCache.cursor.abovePos.tilePos;
			var centeredGridPos = inputCache.cursor.selectedPos.tileCenter;
			var centeredGridPosAbove = inputCache.cursor.abovePos.tileCenter;

			if ( rightMouseIsPressed ) {
				remove = true;
				_clickedSelectedPos.Add(selectedPosition);
			}

			if ( leftMouseIsPressed ) {
				remove = false;
				_clickedAbovePos.Add(positionAbove);
			}

			#endregion

			switch ( mode ) {
				case EditType.Select:
					cursorDrawer.DrawCursorAt(centeredGridPosAbove, CursorMode.Select);
					break;

				case EditType.Paint:
					if (remove) {
						DrawCursor(editMode, CursorMode.Remove, centeredGridPos, centeredGridPosAbove);
					}
					else {
						DrawCursor(editMode, CursorMode.Add, centeredGridPos, centeredGridPosAbove);
					}

					if ( _rightClicked ) {
						RemoveOne(editMode);
						break;
					}

					if ( _leftClicked ) {
						AddOne(editMode);
					}

					break;

				case EditType.Box:

					if ( _leftClicked ) {
						HandleMouseDrag(positionAbove);
						Debug.Log($"left:{_leftClicked} right:{_rightClicked}");
					}
					else if ( _rightClicked) {
						HandleMouseDrag(selectedPosition);
						Debug.Log($"left:{_leftClicked} right:{_rightClicked}");
					}
					else if ( leftMouseWasReleased || rightMouseWasReleased ) {
						HandleMouseDragEnd();
						_dragEnd = true;
					}
					else {
						if (remove) {
							DrawCursor(editMode, CursorMode.Remove, centeredGridPos, centeredGridPosAbove);
						}
						else {
							DrawCursor(editMode, CursorMode.Add, centeredGridPos, centeredGridPosAbove);
						}
					}

					if ( _dragEnd ) {
						//todo remove
						Debug.Log("drag ended");
						if ( remove ) {
							RemoveMany(EditLayer.Terrain);
						}
						else {
							AddMany();
						}
					}
					else {
						if ( !remove ) {
							if ( _clickedAbovePos.Count > 2 ) {
								var start = gridDataSO.GetTileCenter2DFromWorldPos(_clickedAbovePos[0]);
								var end = gridDataSO.GetTileCenter2DFromWorldPos(_clickedAbovePos[1]);
								cursorDrawer.DrawBoxCursorAt(start, end, CursorMode.Add);
							}
						}
						else if ( remove ) {
							if ( _clickedSelectedPos.Count > 2 ) {
								var startRemove = gridDataSO.GetTileCenter2DFromWorldPos(_clickedSelectedPos[0]);
								var endRemove = gridDataSO.GetTileCenter2DFromWorldPos(_clickedSelectedPos[1]);
								cursorDrawer.DrawBoxCursorAt(startRemove, endRemove, CursorMode.Remove);
							}
						}
					}
					break;
			}
		}

		private void DrawCursor(EditLayer layer, CursorMode cursorMode, Vector3 pos, Vector3 abovePos) {

			switch ( layer, cursorMode ) {
				case ( EditLayer.Terrain, CursorMode.Remove ) :
					cursorDrawer.DrawCursorAt(pos, cursorMode);
					break;
				case (EditLayer.Terrain, CursorMode.Add):
					cursorDrawer.DrawCursorAt(abovePos, cursorMode);
					break;
				
				case (EditLayer.Item, CursorMode.Add):
				case (EditLayer.Item, CursorMode.Remove):
					cursorDrawer.DrawCursorAt(abovePos, cursorMode);
					break;
			}
		}

		#endregion

/////////////////////////////////////// Public Functions ///////////////////////////////////////////

			public void RedrawLevel() {
				gridDrawer.DrawGridLayout();
				redrawLevelEC.RaiseEvent();
			}

			#region Add

			private void AddOne(EditLayer layer) {
				
				Debug.Log($"AddOne: {mode} {editMode} {_clickedAbovePos[0]}");
				
				switch ( layer ) {
					case EditLayer.Terrain:
						AddTileAt(_clickedAbovePos[0]);
						break;
					
					case EditLayer.Item:
						gridController.AddItemAt(_clickedAbovePos[0], 0);
						break;
					
					// case EditLayer.Character:
					// 	if ( characterFaction == Faction.Player ) {
					// 		gridController.AddPlayerCharacterAt(_clickedAbovePos[0], Faction.Player, playerTypeSO);	
					// 	}
					// 	else {
					// 		gridController.AddEnemyCharacterAt(_clickedAbovePos[0], Faction.Player, enemyTypeSO);
					// 	}
					// 	break;
				}
				
				_clickedAbovePos.Clear();
				_leftClicked = false;
				RedrawLevel();
			}

			private void AddMany() {
				AddMultipleTilesAt(_clickedAbovePos[0], _clickedAbovePos[1]);
				_clickedAbovePos.Clear();
				_leftClicked = false;
				_dragEnd = false;
				cursorDrawer.HideCursor();
				RedrawLevel();
			}
			
			#endregion

			#region Remove

			private void RemoveOne(EditLayer layer) {
				switch ( layer ) {
					case EditLayer.Terrain:
						gridController.AddTileAt(_clickedSelectedPos[0], tileTypesContainer.tileTypes[0].id);
						break;
					case EditLayer.Item:
						gridController.RemoveItemAt(_clickedAbovePos[0]);
						break;
				}

				_clickedSelectedPos.Clear();
				_rightClicked = false;

				RedrawLevel();
			}

			private void RemoveMany(EditLayer layer) {
				switch ( layer ) {
					case EditLayer.Terrain:
						gridController.AddMultipleTilesAt(_clickedSelectedPos[0], _clickedSelectedPos[1],
							tileTypesContainer.tileTypes[0].id);
						break;
				}

				//todo move to iunput cache??
				// reset input cache
				_clickedSelectedPos.Clear();
				_rightClicked = false;
				_dragEnd = false;
				cursorDrawer.HideCursor();
				RedrawLevel();
			}

			#endregion

			private void HandleMouseDrag(Vector3 pos) {
				if ( _leftClicked ) {
					if ( _clickedAbovePos.Count == 1 ) {
						_clickedAbovePos.Add(pos);
					}
					else {
						_clickedAbovePos[1] = pos;
					}
				}
				else {
					if ( _clickedSelectedPos.Count == 1 ) {
						_clickedSelectedPos.Add(pos);
					}
					else {
						_clickedSelectedPos[1] = pos;
					}
				}
			}

			private void HandleMouseDragEnd() {
				_dragEnd = true;
			}

			private void AddMultipleTilesAt(Vector3 clickPos, Vector3 dragPos) {
				gridController.AddMultipleTilesAt(clickPos, dragPos, selectedTileType.id);
			}

			private void AddTileAt(Vector3 clickPos) {
				gridController.AddTileAt(clickPos, selectedTileType.id);	
			}

			public void ResetLevel() {
				gridController.ResetGrid();
				RedrawLevel();
			}

			// public void AddObjectAt(Vector3 clickPos) {
			//     objectController.AddTileAt(clickPos, worldObjectContainer.worldObjects[1]);
			// }
		}
	}