using GDP01.Loot.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace GDP01.Loot.Editor {
	[CustomEditor(typeof(LootTableSO))]
	public class LootTableSOEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			
			base.OnInspectorGUI();

			var lootTable = ( LootTableSO )target;
			
			if (GUILayout.Button("Roll on each Table")) {  
				// call on button click
				lootTable.RollOnTables();  
			}
		}
	}
}