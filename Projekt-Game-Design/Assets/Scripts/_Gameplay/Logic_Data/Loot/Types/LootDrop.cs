using System;
using System.Collections.Generic;

namespace GDP01.Loot.Types {
	[Serializable]
	public class LootDrop {
		public List<ItemSO> items;
		public int gold;
		public int experience;

		public LootDrop() {
			items = new List<ItemSO>();
		}
	}
}