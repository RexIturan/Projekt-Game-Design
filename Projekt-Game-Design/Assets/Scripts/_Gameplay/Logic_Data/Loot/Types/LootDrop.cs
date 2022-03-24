using System;
using System.Collections.Generic;

namespace GDP01.Loot.Types {
	[Serializable]
	public class LootDrop {
		public List<ItemTypeSO> items;
		public int gold;
		public int experience;

		public LootDrop() {
			items = new List<ItemTypeSO>();
		}
	}
}