using System.Collections.Generic;
using Characters;
using Grid;
using Level.Grid.ItemGrid;
using SaveSystem.SaveFormats;
using UnityEngine;
using WorldObjects;

namespace SaveSystem {
	public class SaveReader {

//////////////////////////////////// Local Variables ///////////////////////////////////////////////

		// grid
		private readonly GridContainerSO _gridContaier;
		private readonly GridDataSO _gridData;

		// inventorys
		private readonly InventorySO _inventory;

		// dictionarys
		private ItemContainerSO _itemContainerSo;
		
		// runtime ref
		private CharacterInitialiser _characterInitializer;
		private WorldObjectInitialiser _worldObjectInitialiser;
		
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
			inventory.playerInventory.Clear();
			foreach (var itemID in saveInventory.itemIds) {
				//todo inventory should just have indices
				inventory.playerInventory.Add(_itemContainerSo.itemList[itemID]);    
			}
		}
		
		private void ReadEquipmentInventory(List<Inventory_Save> saveEquipmentInventory,
			InventorySO inventory) {
			inventory.equipmentInventories.Clear();
			
			foreach (var equipmentInventory in saveEquipmentInventory) {
				InventorySO.Equipment equipment = new InventorySO.Equipment();
				
				if(equipmentInventory.itemIds[0] >= 0)
					equipment.weaponLeft = (WeaponSO) _itemContainerSo.itemList[equipmentInventory.itemIds[0]];
				else
					equipment.weaponLeft = null;

				if(equipmentInventory.itemIds[1] >= 0)
					equipment.weaponRight = (WeaponSO) _itemContainerSo.itemList[equipmentInventory.itemIds[1]];
				else
					equipment.weaponRight = null;

				if(equipmentInventory.itemIds[2] >= 0)
					equipment.headArmor = (HeadArmorSO) _itemContainerSo.itemList[equipmentInventory.itemIds[2]];
				else
					equipment.headArmor = null;

				if(equipmentInventory.itemIds[3] >= 0)
					equipment.bodyArmor = (BodyArmorSO) _itemContainerSo.itemList[equipmentInventory.itemIds[3]];
				else
					equipment.bodyArmor = null;

				if(equipmentInventory.itemIds[4] >= 0)
					equipment.shield = (ShieldSO) _itemContainerSo.itemList[equipmentInventory.itemIds[4]];
				else
					equipment.shield = null;
									
				inventory.equipmentInventories.Add(equipment);
			}
		}
		
		#endregion
		
/////////////////////////////////////// Public Functions ///////////////////////////////////////////
		#region Public Functions

		public SaveReader(
			GridContainerSO gridContaier, 
			GridDataSO gridData,
			InventorySO inventory,
			ItemContainerSO itemContainerSO) {
			
			_gridContaier = gridContaier;
			_gridData = gridData;
			_inventory = inventory;
			_itemContainerSo = itemContainerSO;
		}

		public void SetRuntimeReferences(CharacterInitialiser characterInitialiser,
				WorldObjectInitialiser worldObjectInitialiser) {
			_characterInitializer = characterInitialiser;
			_worldObjectInitialiser = worldObjectInitialiser;
		}
		
		public void ReadSave(Save save) {
			
			ReadGridData(save.gridDataSave, _gridData);
			ReadGrid(save, _gridData, _gridContaier);

			// ReadCharacter(save.players, save.enemies);
			_characterInitializer.Initialise(save.players, save.enemies);
			_worldObjectInitialiser.Initialise(save.doors, save.switches, save.junks);

			ReadInventory(save.inventory, _inventory);

			ReadEquipmentInventory(save.equipmentInventory, _inventory);
		}

		#endregion
	}
}