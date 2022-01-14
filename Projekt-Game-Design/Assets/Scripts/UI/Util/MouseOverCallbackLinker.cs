using System.Collections.Generic;
using System.Linq;
using Events.ScriptableObjects;
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
		private Dictionary<VisualElement, bool> overUIDict = new Dictionary<VisualElement, bool>();

/////////////////////////////////////// Local Variables ////////////////////////////////////////////
		
		private void MouseExitCallback(MouseOutEvent outEvent) {
			if ( outEvent.currentTarget is VisualElement element ) {
				if ( overUIDict.ContainsKey(element) ) {
					if ( overUIDict[element] ) {
						overUIDict[element] = false;
						mouseOverUI_EC.RaiseEvent(false);
					} 
				}
			}

			// Debug.Log($"Exit\n current:{outEvent.currentTarget} target:{outEvent.target}");
		}

		private void MouseEnterCallback(MouseOverEvent enterEvent) {
			// Debug.Log($"Enter\n current:{enterEvent.currentTarget} target:{enterEvent.target}");
			
			if ( enterEvent.currentTarget is VisualElement element ) {
				if ( overUIDict.ContainsKey(element) ) {

					if ( !overUIDict[element] ) {
						overUIDict[element] = true;
						mouseOverUI_EC.RaiseEvent(true);
					} 
				}
			}
		}

		private bool SetupMouseOverCallback(VisualElement element) {
			if ( !_elementsWithCallback.Contains(element) ) {
				//todo if(element.ClassListContains("blocksMouse"))
				if ( element.pickingMode == PickingMode.Position && element.visible) {
					_elementsWithCallback.Add(element);
					overUIDict.Add(element, false);
					element.RegisterCallback<MouseOverEvent>(MouseEnterCallback);
					element.RegisterCallback<MouseOutEvent>(MouseExitCallback);
					// element.RegisterCallback<DetachFromPanelEvent>();
					// Debug.Log($"SetupMouseOverCallback\n ----- {element.name}");
					return true;
				}	
			}

			return false;
		}

		private void SetupCallback(VisualElement element) {
			if ( !SetupMouseOverCallback(element) ) {
				if ( element.childCount > 0 ) {
					foreach ( var childElement in element.Children() ) {
						SetupCallback(childElement);
					}
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

		#region Unity Monobehaviour

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
			overUIDict.Clear();
			
			var root = uiDocument.rootVisualElement;
			SetupCallback(root);
		}
	}
}