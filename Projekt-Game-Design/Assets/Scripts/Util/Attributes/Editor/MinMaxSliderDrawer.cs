using UnityEditor;
using UnityEngine;

// source: https://forum.unity.com/threads/custom-attributes.359888/

namespace GDP01.Util.Attributes.Editor {
	[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
	public class MinMaxSliderDrawer : PropertyDrawer {
		//https://github.com/GucioDevs/SimpleMinMaxSlider/blob/master/Assets/SimpleMinMaxSlider/Scripts/Editor/MinMaxSliderDrawer.cs
		
		private void CapValues(ref float minValue, ref float maxValue, float minLimit, float maxLimit) {
			minValue = minValue < minLimit ? minLimit : minValue;
			maxValue = maxValue > maxLimit ? maxLimit : maxValue;
		}

		private Vector2 GetMinMaxVector2(float minValue, float maxValue) {
			return new Vector2(minValue > maxValue ? maxValue : minValue, maxValue);			
		}
		
		private Vector2Int GetMinMaxVector2Int(float minValue, float maxValue) {
			return Vector2Int.FloorToInt(new Vector2(minValue > maxValue ? maxValue : minValue, maxValue));
		}
		
		private Vector4 GetMinMaxVector4Int(float minValue, float maxValue, float minLimit, float maxLimit) {
			var value = GetMinMaxVector2Int(minValue, maxValue);
			return new Vector4(value.x, value.y, minLimit, maxLimit);
		}
		
		public override void OnGUI(Rect position, SerializedProperty property,
			GUIContent label) {

			EditorGUI.BeginProperty(position, label, property);
			
			var minMaxSlider = ( MinMaxSliderAttribute ) attribute;

			if ( !( minMaxSlider.GetSliderType() == typeof(void) ) ) {
				var propertyVec4 = property.vector4Value;
				minMaxSlider.SetMinMax(propertyVec4.z, propertyVec4.w);
			}
			
			float min = 0;
			float max = 1;
			float minLimit = minMaxSlider.Min;
			float maxLimit = minMaxSlider.Max;

			Rect controlRect = EditorGUI.PrefixLabel(position, label);

			int labelOffset = -0;
			int numberFieldWidth = 50;
			int padding = 5;

			controlRect.x += labelOffset;
			controlRect.width -= labelOffset;
			
			Rect minNumberRect = new Rect(controlRect.x, controlRect.y, numberFieldWidth - padding, controlRect.height);
			controlRect.x += numberFieldWidth;
			controlRect.width -= 2* ( numberFieldWidth);
			Rect minMaxSliderRect = new Rect(controlRect.x, controlRect.y, controlRect.width, controlRect.height);
			controlRect.x += controlRect.width + padding;
			Rect maxNumberRect = new Rect(controlRect.x, controlRect.y, numberFieldWidth - padding, controlRect.height);
			
			if ( property.propertyType == SerializedPropertyType.Vector2 ) {
				var value = property.vector2Value;
				min = value.x;
				max = value.y;
				
				//F2 limits the float to two decimal places (0.00).
				min = EditorGUI.FloatField(minNumberRect, float.Parse(min.ToString("F2")));
				max = EditorGUI.FloatField(maxNumberRect, float.Parse(max.ToString("F2")));
				
				EditorGUI.MinMaxSlider(minMaxSliderRect, ref min, ref max, minLimit, maxLimit);

				CapValues(ref min, ref max, minLimit, maxLimit);
				property.vector2Value = GetMinMaxVector2(min, max);

			} else if ( property.propertyType == SerializedPropertyType.Vector2Int ) {
				var value = property.vector2IntValue;
				min = value.x;
				max = value.y;
				
				min = EditorGUI.FloatField(minNumberRect, min);
				max = EditorGUI.FloatField(maxNumberRect, max);
				
				EditorGUI.MinMaxSlider(minMaxSliderRect, ref min, ref max, minLimit, maxLimit);
				CapValues(ref min, ref max, minLimit, maxLimit);
				property.vector2IntValue = GetMinMaxVector2Int(min, max);
			} else if ( property.propertyType == SerializedPropertyType.Vector4 ) {
				var value = property.vector4Value;
				min = value.x;
				max = value.y;
				
				min = EditorGUI.FloatField(minNumberRect, min);
				max = EditorGUI.FloatField(maxNumberRect, max);
				
				EditorGUI.MinMaxSlider(minMaxSliderRect, ref min, ref max, minLimit, maxLimit);
				CapValues(ref min, ref max, minLimit, maxLimit);
				property.vector4Value = GetMinMaxVector4Int(min, max, minLimit, maxLimit);
			}
			else {
				EditorGUI.LabelField(position, label.text, "Use MinMaxSlider with Vector2 or Vector2Int.");
			}

			property.serializedObject.ApplyModifiedProperties();
			
			EditorGUI.EndProperty();
		}
		

		public override float GetPropertyHeight(SerializedProperty property,
			GUIContent label) {
			return base.GetPropertyHeight(property, label);
		}
	}
}