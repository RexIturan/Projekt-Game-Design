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
			public ItemSO.ItemTypeData weaponLeft;
			public ItemSO.ItemTypeData weaponRight;
			public ItemSO.ItemTypeData headArmor;
			public ItemSO.ItemTypeData bodyArmor;
			public ItemSO.ItemTypeData shield;
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

		public ItemSO GetItemFromEquipment(int playerID, EquipmentPosition equipmentPosition) {
			return EquipmentSheets[playerID].GetEquipedItem(equipmentPosition);
		}

		public ItemSO SetItemInEquipment(int playerID, EquipmentPosition equipmentPosition, ItemSO item) {
			ItemSO previous = null;

			previous = EquipmentSheets[playerID].GetEquipedItem(equipmentPosition);
			EquipmentSheets[playerID].SetEquipedItem(equipmentPosition, item);

			return previous;
		}

		public ItemSO UnequipItemFor(int playerID, EquipmentPosition equipmentPosition) {
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
					weaponLeft  = sheet.weaponLeft?.ToData(),
					weaponRight = sheet.weaponRight?.ToData(),
					headArmor   = sheet.headArmor?.ToData(),
					bodyArmor   = sheet.bodyArmor?.ToData(),
					shield      = sheet.shield?.ToData(),
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