using System.Collections.Generic;
using Characters;
using Grid;
using Level.Grid.ItemGrid;
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
		private readonly EquipmentInventoryContainerSO _equipmentInventoryContainerSo;

		// dictionarys
		private ItemContainerSO _itemContainerSo;
		
		// runtime ref
		private CharacterInitialiser _characterInitializer;
		
//////////////////////////////////// Local Functions ///////////////////////////////////////////////
		#region Local Functions

		private bool ReadGridData(GridData_Save gridDataSave, GridDataSO gridData) {
			
			gridData.InitFromSaveValues(gridDataSave);
			
			//todo check if all read date is valid
			return true;
		}
		
		/// <summary>
		/// Read in from Save, and write into Runtime ScriptableObject
		/// </summary>
		/// <param name="saveGridSave"></param>
		/// <param name="gridContaier"></param>
		private void ReadGrid(Save save, GridDataSO gridData, GridContainerSO gridContaier) {
			gridContaier.InitGrids(gridData);
			
			var saveTileGridSave = save.tileGrids;
			var saveItemGridSave = save.itemGrids;
			var saveCharacterGridSave = save.characterGrids;
			var saveObjectGridSave = save.objectGrids;

			var layers = gridData.Height;

			if ( saveTileGridSave.Count == layers ) {
				gridContaier.tileGrids.Clear();
				//init tile grid
				gridContaier.tileGrids.AddRange(saveTileGridSave);	
			}
			
			if (saveItemGridSave.Count == layers && 
			    saveCharacterGridSave.Count == layers && 
			    saveObjectGridSave.Count == layers) {
				
				for ( int i = 0; i < layers; i++ ) {
					// init item grid
					gridContaier.items[i] = saveItemGridSave[i];
					// inti character grid
					gridContaier.characters[i] = saveCharacterGridSave[i];
					// init object grid
					gridContaier.objects[i] = saveObjectGridSave[i];
				}
				
			}
		}
		
		private void ReadInventory(Inventory_Save saveInventory, InventorySO inventory) {
			inventory.inventory.Clear();
			foreach (var ids in saveInventory.itemIds) {
				//todo inventory should just have indices
				inventory.inventory.Add(_itemContainerSo.itemList[ids]);    
			}
		}
		
		private void ReadEquipmentInventory(List<Inventory_Save> saveEquipmentInventory, 
			EquipmentInventoryContainerSO equipmentContainer) {
			
			foreach (var equipmentInventory in saveEquipmentInventory) {
				var equip = new List<ItemSO>();
				foreach (var ids in equipmentInventory.itemIds) {
					equip.Add(_itemContainerSo.itemList[ids]);
				}

				var eInventory = ScriptableObject.CreateInstance<EquipmentInventorySO>();
				eInventory.inventory = equip;
				equipmentContainer.inventories.Add(eInventory);
			}
		}
		
		#endregion
		
/////////////////////////////////////// Public Functions ///////////////////////////////////////////
		#region Public Functions

		public SaveReader(
			GridContainerSO gridContaier, 
			GridDataSO gridData,
			InventorySO inventory,
			EquipmentInventoryContainerSO equipmentInventoryContainer, 
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
			
			ReadGridData(save.gridDataSave, _gridData);
			ReadGrid(save, _gridData, _gridContaier);

			// ReadCharacter(save.players, save.enemies);
			_characterInitializer.Initialise(save.players, save.enemies);

			ReadInventory(save.inventory, _inventory);

			ReadEquipmentInventory(save.equipmentInventory, _equipmentInventoryContainerSo);
		}

		#endregion
	}
}