using System;
using System.IO;
using System.Linq;
using Grid;
using UnityEngine;

namespace SaveLoad {
    public class SaveManager : MonoBehaviour {
        [SerializeField] private GridContainerSO gridContainer;
        [SerializeField] private GridDataSO globalGridData;

        [Header("Save/Load Settings")]
        [SerializeField] private string pathBase;
        [SerializeField] private string filename;
        [SerializeField] private string fileSuffix;

        [Header("Receiving Events On")]
        [SerializeField] private VoidEventChannelSO saveLevel;
        [SerializeField] private VoidEventChannelSO loadLevel;

        private void Awake() {
            saveLevel.OnEventRaised += SaveGridContainer;
            loadLevel.OnEventRaised += LoadGridContainer;
        }

        //TODO return bool if successful
        public void SaveGridContainer() {
            var json = JsonUtility.ToJson(gridContainer);

            // TODO Debug Macro
            Debug.Log($"Save Test GridContainer to JSON \n{json}");

            var path = pathBase + filename + fileSuffix;

            using (var fs = new FileStream(path, FileMode.Create)) {
                using (var writer = new StreamWriter(fs)) {
                    writer.Write(json);
                }
            }
        }

        public void LoadGridContainer() {

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