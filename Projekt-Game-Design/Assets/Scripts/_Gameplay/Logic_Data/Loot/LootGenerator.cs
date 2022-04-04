using System.Collections.Generic;
using System.Linq;
using System.Text;
using GDP01.Loot.ScriptableObjects;
using GDP01.Loot.Types;
using UnityEngine;
using Util.Extensions;

namespace GDP01._Gameplay.Logic_Data.Loot {
	public static class LootGenerator {

		public static int seed = 314159265;
		
		private static void InitRandom() {
			Random.InitState(seed);
		}

		public static int RollForTableAttempts(LootGroup group) {
			if ( group is null )
				return 0;

			return group.count;
			
			//todo add random amount of table rolls
			
			// float step = 1f / group.max;
			//
			// List<float> attemptSpectrum = new List<float>();
			// float current = 0;
			// current += group.distribution.Evaluate(-step) > 0 ? group.distribution.Evaluate(-step) : 0;
			// attemptSpectrum.Add(current);
			// for ( int i = 1; i < group.max+1; i++ ) {
			// 	var pos = ( i - 1 ) * step;
			// 	current += group.distribution.Evaluate(pos) > 0 ? group.distribution.Evaluate(pos) : 0;
			// 	attemptSpectrum.Add(current);
			// }
			//
			// float rand = Random.Range(0f, 1f) * current;
			//
			// int first = -1;
			//
			// if ( attemptSpectrum.Count > 0 ) {
			// 	for ( int i = 0; i < attemptSpectrum.Count; i++ ) {
			// 		if ( first == -1 && attemptSpectrum[i] >= rand ) {
			// 			first = i;
			// 		}		
			// 	}
			// 	// Debug.Log($"rand: {rand} total{current} max{group.max} | {attemptSpectrum[first]}, --- {first} ---");	
			// }
			// else {
			// 	Debug.Log("Nothing\nTable Was Empty or the Probability was 0 and smaller");
			// }
			//
			// if ( first < 0 ) {
			// 	first = 0;
			// }
			// return first;
		}
		
		public static LootObject RollOnTable(LootGroup group) {

			float total = group.tabel.Sum(o => o.weight);
			
			// create distribution
			SortedDictionary<float, LootObject> lootSpectrum = new SortedDictionary<float, LootObject>();
			float currentEdge = 0; 
			foreach ( var lootObj in group.tabel ) {
				if ( lootObj.weight > 0 ) {
					currentEdge += lootObj.weight;
					lootSpectrum.Add(currentEdge, lootObj);	
				}
			}

			// random roll
			float rand = Random.Range(0f, 1f) * total;
			KeyValuePair<float, LootObject> first = default;
			if ( lootSpectrum.Count > 0 ) {
				first = lootSpectrum.FirstOrDefault(pair => pair.Key > rand);
				// Debug.Log($"rand: {rand} total{total}, {first.Key} {first.Value.type} {first.Value.itemType?.name}");	
			}
			else {
				Debug.Log("Nothing\nTable Was Empty or the Probability was 0 and smaller");
			}

			return first.Value;
		}

		public static LootDrop GenerateLoot(LootTableSO lootTable, int num) {
			var loot = new LootDrop();
			List<LootObject> lootObjects = new List<LootObject>();
			// lootTable.LootTable.Roll
			List<int> attemptsList = new List<int>();
			foreach ( var lootGroup in lootTable.LootTable ) {
				// LootGenerator.RollOnTable(group);
				int attempts = LootGenerator.RollForTableAttempts(lootGroup);
				attemptsList.Add(attempts);
				for ( int i = 0; i < attempts; i++ ) {
					lootObjects.Add(LootGenerator.RollOnTable(lootGroup));	
				}
			}

			// Debug.Log(attemptsList.AllToString());
			
			var itemsDropped =
				from l in lootObjects
				group l by l
				into lootGroup
				select new {
					Type = lootGroup.Key.type,
					Item = lootGroup.Key.itemType,
					Count = lootGroup.Count(),
				};

			var sb = new StringBuilder();
			foreach ( var x1 in itemsDropped ) {
				sb.Append($"Item dropped: {x1.Type} {x1.Item} {x1.Count}\n");	
			}
			Debug.Log(sb.ToString());
			
			
			//build LootDrop from rolled loot

			foreach ( var lootObject in lootObjects ) {
				if ( lootObject.type == LootType.Item ) {
					loot.items.Add(lootObject.itemType);
				}
				//todo add gold & experience
			}
			
			return loot;
		}
	}
}