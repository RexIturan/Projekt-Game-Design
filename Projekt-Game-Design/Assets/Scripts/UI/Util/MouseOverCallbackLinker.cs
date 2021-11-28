using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Util {
	public class MouseOverCallbackLinker : MonoBehaviour {
		[SerializeField] private UIDocument uiDocument;
		//sending on
		[SerializeField] private BoolEventChannelSO mouseOverUI_EC;
		//listening to
		[SerializeField] private VoidEventChannelSO onSceneReady_EC;
		
		private List<VisualElement> _elementsWithCallback;

/////////////////////////////////////// Local Variables ////////////////////////////////////////////
		
		private void MouseExitCallback(MouseOutEvent outEvent) {
			mouseOverUI_EC.RaiseEvent(false);
			// Debug.Log($"Exit\n{outEvent.target}");
		}

		private void MouseEnterCallback(MouseOverEvent enterEvent) {
			//todo do something with the event
			mouseOverUI_EC.RaiseEvent(true);
			// Debug.Log($"Enter\n{enterEvent.target}");
		}

		private void SetupMouseOverCallback(VisualElement element) {
			if ( !_elementsWithCallback.Contains(element) ) {
				//todo if(element.ClassListContains("blocksMouse"))
				if ( element.pickingMode == PickingMode.Position && element.visible) {
					_elementsWithCallback.Add(element);
					element.RegisterCallback<MouseOverEvent>(MouseEnterCallback);
					element.RegisterCallback<MouseOutEvent>(MouseExitCallback);
				}	
			}
		}

		private void SetupCallback(VisualElement element) {
			SetupMouseOverCallback(element);
			if ( element.childCount > 0 ) {
				foreach ( var childElement in element.Children() ) {
					SetupCallback(childElement);
				}
			}
		}

		private void CleanUpCallbacks() {
			foreach ( var element in _elementsWithCallback ) {
				if ( element != null ) {
					element.UnregisterCallback<MouseOverEvent>(MouseEnterCallback);
					element.UnregisterCallback<MouseOutEvent>(MouseExitCallback);
				}
			}

			_elementsWithCallback.Clear();
		}

		#region MyRegion

		private void Awake() {
			_elementsWithCallback = new List<VisualElement>();
		}

		private void OnEnable() {
			onSceneReady_EC.OnEventRaised += SetupCallbacks;
		}

		private void OnDisable() {
			onSceneReady_EC.OnEventRaised -= SetupCallbacks;
		}

		private void OnDestroy() {
			CleanUpCallbacks();
		}

		#endregion Monobehaviour

/////////////////////////////////////// Public Functions ///////////////////////////////////////////		
		
		// fires when scene is loaded
		public void SetupCallbacks() {
			var root = uiDocument.rootVisualElement;
			SetupCallback(root);
		}
	}
}