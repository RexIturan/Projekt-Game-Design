﻿using System;
using Events.ScriptableObjects;
using Grid;
using Input;
using SaveSystem;
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

		[Header("Receiving Events On")] [SerializeField]
		private VoidEventChannelSO levelLoaded;

		[SerializeField] private IntEventChannelSO setModeEC;

		[Header("SendingEventsOn")] [SerializeField]
		private VoidEventChannelSO updateMeshEC;

		[Header("scene References")] [SerializeField]
		private TileMapDrawer drawer;

		[SerializeField] private GridController controller;
		//input
		[SerializeField] private InputReader inputReader;
		[SerializeField] private InputCache inputCache;

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
				Mode = ( CursorMode )mode;
			}
		}

		#region MonoBehaviour

		private void Awake() {
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
			if ( !inputReader.GameInput.Gameplay.enabled ) {
				return;
			}

			//todo also in input cache??
			bool leftMouseWasPressed = false;
			bool leftMouseWasReleased = false;
			bool leftMousePressed = false;
			bool rightMousePressed = false;
			
			if ( !inputCache.IsMouseOverUI ) {
				var mouse = Mouse.current;	
				leftMouseWasPressed =  mouse.leftButton.wasPressedThisFrame;
				leftMouseWasReleased = mouse.leftButton.wasReleasedThisFrame;
				leftMousePressed =     mouse.leftButton.isPressed;
				rightMousePressed =    mouse.rightButton.isPressed;
			}
			
			//todo getMousePosition at level 
			Vector3 mousePosition = MousePosition.GetMouseWorldPosition(Vector3.up, 1);

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
		
		#endregion
		
/////////////////////////////////////// Public Functions ///////////////////////////////////////////

		public void RedrawLevel() {
			drawer.DrawGrid();
			updateMeshEC.RaiseEvent();
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

		private void HandleMouseClick(Vector3 pos) {
			_leftClicked = true;
			_clickPos = pos;
		}

		private void HandleMouseDrag(Vector3 pos) {
			_dragPos = pos;
		}

		private void HandleMouseDragEnd() {
			_dragEnd = true;
		}

		private void AddMultipleTilesAt(Vector3 clickPos, Vector3 dragPos) {
			controller.AddMultipleTilesAt(clickPos, dragPos, selectedTileType.id);
		}

		private void AddTileAt(Vector3 clickPos) {
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