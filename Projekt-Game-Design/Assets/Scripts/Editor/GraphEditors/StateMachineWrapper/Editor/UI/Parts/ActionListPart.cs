using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI {
	public class ActionListPart : BaseModelUIPart {
		public static readonly string ussClassName = "ge-sample-action-list-node-part";
		public static readonly string collapsedUssClassName = "ge-node--collapsed";
		public static readonly string portNotConnectedUssClassName = "ge-port--not-connected";
		
		public ActionListPart(string name, IGraphElementModel model, IModelUI ownerElement,
			string parentClassName) : base(name, model, ownerElement, parentClassName) { }

		public override VisualElement Root => Container;
		private VisualElement Container { get; set; }
		ReorderableList ReorderableList { get; set; }
		private IMGUIContainer ImguiContainer { get; set; }

		private StateSO currentState;
		
		public static ActionListPart Create(string name, IGraphElementModel model, IModelUI modelUI,
			string parentClassName) {
			if ( model is INodeModel ) {
				return new ActionListPart(name, model, modelUI, parentClassName);
			}

			return null;
		}

		protected override void BuildPartUI(VisualElement parent) {
			if (!(m_Model is State_NodeModel stateNode))
				return;
			
			Container = new VisualElement() {
				style = { width = 200}
			};
			// Container = new ResizableElement();
			Container.AddToClassList(ussClassName);
			Container.AddToClassList(m_ParentClassName.WithUssElement(PartName));

			ImguiContainer = new IMGUIContainer();
			
			if ( stateNode.state != null ) {
				CreateReorderableList(stateNode);
			}
			
			Container.Add(ImguiContainer);
			parent.Add(Container);
		}

		protected override void UpdatePartFromModel() {
			if (!(m_Model is State_NodeModel stateNode))
				return;
			
			bool collapsed = (m_Model as ICollapsible)?.Collapsed ?? false;
			Container.EnableInClassList(collapsedUssClassName, collapsed);
			Container.EnableInClassList(portNotConnectedUssClassName, collapsed);
			
			if ( stateNode.state is null ) {
				stateNode.state = null;
				ReorderableList = null;
				// ImguiContainer.onGUIHandler = () => {};
				// ReorderableList = null;
			}
			
			if ( stateNode.state != null && ReorderableList == null || currentState != stateNode.state) {
				CreateReorderableList(stateNode);
			}
		}

		void CreateReorderableList(State_NodeModel stateNode) {
			SerializedObject so = new SerializedObject(stateNode.state);
			stateNode.state = stateNode.state;

			var property = so.FindProperty("_actions");
			var copy = property.Copy();
				
			ReorderableList = new ReorderableList(so, copy);
			
			if ( !stateNode.editable ) {
				ReorderableList.draggable = false;
				ReorderableList.displayAdd = false;
				ReorderableList.displayRemove = false;
				
			}
			
			ImguiContainer.onGUIHandler = () => {
				ReorderableList.DoLayoutList();
			};

			ReorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 2;
			ReorderableList.drawHeaderCallback = rect => {
				EditorGUILayout.BeginHorizontal();
				EditorGUI.LabelField(rect, "State Actions");
				rect.x += rect.width - 40;
				rect.width = 40;
				if ( GUI.Button(rect, "edit") ) {
					// todo(vincent) command?
					using (var graphUpdater = m_OwnerElement.CommandDispatcher.State.GraphViewState.UpdateScope) {
						stateNode.editable = !stateNode.editable;
						graphUpdater.MarkChanged(stateNode);
					} 
				}
				EditorGUILayout.EndHorizontal();
			};

			ReorderableList.drawElementCallback = (rect, index, active, focused) => {
				var r = rect;
				r.height = EditorGUIUtility.singleLineHeight;
				// r.y += 5;
				// r.x += 5;

				
				var prop = ReorderableList.serializedProperty.GetArrayElementAtIndex(index);
				if (prop.objectReferenceValue != null)
				{
					var label = prop.objectReferenceValue.name;

					GUI.Label(r, label, EditorStyles.boldLabel);
					r.y += EditorGUIUtility.singleLineHeight;
					
					EditorGUI.BeginDisabledGroup(!stateNode.editable);
					
					EditorGUI.PropertyField(r, prop, GUIContent.none);
					
					EditorGUI.EndDisabledGroup();
				}
				else
					EditorGUI.PropertyField(r, prop, GUIContent.none);
			};
		}
	}
}