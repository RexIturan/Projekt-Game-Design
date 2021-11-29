using System.Collections.Generic;
using Grid;
using SaveSystem.SaveFormats;

namespace SaveSystem {
	public class SaveWriter {
		// grid
		private readonly GridContainerSO _gridContaier;
		private readonly GridDataSO _globalGridData;

		// character
		private CharacterList _characterList;

		// inventorys
		private readonly InventorySO _inventory;
		private readonly EquipmentContainerSO _equipmentInventoryContainerSo;

//////////////////////////////////// Local Functions ///////////////////////////////////////////////

		#region Local Functions

		private GridData_Save GetGridDataSaveData(GridDataSO globalGridData) {
			GridData_Save gridDataSave = new GridData_Save {
				height = globalGridData.height,
				width = globalGridData.width,
				cellSize = globalGridData.cellSize,
				originPosition = globalGridData.originPosition
			};

			return gridDataSave;
		}
		
		private List<TileGrid> GetGridSaveData(GridContainerSO gridContainer) {
			List<TileGrid> gridSaveData = new List<TileGrid>();
			
			foreach (var grids in gridContainer.tileGrids) {
				gridSaveData.Add(grids);
			}

			return gridSaveData;
		}
		
		private List<PlayerCharacter_Save> GetPlayerSaveData(CharacterList characterList) {
			List<PlayerCharacter_Save> playerChars = new List<PlayerCharacter_Save>();

			if ( characterList ) {
				foreach ( var player in characterList.playerContainer ) {
					var playerCharacterSc = player.GetComponent<PlayerCharacterSC>();
					playerChars.Add(
						new PlayerCharacter_Save() {
							plyerTypeId = playerCharacterSc.playerType.id,
							plyerSpawnDataId = playerCharacterSc.playerSpawnData.id,
							pos = playerCharacterSc.gridPosition
						});
				}	
			}

			return playerChars;
		}

		private List<Enemy_Save> GetEnemySaveData(CharacterList characterList) {
			List<Enemy_Save> enemyChars = new List<Enemy_Save>();

			if ( characterList ) {
				foreach ( var enemy in characterList.playerContainer ) {
					var enemySC = enemy.GetComponent<EnemyCharacterSC>();
					enemyChars.Add(
						new Enemy_Save() {
							enemyTypeId = enemySC.enemyType.id,
							enemySpawnDataId = enemySC.enemySpawnData.id,
							pos = enemySC.gridPosition
						});
				}	
			}
			
			return enemyChars;
		}

		private Inventory_Save GetInventorySaveData(InventorySO inventory) {
			Inventory_Save inventorySave = new Inventory_Save();
			inventorySave.size = inventory.itemIDs.Capacity;
			foreach ( var itemID in inventory.itemIDs) {
				inventorySave.itemIds.Add(itemID);
			}

			return inventorySave;
		}

		// todo shorten names
		private List<Inventory_Save> GetEquipmentInventorySaveData(
			EquipmentContainerSO equipmentInventoryContainerSo) {
			List<Inventory_Save> equipmentInventorys = new List<Inventory_Save>();

			foreach ( var equipment in equipmentInventoryContainerSo.equipmentInventories ) {
				var equiped = new List<int>();
				foreach ( var itemID in equipment.items) {
					equiped.Add(itemID);
				}

				equipmentInventorys.Add(new Inventory_Save() {
					size = equipment.items.Capacity,
					itemIds = equiped
				});
			}

			return equipmentInventorys;
		}

		#endregion

/////////////////////////////////////// Public Functions ///////////////////////////////////////////

		#region Public Functions

		public SaveWriter(
			GridContainerSO gridContaier, 
			GridDataSO gridData,
			InventorySO inventory,
			EquipmentContainerSO equipmentInventoryContainer) {

			_gridContaier = gridContaier;
			_globalGridData = gridData;
			_inventory = inventory;
			_equipmentInventoryContainerSo = equipmentInventoryContainer;
		}

		public void SetRuntimeReferences(CharacterList characterList) {
			_characterList = characterList;
		}
		
		public Save WirteLevelToSave() {
			Save save = new Save {
				gridDataSave = GetGridDataSaveData(_globalGridData),
				gridSave = GetGridSaveData(_gridContaier),
				players = GetPlayerSaveData(_characterList),
				enemies = GetEnemySaveData(_characterList),
				inventory = GetInventorySaveData(_inventory),
				equipmentInventory = GetEquipmentInventorySaveData(_equipmentInventoryContainerSo)
			};

			return save;
		}

		#endregion
	}
}