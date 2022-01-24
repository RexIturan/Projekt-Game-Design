using System;
using System.Collections.Generic;

namespace GDP01.Loot.Types {
	[Serializable]
	public struct LootDrop {
		public List<ItemSO> items;
		public int gold;
		public int experience;
	}
}