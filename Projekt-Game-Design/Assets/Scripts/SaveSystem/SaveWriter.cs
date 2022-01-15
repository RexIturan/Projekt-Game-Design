using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Grid;
using Level.Grid.CharacterGrid;
using Level.Grid.ItemGrid;
using Level.Grid.ObjectGrid;
using QuestSystem.ScriptabelObjects;
using SaveSystem.SaveFormats;
using UnityEngine;
using WorldObjects;

namespace SaveSystem {
	public class SaveWriter {
		// grid
		private readonly GridContainerSO _gridContaier;
		private readonly GridDataSO _globalGridData;

		// character
		private CharacterList _characterList;

		// world objects
		private WorldObjectList _worldObjectList;

		// inventorys
		private readonly InventorySO _inventory;

		// quests
		private readonly QuestContainerSO _questContainer;

//////////////////////////////////// Local Functions ///////////////////////////////////////////////

				#region Local Functions

				private GridData_Save GetGridDataSaveData(GridDataSO gridData) {
			GridData_Save gridDataSave = new GridData_Save {
				height = gridData.Height,
				width = gridData.Width,
				depth = gridData.Depth,
				cellSize = gridData.CellSize,
				originPosition = gridData.OriginPosition
			};

			return gridDataSave;
		}
		
		private List<TileGrid> GetTileGridSaveData(GridContainerSO gridContainer) {
			List<TileGrid> gridSaveData = new List<TileGrid>();
			
			foreach (var grids in gridContainer.tileGrids) {
				gridSaveData.Add(grids);
			}

			return gridSaveData;
		}
		
		private List<ItemGrid> GetItemGridSaveData(GridContainerSO gridContainer) {
			List<ItemGrid> gridSaveData = new List<ItemGrid>();
			
			foreach (var grids in gridContainer.items) {
				gridSaveData.Add(grids);
			}

			return gridSaveData;
		}
		
		private List<CharacterGrid> GetCharacterGridSaveData(GridContainerSO gridContainer) {
			List<CharacterGrid> gridSaveData = new List<CharacterGrid>();
			
			foreach (var grids in gridContainer.characters) {
				gridSaveData.Add(grids);
			}

			return gridSaveData;
		}
		
		private List<ObjectGrid> GetObjectGridSaveData(GridContainerSO gridContainer) {
			List<ObjectGrid> gridSaveData = new List<ObjectGrid>();
			
			foreach (var grids in gridContainer.objects) {
				gridSaveData.Add(grids);
			}

			return gridSaveData;
		}
		
		private List<PlayerCharacter_Save> GetPlayerSaveData(CharacterList characterList) {
			List<PlayerCharacter_Save> playerChars = new List<PlayerCharacter_Save>();

			List<GameObject> allPlayers = new List<GameObject>();
			allPlayers.AddRange(characterList.playerContainer);
			allPlayers.AddRange(characterList.friendlyContainer);

			if ( characterList ) {
				foreach ( var player in allPlayers ) {
					var playerCharacterSc = player.GetComponent<PlayerCharacterSC>();
					var pcGridTransform = player.GetComponent<GridTransform>();
					var playerStatistics = player.GetComponent<Statistics>();
					
					playerChars.Add(
						new PlayerCharacter_Save() {
							id = playerCharacterSc.id,
							active = playerCharacterSc.active,
							plyerTypeId = playerCharacterSc.playerType.id,
							pos = pcGridTransform.gridPosition,
							hitpoints = playerStatistics.StatusValues.HitPoints.value,
							energy = playerStatistics.StatusValues.Energy.value
						});
				}	
			}

			return playerChars;
		}

		private List<Enemy_Save> GetEnemySaveData(CharacterList characterList) {
			List<Enemy_Save> enemyChars = new List<Enemy_Save>();
						
			List<GameObject> allEnemies = new List<GameObject>();
			allEnemies.AddRange(characterList.enemyContainer);
			allEnemies.AddRange(characterList.deadEnemies);

			if ( characterList ) {
				foreach ( var enemy in characterList.enemyContainer ) {
					var enemySC = enemy.GetComponent<EnemyCharacterSC>();
					var enemyGridTransform = enemy.GetComponent<GridTransform>();
					var enemyStatistics = enemy.GetComponent<Statistics>();

					enemyChars.Add(
						new Enemy_Save() {
							enemyTypeId = enemySC.enemyType.id,
							pos = enemyGridTransform.gridPosition,
							hitpoints = enemyStatistics.StatusValues.HitPoints.value,
							energy = enemyStatistics.StatusValues.Energy.value,
						});
				}	
			}
			
			return enemyChars;
		}

