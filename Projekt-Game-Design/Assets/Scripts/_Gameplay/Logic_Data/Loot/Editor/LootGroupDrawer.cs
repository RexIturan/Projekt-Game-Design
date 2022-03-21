using GDP01.Loot.Types;
using UnityEditor;
using UnityEngine;

namespace GDP01.Loot.Editor {
	public class LootGroupDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property,
			GUIContent label) {

			// var lootObjectsProperty = property.FindPropertyRelative();
			
			
			// EditorGUI.PropertyField(r, dropRateProperty);
			
		}

		public override float GetPropertyHeight(SerializedProperty property,
			GUIContent label) {
			return base.GetPropertyHeight(property, label);
		}
	}
}