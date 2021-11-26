using System.Collections.Generic;
using Characters;
using Grid;
using SaveSystem.SaveFormats;
using UnityEngine;

namespace SaveSystem {
	public class SaveReader {

//////////////////////////////////// Local Variables ///////////////////////////////////////////////

		// grid
		private readonly GridContainerSO _gridContaier;
		private readonly GridDataSO _gridData;

		// inventorys
		private readonly InventorySO _inventory;
		private readonly EquipmentContainerSO _equipmentInventoryContainerSo;

		// dictionarys
		private ItemContainerSO _itemContainerSo;
		
		// runtime ref
		private CharacterInitialiser _characterInitializer;
		
//////////////////////////////////// Local Functions ///////////////////////////////////////////////
		#region Local Functions

		private bool ReadGridData(GridData_Save gridDataSave, GridDataSO globalGridData) {
			
			globalGridData.height = gridDataSave.height;
			globalGridData.width = gridDataSave.width;
			globalGridData.cellSize = gridDataSave.cellSize;
			globalGridData.originPosition = gridDataSave.originPosition;
			
			//todo check if all read date is valid
			return true;
		}
		
		/// <summary>
		/// Read in from Save, and write into Runtime ScriptableObject
		/// </summary>
		/// <param name="saveGridSave"></param>
		/// <param name="gridContaier"></param>
		private void ReadGrid(List<TileGrid> saveGridSave, GridContainerSO gridContaier) {
			gridContaier.tileGrids.Clear();
			gridContaier.tileGrids.AddRange(saveGridSave);
		}
		
		private void ReadInventory(Inventory_Save saveInventory, InventorySO inventory) {
			inventory.itemIDs.Clear();
			foreach (var ids in saveInventory.itemIds) {
				//todo inventory should just have indices
				inventory.itemIDs.Add(ids);    
			}
		}
		
		private void ReadEquipmentInventory(List<Inventory_Save> saveEquipmentInventory, 
			EquipmentContainerSO equipmentContainer) {
			
			foreach (var equipmentInventory in saveEquipmentInventory) {
				Equipment equipment = new Equipment();
				for(int i = 0; i < equipment.items.Count && i < equipmentInventory.itemIds.Count; i++)
				{
					equipment.items[i] = equipmentInventory.itemIds[i];
				}
									
				equipmentContainer.inventories.Add(equipment);
			}
		}
		
		#endregion
		
/////////////////////////////////////// Public Functions ///////////////////////////////////////////
		#region Public Functions

		public SaveReader(
			GridContainerSO gridContaier, 
			GridDataSO gridData,
			InventorySO inventory,
			EquipmentContainerSO equipmentInventoryContainer, 
			ItemContainerSO itemContainerSO) {
			
			_gridContaier = gridContaier;
			_gridData = gridData;
			_inventory = inventory;
			_equipmentInventoryContainerSo = equipmentInventoryContainer;
			_itemContainerSo = itemContainerSO;
		}

		public void SetRuntimeReferences(CharacterInitialiser characterInitialiser) {
			_characterInitializer = characterInitialiser;
		}
		
		public void ReadSave(Save save) {
				
			ReadGrid(save.gridSave, _gridContaier);
			ReadGridData(save.gridDataSave, _gridData);

			// ReadCharacter(save.players, save.enemies);
			_characterInitializer.Initialise(save.players, save.enemies);

			ReadInventory(save.inventory, _inventory);

			ReadEquipmentInventory(save.equipmentInventory, _equipmentInventoryContainerSo);
		}

		#endregion
	}
}