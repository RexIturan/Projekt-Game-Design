using Characters.Equipment.ScriptableObjects;
using SaveSystem.SaveFormats;

namespace GDP01.Equipment {
	[System.Serializable]
	public class EquipmentSheet {
		//todo these to [serializedField] private -> and public properties
		public WeaponSO weaponLeft;
		public WeaponSO weaponRight;
		public HeadArmorSO headArmor;
		public BodyArmorSO bodyArmor;
		public ShieldSO shield;
		
		private int _id;
		public int Id {
			get { return _id; }
			private set { _id = value; }
		}

///// Private Functions ////////////////////////////////////////////////////////////////////////////		
		
		private void SetEquipmentSlot<T>(ref T slot, ItemSO item) where T : ItemSO {
			if ( item == null ) slot = null;
			if ( item is T compatibleItem ) {
				slot = compatibleItem;
			}
		}

///// Public Functions /////////////////////////////////////////////////////////////////////////////		
		
		public EquipmentSheet(int id) {
			Id = id;
			weaponLeft = null;
			weaponRight = null;
			headArmor = null;
			bodyArmor = null;
			shield = null;
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
		public ItemSO[] EquipmentToArray() {
			ItemSO[] items = new ItemSO[GetCount()];
			items[0] = weaponLeft;
			items[1] = weaponRight;
			items[2] = headArmor;
			items[3] = bodyArmor;
			items[4] = shield;
			return items;
		}

		public void EquipItemAt(int i, ItemSO item) {
			SetEquipedItem((EquipmentPosition) i, item);
		}

		public void InitialiseFromSave(Inventory_Save equipmentSheetSave,
			ItemContainerSO _itemContainerSo) {
			if ( equipmentSheetSave.itemIds is { } ) {
				foreach ( var itemSlot in equipmentSheetSave.itemIds ) {
					ItemSO item = _itemContainerSo.GetItemFromID(itemSlot.itemID);
					if ( item ) {
						EquipItemAt(itemSlot.id +1, item);	
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

		public ItemSO GetEquipedItem(EquipmentPosition equipmentPosition) {
			ItemSO item = null;

			switch ( equipmentPosition ) {
				case EquipmentPosition.RIGHT:
					item = weaponRight;
					break;
				case EquipmentPosition.LEFT:
					item = weaponLeft;
					break;
				case EquipmentPosition.HEAD:
					item = headArmor;
					break;
				case EquipmentPosition.BODY:
					item = bodyArmor;
					break;
				case EquipmentPosition.SHIELD:
					item = shield;
					break;
				default:
					break;
			}

			return item;
		}


		
		public void SetEquipedItem(EquipmentPosition equipmentPosition, ItemSO item) {
			
			switch ( equipmentPosition ) {
				case EquipmentPosition.RIGHT:
					SetEquipmentSlot(ref weaponRight, item);
					break;
				
				case EquipmentPosition.LEFT:
					SetEquipmentSlot(ref weaponLeft, item);
					break;
				
				case EquipmentPosition.HEAD:
					SetEquipmentSlot(ref headArmor, item);
					break;
				
				case EquipmentPosition.BODY:
					SetEquipmentSlot(ref bodyArmor, item);
					break;
				
				case EquipmentPosition.SHIELD:
					SetEquipmentSlot(ref shield, item);
					break;
				
				default:
					break;
			}
		}

		public ItemSO UnequipItem(EquipmentPosition equipmentPosition) {
			var item = GetEquipedItem(equipmentPosition);
			SetEquipedItem(equipmentPosition, null);
			return item;
		}

		public void Init(EquipmentContainerSO.EquipmentSheetData sheet) {
			weaponLeft  = sheet.weaponLeft?.obj as WeaponSO;
			weaponRight = sheet.weaponRight?.obj as WeaponSO;
			headArmor   = sheet.headArmor?.obj as HeadArmorSO;
			bodyArmor   = sheet.bodyArmor?.obj as BodyArmorSO;
			shield      = sheet.shield?.obj as ShieldSO;
		}
	}
}