		private List<Door_Save> GetDoorsSaveData(WorldObjectList worldObjectList) {
			List<Door_Save> doors = new List<Door_Save>();
			
			foreach(GameObject doorObj in worldObjectList.doors) {
				Door doorComp = doorObj.GetComponent<Door>();
				GridTransform doorTrans = doorObj.GetComponent<GridTransform>();
				Statistics doorStats = doorObj.GetComponent<Statistics>();

				doors.Add(new Door_Save()
				{
					doorTypeId = doorComp.doorType.id,
					gridPos = doorTrans.gridPosition,
					orientation = doorComp.orientation,
					open = doorComp.open,
					keyIds = doorComp.keyIds,
					switchIds = doorComp.switchIds,
					triggerIds = doorComp.triggerIds,
					remainingSwitchIds = doorComp.remainingSwitches,
					remainingTriggerIds = doorComp.remainingTrigger,
					currentHitPoints =  doorStats.StatusValues.HitPoints.value
				});
			}

			return doors;
		}

		private List<Switch_Save> GetSwitchesSaveData(WorldObjectList worldObjectList) {
			List<Switch_Save> switches = new List<Switch_Save>();
			
			foreach(GameObject switchObj in worldObjectList.switches) {
				SwitchComponent switchComp = switchObj.GetComponent<SwitchComponent>();
				GridTransform switchTrans = switchObj.GetComponent<GridTransform>();

				switches.Add(new Switch_Save()
				{
					switchId = switchComp.switchId,
					activated = switchComp.IsActivated,
					switchTypeId = switchComp.switchType.id,
					gridPos = switchTrans.gridPosition,
					orientation = switchComp.orientation
				});
			}

			return switches;
		}

		private List<Junk_Save> GetJunksSaveData(WorldObjectList worldObjectList) {
			List<Junk_Save> junks = new List<Junk_Save>();
			
			foreach(GameObject junkObj in worldObjectList.junks) {
				Junk junkComp = junkObj.GetComponent<Junk>();
				GridTransform junkTrans = junkObj.GetComponent<GridTransform>();
				Statistics junkStats = junkObj.GetComponent<Statistics>();

				junks.Add(new Junk_Save()
				{
					junkTypeId = junkComp.junkType.id,
					gridPos = junkTrans.gridPosition,
					orientation = junkComp.orientation,
					broken = junkComp.broken,
					currentHitPoints = junkStats.StatusValues.HitPoints.value
				});
			}

			return junks;
		}

		private Inventory_Save GetInventorySaveData(InventorySO inventory) {
			Inventory_Save inventorySave = new Inventory_Save();
			inventorySave.size = inventory.playerInventory.Capacity;
			foreach ( var item in inventory.playerInventory) {
				inventorySave.itemIds.Add(item.id);
			}

			return inventorySave;
		}

		// todo shorten names
		private List<Inventory_Save> GetEquipmentInventorySaveData(InventorySO inventory) {
			List<Inventory_Save> equipmentInventorys = new List<Inventory_Save>();

			foreach ( var equipment in inventory.equipmentInventories ) {
				if(equipment is null)
					break;
				
				var equiped = new List<int>();
				foreach ( var item in equipment.EquipmentToArray()) {
					equiped.Add(item ? item.id : -1);
				}

				equipmentInventorys.Add(new Inventory_Save() {
					size = equipment.GetCount(),
					itemIds = equiped
				});
			}

			return equipmentInventorys;
		}

		private List<Quest_Save> GetQuestSaveData(QuestContainerSO questContainer) {
			List<Quest_Save> quests = new List<Quest_Save>();
			
			foreach(QuestSO quest in questContainer.activeQuests) {
				quests.Add(new Quest_Save()
				{
					questId = quest.questId,
					active = quest.IsActive,
					currentTaskIndex = quest.currentTaskIndex
				});
			}
			
			return quests;
		}

		#endregion

/////////////////////////////////////// Public Functions ///////////////////////////////////////////

		#region Public Functions

		public SaveWriter(
			GridContainerSO gridContaier, 
			GridDataSO gridData,
			InventorySO inventory,
			QuestContainerSO questContainer) {

			_gridContaier = gridContaier;
			_globalGridData = gridData;
			_inventory = inventory;
			_questContainer = questContainer;
		}

		public void SetRuntimeReferences(CharacterList characterList, WorldObjectList worldObjectList) {
			_characterList = characterList;
			_worldObjectList = worldObjectList;
		}
		
		public Save WirteLevelToSave() {
			Save save = new Save {
				inventory = GetInventorySaveData(_inventory),
				equipmentInventory = GetEquipmentInventorySaveData(_inventory),
				quests = GetQuestSaveData(_questContainer),
				gridDataSave = GetGridDataSaveData(_globalGridData),
				players = GetPlayerSaveData(_characterList),
				enemies = GetEnemySaveData(_characterList),
				doors = GetDoorsSaveData(_worldObjectList),
				switches = GetSwitchesSaveData(_worldObjectList),
				junks = GetJunksSaveData(_worldObjectList),
				tileGrids = GetTileGridSaveData(_gridContaier),
				itemGrids = GetItemGridSaveData(_gridContaier),
				characterGrids = GetCharacterGridSaveData(_gridContaier),
				objectGrids = GetObjectGridSaveData(_gridContaier)
			};

			return save;
		}

		#endregion
	}
}