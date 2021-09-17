using System.Configuration;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GDP01.Util.ScriptableObjects.Editor {
    
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : PropertyDrawer {
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            return base.CreatePropertyGUI(property);
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            
            EditorGUI.BeginProperty(position, label, property);
            //
            // Draw Lable
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            
            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            //
            bool useConstant = property.FindPropertyRelative("UseConstant").boolValue;
            
            var dropdownRect = new Rect(position.position, new Vector2(1, 1) * 20);
            position.xMin += 20;
            var textFieldRect = new Rect(position);
            var objectFieldRect = new Rect(position);
        
            dropdownRect.xMin -= 1;
            dropdownRect.yMin -= 1.5f;
            
            if (EditorGUI.DropdownButton(
                dropdownRect, 
                new GUIContent(GetTexture(), "drop down"), 
                FocusType.Keyboard,
                GUIStyle.none)
            ) {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Constant"), useConstant, () => SetProperty(property, true));
                
                menu.AddItem(new GUIContent("Variable"), !useConstant, () => SetProperty(property, false));
                // menu.DropDown(position);
                menu.ShowAsContext();
            }
            
            if (useConstant) {
        
                EditorGUI.PropertyField(textFieldRect, property.FindPropertyRelative("ConstantValue"), GUIContent.none);
                // float value = property.FindPropertyRelative("ConstantValue").floatValue;
                // string newValue = EditorGUI.TextField(textFieldRect, value.ToString());
                // float.TryParse(newValue, out value);
                // property.FindPropertyRelative("ConstantValue").floatValue = value;
            }
            else {
                EditorGUI.ObjectField(objectFieldRect, property.FindPropertyRelative("Variable"), GUIContent.none);
            }
        
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return base.GetPropertyHeight(property, label);
        }
        
        private void SetProperty(SerializedProperty property, bool value) {
            var propRelative = property.FindPropertyRelative("UseConstant");
            propRelative.boolValue = value;
            property.serializedObject.ApplyModifiedProperties();
        }
        
        private Texture GetTexture() {
            return EditorGUIUtility.FindTexture("CollabChangesDeleted Icon");
        }
    }
}