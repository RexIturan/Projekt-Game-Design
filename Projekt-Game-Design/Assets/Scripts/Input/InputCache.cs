using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input {
	[CreateAssetMenu(fileName = "InputCache", menuName = "Input/Input Cache", order = 0)]
	public class InputCache : ScriptableObject {
/////////////////////////////////////// Serialized Fields //////////////////////////////////////////

		[Header("Recieving Events On")] [SerializeField]
		private BoolEventChannelSO mouseOverChangedEC;

/////////////////////////////////////// Local Variables ////////////////////////////////////////////

		private int _overUICount = 0;
		private bool _mouseIsOverUI = false;

/////////////////////////////////////// Public Variables ///////////////////////////////////////////

///////////////////////////////////////   Properties    ////////////////////////////////////////////

		public bool IsMouseOverUI => _mouseIsOverUI;

/////////////////////////////////////// Local Functions ////////////////////////////////////////////

		private void Awake() {
			_overUICount = 0;
			_mouseIsOverUI = false;
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