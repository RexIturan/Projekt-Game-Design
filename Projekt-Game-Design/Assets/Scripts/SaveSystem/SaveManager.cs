using System;
using System.Collections.Generic;
using System.IO;
using Characters;
using Events.ScriptableObjects;
using Grid;
using SaveSystem.SaveFormats;
using SaveSystem.ScriptableObjects;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using WorldObjects;

namespace SaveSystem {
	public class SaveManager : MonoBehaviour {
		
		[Header("Receiving Events On")] 
		[SerializeField] private IntEventChannelSO saveGame;
		[SerializeField] private IntEventChannelSO loadGame;
		[SerializeField] private VoidEventChannelSO saveLevel;
		[SerializeField] private VoidEventChannelSO loadLevel;
		[SerializeField] private StringEventChannelSO loadGameFromPath;

		[Header("Sending Events On")] [SerializeField]
		private VoidEventChannelSO levelLoaded;

		[Header("Data References")] 
		[SerializeField] private GridContainerSO gridContainer;
		[SerializeField] private GridDataSO globalGridData;
		public CharacterList characterList;
		public InventorySO inventory;
		public CharacterInitialiser characterInitializer;
		public WorldObjectInitialiser worldObjectInitialiser;
		[SerializeField] private ItemContainerSO itemContainerSO;


	//load level from textasset
		[Header("Test Level Data")] 
		public List<AssetReference> testLevel;
		private readonly List<Save> _saveObjects = new List<Save>();

		//settings
		//save manager state: is something loaded and so on
		public SaveManagerDataSO saveManagerData;
		[SerializeField] private readonly string defaultLevelFilename = "test_level";
		[Header("Debug Settings")] [SerializeField]
		private bool showDebugMessage;
		

//////////////////////////////////////// Local Variables ///////////////////////////////////////////

		// Save Data
		private Save _saveObject = new Save();
		private SaveWriter _saveWriter;
		private SaveReader _saveReader;

//////////////////////////////////////// Local Functions ///////////////////////////////////////////

		#region Local Functions

		// writes the current level data into the save object
		private Save WriteLevelDataToSave() {
			_saveObject.Clear();

			//todo move somewhere else
			var characters = GameObject.Find("Characters");
			if ( characters ) {
				characterList = characters.GetComponent<CharacterList>();
				_saveWriter.SetRuntimeReferences(characterList);
			} 
			
			_saveObject = _saveWriter.WirteLevelToSave();
			return _saveObject;
		}
		
		#endregion

		#region MonoBehaviour

		private void Awake() {
			saveGame.OnEventRaised += SaveGame;
			loadGame.OnEventRaised += LoadGame;
			// loadGameFromPath.OnEventRaised += LoadGridContainer;
			saveManagerData.Reset();
		}

		private void OnDestroy() {
			saveGame.OnEventRaised -= SaveGame;
			loadGame.OnEventRaised -= LoadGame;
			// loadGameFromPath.OnEventRaised -= LoadGridContainer;
		}

		private void Start() {
			// setup save Writer
			_saveWriter = new SaveWriter(gridContainer, globalGridData, inventory);

			// setup save Reader
			_saveReader = new SaveReader(gridContainer, globalGridData, inventory, itemContainerSO);
		}

		#endregion
		
//////////////////////////////////////// Public Functions //////////////////////////////////////////

		// Game

		#region Save Game

		// Save Game 
		// saves the current state of a playthrough
		// save Obejct for game  
		
		public void SaveGame(int value) {
			throw new NotImplementedException();
		}

		#endregion

		#region Load Game

		public void LoadGame(int value) {
			throw new NotImplementedException();
		}

		#endregion
		
		// Level
		
		#region Save Level
		// Save Level/Map
		// saves a seperate level
		//todo SaveObject for Level

		public bool SaveLevel(string saveName) {
			//todo refactor code duplication
			var filename = saveName;
			if ( filename == "" ) {
				filename = defaultLevelFilename;
			}
			
			// write level to save Object
			var save = WriteLevelDataToSave();
			
			// saveObject to JSON
			try {
				var saveJson = save.ToJson();
				
				// filemanager: write saveObject to savefile
				bool written = FileManager.WriteToFile(filename, saveJson);
				
				if ( written ) {
					// set savemanager flag
					//todo idk if we need this, use event instead??
					saveManagerData.saved = true;
					
					Debug.Log("SaveManager > SaveLevel > try > written\nLevel Saved");
				}
				return written;
				// return true;
			}
			catch ( Exception e ) {
				Debug.LogError($"Failed to convert the SaveObject to JSON with exception {e}");
				return false;
			}
		}

