using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using GDP01._Gameplay.Provider;
using Grid;
using Input;
using LevelEditor.EventChannels;
using UnityEngine;
using UnityEngine.EventSystems;
using Visual;

namespace LevelEditor {
	public partial class LevelEditor : MonoBehaviour {
		[Header("Debug")] public bool showCenterPos;
		public bool showMouseHit;

		[Header("Receiving Events On")] 
		[SerializeField] private VoidEventChannelSO levelLoaded;
		[SerializeField] private IntEventChannelSO setModeEC;
		[SerializeField] private LevelEditorLayerEventChannel levelEditorLayerEC;
		[SerializeField] private LevelEditorModeEventChannel levelEditorModeEC;
		
		[Header("Sending Events On")] [SerializeField]
		private VoidEventChannelSO redrawLevelEC;
		[SerializeField] private LevelEditorStateEventChannel levelEditorStateUpdateEC;

		[Header("scene References")] 
		// [SerializeField] private TileMapDrawer gridDrawer;
		[SerializeField] private CursorDrawer cursorDrawer;
		// [SerializeField] private GridController gridController;

		[Header("Input")] 
		[SerializeField] private InputReader inputReader;
		[SerializeField] private InputCache inputCache;

		[Header("SO References")] 
		[SerializeField] private TileTypeContainerSO tileTypesContainer;
		[SerializeField] private GridDataSO gridDataSO;

		[Header("Settings"), SerializeField] private EditorState _editorState;

/////////////////////////////////////// Properties ////////////////////////////////////////////

		public TileMapDrawer GridDrawer => GameplayProvider.Current.GridDrawer;
		private GridController GridController => GameplayProvider.Current.GridController;

		public EditorMode Mode {
			get => _editorState.editorMode;
			set => _editorState.editorMode = value;
		}

