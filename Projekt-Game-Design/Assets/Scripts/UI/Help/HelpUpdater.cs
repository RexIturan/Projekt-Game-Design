using System;
using System.Linq;
using Events.ScriptableObjects;
using Input;
using UnityEngine;

namespace UI.Help {
	public class HelpUpdater : MonoBehaviour {
		
		[SerializeField] private InputReader inputReader;
		
		[Header("Recieving Event On")]
		[SerializeField] private StringEventChannelSO setHelpTextEC;

		private void Start() {
			string str = "";

			var inputs = inputReader.GameInput.LevelEditor.Get().ToArray();

			foreach ( var inputAction in inputs ) {
				str += inputAction.name + ": ";
				if ( inputAction.bindings.Count > 0 ) {
					if ( inputAction.controls.Count > 0 ) {
						foreach ( var control in inputAction.controls ) {
							str += control.name + "\n";	
						}
					}
				}
				else {
					str += "\n";
				}
			}
			
			setHelpTextEC.RaiseEvent(str);
		}
	}
}