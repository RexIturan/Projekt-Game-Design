using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Characters;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.PlayerCharacter.ScriptableObjects;
using Characters.ScriptableObjects;
using Events.ScriptableObjects;
using Grid;
using SaveLoad.ScriptableObjects;
using SaveSystem;
using SaveSystem.SaveForamts;
using UnityEngine;

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

        public void LoadLevel() {
            
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
    }
}