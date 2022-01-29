using UnityEditor;
using UnityEngine;

namespace Util.Editor.SpritePropertyDrawer {
	// [CustomPropertyDrawer(typeof(Texture2D))]
	public class TextureDrawer : PropertyDrawer {
		
		private static GUIStyle s_TempStyle = new GUIStyle();
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			if (property.serializedObject.isEditingMultipleObjects) {
				GUI.Label(position, "Sprite multiediting not supported");
				return;
			}

			var topMargin = 5;
			//
			// var ident = EditorGUI.indentLevel;
			// EditorGUI.indentLevel = 0;
 	 //
			// Rect spriteRect;
   //   
			// //create object field for the sprite
			// spriteRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			// property.objectReferenceValue = EditorGUI.ObjectField(spriteRect, property.name, property.objectReferenceValue, typeof(Sprite), false);
 	 //
			// //if this is not a repain or the property is null exit now
			// if (Event.current.type != EventType.Repaint || property.objectReferenceValue == null)
			// 	return;
 	 //
			//
			//
			// // //draw a sprite
			// // Sprite sp = property.objectReferenceValue as Sprite;
 		// //
			// // spriteRect.y += EditorGUIUtility.singleLineHeight+4;
			// // spriteRect.width = 64;
			// // spriteRect.height = 64;    
			// // s_TempStyle.normal.background = sp.texture;
			// // s_TempStyle.Draw(spriteRect, GUIContent.none, false, false, false, false);
   //           
			// EditorGUI.indentLevel = ident;
			//
			Rect spriteRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			property.objectReferenceValue = EditorGUI.ObjectField(spriteRect, property.name, property.objectReferenceValue, typeof(Texture2D), false);

			var size = 300;
			var textureSize = position.width < size ? position.width : size;
			
			spriteRect.height = textureSize;
			spriteRect.width = textureSize;
			spriteRect.y += EditorGUIUtility.singleLineHeight + topMargin;
			
			Texture2D texture = property.objectReferenceValue as Texture2D;
			EditorGUI.DrawPreviewTexture(spriteRect, texture);

			//todo
			// SetDrawerHeight(property, textureSize);
		}
 
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			return base.GetPropertyHeight(property, label) + 70f;
		}
		
		//todo 
		// void SetDrawerHeight(SerializedProperty property, float height) {
		// 	var drawerHeight = property.FindPropertyRelative("drawerHeight");
		// 	drawerHeight.floatValue += height;
		// }
	}
}