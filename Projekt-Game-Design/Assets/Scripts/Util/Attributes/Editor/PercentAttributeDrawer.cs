using UnityEditor;
using UnityEngine;

namespace GDP01.Util.Attributes.Editor {
	[CustomPropertyDrawer(typeof(PercentAttribute))]
	public class PercentAttributeDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property,
			GUIContent label) {
			if ( property.propertyType == SerializedPropertyType.Float ) {
				Rect dropRateRect = new Rect(position);
				// dropRateRect.width = 100;
				// EditorGUI.LabelField(dropRateRect, property.displayName);
				// dropRateRect.x += 100;
				dropRateRect.width = ( position.width / 2 ); 
				var newValue = EditorGUI.FloatField(dropRateRect, property.displayName, property.floatValue);
				property.floatValue = newValue;
				dropRateRect.x += dropRateRect.width + 10;
				var dropRateInPercent = $"{property.floatValue:0.00%}";
				EditorGUI.LabelField(dropRateRect, dropRateInPercent);	
			}
			else {
				EditorGUI.LabelField(position, "PercentAttribute needs a float field");
			}
		}

		public override float GetPropertyHeight(SerializedProperty property,
			GUIContent label) {
			return base.GetPropertyHeight(property, label);
		}
	}
}