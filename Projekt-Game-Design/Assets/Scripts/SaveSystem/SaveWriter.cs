using System.Collections.Generic;
using _Gameplay.Environment.FogOfWar.FogOfWarV2;
using _Gameplay.Environment.FogOfWar.FogOfWarV2.Types;
using Characters;
using Characters.Equipment.ScriptableObjects;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using Grid;
using QuestSystem.ScriptabelObjects;
using SaveSystem.SaveFormats;
using UnityEngine;
using WorldObjects;

namespace SaveSystem {
	public class SaveWriter {
		// grid
		private readonly GridDataSO _gridData;

		// world objects
		// private WorldObjectList _worldObjectList;

		// inventorys
		private readonly InventorySO _inventory;
		private readonly EquipmentContainerSO _equipmentContainer;

		// quests
		private readonly QuestContainerSO _questContainer;

		private WorldObjectManager WorldObjectManager => GameplayProvider.Current.WorldObjectManager; 
		
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

		private List<TileGrid> GetTileGridSaveData(GridDataSO gridData) {
			List<TileGrid> gridSaveData = new List<TileGrid>();

			foreach ( var grids in gridData.TileGrids ) {
				gridSaveData.Add(grids);
			}

			return gridSaveData;
		}

		private List<PlayerCharacter_Save> GetPlayerSaveData(CharacterManager characterManager) {
			List<PlayerCharacter_Save> playerChars = new List<PlayerCharacter_Save>();

			List<PlayerCharacterSC> allPlayers = new List<PlayerCharacterSC>();
			allPlayers.AddRange(characterManager.GetPlayerCharacters());

			if ( characterManager ) {
				foreach ( var player in allPlayers ) {
					var playerStatistics = player.GetComponent<Statistics>();

					playerChars.Add(
						new PlayerCharacter_Save() {
							id = player.id,
							active = player.IsActive,
							plyerTypeId = player.Type.id,
							pos = player.GridPosition,
							hitpoints = playerStatistics.StatusValues.HitPoints.Value,
							energy = playerStatistics.StatusValues.Energy.Value
						});
				}
			}

			return playerChars;
		}

		private List<Enemy_Save> GetEnemySaveData(CharacterManager characterManager) {
			List<Enemy_Save> enemyChars = new List<Enemy_Save>();

			var allEnemies = new List<EnemyCharacterSC>();
			allEnemies.AddRange(characterManager.GetEnemyCahracters());

			if ( characterManager ) {
				foreach ( var enemy in characterManager.GetEnemyCahracters() ) {
					var enemyStatistics = enemy.GetComponent<Statistics>();

					enemyChars.Add(
						new Enemy_Save() {
							enemyTypeId = enemy.Type.id,
							pos = enemy.GridPosition,
							hitpoints = enemyStatistics.StatusValues.HitPoints.Value,
							energy = enemyStatistics.StatusValues.Energy.Value,
						});
				}
			}

			return enemyChars;
		}

		private List<Item_Save> GetItemSaveData() {
			List<Item_Save> itemSaves = new List<Item_Save>();

			foreach ( var item in WorldObjectManager.GetItems() ) {
				if ( item != null ) {
					itemSaves.Add(new Item_Save {
						id = item.Type.id,
						gridPos = item.GridPosition
					});
				}
			}

			return itemSaves;
		}
		
		private List<string> GetViewSaveData() {
			return FogOfWarController.Current.GetViewAsStringList();
		}
		
		private List<Door_Save> GetDoorsSaveData() {
			List<Door_Save> doors = new List<Door_Save>();

			foreach ( var door in WorldObjectManager.GetDoors() ) {
				if ( door != null ) {
					Statistics doorStats = door.GetComponent<Statistics>();

					doors.Add(new Door_Save() {
						doorTypeId = door.Type.id,
						gridPos = door.GridPosition,
						orientation = door.Rotation,
						open = door.IsOpen,
						keyIds = door.Keys,
						switchIds = door.Switches,
						triggerIds = door.Triggers,
						remainingSwitchIds = door.RemainingSwitches,
						remainingTriggerIds = door.RemainingTriggers,
						currentHitPoints = doorStats.StatusValues.HitPoints.Value
					});	
				}
			}

			return doors;
		}

		private List<Switch_Save> GetSwitchesSaveData() {
			List<Switch_Save> switches = new List<Switch_Save>();

			foreach ( var switchComponent in WorldObjectManager.GetSwitches() ) {
				switches.Add(new Switch_Save() {
					switchId = switchComponent.Id,
					activated = switchComponent.IsActivated,
					switchTypeId = switchComponent.Type.id,
					gridPos = switchComponent.GridPosition,
					orientation = switchComponent.Rotation
				});
			}

			return switches;
		}

