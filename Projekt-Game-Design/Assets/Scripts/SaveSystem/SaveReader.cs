using System.Collections.Generic;
using System.Linq.Expressions;
using Characters;
using Characters.Equipment.ScriptableObjects;
using GDP01.Equipment;
using Grid;
using QuestSystem.ScriptabelObjects;
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
		private readonly EquipmentContainerSO _equipmentContainer;

		// quests
		private readonly QuestContainerSO _questContainer;

		// dictionarys
		private ItemTypeContainerSO _itemTypeContainerSO;
		
		// runtime ref
		private CharacterInitialiser _characterInitializer;
		private WorldObjectInitialiser _worldObjectInitialiser;
		
//////////////////////////////////// Local Functions ///////////////////////////////////////////////
		#region Local Functions

		private bool ReadGridData(Save save, GridDataSO gridData) {
			
			gridData.InitFromSaveValues(save.gridDataSave, save.FileName);
			
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
			// var saveItemGridSave = save.itemGrids;
			// var saveCharacterGridSave = save.characterGrids;
			// var saveObjectGridSave = save.objectGrids;

			var layers = gridData.Height;

			if ( saveTileGridSave.Count == layers ) {
				gridContaier.tileGrids.Clear();
				//init tile grid
				gridContaier.tileGrids.AddRange(saveTileGridSave);	
			}
			
			// if (saveItemGridSave.Count == layers && 
			//     saveCharacterGridSave.Count == layers && 
			//     saveObjectGridSave.Count == layers) {
			// 	
			// 	for ( int i = 0; i < layers; i++ ) {
			// 		// init item grid
			// 		gridContaier.items[i] = saveItemGridSave[i];
			// 		// inti character grid
			// 		gridContaier.characters[i] = saveCharacterGridSave[i];
			// 		// init object grid
			// 		gridContaier.objects[i] = saveObjectGridSave[i];
			// 	}
			// 	
			// }
		}
		
		private void ReadInventory(Inventory_Save saveInventory, InventorySO inventory) {
			inventory.Claer(saveInventory.size);

			for ( int i = 0; i < saveInventory.size; i++ ) {
				
			}
			
			foreach (var itemID in saveInventory.itemIds) {
				inventory.AddItemAt(itemID.id, _itemTypeContainerSO.itemList[itemID.itemID]);
				//inventory has just indices
				// inventory.InventorySlots.Add(_itemContainerSo.itemList[itemID]);    
			}
		}
		
		private void ReadEquipmentInventory(List<Inventory_Save> saveEquipmentInventory,
			EquipmentContainerSO equipmentContainer) {
			equipmentContainer.Init();
			
			
			foreach (var equipmentSheetSave in saveEquipmentInventory) {
				int id = equipmentContainer.CreateNewEquipmentSheet();
				var equipment = equipmentContainer.EquipmentSheets[id];
				
				equipment.InitialiseFromSave(equipmentSheetSave, _itemTypeContainerSO);
			}
			
			//todo rethink this -> do here, maybe get initialised chars as parameter?
			var characterList = CharacterList.FindInstant();
			if ( characterList is { } ) {
				var playerCharNum = characterList.playerContainer.Count +
				                    characterList.friendlyContainer.Count;
				var equipmentInvCount = equipmentContainer.EquipmentSheets.Count;
				if ( equipmentInvCount < playerCharNum) {
					for ( int i = 0; i < playerCharNum - equipmentInvCount; i++ ) {
						equipmentContainer.CreateNewEquipmentSheet();
					}
				}
			}
		}
		
		private void ReadQuests(List<Quest_Save> saveQuests, QuestContainerSO questContainer) {
			questContainer.Initialise(saveQuests);
		}
		
		#endregion
		
/////////////////////////////////////// Public Functions ///////////////////////////////////////////
		#region Public Functions

		public SaveReader(
			GridContainerSO gridContaier, 
			GridDataSO gridData,
			InventorySO inventory,
			QuestContainerSO questContainer,
			ItemTypeContainerSO itemTypeContainerSO,
			EquipmentContainerSO equipmentContainer) {
			
			_gridContaier = gridContaier;
			_gridData = gridData;
			_inventory = inventory;
			_equipmentContainer = equipmentContainer;
			_questContainer = questContainer;
			_itemTypeContainerSO = itemTypeContainerSO;
		}

		public void SetRuntimeReferences(CharacterInitialiser characterInitialiser,
				WorldObjectInitialiser worldObjectInitialiser) {
			_characterInitializer = characterInitialiser;
			_worldObjectInitialiser = worldObjectInitialiser;
		}
		
		public void ReadSave(Save save) {
			
			ReadGridData(save, _gridData);
			ReadGrid(save, _gridData, _gridContaier);

			// ReadCharacter(save.players, save.enemies);
			_characterInitializer.Initialise(save.players, save.enemies);
			_worldObjectInitialiser.Initialise(save.doors, save.switches, save.junks, save.items);

			ReadInventory(save.inventory, _inventory);

			ReadEquipmentInventory(save.equipmentInventory, _equipmentContainer);
			
			ReadQuests(save.quests, _questContainer);
		}


		#endregion
	}
}