using System;
using UnityEngine;

namespace GDP01.Statemachine.Imp.Statemachine.GameState {
	public class ExitGameListener : MonoBehaviour {
		[SerializeField] private VoidEventChannelSO ExitGameEC;

		private void HandleExitGame() {
			Application.Quit();
		}
		
		private void OnEnable() {
			ExitGameEC.OnEventRaised += HandleExitGame;
		}

		private void OnDisable() {
			ExitGameEC.OnEventRaised -= HandleExitGame;
		}
	}
}