		private List<Junk_Save> GetJunksSaveData() {
			List<Junk_Save> junks = new List<Junk_Save>();

			//TODO JUNK
			
			// foreach ( GameObject junkObj in worldObjectList.junks ) {
			// 	Junk junkComp = junkObj.GetComponent<Junk>();
			// 	GridTransform junkTrans = junkObj.GetComponent<GridTransform>();
			// 	Statistics junkStats = junkObj.GetComponent<Statistics>();
			//
			// 	junks.Add(new Junk_Save() {
			// 		junkTypeId = junkComp.junkType.id,
			// 		gridPos = junkTrans.gridPosition,
			// 		orientation = junkComp.orientation,
			// 		broken = junkComp.broken,
			// 		currentHitPoints = junkStats.StatusValues.HitPoints.Value
			// 	});
			// }

			return junks;
		}

		private Inventory_Save GetInventorySaveData(InventorySO inventory) {
			Inventory_Save inventorySave = new Inventory_Save(0);
			inventorySave.size = inventory.InventorySlots.Length;
			for ( int i = 0; i < inventorySave.size; i++ ) {
				if ( inventory.InventorySlots[i] is { } ) {
					inventorySave.itemIds.Add(new Inventory_Save.ItemSlot{
						id = i,
						itemID = inventory.InventorySlots[i].id
					});
				}
			}
			// foreach ( var item in inventory.InventorySlots ) {
			// 	inventorySave.itemIds.Add(item.id);
			// }

			return inventorySave;
		}

		// todo shorten names
		private List<Inventory_Save> GetEquipmentInventorySaveData(EquipmentContainerSO equipmentContainer) {
			List<Inventory_Save> equipmentInventorys = new List<Inventory_Save>();

			foreach ( var equipmentSheet in equipmentContainer.EquipmentSheets ) {
				if ( equipmentSheet is null )
					break;

				var equiped = new List<Inventory_Save.ItemSlot>();
				var equipmentArray = equipmentSheet.EquipmentToArray();
				for ( int i = 0; i < equipmentArray.Length; i++ ) {
					if ( equipmentArray[i] is { } ) {
						equiped.Add(new Inventory_Save.ItemSlot {
							id = i,
							itemID = equipmentArray[i].id 
						});	
					}
				}
				// foreach ( var item in equipmentSheet.EquipmentToArray() ) {
				// 	equiped.Add(new KeyValuePair<int, int>(item ? item.id : -1));
				// }

				equipmentInventorys.Add(new Inventory_Save(equipmentArray.Length) {
					size = equipmentSheet.GetCount(),
					itemIds = equiped
				});
			}

			return equipmentInventorys;
		}

		private List<Quest_Save> GetQuestSaveData(QuestContainerSO questContainer) {
			List<Quest_Save> quests = new List<Quest_Save>();

			foreach ( QuestSO quest in questContainer.activeQuests ) {
				quests.Add(new Quest_Save() {
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
			GridDataSO gridData,
			InventorySO inventory,
			EquipmentContainerSO equipmentContainer,
			QuestContainerSO questContainer) {
			_gridData = gridData;
			_inventory = inventory;
			_equipmentContainer = equipmentContainer;
			_questContainer = questContainer;
		}

		public void SetRuntimeReferences() {
		}

		public Save WirteLevelToSave() {
			Save save = new Save {
				inventory = GetInventorySaveData(_inventory),
				equipmentInventory = GetEquipmentInventorySaveData(_equipmentContainer),
				quests = GetQuestSaveData(_questContainer),
				gridDataSave = GetGridDataSaveData(_gridData),
				players = GetPlayerSaveData(GameplayProvider.Current.CharacterManager),
				enemies = GetEnemySaveData(GameplayProvider.Current.CharacterManager),
				doors = GetDoorsSaveData(),
				switches = GetSwitchesSaveData(),
				junks = GetJunksSaveData(),
				tileGrids = GetTileGridSaveData(_gridData),
				items = GetItemSaveData(),
				view = GetViewSaveData(),
				// itemGrids = GetItemGridSaveData(_gridContaier),
				// characterGrids = GetCharacterGridSaveData(_gridContaier),
				// objectGrids = GetObjectGridSaveData(_gridContaier)
			};

			return save;
		}

		#endregion
	}
}