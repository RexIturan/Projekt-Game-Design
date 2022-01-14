using System;
using SaveSystem;
using UnityEngine;

namespace SceneManagement {
	public class LocationLoader : MonoBehaviour {
		[Header("Sending Events On")] 
		[SerializeField] private VoidEventChannelSO enableLoadingScreenInputEC;
		[SerializeField] private VoidEventChannelSO fov_PlayerCharViewUpdateEC;
		[SerializeField] private VoidEventChannelSO enableGampleyInputEC;

		[Header("Recieving Events On")]
		[SerializeField] private VoidEventChannelSO onSceneReady;
		
		[Header("Initialization Settings")]
		[SerializeField] private bool initializeFromSave;
		[SerializeField] private string initializeLevelName;

///// Pivate Variables /////////////////////////////////////////////////////////////////////////////		
		// get at runtime
		private SaveManager _saveManager;

///// Pivate Function //////////////////////////////////////////////////////////////////////////////		

		

///// Unity Function ///////////////////////////////////////////////////////////////////////////////

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
					if ( !_saveManager.IsSaveLoaded() ) {
						_saveManager.LoadLevel(initializeLevelName);
					}

					//todo probably remove loading screen control 
					// enableLoadingScreenInputEC.RaiseEvent();
				}
				else {
					//todo load empty or default??
					// _saveManager.LoadLevel("");
					// _saveManager.InitializeLevel();
				
					//todo enable ui after level loaded
				}
			}
			_saveManager.InitializeLevel();
					
			// enable on Start
			enableGampleyInputEC.RaiseEvent();
			fov_PlayerCharViewUpdateEC.RaiseEvent();
		}
	}
}