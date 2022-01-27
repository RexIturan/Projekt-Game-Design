using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUIUtility;

namespace GDP01.UI.Structure.Editor {
	[CustomPropertyDrawer(typeof(ScreenController))]
	public class ScreenControllerDrawer : PropertyDrawer {

		private static float topPadding = 2;
		private static float propertyHeighPadding = 2 * topPadding + singleLineHeight;
		
		private void DisableButtonGUI(ref Rect position, float width, ScreenController screenController, bool enabled) {
			GUI.enabled = enabled;
			position.width = width;
			if(GUI.Button(position,"Disable")) {
				screenController.Deactivate();
			}
			position.x += width;
			GUI.enabled = true;
		}
		
		private void EnableButtonGUI(ref Rect position, float width, ScreenController screenController, bool enabled) {
			GUI.enabled = enabled;
			position.width = width;
			if(GUI.Button(position,"Enable")) {
				screenController.Activate();
			}
			position.x += width;
			GUI.enabled = true;
		}
		
		public override void OnGUI(Rect position, SerializedProperty property,
			GUIContent label) {

			var name = property.objectReferenceValue?.name ?? property.name;
			var screenController = (ScreenController) property.objectReferenceValue;
			//do nothing
			// name + button + field

			float propertyWidth = position.width;
			float labelWidth = 80;
			float buttonWidth = ( propertyWidth - labelWidth ) / 2;
			Rect controlRect = new Rect(position);
			Rect objectFieldRect = new Rect(position);

			controlRect.width = labelWidth;
			controlRect.height = singleLineHeight;
			objectFieldRect.height = singleLineHeight;
			// controlRect.y += ( propertyHeighPadding - singleLineHeight ) / 2;
			EditorGUI.LabelField(controlRect, name);
			controlRect.x += labelWidth;
			
			if ( screenController is { } ) {
				EnableButtonGUI(ref controlRect, buttonWidth, screenController, !screenController.Active);
				DisableButtonGUI(ref controlRect, buttonWidth, screenController, screenController.Active);
			}
			
			objectFieldRect.y += singleLineHeight + topPadding;
			EditorGUI.ObjectField(objectFieldRect, property, GUIContent.none);
		}

		public override float GetPropertyHeight(SerializedProperty property,
			GUIContent label) {
			return base.GetPropertyHeight(property, label) + propertyHeighPadding;
		}
	}
}