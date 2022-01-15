using System;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace QuestSystem.ScriptabelObjects {
	public class Task_KeyPress_SO : TaskSO {

		#region Debug helper

		[Serializable]
		public struct InputActionMapInfoWrapper {
			public string name;
			public List<InputActionInfoWrapper> actions;
		}
		
		[Serializable]
		public struct InputActionInfoWrapper {
			public string name;
			public string map;
			public string id;
			public BindingInfoWrapper[] bindingName;
		}
		
		[Serializable]
		public struct BindingInfoWrapper {
			public string name;
			public string key;
		}

		#endregion
		
///// SerializeField ///////////////////////////////////////////////////////////////////////////////		
		
		[SerializeField] private InputReader inputReader;
		
		[SerializeField] private string actionName;
		[SerializeField] private string mapName;
		[SerializeField] private string compositeName;

		//todo helper, remove later
		[SerializeField] private List<InputActionInfoWrapper> actionNames;

///// Private Variables ////////////////////////////////////////////////////////////////////////////

		private InputAction action;
		private List<string> bindingControls;

///// Private Functions ////////////////////////////////////////////////////////////////////////////
		
		private bool MatchesBindingControls(string currentInput, List<string> checkAgainst) {
			return checkAgainst.Contains(currentInput);
		}

		private List<string> GetBindingControls(InputAction action, string compositeName) {
			List<string> controls = new List<string>();
			foreach ( var binding in action.bindings ) {
				if (!compositeName.Equals("")) {
					if ( binding.name.Equals(compositeName) ) {
						binding.ToDisplayString(out string deviceName, out string controlPath);
						controls.Add(controlPath);	
					}
				}
				else {
					binding.ToDisplayString(out string deviceName, out string controlPath);
					controls.Add(controlPath);
				}
			}

			return controls;
		}
		
		private BindingInfoWrapper[] GetBindingNames(ReadOnlyArray<InputBinding> bindings) {
			BindingInfoWrapper[] result = new BindingInfoWrapper[bindings.Count];
			for ( int i = 0; i < bindings.Count; i++ ) {
				result[i].name = bindings[i].name;
				result[i].key = bindings[i].ToDisplayString();
			}
			return result;
		}

		private InputAction GetInputAction(string mapName, string actionName) {
			var gameInput = inputReader.GameInput;
			InputAction inputAction = null;
			
			foreach ( var action in gameInput ) {
				if (action.name.Equals(actionName) && action.actionMap.name.Equals(mapName) ) {
					inputAction = action;
				}
				
			}
			return inputAction;
		}

		private void Setup() {
			if ( inputReader is null ) {
				Debug.LogError("InputReader Reference not set!");
				return;
			}
			
			action = GetInputAction(mapName, actionName);
			bindingControls = GetBindingControls(action, compositeName);
			action.performed += Callback;
		}

		private void Callback(InputAction.CallbackContext context) {
			var controlPath = context.control.name;
			if ( context.performed && MatchesBindingControls(controlPath, bindingControls)) {
				this.done = true;
				// Debug.Log(controlPath + $"#\n{compositeName}");
			}
		}
		
		private void Cleanup() {
			if ( action is { } ) {
				action.performed -= Callback;	
			}
		}
		
///// TaskSO Overrides /////////////////////////////////////////////////////////////////////////////
		
		public override TaskType Type { get; } = TaskType.Key_Press;

		public override string BaseName { get; } = "KeyPress";

		public override void ResetTask() {
			base.ResetTask();
			Cleanup();
		}

		public override void StartTask() {
			base.StartTask();
			Setup();
		}

		public override void StopTask() {
			base.StopTask();
			Cleanup();
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////		
		
		private void OnEnable() {
			// if(inputReader is null)
			// 	return;
			//
			// actionNames = new List<InputActionInfoWrapper>();
			//
			// var gameInput = inputReader.GameInput;
			// foreach ( var inputAction in gameInput ) {
			// 	actionNames.Add(new InputActionInfoWrapper {
			// 			name = inputAction.name,
			// 			map = inputAction.actionMap.name,
			// 			id = inputAction.id.ToString(),
			// 			bindingName = GetBindingNames(inputAction.bindings)
			// 		}
			// 	);
			// }
		}
		
		private void OnValidate() {
			if(inputReader is null)
				return;
			
			//todo check if settings are correct			
		}
	}
}