		#endregion

		#region Load Level

		public bool LoadLevel(string saveName) {
			//todo refactor code duplication
			var filename = saveName;
			if ( filename == "" ) {
				filename = defaultLevelFilename;
			}

			// exists??
			if ( !FileManager.FileExists(filename) )
				return false;

			// FileManager: load json
			var fileRead = FileManager.ReadFromFile(filename, out string saveJson);
			
			//todo save writer write Json to Save??
			try {
				// write json to save Object
				_saveObject.LoadFromJson(saveJson);

				//todo remove and manage otherwise
				// save manager state
				saveManagerData.loaded = true;
				
				//todo debug
				Debug.Log("SaveManager \u27A4 LoadLevel \u27A4 try:\nLevel Loaded");
				
				return true;
			}
			catch ( Exception e ) {
				Debug.LogError($"Failed to Load Save Object from Json {e}");
				return false;
			}

			//todo initialise level??
		}

		#endregion

		//todo v Refactor
		
		// Level Form TextAsset

		#region Load Level From TextAsset

		void LoadSaveFromTextAsset(AssetReference assetReference) {
			if ( !assetReference.RuntimeKeyIsValid() )
				return;
		}

		#endregion
		
		// todo move to level controller or so
		/// <summary> InitializeLevel:
		/// sets up all the scriptable objects that represent the runtime level
		/// uses the data from saveData, which is read in beforehand, for example in LoadTextAssetsAsSaves
		/// </summary>
		public void InitializeLevel() {
			_saveReader.SetRuntimeReferences(characterInitializer, worldObjectInitialiser);
			_saveReader.ReadSave(_saveObject);

			saveManagerData.loaded = true;
			levelLoaded.RaiseEvent();
		}

		#region Addressable Save Loading

		// todo move to filemanager or 
		public void LoadTextAssetsAsSaves(Action<string, bool, int> callback,
			List<AssetReference> assetReferences) {
			// start loading procedure
			// Assert.AreEqual(assetReferences.Count, callbacks.Count, "there should be 1 callback for each testlevel");

			Dictionary<AsyncOperationHandle<TextAsset>, int> indexDict =
				new Dictionary<AsyncOperationHandle<TextAsset>, int>();

			_saveObjects.Clear();
			for ( int i = 0; i < assetReferences.Count; i++ ) {
				var assetReference = assetReferences[i];
				if ( assetReference.RuntimeKeyIsValid() ) {
					// Debug.Log($"before {i} {assetReference.AssetGUID}");
					var operationHandle = assetReference.LoadAssetAsync<TextAsset>();
					indexDict.Add(operationHandle, i);
					operationHandle.Completed +=
						handle => {
							var index = indexDict[handle];
							// Debug.Log($"completed {index} {handle.Result.name}");
							var save = new Save();
							save.LoadFromJson(handle.Result.text);
							_saveObjects.Add(save);
							if ( handle.IsDone ) {
								callback(handle.Result.name, true, index);
							}
							else {
								callback("Could Not Load Save", false, index);
							}

							Addressables.Release(handle);
						};
				}
				else {
					callback("Could Not Load Save", false, i);
				}
			}
		}

		public List<string> GetAllTestLevelNames() {
			List<string> filenames = new List<string>();
			foreach ( var asset in testLevel ) {
				if ( asset.RuntimeKeyIsValid() ) {
					filenames.Add(asset.AssetGUID);
				}
			}

			return filenames;
		}
		
		public List<AssetReference> GetValidTestLevel() {
			List<AssetReference> validTestLevel = new List<AssetReference>();
			foreach ( var asset in testLevel ) {
				if ( asset.RuntimeKeyIsValid() ) {
					validTestLevel.Add(asset);
				}
			}

			return validTestLevel;
		}

		#endregion
		
		public void SetCurrentSaveTo(int index) {
			_saveObject = _saveObjects[index];

			// todo clear saveObjects here?
			_saveObjects.Clear();
		}

		public bool IsSaveLoaded() {
			return saveManagerData.loaded;
		}
	}
}