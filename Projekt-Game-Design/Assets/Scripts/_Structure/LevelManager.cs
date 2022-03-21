using System;
using System.Collections;
using System.Linq;
using Events.ScriptableObjects;
// using GDP01._Gameplay.Provider;
using GDP01.SceneManagement.EventChannels;
using SaveSystem.V2.Data;
using SceneManagement.ScriptableObjects;
using SceneManagement.Types;
using UnityEngine;

namespace GDP01.Structure {
	public struct LevelManagerData {
		public LevelDataSO Level { get; set; }
	}
	
	public class LevelManager : MonoBehaviour, ISaveState<LevelManagerData> {
		public struct ConnectionData {
			public int id;
			public string name;
		}
		
		[SerializeField] private LevelDataSO startLevel;
		[SerializeField] private LevelDataSO currentLevel;
		[SerializeField] private GameSceneSO startScene;
		[SerializeField] private GameSceneSO gameScene;
		
		[Header("Send Events On")]
		[SerializeField] private SceneLoadingInfoEventChannelSO loadSceneEC;
		[SerializeField] private SceneLoadingInfoEventChannelSO unloadSceneEC;
		[SerializeField] private VoidEventChannelSO UpdateSaveDataEC;
		[SerializeField] private VoidEventChannelSO OnSceneReadyEC;
		
		[Header("Recieve Events On"), SerializeField] private IntEventChannelSO useConnectorEC;
		
		
		public LevelDataSO CurrentLevel => currentLevel;
		public string StartLevelName => startLevel.name;

		public void LoadNextLevel() {
			LevelConnectorSO exit = null;
			foreach ( var levelConnector in currentLevel.Connectors ) {
				if ( levelConnector.IsExit ) {
					exit = levelConnector;
					break;
				}
			}

			if ( exit is { } && exit.Target != null ) {
				LoadLevel(exit.Target.LevelData);
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

			if ( entrance is {} && entrance.Target != null ) {
				LoadLevel(entrance.Target.LevelData);
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
					$"---------------------------\n" +
				          $"Load Currently Loaded Level: {level}");
				//todo reload level, send load level event to scene loader and reload from there
				OnSceneReadyEC.RaiseEvent();
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

				if ( connector is { IsExit: true } && connector.Target != null ) {
					//inform all valid chars?
					
					LoadLevel(connector.Target.LevelData);
				}
			}
		}

		//todo veralgemeinern -> make generic?
		// private void LoadLevelAfterUpdateSaveData(LevelDataSO levelData) {
		// 	void LoadLevelAndUnregister() {
		// 		LoadLevel(levelData);
		// 		UpdateSaveDataEC.AfterEventRaised -= LoadLevelAndUnregister;
		// 	}
		//
		// 	UpdateSaveDataEC.AfterEventRaised += LoadLevelAndUnregister;
		// 	UpdateSaveDataEC.RaiseEvent();
		// }
		
		
		public ConnectionData GetEnteringLocationData(int connectorId) {
			LevelConnectorSO connector = currentLevel.Connectors.FirstOrDefault(c => c.Id == connectorId);
			if ( connector is { } ) {
				return new ConnectionData {
					id = connector.Target.Id,
					name = connector.Target.LevelData.name
				};	
			}
			else {
				return new ConnectionData();
			}
		}

		public bool IsCurrentLevel(string name) {
			return currentLevel.name.Equals(name);
		}
		
///// Save State ///////////////////////////////////////////////////////////////////////////////////		

		public LevelManagerData Save() {
			return new LevelManagerData { Level = currentLevel ?? startLevel };
		}

		public void Load(LevelManagerData data) {
			LoadLevel(data.Level ? data.Level : startLevel);
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////
		
		private void OnEnable() {
			//load game save -> save system
			// get current game save
			
			
			//is level loaded?
			// get current location
			// load current location

			// LoadLevel(startLevel);

			useConnectorEC.OnEventRaised += HandleUseConnector;
		}

		private void OnDisable() {
			useConnectorEC.OnEventRaised -= HandleUseConnector;
		}
	}
}