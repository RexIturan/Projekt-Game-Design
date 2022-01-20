using SaveSystem.SaveFormats;

namespace GDP01.Equipment {
	[System.Serializable]
	public class EquipmentSheet {
		public WeaponSO weaponLeft;
		public WeaponSO weaponRight;
		public HeadArmorSO headArmor;
		public BodyArmorSO bodyArmor;
		public ShieldSO shield;

		public EquipmentSheet() {
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
			for ( int i = 0; i < equipmentSheetSave.itemIds.Count; i++ ) {
				int itemId = equipmentSheetSave.itemIds[i];
				ItemSO item = _itemContainerSo.GetItemFromID(itemId);
				if ( item ) {
					EquipItemAt(i, item);	
				}
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
					if ( item is WeaponSO left ) {
						weaponLeft = left;
					}

					break;
				case EquipmentPosition.LEFT:
					if ( item is WeaponSO right ) {
						weaponRight = right;
					}

					break;
				case EquipmentPosition.HEAD:
					if ( item is HeadArmorSO head ) {
						headArmor = head;
					}

					break;
				case EquipmentPosition.BODY:
					if ( item is BodyArmorSO body ) {
						bodyArmor = body;
					}

					break;
				case EquipmentPosition.SHIELD:
					if ( item is ShieldSO shield ) {
						this.shield = shield;
					}

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
	}
}