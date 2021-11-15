using System;
using System.Linq;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace UI.Help {
	public class HelpUpdater : MonoBehaviour {
		
		[SerializeField] private InputReader inputReader;
		
		[Header("Recieving Event On")]
		[SerializeField] private StringEventChannelSO setHelpTextEC;

		private void Start() {
			string str = "";

			str += "Level Editor\n";
			str += GetInputMappingFromActionMap(inputReader.GameInput.LevelEditor.Get().actions);
		
			// str += "\nCamera\n";
			// str += GetInputMappingFromActionMap(inputReader.GameInput.Camera.Get().actions);
			
			setHelpTextEC.RaiseEvent(str);
		}

		private string GetInputMappingFromActionMap(ReadOnlyArray<InputAction> inputActions) {
			string str = "";

			foreach ( var inputAction in inputActions ) {
				str += inputAction.name + ": ";
				if ( inputAction.bindings.Count > 0 ) {
					if ( inputAction.controls.Count > 0 ) {
						foreach ( var control in inputAction.controls ) {
							str += control.name + ", ";	
						}
						str += "\n";
					}
				}
				else {
					str += "\n";
				}
			}

			return str;
		}
	}
}