using System;
using System.Collections.Generic;
using System.Linq;
using GDP01._Gameplay.Logic_Data.Loot;
using GDP01.Loot.Types;
using UnityEngine;

namespace GDP01.Loot.ScriptableObjects {
	/// <summary>
	/// Singel Loot Table
	/// </summary>
	[CreateAssetMenu(fileName = "newLootTable", menuName = "Loot/Loot Table", order = 0)]
	public class LootTableSO : ScriptableObject {
		[SerializeField] private List<LootGroup> dropTables = new List<LootGroup>();

		public List<LootGroup> LootTable => dropTables;

		public LootDrop GetLootDrop() {
			return LootGenerator.GenerateLoot(this, 1);
		}
		
		public void RollOnTables() {
			LootGenerator.GenerateLoot(this, 1);
		}

		private void OnValidate() {
			foreach ( var lootGroup in dropTables ) {
				float total = lootGroup.tabel.Sum(o => o.weight);
				foreach ( var lootObject in lootGroup.tabel ) {
					lootObject.dropRate = lootObject.weight / total;
				}
			}
		}
	}
}