using System.Collections.Generic;
using Grid;
using SaveSystem.SaveFormats;
using UnityEngine;

namespace SaveSystem {
    public class Save {
        public GridData_Save gridDataSave;
        public List<PC_Save> players;
        public List<Enemy_Save> enemies;
        public Inventory_Save inventory;
        public List<Inventory_Save> equipmentInventory;
        public List<TileGrid> gridSave;

        public Save() {
            inventory = new Inventory_Save();
            equipmentInventory = new List<Inventory_Save>();
            gridDataSave = new GridData_Save();
            players = new List<PC_Save>();
            enemies = new List<Enemy_Save>();
            gridSave = new List<TileGrid>();
        }
        
        public string ToJson() {
            var json = JsonUtility.ToJson(this, true);
            return json;
        }

        public void LoadFromJson(string json) {
            JsonUtility.FromJsonOverwrite(json, this);
        }

        public void Clear() {
            players.Clear();
            enemies.Clear();
            gridSave.Clear();
            gridDataSave = new GridData_Save();
        }
    }
}
