using System;
using System.Collections.Generic;

namespace SaveSystem.SaveFormats {
	[Serializable]
	public class Inventory_Save {
		[Serializable]
		public struct ItemSlot {
			public int id;
			public int itemID;
		}
		
		public int size;
		public List<ItemSlot> itemIds;

		public Inventory_Save(int size) {
			this.size = size;
			this.itemIds = new List<ItemSlot>();
		}
	}
}