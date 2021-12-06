using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Grid;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using Util;

namespace Input {
	[CreateAssetMenu(fileName = "InputCache", menuName = "Input/Input Cache", order = 0)]
	public class InputCache : ScriptableObject {

		public struct DragState {
			public bool started;
			public bool ended;
		}

		public struct MouseButton {
			public bool started;
			public bool isPressed;
			public bool wasRelesed;
		}

		[Serializable]
		public struct CursorData {
			public CursorPos selectedPos;
			public CursorPos abovePos;
		}

		[Serializable]		
		public struct CursorPos {
			// raw mouse Pos
			public Vector3 mousePosition;
			
			// tile Pos
			public Vector3Int tilePos;
			public Vector3 tileCenter;
			
			// grid Pos
			public Vector3Int gridPos;
		}
		
		[Serializable]
		public struct CursorDebug {
			public bool showGridCenterPos;
			public bool showMousePos;
		}
		
/////////////////////////////////////// Serialized Fields //////////////////////////////////////////

		[Header("Recieving Events On")] 
		[SerializeField] private BoolEventChannelSO mouseOverChangedEC;

		[SerializeField] private CursorDebug cursorDebug;
		
/////////////////////////////////////// Local Variables ////////////////////////////////////////////

		private int _overUICount = 0;
		private bool _mouseIsOverUI = false;

		
		private bool hitBottomSelected;
		private bool hitBottomAbove;
		
/////////////////////////////////////// Public Variables ///////////////////////////////////////////

///////////////////////////////////////   Properties    ////////////////////////////////////////////

		public bool IsMouseOverUI => _mouseIsOverUI;

		public CursorData cursor;
		// public CursorPos cursorSelectedPos;
		// public CursorPos cursorAbovePos;
		
		public MouseButton leftButton;
		public MouseButton rightButton;
		
		public DragState leftDrag;
		public DragState rightDrag;
		
		public bool DragEnded() {
			return leftButton.isPressed && leftButton.wasRelesed ||
			       rightButton.isPressed && rightButton.wasRelesed;
		}
		
/////////////////////////////////////// Local Functions ////////////////////////////////////////////

		private void Awake() {
			_overUICount = 0;
			_mouseIsOverUI = false;
			cursorDebug.showGridCenterPos = true;
			cursorDebug.showMousePos = true;
		}

		private void OnEnable() {
			mouseOverChangedEC.OnEventRaised += SetMouseOverUI;
		}

		private void OnDisable() {
			mouseOverChangedEC.OnEventRaised -= SetMouseOverUI;
		}
		
/////////////////////////////////////// Public Functions ///////////////////////////////////////////

		#region public functions

		#region Mouse

		private void ReadCursorSelectedPos(GridDataSO gridData) {
			cursor.selectedPos.mousePosition = MousePosition.GetTilePos(gridData, false, out hitBottomSelected, cursorDebug.showGridCenterPos, cursorDebug.showMousePos);
			cursor.selectedPos.gridPos = gridData.GetGridPos3DFromWorldPos(cursor.selectedPos.mousePosition);
			cursor.selectedPos.tilePos = gridData.GetTilePos3DFromWorldPos(cursor.selectedPos.mousePosition);
			cursor.selectedPos.tileCenter = gridData.GetTileCenter2DFromWorldPos(cursor.selectedPos.mousePosition);
		}
		
		private void ReadCursorAbovePos(GridDataSO gridData) {
			cursor.abovePos.mousePosition = MousePosition.GetTilePos(gridData, true, out hitBottomSelected, cursorDebug.showGridCenterPos, cursorDebug.showMousePos);
			cursor.abovePos.gridPos = gridData.GetGridPos3DFromWorldPos(cursor.abovePos.mousePosition);
			cursor.abovePos.tilePos = gridData.GetTilePos3DFromWorldPos(cursor.abovePos.mousePosition);
			cursor.abovePos.tileCenter = gridData.GetTileCenter2DFromWorldPos(cursor.abovePos.mousePosition);
		}
		
		public void UpdateMouseInput(GridDataSO gridData) {
			var mouse = Mouse.current;

			ReadCursorSelectedPos(gridData);
			ReadCursorAbovePos(gridData);

			leftButton.started    = mouse.leftButton.wasPressedThisFrame;
			leftButton.isPressed  = mouse.leftButton.isPressed;
			leftButton.wasRelesed = mouse.leftButton.wasReleasedThisFrame;
			
			rightButton.started    = mouse.rightButton.wasPressedThisFrame;
			rightButton.isPressed  = mouse.rightButton.isPressed;
			rightButton.wasRelesed = mouse.rightButton.wasReleasedThisFrame;

			leftDrag.started = leftButton.started;
			leftDrag.ended   = leftButton.wasRelesed;
			
			rightDrag.started = rightButton.started;
			rightDrag.ended   = rightButton.wasRelesed;
		}

		#endregion
		
		#region Mouse UI

		public void SetMouseOverUI(bool over) {
			//todo map id with if over
			if ( over ) {
				_overUICount++;
				_mouseIsOverUI = true;
				// Debug.Log($"InputCache > SetMouseOverUI:\nInc ++ ref: {_overUICount} value: {IsMouseOverUI}" );
			}
			else {
				_overUICount--;
				if ( _overUICount <= 0 ) {
					_mouseIsOverUI = false;
				}
				if ( _overUICount < 0 ) {
					_overUICount = 0;
					Debug.LogError("InputCache > SetMouseOverUI \n too much Elements want to disable");
				}
				// Debug.Log($"InputCache > SetMouseOverUI:\nDec -- ref: {_overUICount} value: {IsMouseOverUI}" );
			}
		}

		//todo move to mouse input class??
		//calculates if mouse is outside of the window
		public bool IsMouseOutsideWindow() {
			//get mouse position
			var pos = Mouse.current.position.ReadValue();
			if ( Camera.main is { } ) {
				var view = Camera.main.ScreenToViewportPoint(pos);

				// check against viewport
				var isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
				return isOutside;
			}
			else {
				return true;
			}
		}

		#endregion

		#endregion
	}
}