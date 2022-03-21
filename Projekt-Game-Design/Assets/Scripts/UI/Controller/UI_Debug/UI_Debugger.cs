using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.UI_Debug {
	public class UI_Debugger : MonoBehaviour {
		[SerializeField] private UIDocument uiDocument;

		public void PrintCurrentFocus() {
			Debug.Log(uiDocument.rootVisualElement.focusController?.focusedElement);
		}

		private void Start() {
			// uiDocument.rootVisualElement.RegisterCallback<FocusInEvent>(evt => {
			// 	Debug.Log($"In\n{evt.target}\n{evt.currentTarget}");
			// });
			
			// uiDocument.rootVisualElement.RegisterCallback<FocusOutEvent>(evt => {
			// 	Debug.Log($"Out\n{evt.target}\n{evt.currentTarget}");
			// });
		}

		private void Update() {
			PrintCurrentFocus();
		}
	}
}