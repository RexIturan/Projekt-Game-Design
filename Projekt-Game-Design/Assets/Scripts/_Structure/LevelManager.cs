using Events.ScriptableObjects;
using GDP01.SceneManagement.EventChannels;
using SceneManagement.ScriptableObjects;
using SceneManagement.Types;
using UnityEngine;

namespace GDP01.Structure {
	public class LevelManager : MonoBehaviour {
		[SerializeField] private LevelDataSO startLevel;
		[SerializeField] private LevelDataSO currentLevel;
		[SerializeField] private GameSceneSO startScene;
		[SerializeField] private GameSceneSO gameScene;
		
		[SerializeField] private SceneLoadingInfoEventChannelSO loadSceneEC;
		[SerializeField] private SceneLoadingInfoEventChannelSO unloadSceneEC;

		[SerializeField] private IntEventChannelSO useConnectorEC;
		// [SerializeField] private 

		public void LoadNextLevel() {
			LevelConnectorSO exit = null;
			foreach ( var levelConnector in currentLevel.Connectors ) {
				if ( levelConnector.IsExit ) {
					exit = levelConnector;
					break;
				}
			}

			if ( exit is {} && exit.Targets.Length > 0 ) {
				LoadLevel(exit.Targets[0].LevelData);
			}
		}

		public void LaodLastLevel() {
			LevelConnectorSO entrance = null;
			foreach ( var levelConnector in currentLevel.Connectors ) {
				if ( levelConnector.IsEntrance ) {
					entrance = levelConnector;
					break;
				}
			}

			if ( entrance is {} && entrance.Targets.Length > 0 ) {
				LoadLevel(entrance.Targets[0].LevelData);
			}
		}

		public void LoadStartScene() {
			var loadingData = new SceneLoadingData {
				MainSceneData = startScene,
				ActivateOnLoad = true,
				DontUnload = false
			};
			
			loadSceneEC.RaiseEvent(loadingData);
			
			var unloadData = new SceneLoadingData {
				MainSceneData = gameScene,
				ActivateOnLoad = true,
				DontUnload = false
			}; 
			unloadSceneEC.RaiseEvent(unloadData);
		}

		private void LoadLevel(LevelDataSO level) {

			if ( currentLevel == level ) {
				Debug.Log(
					"---------------------------\n" +
				          "Load Currently Loaded Level");
				return;
			}
			
			var sceneLoadingData = new SceneLoadingData {
				MainSceneData = level.Scene,
				ActivateOnLoad = true,
				DontUnload = false
			};
			
			loadSceneEC.RaiseEvent(sceneLoadingData);
			MoveToLevel(level);
		}
		
		private void MoveToLevel(LevelDataSO level) {
			currentLevel = level;
		}
		
		private void HandleUseConnector(int id) {
			if ( currentLevel is { } ) {
				LevelConnectorSO connector = currentLevel.Connectors.Find(so => so.Id == id);

				if ( connector is { IsExit: true } && connector.Targets.Length > 0 ) {
					LoadLevel(connector.Targets[0].LevelData);
				}
			}
		}
		
		private void OnEnable() {
			//load game save -> save system
			// get current game save
			
			
			//is level loaded?
			// get current location
			// load current location

			LoadLevel(startLevel);

			useConnectorEC.OnEventRaised += HandleUseConnector;
		}

		private void OnDisable() {
			useConnectorEC.OnEventRaised -= HandleUseConnector;
		}
	}
}