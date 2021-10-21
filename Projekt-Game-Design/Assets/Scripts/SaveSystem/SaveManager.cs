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
        public EquipmentInventoryContainerSO equipmentInventoryContainerSo;
        public InventorySO inventory;

        public CharacterInitialiser characterInitializer;

        [Header("Test Level Data")] 
        public List<AssetReference> testLevel;

        private readonly List<Save> _saveObjects = new List<Save>();

        [Header("Debug Settings")]
        [SerializeField] private bool showDebugMessage;

        public Save saveData = new Save();
        
        private void Awake() {
            saveLevel.OnEventRaised += SaveGridContainer;
            loadLevel.OnEventRaised += LoadGridContainer;
            saveGame.OnEventRaised += SaveGame;
            loadGame.OnEventRaised += LoadGame;
            loadGameFromPath.OnEventRaised += LoadGridContainer;
            saveManagerData.Reset();
        }

        private void Start() {
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
                //TODO Error
            }
        }

        public void LoadGame(int value) {
            if (value >= 0 && value < gameSaveFilenames.Length) {
                LoadGridContainer(gameSavePathBase, gameSaveFilenames[value]);    
            }
            else {
                //TODO Error
            }
        }

        public void SaveGridContainer() {
            SaveGridContainer(pathBase, defaultFilename);
        }
        
        //TODO return bool if successful
        public void SaveGridContainer(string directoryPath, string filename) {
            characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
            
            // var json = JsonUtility.ToJson(gridContainer);
            var path = $"{directoryPath}{filename}{FileSuffix}";

            //fill saveData

        #region FillSaveData

            saveData.Clear();
            //get all players,
            foreach (var player in characterList.playerContainer) {
                var playerCharacterSc = player.GetComponent<PlayerCharacterSC>();
                saveData.players.Add(
                    new PC_Save() {
                        plyerTypeId = playerCharacterSc.playerType.id,
                        plyerSpawnDataId = playerCharacterSc.playerSpawnData.id,
                        pos = playerCharacterSc.gridPosition
                    });                 
            }
            foreach (var enemy in characterList.enemyContainer) {
                var enemySC = enemy.GetComponent<EnemyCharacterSC>();
                saveData.enemies.Add(new Enemy_Save() {
                    enemyTypeId = enemySC.enemyType.id,
                    enemySpawnDataId = enemySC.enemySpawnData.id,
                    pos = enemySC.gridPosition
                });                 
            }
            foreach (var grids in gridContainer.tileGrids) {
                saveData.gridSave.Add(grids);
            }
            saveData.gridDataSave.SetValues(globalGridData);

            saveData.inventory.size = inventory.inventory.Capacity;
            foreach (var item in inventory.inventory) {
                saveData.inventory.itemIds.Add(item.id);    
            }

            foreach (var equipment in equipmentInventoryContainerSo.inventories) {

                var equiped = new List<int>();
                foreach (var item in equipment.inventory) {
                    equiped.Add(item.id);
                }
                
                saveData.equipmentInventory.Add(new Inventory_Save() {
                    size = equipment.inventory.Capacity,
                    itemIds = equiped
                });    
            }
            #endregion    
            
            var json = saveData.ToJson();

            if (showDebugMessage) {
                // TODO Debug Macro
                Debug.Log($"Save Test GridContainer to JSON at {path} \n{json}");    
            }
            
            using (var fs = new FileStream(path, FileMode.Create)) {
                using (var writer = new StreamWriter(fs)) {
                    writer.Write(json);
                }
            }
            
            saveManagerData.saved = true;
        }

        public void LoadGridContainer() {
            var path = pathBase + defaultFilename + FileSuffix;
            LoadSaveDataFromDisk(path);
        }

        void LoadGridContainer(string path) {
            LoadSaveDataFromDisk(path);
        }
        
        public bool LoadGridContainer(string directoryPath, string filename) {
            var path = $"{directoryPath}{filename}{FileSuffix}";
            return LoadSaveDataFromDisk(path);
        }
        
        public bool LoadSaveDataFromDisk(string path) {
	        using (var fs = new FileStream(path, FileMode.Open)) {
                using (var reader = new StreamReader(fs)) {
                    var json = reader.ReadToEnd();
                    saveData.LoadFromJson(json);
                    Debug.Log($"Load JSON from {path} \n{json}");
                    return true;
                }
            }
        }

        public void InitializeLevel() {
            
            // globalGridData.Width = gridContainer.tileGrids[0].Width;
            // globalGridData.Height = gridContainer.tileGrids[0].Height;
            // globalGridData.cellSize = gridContainer.tileGrids[0].CellSize;
            // globalGridData.OriginPosition = gridContainer.tileGrids[0].OriginPosition;
            // // JsonUtility.FromJson<GridContainerSO>(json);

            gridContainer.tileGrids = new List<TileGrid>();
            gridContainer.tileGrids.AddRange(saveData.gridSave);
            
            saveData.gridDataSave.GetValues(globalGridData);

            characterInitializer.Initialise(saveData.players, saveData.enemies);

            inventory.inventory.Clear();
            foreach (var ids in saveData.inventory.itemIds) {
                inventory.inventory.Add(itemContainerSo.itemList[ids]);    
            }
            
            foreach (var equipmentInventory in saveData.equipmentInventory) {
                var equip = new List<ItemSO>();
                foreach (var ids in equipmentInventory.itemIds) {
                    equip.Add(itemContainerSo.itemList[ids]);
                }

                var eInventory = ScriptableObject.CreateInstance<EquipmentInventorySO>();
                eInventory.inventory = equip;
                equipmentInventoryContainerSo.inventories.Add(eInventory);
            }
            
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
                    operationHandle.Completed += //(handle) => HandleTextAssetLoaded(handle, index, callbacks);
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

        // void HandleTextAssetLoaded(AsyncOperationHandle<TextAsset> handle, int index, List<Action<string, bool, int>> callbacks) {
        //     
        //     // Debug.Log($"loadTextAsset  {index}");
        //     var save = new Save();
        //     save.LoadFromJson(handle.Result.text);
        //     _saveObjects.Add(save);
        //     index = _saveObjects.Count-1;
        //     if (handle.IsDone) {
        //         callbacks[index](handle.Result.name, true, index);    
        //     }
        //     else {
        //         callbacks[index]("Could Not Load Save", false, index);
        //     }
        //     Addressables.Release(handle);
        // }
        
        // public async Task<bool> LoadLevelFromAssetReferenc(AssetReference assetReference) {
        //     bool result = false;
        //     if (assetReference.RuntimeKeyIsValid()) {
        //         var handle = assetReference.LoadAssetAsync<TextAsset>();
        //         await Task.WhenAll(handle.Task);
        //         handle.Completed += operationHandle => {
        //             operationHandles.Add(assetReference, operationHandle);
        //         };
        //         if (handle.IsDone) {
        //             result = true;
        //         }
        //     }
        //
        //     return result;
        // }
        
        // public IEnumerable<AsyncOperationHandle<TextAsset>> LoadTestLevel() {
        //     foreach (var level in testLevel) {
        //         if (level.RuntimeKeyIsValid()) {
        //             var handle = level.LoadAssetAsync<TextAsset>();
        //             handle.Completed += operationHandle => {
        //                 operationHandles.Add(level, operationHandle);
        //             };
        //             yield return handle;
        //         }
        //     }
        // }

        // public async Task<List<string>> GetAllTestSaveNames() {
        //     List<string> filenames = new List<string>();
        //
        //     foreach (AsyncOperationHandle<TextAsset> operationHandle in LoadTestLevel()) {
        //         await Task.WhenAll(operationHandle.Task);
        //         Debug.Log(operationHandle.Result.name);
        //         filenames.Add(operationHandle.Result.name);
        //     }
        //     
        //     return filenames;
        // }

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
            saveData = _saveObjects[index];
            
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
