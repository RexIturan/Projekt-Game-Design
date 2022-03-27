using System;
using GDP01.SceneManagement.EventChannels;
using SaveSystem.V2.Data;
using SaveSystem.V2.Serializer;
using SceneManagement.ScriptableObjects;
using SceneManagement.Types;
using UnityEngine;

namespace SaveSystem.V2.Component {
	
	public class SaveSystem_V2 : MonoBehaviour {

		[SerializeField] private string directory = "save_data";
		[SerializeField] private string startLoading = "mvp";
		[SerializeField] private string startSave = "mvp_save";
		
		//todo remove
		private const string testSave = "FullSerializerTest";
		
		[Header("Recieve Events On")]
		[SerializeField] private VoidEventChannelSO OnSceneReadyEC;
		[SerializeField] private VoidEventChannelSO UpdateSaveDataEC;
		[SerializeField] private SceneLoadingInfoEventChannelSO loadSceneEC;
		
		private SaveData _saveData = null;
		private SaveData SaveData {
			get { return _saveData ??= new SaveData(); }
			set { _saveData = value; }
		}

///// Callbacks ////////////////////////////////////////////////////////////////////////////////////		
		
		private void HandleSceneLoaded() {
			//todo(vincent) must laod old levels without breaking
			if ( _saveData is { } ) {
				SaveData.LoadLevel();	
			}
			
			// _saveData.Load();
		}
		
		private void HandleUpdateSaveDataBeforeLevelLoaded(SceneLoadingData sceneLoadingData) {
			if (sceneLoadingData.UpdateSave ) {
				UpdateSaveData();
			}
		}
		
		private void HandleUpdateSaveData() {
			UpdateSaveData();
		}

///// Public Functions /////////////////////////////////////////////////////////////////////////////

		public void UpdateSaveData() {
			Debug.Log("Updating SaveData");
			SaveData.Save();
		}				
		
		[ContextMenu("Load")]
		public void Load() {
			string filename = startLoading;
			string dir = "test";

			if ( FileManager.FileExists(filename, dir) ) {
				FileManager.ReadFromFile(filename, out string json, dir);
				SaveData = (SaveData)StringSerializationAPI.Deserialize(typeof(SaveData), json);
			}
			else {
				//todo load default level
				SaveData = new SaveData();
			}
			
			//todo load objects in gamedata -> initialize from game data
			// per Object, find group -> add itself to the group
			SaveData.Load();
		}

		[ContextMenu("Save")]
		public void Save() {
			
			//todo update game data
			SaveData.Save();
			
			string json = StringSerializationAPI.Serialize(typeof(SaveData), SaveData);
			FileManager.WriteToFile(json, startSave, true, directory);
		}
		
///// Unity Functions		
		
		private void OnEnable() {
			OnSceneReadyEC.OnEventRaised += HandleSceneLoaded;
			UpdateSaveDataEC.OnEventRaised += HandleUpdateSaveData;
			
			// Load();
			
			//todo activate when first level is loaded
			loadSceneEC.BeforeLoadingRequested += HandleUpdateSaveDataBeforeLevelLoaded;
		}

		private void OnDisable() {
			//todo save?
			OnSceneReadyEC.OnEventRaised -= HandleSceneLoaded;
			UpdateSaveDataEC.OnEventRaised -= UpdateSaveData;
			loadSceneEC.BeforeLoadingRequested -= HandleUpdateSaveDataBeforeLevelLoaded;
		}
	}
}