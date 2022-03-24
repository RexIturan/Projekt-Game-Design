using System.Collections.Generic;
using System.Linq;
using GDP01.Equipment;
using SaveSystem.V2.Data;
using UnityEngine;
using Util.Extensions;

namespace Characters.Equipment.ScriptableObjects {
	[CreateAssetMenu(fileName = "EquipmentContainerSO", menuName = "Equipment/EquipmentContainerSO", order = 0)]
	public class EquipmentContainerSO : ScriptableObject, ISaveState<EquipmentContainerSO.EquipmentContainerData> {

///// Data Type ////////////////////////////////////////////////////////////////////////////////////		
		public struct EquipmentContainerData {
			public List<EquipmentSheetData> equipmentSheets;
		}

		public class EquipmentSheetData {
			public ItemTypeSO.ItemTypeData weaponLeft;
			public ItemTypeSO.ItemTypeData weaponRight;
			public ItemTypeSO.ItemTypeData headArmor;
			public ItemTypeSO.ItemTypeData bodyArmor;
			public ItemTypeSO.ItemTypeData shield;
		}

///// Fields ///////////////////////////////////////////////////////////////////////////////////////		
		
		[SerializeField] private List<EquipmentSheet> equipmentSheets = new List<EquipmentSheet>();
		public List<EquipmentSheet> EquipmentSheets {
			get { return equipmentSheets; }
			private set { equipmentSheets = value; }
		}

		///// Private Fields ///////////////////////////////////////////////////////////////////////////////

		private readonly HashSet<int> claimedIds = new HashSet<int>();

///// Public Functions /////////////////////////////////////////////////////////////////////////////		
		
		public void Init() {
			ClearClaimedIds();
			equipmentSheets.Clear();
		}

		public ItemTypeSO GetItemFromEquipment(int playerID, EquipmentPosition equipmentPosition) {
			return EquipmentSheets[playerID].GetEquipedItem(equipmentPosition);
		}

		public ItemTypeSO SetItemInEquipment(int playerID, EquipmentPosition equipmentPosition, ItemTypeSO itemType) {
			ItemTypeSO previous = null;

			previous = EquipmentSheets[playerID].GetEquipedItem(equipmentPosition);
			EquipmentSheets[playerID].SetEquipedItem(equipmentPosition, itemType);

			return previous;
		}

		public ItemTypeSO UnequipItemFor(int playerID, EquipmentPosition equipmentPosition) {
			return EquipmentSheets[playerID].UnequipItem(equipmentPosition);
		}

		public int CreateNewEquipmentSheet() {
			int newId = EquipmentSheets.Count;
			EquipmentSheets.Add(new EquipmentSheet(newId));
			return newId;
		}

		public bool IdExists(int id) {
			return EquipmentSheets.IsValidIndex(id);
		}

		public bool IdClaimed(int id) {
			return claimedIds.Contains(id);
		}

		public bool ClaimId( int id ) {
			return claimedIds.Add(id);
		}

		public bool UnclaimId( int id ) {
			return claimedIds.Remove(id);
		}

		public void RemoveUnusedEquipmentSheets() {
			equipmentSheets.RemoveAll(sheet => !claimedIds.Contains(sheet.Id));
		}

		public void ClearClaimedIds() {
			claimedIds.Clear();
		}
		
///// Save State ///////////////////////////////////////////////////////////////////////////////////		
		
		public EquipmentContainerData Save() {
			//todo refactore?
			return new EquipmentContainerData {
				equipmentSheets = EquipmentSheets.Select(sheet => new EquipmentSheetData {
					weaponLeft  = sheet.weaponTypeLeft?.ToData(),
					weaponRight = sheet.weaponTypeRight?.ToData(),
					headArmor   = sheet.headArmorType?.ToData(),
					bodyArmor   = sheet.bodyArmorType?.ToData(),
					shield      = sheet.shieldType?.ToData(),
				}).ToList()
			};
		}

		public void Load(EquipmentContainerData data) {
			ClearClaimedIds();
			EquipmentSheets.Clear();
			if ( data.equipmentSheets != null ) {
				EquipmentSheets = new List<EquipmentSheet>();
				foreach ( EquipmentSheetData sheet in data.equipmentSheets ) {
					EquipmentSheets[CreateNewEquipmentSheet()].Init(sheet);
				}
			}
		}
	}
}