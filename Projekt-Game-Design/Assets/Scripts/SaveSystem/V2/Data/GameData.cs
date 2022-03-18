using System.Collections.Generic;
using Characters.Equipment.ScriptableObjects;
using FullSerializer;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using GDP01._Gameplay.World.Character.Data;
using GDP01.Structure;
using GDP01.Structure.Provider;
using UnityEngine;
using static Characters.Equipment.ScriptableObjects.EquipmentContainerSO;
using static InventorySO;

namespace SaveSystem.V2.Data {
	public class GameData : ISaveState {

		// private LevelManager _levelManager;
		// private CharacterManager _characterManager;
		[fsIgnore] private LevelDataContainerSO LevelDataContainer => 
			StructureProvider.Current.LevelDataContainer;
		
		[fsIgnore] private LevelManager LevelManager => 
			StructureProvider.Current.LevelManager;
		
		[fsIgnore] private CharacterManager CharacterManager => 
			GameplayProvider.Current.CharacterManager;

		[fsIgnore]
		private EquipmentContainerSO EquipmentContainerSO =>
			GameplayDataProvider.Current.EquipmentContainerSO;
		
		[fsIgnore]
		private InventorySO InventorySO =>
			GameplayDataProvider.Current.InventorySO;
		
		// todo inventory manager?
		// todo equipment manager
		// todo turn manager
		// todo current location?
		
		public LocationData LocationData { get; set; }
		public List<PlayerCharacterData> PlayerCharacters { get; set; }
		public EquipmentContainerData EquipmentContainerData { get; set; }
		public InventoryData InventoryData { get; set; }

		public GameData() {
			LocationData = new LocationData(LevelManager.StartLevelName);
			PlayerCharacters = new List<PlayerCharacterData>();
			EquipmentContainerData = new EquipmentContainerData();
			InventoryData = new InventoryData();
		}
		
		public void Save() {
			PlayerCharacters = CharacterManager?.Save().PlayerCharacterDataList;
			LocationData = new LocationData {
				Name = LevelManager.Save().Level.name ?? "---"
			};

			EquipmentContainerData = EquipmentContainerSO.Save();
			InventoryData = InventorySO.Save();
		}

		public void Load() {
			
			LevelManager.Load(new LevelManagerData {
				Level = LevelDataContainer.GetLevelDataByName(LocationData.Name)
			});
			
			EquipmentContainerSO.Load(EquipmentContainerData);
			InventorySO.Load(InventoryData);
		}
		
		public void LoadPlayerCharacters() {
			CharacterManager.LoadPlayerCharacterData(PlayerCharacters);
			
			EquipmentContainerSO.RemoveUnusedEquipmentSheets();
		}
		
		//todo
		// player char
		// inventory
		// equipment
		// quest
		// general
		// - Current Turn
		// - Current Location
		// - Current Faction/Player

		// List<Level Data>
		// Level Data
		// - Name
		// - NPC
		// - Enemy
		// - Environment
		// - Vision
		// - ObjectData

		public void OnLevelLoaded() {
			EquipmentContainerSO.ClearClaimedIds();
			LoadPlayerCharacters();
		}
	}
}