		public LayerType Layer {
			get => _editorState.layerType;
			set => _editorState.layerType = value;
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
		private bool _dragStarted;

		private bool remove = false;

/////////////////////////////////////// Local Functions ////////////////////////////////////////////

		private void SetMode(int newMode) {
			var len = Enum.GetNames(typeof(EditorMode)).Length;

			if ( newMode >= len ) {
				Layer = ( LayerType )( newMode - len );
			}

			if ( newMode >= 0 && newMode < len ) {
				Mode = ( EditorMode )newMode;
			}

			Debug.Log($"Level Editor > Set Mode\n {Layer} {Mode}");
		}

		private void DrawCursor(LayerType layerType, CursorMode cursorMode, Vector3 pos,
			Vector3 abovePos) {
			switch ( layer: layerType, cursorMode ) {
				case (LayerType.Tile, CursorMode.Remove):
					cursorDrawer.DrawCursorAt(pos, cursorMode);
					break;
				case (LayerType.Tile, CursorMode.Add):
					cursorDrawer.DrawCursorAt(abovePos, cursorMode);
					break;

				case (LayerType.Item, CursorMode.Add):
				case (LayerType.Item, CursorMode.Remove):
					cursorDrawer.DrawCursorAt(abovePos, cursorMode);
					break;
				
				// layer none? mode none?
				default:
					cursorDrawer.DrawCursorAt(abovePos, CursorMode.Error);
					break;
			}
		}

///// Callbacks ////////////////////////////////////////////////////////////////////////////////////
		
		private void HandleModeChange(EditorMode mode) {
			_editorState.editorMode = mode;
			Debug.Log($"Mode Update: {_editorState}");
			levelEditorStateUpdateEC.RaiseEvent(_editorState);
		}

		private void HandleLayerChange(LayerType layer) {
			_editorState.layerType = layer;
			Debug.Log($"Layer Update: {_editorState}");
			levelEditorStateUpdateEC.RaiseEvent(_editorState);
		}
		
		private void HandleMouseDrag(Vector3 pos) {
			// if ( _leftClicked ) {
			// 	if ( _clickedAbovePos.Count == 1 ) {
			// 		_clickedAbovePos.Add(pos);
			// 	}
			// 	else {
			// 		_clickedAbovePos[1] = pos;
			// 		Debug.Log($"above [{_clickedAbovePos[0]}, {_clickedAbovePos[1]}]");
			// 	}
			// }
			// else {
			// 	if ( _clickedSelectedPos.Count == 1 ) {
			// 		_clickedSelectedPos.Add(pos);
			// 	}
			// 	else {
			// 		_clickedSelectedPos[1] = pos;
			// 		Debug.Log($"selected [{_clickedSelectedPos[0]}, {_clickedSelectedPos[1]}]");
			// 	}
			// }
			
			// Debug.Log($"above [{_clickedAbovePos[0]}, {_clickedAbovePos[1]}]\nselected [{_clickedSelectedPos[0]}, {_clickedSelectedPos[1]}]");
		}

		// private void HandleMouseDragEnd() {
		// 	_dragEnd = true;
		// }

/////////////////////////////////////// Public Functions ///////////////////////////////////////////

		[ContextMenu("Update Editor State")]
		public void UpdateEditorState() {
			levelEditorStateUpdateEC.RaiseEvent(_editorState);	
		}

		public void RedrawLevel() {
			GridDrawer.DrawGridLayout();
			redrawLevelEC.RaiseEvent();
		}

///// Unity Functions //////////////////////////////////////////////////////////////////////////////		
		
		#region MonoBehaviour

		private void OnEnable() {
			_editorState.selectedTileType = tileTypesContainer.tileTypes[1];
			inputReader.ResetEditorLevelEvent += ResetLevel;
			levelLoaded.OnEventRaised += RedrawLevel;
			setModeEC.OnEventRaised += SetMode;
			levelEditorLayerEC.OnEventRaised += HandleLayerChange;
			levelEditorModeEC.OnEventRaised += HandleModeChange;

			UpdateEditorState();
		}

		private void OnDisable() {
			inputReader.ResetEditorLevelEvent -= ResetLevel;
			levelLoaded.OnEventRaised -= RedrawLevel;
			setModeEC.OnEventRaised -= SetMode;
			levelEditorLayerEC.OnEventRaised -= HandleLayerChange;
			levelEditorModeEC.OnEventRaised -= HandleModeChange;
		}

		private void Update() {
			if(EventSystem.current is {} && EventSystem.current.IsPointerOverGameObject()) return;
			
			if ( !inputReader.GameInput.Gameplay.enabled || inputCache.IsMouseOutsideWindow() ) {
				return;
			}

			inputCache.UpdateMouseInput(gridDataSO);

			#region refactor to mouse input / input cache

			rightMouseWasReleased = inputCache.rightButton.wasRelesed;
			leftMouseWasReleased = inputCache.leftButton.wasRelesed;
			leftMouseIsPressed = inputCache.leftButton.isPressed;
			rightMouseIsPressed = inputCache.rightButton.isPressed;
			_rightClicked = inputCache.rightButton.started;
			_leftClicked = inputCache.leftButton.started;
			_dragEnd = inputCache.DragEnded();

			Vector3 tileWorldPositionSelected = inputCache.cursor.selectedPos.tilePos;
			Vector3 tileWorldPositionAbove = inputCache.cursor.abovePos.tilePos;
			var centeredGridPos = inputCache.cursor.selectedPos.tileCenter;
			var centeredGridPosAbove = inputCache.cursor.abovePos.tileCenter;

			// remove = rightMouseIsPressed && !(leftMouseIsPressed || _leftClicked);


			if ( _leftClicked || leftMouseIsPressed ) {
				remove = false;
			}
			if ( rightMouseIsPressed ) {
				remove = true;
			}

			editing = leftMouseIsPressed || rightMouseIsPressed || leftMouseWasReleased || rightMouseWasReleased;

			if ( editing ) {
				CurrentPos = tileWorldPositionSelected;
				CurrentAbovePos = tileWorldPositionAbove;

				if ( _leftClicked || _rightClicked ) {
					StartPos = CurrentPos;
					StartAbovePos = CurrentAbovePos;
					EndPos = CurrentPos;
					EndAbovePos = CurrentAbovePos;
				}

				if ( leftMouseWasReleased || rightMouseWasReleased ) {
					EndPos = CurrentPos;
					EndAbovePos = CurrentAbovePos;
				}
			}
			
			
			
			// if ( leftMouseIsPressed || rightMouseIsPressed ) {
			// 	_clickedSelectedPos.Add(tileWorldPositionSelected);
			// 	_clickedAbovePos.Add(tileWorldPositionAbove);	
			// }

			// if ( _leftClicked ) {
			// 	ClearCachedClickedPositions();
			// 	_clickedSelectedPos.Add(tileWorldPositionSelected);
			// 	_clickedAbovePos.Add(tileWorldPositionAbove);
			// }

			// kein input aber gecached daten -> clear
			// if ( !rightMouseIsPressed && !leftMouseIsPressed &&
			//      !( rightMouseWasReleased || leftMouseWasReleased ) 
			//      && _clickedAbovePos is {Count: >0 } 
			//      && _clickedSelectedPos is {Count: >0 }
			//      && !_dragEnd) {
			// 	
			// 	ClearCachedClickedPositions();
			// }
			
			#endregion
			
			var cursorMode = remove ? CursorMode.Remove : CursorMode.Add;
			
			switch ( Mode ) {
				case EditorMode.Select:
					cursorDrawer.DrawCursorAt(centeredGridPosAbove, CursorMode.Select);
					break;

				case EditorMode.Paint:
					DrawCursor(Layer, cursorMode, centeredGridPos, centeredGridPosAbove);

					if ( rightMouseIsPressed ) {
						RemoveOne();
						break;
					}

					if ( _leftClicked ) {
						AddOne();
					}

					if ( leftMouseWasReleased || rightMouseWasReleased ) {
						ClearCachedClickedPositions();
					}

					break;

				case EditorMode.Box:

					if ( _leftClicked ) {
						Debug.Log("StartDrag left");
						_dragStarted = true;
						_dragEnd = false;
						// HandleMouseDrag(tileWorldPositionAbove);
						// Debug.Log($"left:{_leftClicked} right:{_rightClicked}");
					}
					else if ( _rightClicked ) {
						Debug.Log("StartDrag left");
						_dragStarted = true;
						_dragEnd = false;
						// HandleMouseDrag(tileWorldPositionSelected);
						// Debug.Log($"left:{_leftClicked} right:{_rightClicked}");
					}
					else if ( leftMouseWasReleased || rightMouseWasReleased ) {
						Debug.Log("End drag");
						
						// HandleMouseDragEnd();
						_dragEnd = true;
						_dragStarted = false;
					}

					if ( _dragEnd ) {
						// Debug.Log("Drag ended Resolve -> ");
						
						if ( remove ) {
							// Debug.Log("Drag Remove");
							
							RemoveMany();
						}
						else {
							// Debug.Log("Drag add");
							
							AddMany();
						}
						
						cursorDrawer.HideCursor();
					}
					else if(_dragStarted) {
						if ( !remove ) {
							var start = gridDataSO.GetTileCenter2DFromWorldPos(StartAbovePos);
							var end = gridDataSO.GetTileCenter2DFromWorldPos(CurrentAbovePos);
							cursorDrawer.DrawBoxCursorAt(start, end, CursorMode.Add);
						}
						else if ( remove ) {
							var startRemove = gridDataSO.GetTileCenter2DFromWorldPos(StartPos);
							var endRemove = gridDataSO.GetTileCenter2DFromWorldPos(CurrentPos);
							cursorDrawer.DrawBoxCursorAt(startRemove, endRemove, CursorMode.Remove);
						}
					}
					else {
						DrawCursor(Layer, cursorMode, centeredGridPos, centeredGridPosAbove);
					}

					break;
			}
		}

		#endregion

		
	}
}