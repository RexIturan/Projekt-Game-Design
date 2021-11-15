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

namespace SaveSystem {
    public class SaveManager : MonoBehaviour {
        [Header("Receiving Events On")]
        [SerializeField] private IntEventChannelSO saveGame;
        [SerializeField] private IntEventChannelSO loadGame;
        [SerializeField] private VoidEventChannelSO saveLevel;
        [SerializeField] private VoidEventChannelSO loadLevel;
        [SerializeField] private StringEventChannelSO loadGameFromPath;
        
        [Header("Sending Events On")]
        [SerializeField] private VoidEventChannelSO levelLoaded;
        
        [Header("Data References")]
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO globalGridData;
        public CharacterList characterList;
        public SaveManagerDataSO saveManagerData;
        
        [Header("Save/Load Settings")]
        [SerializeField] private string pathBase;
        [SerializeField] private string defaultFilename;
        private const string FileSuffix = ".json";

        //todo make scriptable object
        [Header("GameSave Settings")]
        [SerializeField] private string gameSavePathBase;
        [SerializeField] private string[] gameSaveFilenames;

        [Header("Data Container")] 
        // public PlayerDataContainerSO PlayerDataContainerSo;
        // public EnemyDataContainerSO EnemyDataContainerSo;
        public ItemContainerSO itemContainerSo;
        public EquipmentInventoryContainerSO equipmentContainer;
        public InventorySO inventory;

        public CharacterInitialiser characterInitializer;

        [Header("Test Level Data")] 
        public List<AssetReference> testLevel;

        private readonly List<Save> _saveObjects = new List<Save>();

        [Header("Debug Settings")]
        [SerializeField] private bool showDebugMessage;

//////////////////////////////////////// Local Variables ///////////////////////////////////////////
        
		// Save Data
        private Save _saveData = new Save();
        private SaveWriter _saveWriter;
        private SaveReader _saveReader;


//////////////////////////////////////// Local Functions ///////////////////////////////////////////
        
        #region Local Functions

        private Save UpdateSaveData() {
	        _saveData.Clear();
	        _saveWriter.SetRuntimeReferences(characterList);
	        _saveData = _saveWriter.WirteLevelToSave();
	        return _saveData;
        }

        #endregion
        
//////////////////////////////////////// Public Functions //////////////////////////////////////////
		
        private void Awake() {
            saveLevel.OnEventRaised += SaveGridContainer;
            loadLevel.OnEventRaised += LoadGridContainer;
            saveGame.OnEventRaised += SaveGame;
            loadGame.OnEventRaised += LoadGame;
            // loadGameFromPath.OnEventRaised += LoadGridContainer;
            saveManagerData.Reset();
        }

        private void OnDestroy() {
	        saveLevel.OnEventRaised -= SaveGridContainer;
	        loadLevel.OnEventRaised -= LoadGridContainer;
	        saveGame.OnEventRaised -= SaveGame;
	        loadGame.OnEventRaised -= LoadGame;
	        // loadGameFromPath.OnEventRaised -= LoadGridContainer;
        }

        private void Start() {

	        // setup save Writer
	        _saveWriter = new SaveWriter(gridContainer, globalGridData, inventory, equipmentContainer);

	        // setup save Reader
	        _saveReader = new SaveReader(gridContainer, globalGridData, inventory,
		        equipmentContainer);
	        
            if (gameSaveFilenames == null || gameSaveFilenames.Length < 3) {
                gameSaveFilenames = new string[3];
                for (int i = 0; i < gameSaveFilenames.Length; i++) {
                    gameSaveFilenames[i] = $"savefile_{i}";
                }    
            }
        }

        public void SaveGame(int value) {
            if (value >= 0 && value < gameSaveFilenames.Length) {
                SaveGridContainer(gameSavePathBase, gameSaveFilenames[value]);    
            }
            else {
	            Debug.LogError("SaveGame(int value)");
            }
        }

        public void LoadGame(int value) {
            if (value >= 0 && value < gameSaveFilenames.Length) {
                LoadGridContainer(gameSavePathBase, gameSaveFilenames[value]);    
            }
            else {
	            Debug.LogError("LoadGame(int value)");
            }
        }

