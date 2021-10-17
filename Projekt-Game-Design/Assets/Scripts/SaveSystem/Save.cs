using System.Collections.Generic;
using Characters.ScriptableObjects;
using Grid;
using SaveSystem.SaveForamts;
using UnityEngine;

namespace SaveSystem {
    public class CharacterSaveContainer {
        public List<PC_Save> players;
        public List<Enemy_Save> enemys;

        public CharacterSaveContainer() {
            players = new List<PC_Save>();
            enemys = new List<Enemy_Save>();
        }
        
        public void Clear() {
            players.Clear();
            enemys.Clear();
        }
    }
    
    public class Save {
        public GridData_Save GridDataSave;
        public List<PC_Save> players;
        public List<Enemy_Save> enemys;
        public Inventory_Save inventory;
        public List<Inventory_Save> EquipmentInventory;
        public List<TileGrid> grid_Save;

        
// TODO inventory
// TODO equipment

        public Save() {
            inventory = new Inventory_Save();
            EquipmentInventory = new List<Inventory_Save>();
            GridDataSave = new GridData_Save();
            players = new List<PC_Save>();
            enemys = new List<Enemy_Save>();
            grid_Save = new List<TileGrid>();
        }
        
        public string ToJson() {
            var json = JsonUtility.ToJson(this, true);
            // json += "{";
            // json += "\n\"character\":" + JsonUtility.ToJson(characterSaveContainer, true);
            // json += ",\n\"globalGridData\":" + JsonUtility.ToJson(globalGridData, true);
            // json += ",\n\"gridContainer\":" + JsonUtility.ToJson(gridContainerSo, true);
            //
            // json += "\n}";
            return json;
        }

        public void LoadFromJson(string json) {
            // JsonUtility.FromJson(json);
            JsonUtility.FromJsonOverwrite(json, this);
            Debug.Log($"LoadFromJson: json{json}");   
            // var cutted = json..Split(',');
            // foreach (var cut in cutted) {
                // Debug.Log($"cut{cut}");    
            // }
            // Debug.Log($"{characterSaveContainer}\n{globalGridData}\n{gridContainerSo}");
        }

        public void Clear() {
            players.Clear();
            enemys.Clear();
            grid_Save.Clear();
            GridDataSave = new GridData_Save();
        }
    }
}
