using System;
using SaveSystem;
using UnityEngine;

namespace SceneManagement {
	public class LocationLoader : MonoBehaviour {
		[Header("Sending Events On")] [SerializeField]
		private VoidEventChannelSO enableLoadingScreenInputEC;

		[SerializeField] private VoidEventChannelSO enableGampleyInputEC;

		[Header("Recieving Events On")]
		[SerializeField]
		private VoidEventChannelSO onSceneReady;
		
		[SerializeField] private bool initializeFromSave;

		// get at runtime
		private SaveManager _saveManager;

		private void Awake() {
			onSceneReady.OnEventRaised += LoadLocationData;
			_saveManager = FindObjectOfType<SaveManager>();
		}

		private void OnDestroy() {
			onSceneReady.OnEventRaised -= LoadLocationData;
		}

		// private void Start() {
		// 	if ( _saveSystem ) {
		// 		if ( initializeFromSave ) {
		// 			// try to load level from save object
		// 			_saveSystem.InitializeLevel();
		// 			enableLoadingScreenInputEC.RaiseEvent();
		// 		}
		// 		else {
		// 			//todo load empty or default??
		// 			_saveSystem.LoadLevel("");
		//
		// 			// enable on Start
		// 			enableGampleyInputEC.RaiseEvent();
		//
		// 			//todo enable ui after level loaded
		// 		}
		// 	}
		// }

		private void LoadLocationData() {
			if ( _saveManager ) {
				if ( initializeFromSave ) {
					// try to load level from save object
					_saveManager.InitializeLevel();
					
					//todo probably remove loading screen control 
					// enableLoadingScreenInputEC.RaiseEvent();
				}
				else {
					//todo load empty or default??
					// _saveManager.LoadLevel("");
					// _saveManager.InitializeLevel();
				
					// enable on Start
					enableGampleyInputEC.RaiseEvent();
				
					//todo enable ui after level loaded
				}
			}
		}
	}
}