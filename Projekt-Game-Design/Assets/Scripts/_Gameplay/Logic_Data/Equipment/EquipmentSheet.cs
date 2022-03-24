using Characters.Equipment.ScriptableObjects;
using SaveSystem.SaveFormats;

namespace GDP01.Equipment {
	[System.Serializable]
	public class EquipmentSheet {
		//todo these to [serializedField] private -> and public properties
		public WeaponTypeSO weaponTypeLeft;
		public WeaponTypeSO weaponTypeRight;
		public HeadArmorTypeSO headArmorType;
		public BodyArmorTypeSO bodyArmorType;
		public ShieldTypeSO shieldType;
		
		private int _id;
		public int Id {
			get { return _id; }
			private set { _id = value; }
		}

///// Private Functions ////////////////////////////////////////////////////////////////////////////		
		
		private void SetEquipmentSlot<T>(ref T slot, ItemTypeSO itemType) where T : ItemTypeSO {
			if ( itemType == null ) slot = null;
			if ( itemType is T compatibleItem ) {
				slot = compatibleItem;
			}
		}

///// Public Functions /////////////////////////////////////////////////////////////////////////////		
		
		public EquipmentSheet(int id) {
			Id = id;
			weaponTypeLeft = null;
			weaponTypeRight = null;
			headArmorType = null;
			bodyArmorType = null;
			shieldType = null;
		}

		/// <summary>returns the number of equipment slots</summary>
		public int GetCount() {
			//todo magic number
			return 5;
		}

		/// <summary>
		/// <para>relevant for SaveWriter</para> 
		/// Converts equipment to array of Items.
		/// </summary>
		/// <remarks>
		/// Not equipped equipment slots are set to null
		/// The elements of the list are ordered as such (the order of
		/// the elements is relevant for valid save data):
		/// <list type="|">
		/// <item>1. weaponLeft</item>
		/// <item>2. weaponRight</item>
		/// <item>3. headArmor</item>
		/// <item>4. bodyArmor</item>
		/// <item>5. shield</item> 
		/// </list>
		/// </remarks>
		public ItemTypeSO[] EquipmentToArray() {
			ItemTypeSO[] items = new ItemTypeSO[GetCount()];
			items[0] = weaponTypeLeft;
			items[1] = weaponTypeRight;
			items[2] = headArmorType;
			items[3] = bodyArmorType;
			items[4] = shieldType;
			return items;
		}

		public void EquipItemAt(int i, ItemTypeSO itemType) {
			SetEquipedItem((EquipmentPosition) i, itemType);
		}

		public void InitialiseFromSave(Inventory_Save equipmentSheetSave,
			ItemTypeContainerSO itemTypeContainerSO) {
			if ( equipmentSheetSave.itemIds is { } ) {
				foreach ( var itemSlot in equipmentSheetSave.itemIds ) {
					ItemTypeSO itemType = itemTypeContainerSO.GetItemFromID(itemSlot.itemID);
					if ( itemType ) {
						EquipItemAt(itemSlot.id +1, itemType);	
					}
				}
				//
				// for ( int i = 0; i < equipmentSheetSave.itemIds.Count; i++ ) {
				// 	int itemId = equipmentSheetSave.itemIds[i].itemID;
				// 	ItemSO item = _itemContainerSo.GetItemFromID(itemId);
				// 	if ( item ) {
				// 		EquipItemAt(i, item);	
				// 	}
				// }	
			}
		}

		public ItemTypeSO GetEquipedItem(EquipmentPosition equipmentPosition) {
			ItemTypeSO itemType = null;

			switch ( equipmentPosition ) {
				case EquipmentPosition.RIGHT:
					itemType = weaponTypeRight;
					break;
				case EquipmentPosition.LEFT:
					itemType = weaponTypeLeft;
					break;
				case EquipmentPosition.HEAD:
					itemType = headArmorType;
					break;
				case EquipmentPosition.BODY:
					itemType = bodyArmorType;
					break;
				case EquipmentPosition.SHIELD:
					itemType = shieldType;
					break;
				default:
					break;
			}

			return itemType;
		}


		
		public void SetEquipedItem(EquipmentPosition equipmentPosition, ItemTypeSO itemType) {
			
			switch ( equipmentPosition ) {
				case EquipmentPosition.RIGHT:
					SetEquipmentSlot(ref weaponTypeRight, itemType);
					break;
				
				case EquipmentPosition.LEFT:
					SetEquipmentSlot(ref weaponTypeLeft, itemType);
					break;
				
				case EquipmentPosition.HEAD:
					SetEquipmentSlot(ref headArmorType, itemType);
					break;
				
				case EquipmentPosition.BODY:
					SetEquipmentSlot(ref bodyArmorType, itemType);
					break;
				
				case EquipmentPosition.SHIELD:
					SetEquipmentSlot(ref shieldType, itemType);
					break;
				
				default:
					break;
			}
		}

		public ItemTypeSO UnequipItem(EquipmentPosition equipmentPosition) {
			var item = GetEquipedItem(equipmentPosition);
			SetEquipedItem(equipmentPosition, null);
			return item;
		}

		public void Init(EquipmentContainerSO.EquipmentSheetData sheet) {
			weaponTypeLeft  = sheet.weaponLeft?.obj as WeaponTypeSO;
			weaponTypeRight = sheet.weaponRight?.obj as WeaponTypeSO;
			headArmorType   = sheet.headArmor?.obj as HeadArmorTypeSO;
			bodyArmorType   = sheet.bodyArmor?.obj as BodyArmorTypeSO;
			shieldType      = sheet.shield?.obj as ShieldTypeSO;
		}
	}
}