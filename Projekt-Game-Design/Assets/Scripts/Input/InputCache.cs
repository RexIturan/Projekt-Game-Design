using System;
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

		public void SetMouseOverUI(bool over) {
			//todo map id with if over
			if ( over ) {
				_overUICount++;
				_mouseIsOverUI = true;
				Debug.Log($"InputCache > SetMouseOverUI:\nInc ++ ref: {_overUICount} value: {IsMouseOverUI}" );
			}
			else {
				_overUICount--;
				if ( _overUICount == 0 ) {
					_mouseIsOverUI = false;
				}
				else if ( _overUICount < 0 ) {
					Debug.LogError("InputCache > SetMouseOverUI \n too much want to disable");
				}
				Debug.Log($"InputCache > SetMouseOverUI:\nDec -- ref: {_overUICount} value: {IsMouseOverUI}" );
			}
		}

		//todo move to mouse input class??
		//calculates if mouse is outside of the window
		public bool IsMouseOutsideWindow() {
			//get mouse position
			var pos = Mouse.current.position.ReadValue();
			var view = Camera.current.ScreenToViewportPoint(pos);

			// check against viewport
			var isOutside = view.x < 0 || view.x > 1 || view.y < 0 || view.y > 1;
			return isOutside;
		}

		#endregion
	}
}