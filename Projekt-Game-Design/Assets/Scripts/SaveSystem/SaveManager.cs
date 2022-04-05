using System;
using System.Collections.Generic;
using Characters;
using Characters.Equipment.ScriptableObjects;
using Events.ScriptableObjects;
using GDP01.SceneManagement.EventChannels;
using GDP01.Structure;
using Grid;
using QuestSystem.ScriptabelObjects;
using SaveSystem.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using SceneManagement.Types;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using WorldObjects;

namespace SaveSystem {
	public class SaveManager : MonoBehaviour {
		
		[Header("Receiving Events On")] 
		[SerializeField] private IntEventChannelSO saveGameEC;
		[SerializeField] private IntEventChannelSO loadGameEC;
		[SerializeField] private VoidEventChannelSO saveLevelEC;
		[SerializeField] private VoidEventChannelSO loadLevelEC;
		[SerializeField] private StringEventChannelSO loadGameFromPathEC;

		[Header("Sending Events On")] 
		[SerializeField] private VoidEventChannelSO levelLoaded;
		[SerializeField] private SceneLoadingInfoEventChannelSO loadSceneEC;
		[SerializeField] private SceneLoadingInfoEventChannelSO unloadSceneEC;

		[Header("Data References")] 
		[SerializeField] private GridDataSO gridData;
		public InventorySO inventory;
		public QuestContainerSO questContainer;
		public CharacterInitialiser characterInitializer;
		public WorldObjectInitialiser worldObjectInitialiser;
		[SerializeField] private ItemTypeContainerSO itemTypeContainerSO;
		[SerializeField] private EquipmentContainerSO equipmentContainer;

	//load level from textasset
		[Header("Test Level Data")] 
		public List<AssetReference> testLevel;
		private readonly List<Save> _saveObjects = new List<Save>();

		//settings
		//save manager state: is something loaded and so on
		public SaveManagerDataSO saveManagerData;
		[SerializeField] private string defaultLevelFilename = "test_level";
		[SerializeField] private string defaultGameFilename = "tutorial";
		
		[SerializeField] private LevelDataSO prototyLevel;
		[SerializeField] private LevelDataSO prototyBoss;
		[SerializeField] private GameSceneSO gameplayScene;
		[SerializeField] private GameSceneSO mainMenuSceneData;
		
		[Header("Sending Events On")]
		[SerializeField] private LoadEventChannelSO loadLocation;
		[Header("Location Scene To Load")]
		[SerializeField] private GameSceneSO[] locationsToLoad;
		
		[Header("Debug Settings")] [SerializeField]
		private bool showDebugMessage;
		

//////////////////////////////////////// Local Variables ///////////////////////////////////////////

		// Save Data
		private Save _saveObject = new Save();
		private SaveWriter _saveWriter;
		private SaveReader _saveReader;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		public SaveReader SaveReader => _saveReader ??= new SaveReader(
			gridData, inventory, questContainer, itemTypeContainerSO, equipmentContainer);
		
//////////////////////////////////////// Local Functions ///////////////////////////////////////////

		#region Local Functions

		// writes the current level data into the save object
		private Save WriteLevelDataToSave() {
			_saveObject.Clear();
						
			WorldObjectList worldObjectList = WorldObjectList.FindInstant();

			_saveWriter.SetRuntimeReferences();
			
			_saveObject = _saveWriter.WirteLevelToSave();
			return _saveObject;
		}
		
		#endregion

		#region MonoBehaviour

		private void Awake() {
			saveGameEC.OnEventRaised += SaveGameEC;
			loadGameEC.OnEventRaised += LoadGameEC;
			// loadGameFromPath.OnEventRaised += LoadGridContainer;
			saveLevelEC.OnEventRaised += HandleSaveLevel;
			loadLevelEC.OnEventRaised += HandleLoadLevel;
			
			saveManagerData.Reset();
		}

		private void OnDestroy() {
			saveGameEC.OnEventRaised -= SaveGameEC;
			loadGameEC.OnEventRaised -= LoadGameEC;
			// loadGameFromPath.OnEventRaised -= LoadGridContainer;
		}

		private void Start() {
			// setup save Writer
			_saveWriter = new SaveWriter(gridData, inventory, equipmentContainer, questContainer);

			// setup save Reader
			_saveReader = new SaveReader(gridData, inventory, questContainer, itemTypeContainerSO, equipmentContainer);
		}

