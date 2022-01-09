using QuestSystem.ScriptabelObjects;
using UnityEditor;
using UnityEngine;

namespace QuestSystem.Editor {
	[CustomPropertyDrawer(typeof(Task_Wrapper))]
	public class TaskWrapperPropertyDrawer : PropertyDrawer {
		// bool showProperty = false;
		// float DrawerHeight = 0;
		
		// string button = "-";

		// Draw the property inside the given rect
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			var indent = EditorGUI.indentLevel;
			var lineHight = EditorGUIUtility.singleLineHeight;
			var marginTop = 5;
			var marginLeft = 5;

			// var wso = new SerializedObject(property.objectReferenceValue);
			// wso.Update();
			
			var name = property.FindPropertyRelative("name");
			var type = property.FindPropertyRelative("type");
			var taskProperty = property.FindPropertyRelative("task");
			var showTask = property.FindPropertyRelative("showProperty");
			var button = property.FindPropertyRelative("button");
			var drawerHeight = property.FindPropertyRelative("drawerHeight");
			// showProperty = showTask.boolValue;
			
			var r = position;

			drawerHeight.floatValue = 0;
			// DrawerHeight = 0;
			
			r.height = lineHight;
			r.y += marginTop;
			// r.x += marginLeft;
			r.width = r.width/2 - marginLeft;
			EditorGUI.PropertyField(r, name, GUIContent.none);
			r.x += r.width + marginLeft;
			EditorGUI.PropertyField(r, type, GUIContent.none);
			// r.x += 100;
			// EditorGUI.PropertyField(r, showTask, GUIContent.none);

			position.y += r.height + marginTop;
			SetDrawerHeight(property, r.height + marginTop);
			
			Rect temp = new Rect(position.x, position.y, 16, 16);
			if ( GUI.Button(temp, button.stringValue) ) {
				EditorUtility.SetDirty(property.serializedObject.targetObject);
				if ( showTask.boolValue ) {
					showTask.boolValue = false;
					// showProperty = false;
					// button = "-";
				}
				else {
					showTask.boolValue = true;
					// showProperty = true;
					// button = "|";
				}
			}

			if ( showTask.boolValue ) {
				button.stringValue = "-";
			}
			else {
				button.stringValue = "|";
				
			}
			
			if ( taskProperty.objectReferenceValue is null ) {
				position.x += 20;
				var createButtonWidth = 100;
				Rect createTaskButtonRect = new Rect(position.x, position.y, createButtonWidth, 16);

				if ( GUI.Button(createTaskButtonRect, "createNew") ) {
					if ( property.serializedObject.targetObject is QuestSO quest ) {
						var task = quest.CreateNewTask((TaskType)type.enumValueIndex);
						if ( task is { } ) {
							taskProperty.objectReferenceValue = task;
						}
						// task.objectReferenceValue = 
						// property.objectReferenceValue = quest.CreateNewTask(TaskConditionType.Key_Press);
						// //todo update
						// property.serializedObject.Update();
					}
				}
				
				position.x += createButtonWidth;
				position.width -= 20 + createButtonWidth;
			}
			
			position.height = EditorGUIUtility.singleLineHeight;
			position.x += 20;
			position.width -= 20;
			EditorGUI.ObjectField(position, taskProperty, GUIContent.none);
			// EditorGUI.PropertyField(position, taskProperty);
			position.y += 20;

			if ( !showTask.boolValue ) return;
			
			if (taskProperty.objectReferenceValue is { } ) {
				// position.x += 20;
				// position.width -= 40;
				
				// var so = e.serializedObject;
				var so = new SerializedObject(taskProperty.objectReferenceValue);
				so.Update();
				var prop = so.GetIterator();
				//Debug.Log(" prop.hasVisibleChildren " + prop.hasVisibleChildren);
				prop.NextVisible(true);
				int depthChilden = 0;
				bool showChilden = false;
				while ( prop.NextVisible(true) ) {
					
					var propHeight = EditorGUI.GetPropertyHeight(prop, false);
					float elementOffset = 0;
					
					if ( prop.depth == 0 ) {
						showChilden = false;
						depthChilden = 0;
					}
			
					if ( showChilden && prop.depth > depthChilden ) {
						continue;
					}
			
					position.height = propHeight;
					EditorGUI.indentLevel = indent + prop.depth;
					if ( EditorGUI.PropertyField(position, prop) ) {
						showChilden = false;
					}
					else {
						showChilden = true;
						depthChilden = prop.depth;
					}
			
					position.y += propHeight + elementOffset;
					SetDrawerHeight(property, propHeight);
				}
			
				if ( GUI.changed ) {
					so.ApplyModifiedProperties();
				}
				
				// position.x -= 20;
				// position.width += 40;
			}
			// EditorGUI.EndFoldoutHeaderGroup();

			if ( property.serializedObject.ApplyModifiedProperties() ) {
				
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			var drawerHeight = property.FindPropertyRelative("drawerHeight");
			float height = base.GetPropertyHeight(property, label);
			height += drawerHeight.floatValue;
			return height;
		}

		void SetDrawerHeight(SerializedProperty property, float height) {
			var drawerHeight = property.FindPropertyRelative("drawerHeight");
			drawerHeight.floatValue += height;
		}
	}
}