        public void SaveGridContainer() {
            SaveGridContainer(pathBase, defaultFilename);
        }
        
        private bool SaveGridContainer(string directoryPath, string filename) {
            characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
            
            // var json = JsonUtility.ToJson(gridContainer);
            var path = $"{directoryPath}{filename}{FileSuffix}";

            //fill saveData
            // buildup saveData by reading from all scriptable objects, and converting them into save objects
            Save save = UpdateSaveData();

            if ( save == null ) {
	            Debug.LogError("SaveGridContainer: save is null");
            }
            
            bool saveWritten = FileManager.WriteToFile(path, save.ToJson()); 
            if ( saveWritten ) {
	            // set savemanager flag
	            //todo idk if we need this, use event instead??
	            saveManagerData.saved = true;    
            }
            else {
	            Debug.LogError("SaveGridContainer: couldn't save");
            }
            
            // var json = _saveData.ToJson();
            //
            // if (showDebugMessage) {
            //     // TODO Debug Macro
            //     Debug.Log($"Save Test GridContainer to JSON at {path} \n{json}");    
            // }
            //
            // using (var fs = new FileStream(path, FileMode.Create)) {
            //     using (var writer = new StreamWriter(fs)) {
            //         writer.Write(json);
            //     }
            // }
            //
            return saveWritten;
        }

        public void LoadGridContainer() {
            var path = pathBase + defaultFilename + FileSuffix;
            LoadGridContainer(path);
        }

        public bool LoadGridContainer(string directoryPath, string filename) {
            string path = $"{directoryPath}{filename}{FileSuffix}";
            return LoadGridContainer(path);
        }
        
        private bool LoadGridContainer(string path) {
	        return LoadSaveDataFromDisk(path);
        }
        
        public bool LoadSaveDataFromDisk(string path) {
	        using (var fs = new FileStream(path, FileMode.Open)) {
                using (var reader = new StreamReader(fs)) {
                    var json = reader.ReadToEnd();
                    _saveData.LoadFromJson(json);
                    Debug.Log($"Load JSON from {path} \n{json}");
                    return true;
                }
            }
        }

        /// <summary> InitializeLevel:
        /// sets up all the scriptable objects that represent the runtime level
        /// uses the data from saveData, which is read in beforehand, for example in LoadTextAssetsAsSaves
        /// </summary>
        public void InitializeLevel() {

	        _saveReader.SetRuntimeReferences(characterInitializer);
	        _saveReader.ReadSave(_saveData);
            
            saveManagerData.loaded = true;
            levelLoaded.RaiseEvent();
        }

        public void LoadTextAssetsAsSaves(Action<string, bool, int> callback, List<AssetReference> assetReferences) {
            // start loading procedure
            // Assert.AreEqual(assetReferences.Count, callbacks.Count, "there should be 1 callback for each testlevel");

            Dictionary<AsyncOperationHandle<TextAsset>, int> indexDict =
                new Dictionary<AsyncOperationHandle<TextAsset>, int>();

            _saveObjects.Clear();
            for (int i = 0; i < assetReferences.Count; i++) {
                
                var assetReference = assetReferences[i];
                if (assetReference.RuntimeKeyIsValid()) {
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
                            if (handle.IsDone) {
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

        void LoadSaveFromTextAsset(AssetReference assetReference) {
	        if(!assetReference.RuntimeKeyIsValid())
		        return;
	        
	        
        }

        public List<string> GetAllTestLevelNames() {
            List<string> filenames = new List<string>();
            foreach (var asset in testLevel) {
                if (asset.RuntimeKeyIsValid()) {
                    filenames.Add(asset.AssetGUID);    
                }
            }
            return filenames;
        }

        public void SetCurrentSaveTo(int index) {
            _saveData = _saveObjects[index];
            
            // todo clear saveObjects here?
            _saveObjects.Clear();
        }

        public List<AssetReference> GetValidTestLevel() {
            List<AssetReference> validTestLevel = new List<AssetReference>();
            foreach (var asset in testLevel) {
                if (asset.RuntimeKeyIsValid()) {
                    validTestLevel.Add(asset);    
                }
            }
            return validTestLevel;
        }
    }
}
