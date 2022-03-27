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
			_clickedAbovePos = new List<Vector3>();
			_clickedSelectedPos = new List<Vector3>();

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

			//todo(vincent) refactor #input cache

			#region refactor to mouse input / input cache

			// Debug.Log(inputCache.IsMouseOverUI);

			if ( !inputCache.IsMouseOverUI ) {
				rightMouseWasReleased = inputCache.rightButton.wasRelesed;
				leftMouseWasReleased = inputCache.leftButton.wasRelesed;
				leftMouseIsPressed = inputCache.leftButton.isPressed;
				rightMouseIsPressed = inputCache.rightButton.isPressed;
				_rightClicked = inputCache.rightButton.isPressed;
				_leftClicked = inputCache.leftButton.isPressed;
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
			
			var cursorMode = remove ? CursorMode.Remove : CursorMode.Add;
			
			switch ( Mode ) {
				case EditorMode.Select:
					cursorDrawer.DrawCursorAt(centeredGridPosAbove, CursorMode.Select);
					break;

				case EditorMode.Paint:
					DrawCursor(Layer, cursorMode, centeredGridPos, centeredGridPosAbove);

					if ( _rightClicked ) {
						RemoveOne();
						break;
					}

					if ( _leftClicked ) {
						AddOne();
					}

					break;

				case EditorMode.Box:

					if ( _leftClicked ) {
						HandleMouseDrag(positionAbove);
						// Debug.Log($"left:{_leftClicked} right:{_rightClicked}");
					}
					else if ( _rightClicked ) {
						HandleMouseDrag(selectedPosition);
						// Debug.Log($"left:{_leftClicked} right:{_rightClicked}");
					}
					else if ( leftMouseWasReleased || rightMouseWasReleased ) {
						HandleMouseDragEnd();
						_dragEnd = true;
					}
					else {
						DrawCursor(Layer, cursorMode, centeredGridPos, centeredGridPosAbove);
					}

					if ( _dragEnd ) {
						//todo remove
						// Debug.Log("drag ended");
						if ( remove ) {
							RemoveMany();
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

		#endregion

		
	}
}