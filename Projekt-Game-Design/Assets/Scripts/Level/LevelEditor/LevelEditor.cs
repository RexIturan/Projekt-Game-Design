using System;
using Events.ScriptableObjects;
using Grid;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Util;
using Visual;

namespace LevelEditor {
	public class LevelEditor : MonoBehaviour {
		public enum CursorMode {
			Select,
			Paint,
			Box,
			Fill,
		}

		[Header("Receiving Events On")] 
		[SerializeField] private VoidEventChannelSO levelLoaded;
		[SerializeField] private IntEventChannelSO setModeEC;

		[Header("SendingEventsOn")] [SerializeField]
		private VoidEventChannelSO updateMeshEC;

		[Header("scene References")] [SerializeField]
		private TileMapDrawer drawer;

		[SerializeField] private GridController controller;
		[SerializeField] private InputReader inputReader;

		[Header("SO References")] [SerializeField]
		private TileTypeContainerSO tileTypesContainer;

		[Header("Settings")] [SerializeField] private CursorMode mode = CursorMode.Paint;

		public CursorMode Mode {
			get => mode;
			set => mode = value;
		}

		[SerializeField] private TileTypeSO selectedTileType;

		// runtime input data data
		private Vector3 _clickPos;
		private Vector3 _dragPos;
		private bool _leftClicked;
		private bool _rightClicked;
		private bool _dragEnd;

/////////////////////////////////////// Local Functions ////////////////////////////////////////////
		
		private void SetMode(int mode) {
			var len = Enum.GetNames(typeof(CursorMode)).Length;

			if ( mode >= 0 && mode < len ) {
				Mode = (CursorMode)mode;
			}
		}
		
/////////////////////////////////////// Public Functions ///////////////////////////////////////////

		public void Awake() {
			selectedTileType = tileTypesContainer.tileTypes[1];
			inputReader.ResetEditorLevelEvent += ResetLevel;
			levelLoaded.OnEventRaised += RedrawLevel;
			setModeEC.OnEventRaised += SetMode;
		}

		public void OnDestroy() {
			inputReader.ResetEditorLevelEvent -= ResetLevel;
			levelLoaded.OnEventRaised -= RedrawLevel;
			setModeEC.OnEventRaised -= SetMode;
		}

		public void RedrawLevel() {
			drawer.DrawGrid();
			updateMeshEC.RaiseEvent();
		}

		private void Update() {
			if ( !inputReader.GameInput.Gameplay.enabled ) {
				return;
			}

			//todo getMousePosition at level 
			Vector3 mousePosition = MousePosition.GetMouseWorldPosition(Vector3.up, 1);

			var mouse = Mouse.current;
			bool leftMouseWasPressed = mouse.leftButton.wasPressedThisFrame;
			bool leftMouseWasReleased = mouse.leftButton.wasReleasedThisFrame;
			bool leftMousePressed = mouse.leftButton.isPressed;
			bool rightMousePressed = mouse.rightButton.isPressed;

			if ( rightMousePressed ) {
				_rightClicked = true;
				_clickPos = mousePosition;
			}

			switch ( mode ) {
				case CursorMode.Select:
					drawer.DrawCursorAt(mousePosition);
					break;
				
				case CursorMode.Paint:
					drawer.DrawCursorAt(mousePosition);

					if ( _rightClicked ) {
						HandleRemove();
						break;
					}

					if ( _leftClicked ) {
						HandlePaint();
					}

					if ( leftMousePressed ) {
						HandleMouseClick(mousePosition);
					}

					break;
				
				case CursorMode.Box:

					if ( leftMouseWasPressed ) {
						HandleMouseClick(mousePosition);
					}

					if ( leftMousePressed ) {
						HandleMouseDrag(mousePosition);
					}
					else if ( leftMouseWasReleased ) {
						HandleMouseDragEnd();
					}

					if ( _dragEnd ) {
						HandleBox();
					}
					else {
						if ( _leftClicked ) {
							drawer.DrawBoxCursorAt(_clickPos, _dragPos);
						}
					}

					break;
				
				case CursorMode.Fill:
					break;
			}
		}

		private void HandleBox() {
			AddMultipleTilesAt(_clickPos, _dragPos);
			_leftClicked = false;
			_dragEnd = false;
			_clickPos = Vector3Int.zero;
			_dragPos = Vector3Int.zero;
			drawer.ClearCursor();
			RedrawLevel();
		}

		private void HandleRemove() {
			controller.AddTileAt(_clickPos, tileTypesContainer.tileTypes[0].id);
			_clickPos = Vector3Int.zero;
			_rightClicked = false;
			RedrawLevel();
		}

		private void HandlePaint() {
			AddTileAt(_clickPos);
			_clickPos = Vector3Int.zero;
			_leftClicked = false;
			RedrawLevel();
		}

		public void HandleMouseClick(Vector3 pos) {
			_leftClicked = true;
			_clickPos = pos;
		}

		public void HandleMouseDrag(Vector3 pos) {
			_dragPos = pos;
		}

		public void HandleMouseDragEnd() {
			_dragEnd = true;
		}

		public void AddMultipleTilesAt(Vector3 clickPos, Vector3 dragPos) {
			controller.AddMultipleTilesAt(clickPos, dragPos, selectedTileType.id);
		}

		public void AddTileAt(Vector3 clickPos) {
			controller.AddTileAt(clickPos, selectedTileType.id);
		}

		public void ResetLevel() {
			controller.ResetGrid();
			RedrawLevel();
		}

		// public void AddObjectAt(Vector3 clickPos) {
		//     objectController.AddTileAt(clickPos, worldObjectContainer.worldObjects[1]);
		// }
	}
}