using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Characters;
using Events.ScriptableObjects;
using Grid;
using SaveLoad.ScriptableObjects;
using SaveSystem;
using SaveSystem.SaveForamts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SaveLoad {
    //todo make scriptable object??
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
        [SerializeField] private string filename;
        [HideInInspector] private string fileSuffix = ".json";
        
        //todo make scriptable object
        [Header("GameSave Settings")]
        [SerializeField] private string gameSavePathBase;
        [SerializeField] private string[] gameSaveFilenames;

        [Header("Data Container")] 
        // public PlayerDataContainerSO PlayerDataContainerSo;
        // public EnemyDataContianerSO EnemyDataContianerSo;
        public ItemContainerSO itemContainerSo;
        public EquipmentInventoryContainerSO equipmentInventoryContainerSo;
        public InventorySO inventory;

        public CharacterInitialiser characterInitialiser;

        [Header("Test Level Data")] 
        public List<AssetReference> testLevel;

        private Dictionary<AssetReference, AsyncOperationHandle> operationHandles =
            new Dictionary<AssetReference, AsyncOperationHandle>();
        List<Save> saveObjects = new List<Save>();

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
            SaveGridContainer(pathBase, filename);
        }
        
        //TODO return bool if successful
        public void SaveGridContainer(string pathBase, string filename) {
            characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
            
            // var json = JsonUtility.ToJson(gridContainer);
            var path = pathBase + filename + fileSuffix;

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
                saveData.enemys.Add(new Enemy_Save() {
                    enemyTypeId = enemySC.enemyType.id,
                    enemySpawnDataId = enemySC.enemySpawnData.id,
                    pos = enemySC.gridPosition
                });                 
            }
            foreach (var grids in gridContainer.tileGrids) {
                saveData.grid_Save.Add(grids);
            }
            saveData.GridDataSave.SetValues(globalGridData);

            saveData.inventory.size = inventory.Inventory.Capacity;
            foreach (var item in inventory.Inventory) {
                saveData.inventory.itemIds.Add(item.id);    
            }

            foreach (var equipment in equipmentInventoryContainerSo.Inventorys) {

                var equiped = new List<int>();
                foreach (var item in equipment.Inventory) {
                    equiped.Add(item.id);
                }
                
                saveData.EquipmentInventory.Add(new Inventory_Save() {
                    size = equipment.Inventory.Capacity,
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
            var path = pathBase + filename + fileSuffix;
            LoadSaveDataFromDisk(path);
        }

        void LoadGridContainer(string path) {
            LoadSaveDataFromDisk(path);
        }
        
        public bool LoadGridContainer(string pathBase, string filename) {
            var path = pathBase + filename + fileSuffix;
            return LoadSaveDataFromDisk(path);
        }
        
        public bool LoadSaveDataFromDisk(string path) {

            Debug.Log($"Hi {path}");

            var json = "";
            // json = jsonTextFile.text;
            
            // json = File.ReadAllText(path);
            // saveData.LoadFromJson(json);
            
            using (var fs = new FileStream(path, FileMode.Open)) {
                using (var reader = new StreamReader(fs)) {
                    json = reader.ReadToEnd();
                    saveData.LoadFromJson(json);
                    Debug.Log($"Load JSON from {path} \n{json}");
                    return true;
                }
            }
            return true;
            // if (FileManager.LoadFromFile(path, out var json))
            // {
            //     saveData.LoadFromJson(json);
            //     if (showDebugMessage) {
            //         Debug.Log($"Load JSON from {path} \n{json}");
            //     }
            //     saveManagerData.loaded = true;
            //     return true;
            // }

            // return false;
            //
            // using (var fs = new FileStream(path, FileMode.Open)) {
            //     using (var reader = new StreamReader(fs)) {
            //         json = reader.ReadToEnd();
            //     }
            // }
            //
            // if (showDebugMessage) {
            //     Debug.Log($"Load JSON from {path} \n{json}");
            // }
            //
            // JsonUtility.FromJsonOverwrite(json, gridContainer);
            //
            // globalGridData.Width = gridContainer.tileGrids[0].Width;
            // globalGridData.Height = gridContainer.tileGrids[0].Height;
            // globalGridData.cellSize = gridContainer.tileGrids[0].CellSize;
            // globalGridData.OriginPosition = gridContainer.tileGrids[0].OriginPosition;
            // // JsonUtility.FromJson<GridContainerSO>(json);
            // levelLoaded.RaiseEvent();
            //
            // saveManagerData.loaded = true;
        }

        public void InitializeLevel() {
            
            // globalGridData.Width = gridContainer.tileGrids[0].Width;
            // globalGridData.Height = gridContainer.tileGrids[0].Height;
            // globalGridData.cellSize = gridContainer.tileGrids[0].CellSize;
            // globalGridData.OriginPosition = gridContainer.tileGrids[0].OriginPosition;
            // // JsonUtility.FromJson<GridContainerSO>(json);

            gridContainer.tileGrids = new List<TileGrid>();
            gridContainer.tileGrids.AddRange(saveData.grid_Save);
            
            saveData.GridDataSave.GetValues(globalGridData);

            characterInitialiser.Initialise(saveData.players, saveData.enemys);

            inventory.Inventory.Clear();
            foreach (var ids in saveData.inventory.itemIds) {
                inventory.Inventory.Add(itemContainerSo.ItemList[ids]);    
            }
            
            foreach (var equipmentInventory in saveData.EquipmentInventory) {
                var equip = new List<ItemSO>();
                foreach (var ids in equipmentInventory.itemIds) {
                    equip.Add(itemContainerSo.ItemList[ids]);
                }

                var eInventory = ScriptableObject.CreateInstance<EquipmentInventorySO>();
                eInventory.Inventory = equip;
                equipmentInventoryContainerSo.Inventorys.Add(eInventory);
            }
            
            saveManagerData.loaded = true;
            levelLoaded.RaiseEvent();
        }

        public void LoadTextAssetsAsSaves(Action<string, bool, int> callback, List<AssetReference> assetReferences) {
            // start loading procedure
            // Assert.AreEqual(assetReferences.Count, callbacks.Count, "there should be 1 callback for each testlevel");

            Dictionary<AsyncOperationHandle<TextAsset>, int> indexDict =
                new Dictionary<AsyncOperationHandle<TextAsset>, int>();

            saveObjects.Clear();
            for (int i = 0; i < assetReferences.Count; i++) {
                
                var assetReference = assetReferences[i];
                if (assetReference.RuntimeKeyIsValid()) {
                    Debug.Log($"before {i} {assetReference.AssetGUID}");
                    var operationHandle = assetReference.LoadAssetAsync<TextAsset>();
                    indexDict.Add(operationHandle, i);
                    operationHandle.Completed += //(handle) => HandleTextAssetLoaded(handle, index, callbacks);
                        handle => {
                            var index = indexDict[handle];
                            Debug.Log($"completed {index} {handle.Result.name}");
                            var save = new Save();
                            save.LoadFromJson(handle.Result.text);
                            saveObjects.Add(save);
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

        void HandleTextAssetLoaded(AsyncOperationHandle<TextAsset> handle, int index, List<Action<string, bool, int>> callbacks) {
            
            Debug.Log($"loadTextAsset  {index}");
            var save = new Save();
            save.LoadFromJson(handle.Result.text);
            saveObjects.Add(save);
            index = saveObjects.Count-1;
            if (handle.IsDone) {
                callbacks[index](handle.Result.name, true, index);    
            }
            else {
                callbacks[index]("Could Not Load Save", false, index);
            }
            Addressables.Release(handle);
        }
        
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
            saveData = saveObjects[index];
            
            // todo clear saveObjects here?
            saveObjects.Clear();
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
