using System;
using System.Collections.Generic;

namespace GDP01.Loot.Types {
	[Serializable]
	public class LootGroup {
		public int count;
		public List<LootObject> tabel = new List<LootObject>();
	}
}