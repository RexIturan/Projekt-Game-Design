using System;
using System.Collections.Generic;
using Characters;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.PlayerCharacter.ScriptableObjects;
using Events.ScriptableObjects;
using Grid;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;
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
		[SerializeField] private PlayerSpawnDataSO playerSpawnDataSO;
		[SerializeField] private PlayerTypeSO playerTypeSO;
		[SerializeField] private EnemySpawnDataSO enemySpawnDataSO;
		[SerializeField] private EnemyTypeSO enemyTypeSO;

		[Header("Settings")] [SerializeField] private TileTypeSO selectedTileType;

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

		private List<Vector3> _leftClickedPos;
		private List<Vector3> _rightClickedPos;

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
			_leftClickedPos = new List<Vector3>();
			_rightClickedPos = new List<Vector3>();

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

			//todo(vincent) refactor #input cache

			#region refactor to mouse input / input cache

			// Debug.Log(inputCache.IsMouseOverUI);
			
			if ( !inputCache.IsMouseOverUI ) {
				var mouse = Mouse.current;
				rightMouseWasReleased = mouse.rightButton.wasReleasedThisFrame;
				leftMouseWasReleased = mouse.leftButton.wasReleasedThisFrame;
				leftMouseIsPressed = mouse.leftButton.isPressed;
				rightMouseIsPressed = mouse.rightButton.isPressed;
			}
			else {
				rightMouseWasReleased = false;
				leftMouseWasReleased = false;
				leftMouseIsPressed = false;
				rightMouseIsPressed = false;
			}

			//todo getMousePosition at level 
			// Vector3 mousePosition = MousePosition.GetMouseWorldPosition(Vector3.up, 1);

			Vector3 positionAbove = MousePosition.GetTilePositionFromMousePosition(gridDataSO, true, out bool hitBottomAbove);
			Vector3 positionBelow = MousePosition.GetTilePositionFromMousePosition(gridDataSO, false, out bool hitBottomBelow);
			
			if ( rightMouseIsPressed ) {
				remove = true;
				_rightClicked = true;
				_rightClickedPos.Add(positionBelow);
			}
			else {
				_rightClicked = false;
			}

			if ( leftMouseIsPressed ) {
				remove = false;
				_leftClicked = true;
				_leftClickedPos.Add(positionAbove);
			}
			else {
				_leftClicked = false;
			}

			#endregion

			var gridPosAbove = gridDataSO.GetCellPositionFromWorldPos(positionAbove);
			var gridPosBelow = gridDataSO.GetCellPositionFromWorldPos(positionBelow);

			switch ( mode ) {
				case EditType.Select:
					cursorDrawer.DrawCursorAt(gridPosAbove, CursorMode.Select);
					break;

				case EditType.Paint:
					if (remove) {
						cursorDrawer.DrawCursorAt(gridPosBelow, CursorMode.Remove);
					}
					else {
						cursorDrawer.DrawCursorAt(gridPosAbove, CursorMode.Add);
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
						HandleMouseDrag(positionBelow);
						Debug.Log($"left:{_leftClicked} right:{_rightClicked}");
					}
					else if ( leftMouseWasReleased || rightMouseWasReleased ) {
						HandleMouseDragEnd();
						_dragEnd = true;
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
							//todo nicht so 
							if ( _leftClickedPos.Count > 2 ) {
								var start = gridDataSO.GetCellPositionFromWorldPos(_leftClickedPos[0]);
								var end = gridDataSO.GetCellPositionFromWorldPos(_leftClickedPos[1]);
								cursorDrawer.DrawBoxCursorAt(start, end, CursorMode.Add);
							}
						}
						else if ( remove ) {
							if ( _rightClickedPos.Count > 2 ) {
								var startRemove = gridDataSO.GetCellPositionFromWorldPos(_rightClickedPos[0]);
								var endRemove = gridDataSO.GetCellPositionFromWorldPos(_rightClickedPos[1]);
								cursorDrawer.DrawBoxCursorAt(startRemove, endRemove, CursorMode.Remove);
							}
						}
					}
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
				
				Debug.Log($"AddOne: {mode} {editMode} {_leftClickedPos[0]}");
				
				switch ( layer ) {
					case EditLayer.Terrain:
						AddTileAt(_leftClickedPos[0]);
						break;
					
					case EditLayer.Item:
						gridController.AddItemAt(_leftClickedPos[0], 0);
						break;
					
					case EditLayer.Character:
						if ( characterFaction == Faction.Player ) {
							gridController.AddPlayerCharacterAt(_leftClickedPos[0], Faction.Player, playerSpawnDataSO, playerTypeSO);	
						}
						else {
							gridController.AddEnemyCharacterAt(_leftClickedPos[0], Faction.Player, enemySpawnDataSO, enemyTypeSO);
						}
						break;
				}
				
				_leftClickedPos.Clear();
				_leftClicked = false;
				RedrawLevel();
			}

			private void AddMany() {
				AddMultipleTilesAt(_leftClickedPos[0], _leftClickedPos[1]);
				_leftClickedPos.Clear();
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
						gridController.AddTileAt(_rightClickedPos[0], tileTypesContainer.tileTypes[0].id);
						break;
					case EditLayer.Item:
						gridController.RemoveItemAt(_rightClickedPos[0]);
						break;
				}

				_rightClickedPos.Clear();
				_rightClicked = false;

				RedrawLevel();
			}

			private void RemoveMany(EditLayer layer) {
				switch ( layer ) {
					case EditLayer.Terrain:
						gridController.AddMultipleTilesAt(_rightClickedPos[0], _rightClickedPos[1],
							tileTypesContainer.tileTypes[0].id);
						break;
				}

				//todo move to iunput cache??
				// reset input cache
				_rightClickedPos.Clear();
				_rightClicked = false;
				_dragEnd = false;
				cursorDrawer.HideCursor();
				RedrawLevel();
			}

			#endregion

			private void HandleMouseDrag(Vector3 pos) {
				if ( _leftClicked ) {
					if ( _leftClickedPos.Count == 1 ) {
						_leftClickedPos.Add(pos);
					}
					else {
						_leftClickedPos[1] = pos;
					}
				}
				else {
					if ( _rightClickedPos.Count == 1 ) {
						_rightClickedPos.Add(pos);
					}
					else {
						_rightClickedPos[1] = pos;
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