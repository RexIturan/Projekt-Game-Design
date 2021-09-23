using System;
using System.IO;
using System.Linq;
using Events.ScriptableObjects;
using Grid;
using UnityEngine;

namespace SaveLoad {
    //todo make scriptable object??
    public class SaveManager : MonoBehaviour {
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO globalGridData;

        [Header("Save/Load Settings")]
        [SerializeField] private string pathBase;
        [SerializeField] private string filename;
        [HideInInspector] private string fileSuffix = ".json";
        
        //todo make scriptable object
        [Header("GameSave Settings")]
        [SerializeField] private string gameSavePathBase;
        [SerializeField] private string[] gameSaveFilenames;
        
        [Header("Receiving Events On")]
        [SerializeField] private IntEventChannelSO saveGame;
        [SerializeField] private IntEventChannelSO loadGame;
        [SerializeField] private VoidEventChannelSO saveMap;
        [SerializeField] private VoidEventChannelSO loadMap;
        
        private void Awake() {
            saveMap.OnEventRaised += SaveGridContainer;
            loadMap.OnEventRaised += LoadGridContainer;
            saveGame.OnEventRaised += SaveGame;
            loadGame.OnEventRaised += LoadGame;
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
            var json = JsonUtility.ToJson(gridContainer);
            var path = pathBase + filename + fileSuffix;
            
            // TODO Debug Macro
            Debug.Log($"Save Test GridContainer to JSON at {path} \n{json}");

            using (var fs = new FileStream(path, FileMode.Create)) {
                using (var writer = new StreamWriter(fs)) {
                    writer.Write(json);
                }
            }
        }

        public void LoadGridContainer() {
            LoadGridContainer(pathBase, filename);
        }
        
        public void LoadGridContainer(string pathBase, string filename) {

            string json;
            
            var path = pathBase + filename + fileSuffix;
            
            using (var fs = new FileStream(path, FileMode.Open)) {
                using (var reader = new StreamReader(fs)) {
                    json = reader.ReadToEnd();
                }
            }
            
            Debug.Log(json);

            JsonUtility.FromJsonOverwrite(json, gridContainer);

            globalGridData.Width = gridContainer.tileGrids[0].Width;
            globalGridData.Height = gridContainer.tileGrids[0].Height;
            globalGridData.cellSize = gridContainer.tileGrids[0].CellSize;
            globalGridData.OriginPosition = gridContainer.tileGrids[0].OriginPosition;
            // JsonUtility.FromJson<GridContainerSO>(json);
        }
    }
}