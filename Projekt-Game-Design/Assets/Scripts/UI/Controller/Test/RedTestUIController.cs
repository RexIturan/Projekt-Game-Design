using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace GDP01.UI.Controller.Test {
	public class RedTestUIController : MonoBehaviour {
		
		[Serializable]
		private struct SettingsButtonNames {
			public string one;
			public string two;
			public string three;
		}

		[Header("Button Settings")]
		[SerializeField] private SettingsButtonNames buttonNames =
			new SettingsButtonNames { one = "Btn01", two = "Btn02", three = "Btn03" };
		[SerializeField] private bool showDebugMessages = false;
		[SerializeField] private bool localRoot = false;
		
///// Private Variables ////////////////////////////////////////////////////////////////////////////

		private VisualElement root;
		private Button _Button1;
		private Button _Button2;
		private Button _Button3;

///// Properties ///////////////////////////////////////////////////////////////////////////////////		

		private VisualElement Root {
			get {
				if ( root == null ) {
					if ( localRoot ) {
						root = GetComponent<UIDocument>().rootVisualElement;
					} else {
						var insertPanel = GetComponent<InsertPanel>();
						root = insertPanel.Root;
					}

					if ( root == null ) {
						Debug.LogError("No Root Element Found");
					}
				}
				return root;
			}
		}

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void UnbindButton(Button button, Action action) {
			if ( button is { } ) {
				button.clicked -= action;
			}
			else {
				Debug.LogError($"UnbindButton\n{name} Button not Found.");
			}
		}

		private void BindButton(ref Button button, VisualElement root, string name, Action action) {
			button = root.Q<Button>(name);
			if ( button is { } ) {
				button.clicked += action;
			}
			else {
				Debug.LogError($"BindButton\n{name} Button not Found in {root}.");
			}
		}

		private void BindButtons() {
			BindButton(ref _Button1, Root, buttonNames.one, () => HandleButton(1));
			BindButton(ref _Button2, Root, buttonNames.two, () => HandleButton(2));
			BindButton(ref _Button3, Root, buttonNames.three, () => HandleButton(3));
		}

		private void UnbindButtons() {
			UnbindButton(_Button1, () => HandleButton(1));
			UnbindButton(_Button2, () => HandleButton(2));
			UnbindButton(_Button3, () => HandleButton(3));
		}
		
///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

		private void HandleButton(int i) {
			if ( showDebugMessages ) {
				Debug.Log($"Button {i} Pressed");	
			}
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void OnEnable() {
			root = null;
			BindButtons();
		}
		
		private void OnDisable() {
			UnbindButtons();
		}
	}
}