		#endregion

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void HandleLoadLevel() {
			LoadLevel("");
			InitializeLevel();
		}

		private void HandleSaveLevel() {
			SaveLevel("");
		}
		
//////////////////////////////////////// Public Functions //////////////////////////////////////////

		// Game

		#region Save Game

		// Save Game 
		// saves the current state of a playthrough
		// save Obejct for game  
		
		public void SaveGameEC(int value) {
			//todo fix
			String checkpointFile = defaultGameFilename;
			checkpointFile += "0";
			
			SaveLevelWithOverride(checkpointFile);
		}

		#endregion

		#region Load Game

		public void LoadGameEC(int value) {

			var filename = defaultGameFilename;
			bool loaded;
			bool newSceneLoading = false;
			SceneLoadingData loadingData = new SceneLoadingData {
				Dependencies = new List<SceneLoadingData.SceneDataDependencySettings> {
					new SceneLoadingData.SceneDataDependencySettings{ reset = false, sceneData = gameplayScene }
				},
				UpdateSave = false,
				ActivateOnLoad = true,
			};

			switch ( value ) {
				case 0:
					//load prototyp level
					filename = prototyLevel.LevelName;
					loadingData.MainSceneData = prototyLevel.GameScene;
					newSceneLoading = true;
					break;
				case 1:
					//load prototyp level
					filename = prototyBoss.LevelName;
					loadingData.MainSceneData = prototyBoss.GameScene;
					newSceneLoading = true;
					break;
				default:
					// String checkpointFile = defaultGameFilename;
					// checkpointFile += value > 0 ? "0" : "";
					break;
			}
			
			
			try {
				loaded = LoadLevel(filename);
			
				if ( loaded && newSceneLoading ) {

					loadSceneEC.RaiseEvent(loadingData);
					// unloadSceneEC.RaiseEvent(unloadMenuLoadingData);

				} else if ( loaded ) {
					loadLocation.RaiseEvent(locationsToLoad, false, true);
					// saveManagerData.inputLoad = true;
					// saveManagerData.loaded = true;
				} 
			}
			catch ( Exception e ) {
				// Debug.Log($"Could not load level: {filename}, with Exeption: {e}");
				//todo does this work?
				Console.WriteLine($"Could not load level: {filename}, with Exeption: {e}");
				throw;
			}
		}

		#endregion
		
		// Level
		
		#region Save Level
		// Save Level/Map
		// saves a seperate level
		//todo SaveObject for Level

		public bool SaveLevelWithOverride(string saveName) {
			return SaveLevel(saveName, true); 
		}
		
		public bool SaveLevel(string saveName, bool overrideFile = false) {
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
				bool written = FileManager.WriteToFile(saveJson, filename, overrideFile);
				
				if ( written ) {
					// set savemanager flag
					//todo idk if we need this, use event instead??
					// saveManagerData.saved = true;
					
					Debug.Log($"SaveManager > SaveLevel > try > written\nLevel Saved{filename}");
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
				_saveObject.FileName = filename;

				//todo remove and manage otherwise
				// save manager state
				// saveManagerData.loaded = true;
				
				//todo debug
				// Debug.Log($"SaveManager \u27A4 LoadLevel \u27A4 try:\n\"{filename}\" Level Loaded");
				
				return true;
			}
			catch ( Exception e ) {
				Debug.LogError($"Failed to Load Save Object from Json \"{filename}\", with Exception: {e}");
				return false;
			}

			//todo initialise level??
		}

		[ContextMenu("LoadGridData")]
		private void LoadGridData() {
			LoadLevel("");
			SaveReader.ReadGridDataFromSave(_saveObject, gridData);
		}
		
		#endregion

		//todo v Refactor
		
		// Level Form TextAsset

		#region Load Level From TextAsset

		//todo does nothing
		private void LoadSaveFromTextAsset(AssetReference assetReference) {
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
			SaveReader.SetRuntimeReferences(characterInitializer, worldObjectInitialiser);
			SaveReader.ReadSave(_saveObject);

			// saveManagerData.loaded = true;
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

		//todo remove
		public bool IsSaveLoaded() {
			// throw new NotImplementedException();
			return true;
		}
	}
}