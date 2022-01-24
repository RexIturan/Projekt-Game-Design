using System;
using GDP01.Util.Attributes;
using UnityEngine;

namespace GDP01.Loot.Types {
	[Serializable]
	public class LootObject {
		public LootType type;
		public ItemSO item;
		public int weight = 1;
		[Percent] public float dropRate;
		[MinMaxSlider(typeof(Vector2Int))] public Vector4 dropAmount = new Vector4(0, 1, 0, 100);
		
		// UI field 		
		[HideInInspector][SerializeField] private float propertyHeight;
	}
}