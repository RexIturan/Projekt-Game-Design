using GDP01.Loot.Types;
using UnityEditor;
using UnityEngine;
using Util.Extensions;
using static UnityEditor.EditorGUIUtility;

namespace GDP01.Loot.Editor {
	[CustomPropertyDrawer(typeof(LootObject))]
	public class LootObjectDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property,
			GUIContent label) {
			
			var typeProperty = property.FindPropertyRelative("type");
			var itemProperty = property.FindPropertyRelative("itemType");
			var weightProperty = property.FindPropertyRelative("weight");
			var dropRateProperty = property.FindPropertyRelative("dropRate");
			var dropAmountProperty = property.FindPropertyRelative("dropAmount");
			var drawerHeight = property.FindPropertyRelative("propertyHeight");
			drawerHeight.floatValue = 0;

			EditorGUI.BeginProperty(position, label, property);

			float marginTop = 4;
			float lineHeight = singleLineHeight;
			float totalLineHeight = singleLineHeight + marginTop;
			
			Rect r = new Rect(position.x, position.y + marginTop/2, position.width, lineHeight);

			EditorGUI.PropertyField(r, typeProperty);

			if ( typeProperty.enumValueIndex == (int)LootType.Item ) {
				r.y += totalLineHeight;
				EditorGUI.PropertyField(r, itemProperty);
				SetDrawerHeight(property, totalLineHeight);

				// Set range of items to be dropped to 1
				// TODO: If and when enemies are supposed to drop multiple items at once, remove this and re-include dropAmount/propertyHeight
				dropAmountProperty.vector4Value = new Vector4(1,1,1,1);
			}
			
			r.y += totalLineHeight;
			EditorGUI.PropertyField(r, weightProperty);
			SetDrawerHeight(property, totalLineHeight);
			
			r.y += totalLineHeight;
			EditorGUI.PropertyField(r, dropRateProperty);
			SetDrawerHeight(property, totalLineHeight);

			if( typeProperty.enumValueIndex != (int)LootType.Item && typeProperty.enumValueIndex != ( int )LootType.Nothing ) {
				r.y += totalLineHeight;
				EditorGUI.PropertyField(r, dropAmountProperty);
				SetDrawerHeight(property, totalLineHeight);

				// set limit and save it in the vec4
				var value = dropAmountProperty.vector4Value;
				r.y += totalLineHeight;
				var limits = Vector2Int.FloorToInt(dropAmountProperty.vector4Value.ZWInt());
				var newLimits = EditorGUI.Vector2IntField(r, "Limits", limits);
				SetDrawerHeight(property, totalLineHeight);

				if ( newLimits.y < newLimits.x ) {
					newLimits.y = newLimits.x;
				}
				if ( newLimits.x > newLimits.y ) {
					newLimits.x = newLimits.y;
				}
				value.SetZW(newLimits);
				dropAmountProperty.vector4Value = value;
			}

			if ( property.serializedObject.ApplyModifiedProperties() ) {
				EditorUtility.SetDirty(property.serializedObject.targetObject);
			}
			
			SetDrawerHeight(property, marginTop);
			
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property,
			GUIContent label) {
			var drawerHeight = property.FindPropertyRelative("propertyHeight");
			float height = base.GetPropertyHeight(property, label);
			height += drawerHeight.floatValue;
			return height;
		}
		
		void SetDrawerHeight(SerializedProperty property, float height) {
			var drawerHeight = property.FindPropertyRelative("propertyHeight");
			drawerHeight.floatValue += height;
		}
	}
}