using System.Collections.Generic;
using Grid;
using SaveSystem.SaveFormats;
using UnityEngine;

namespace SaveSystem {
	public class Save {
		//todo rename achtung savefiles will be incompatible
		public GridData_Save gridDataSave;
		public List<PlayerCharacter_Save> players;
		public List<Enemy_Save> enemies;
		public List<Door_Save> doors;
		public List<Switch_Save> switches;
		public List<Junk_Save> junks;
		public List<TileEffect_Save> tileEffects;
		public List<Item_Save> items;
		public Inventory_Save inventory;
		public List<Inventory_Save> equipmentInventory;
		public List<Quest_Save> quests;
		public List<TileGrid> tileGrids;

///// Properties ////////////////////////////////////////////////////////////////////////////////

		public string FileName { get; set; }
		
///// Properties ////////////////////////////////////////////////////////////////////////////////
		
		public Save() {
			inventory = new Inventory_Save();
			equipmentInventory = new List<Inventory_Save>();
			quests = new List<Quest_Save>();
			gridDataSave = new GridData_Save();
			players = new List<PlayerCharacter_Save>();
			enemies = new List<Enemy_Save>();
			doors = new List<Door_Save>();
			switches = new List<Switch_Save>();
			junks = new List<Junk_Save>();
			tileEffects = new List<TileEffect_Save>();
			items = new List<Item_Save>();
			tileGrids = new List<TileGrid>();
		}

		public string ToJson() {
			var json = JsonUtility.ToJson(this, true);
			return json;
		}

		public void LoadFromJson(string json) {
			var save = JsonUtility.FromJson<Save>(json);
			JsonUtility.FromJsonOverwrite(json, this);
		}

		public void Clear() {
			players.Clear();
			enemies.Clear();
			doors.Clear();
			switches.Clear();
			junks.Clear();
			tileEffects.Clear();
			tileGrids.Clear();
			gridDataSave = new GridData_Save();
			inventory = new Inventory_Save();
			quests.Clear();
		}
	}
}