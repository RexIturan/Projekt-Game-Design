using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI {
	public class ConditionListPart : BaseModelUIPart {
		public static readonly string ussClassName = "ge-sample-action-list-node-part";
		public static readonly string collapsedUssClassName = "ge-node--collapsed";
		public static readonly string portNotConnectedUssClassName = "ge-port--not-connected";
		
		public ConditionListPart(string name, IGraphElementModel model, IModelUI ownerElement,
			string parentClassName) : base(name, model, ownerElement, parentClassName) { }

		public override VisualElement Root => Container;
		private VisualElement Container { get; set; }
		ReorderableList ReorderableList { get; set; }
		private IMGUIContainer ImguiContainer { get; set; }

		private TransitionTableSO currentTT;
		
		public static ConditionListPart Create(string name, IGraphElementModel model, IModelUI modelUI,
			string parentClassName) {
			if ( model is INodeModel ) {
				return new ConditionListPart(name, model, modelUI, parentClassName);
			}

			return null;
		}

		protected override void BuildPartUI(VisualElement parent) {
			if (!(m_Model is Transition_NodeModel transitionNodeModel))
				return;
			
			Container = new VisualElement() {
				style = { width = 200}
			};
			// Container = new ResizableElement();
			Container.AddToClassList(ussClassName);
			Container.AddToClassList(m_ParentClassName.WithUssElement(PartName));

			ImguiContainer = new IMGUIContainer();
			
			if ( transitionNodeModel.hasValidValues() ) {
				CreateReorderableList(transitionNodeModel);
			}
			
			Container.Add(ImguiContainer);
			parent.Add(Container);
		}

		protected override void UpdatePartFromModel() {
			if (!(m_Model is Transition_NodeModel transitionNode))
				return;
			
			bool collapsed = (m_Model as ICollapsible)?.Collapsed ?? false;
			Container.EnableInClassList(collapsedUssClassName, collapsed);
			Container.EnableInClassList(portNotConnectedUssClassName, collapsed);
			
			if ( !transitionNode.hasValidValues() ) {
				transitionNode.transitionTable = null;
				ReorderableList = null;
				// ImguiContainer.onGUIHandler = () => {};
				// ReorderableList = null;
			}
			
			if ( transitionNode.hasValidValues() && 
			     ReorderableList == null || 
			     currentTT != transitionNode.transitionTable) {
				
				CreateReorderableList(transitionNode);
			}
		}

		void CreateReorderableList(Transition_NodeModel transitionNode) {
			SerializedObject so = new SerializedObject(transitionNode.transitionTable);

			
			var property = so.FindProperty("_transitions").GetArrayElementAtIndex(transitionNode.transitionID).FindPropertyRelative("Conditions");
			// var transitionItem = property.GetArrayElementAtIndex(transitionNode.transitionID);
			// var conditions = transitionItem.FindPropertyRelative("Conditions");
			
			var copy = property.Copy();
				
			ReorderableList = new ReorderableList(so, copy);
			
			if ( !transitionNode.editable ) {
				ReorderableList.draggable = false;
				ReorderableList.displayAdd = false;
				ReorderableList.displayRemove = false;
			}
			
			ImguiContainer.onGUIHandler = () => {
				ReorderableList.DoLayoutList();
			};

			ReorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 3;
			ReorderableList.drawHeaderCallback = rect => {
				EditorGUILayout.BeginHorizontal();
				EditorGUI.LabelField(rect, "Conditions");
				rect.x += rect.width - 40;
				rect.width = 40;
				if ( GUI.Button(rect, "edit") ) {
					// todo(vincent) command?
					using (var graphUpdater = m_OwnerElement.CommandDispatcher.State.GraphViewState.UpdateScope) {
						transitionNode.editable = !transitionNode.editable;
						graphUpdater.MarkChanged(transitionNode);
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

				var operatorProp = prop.FindPropertyRelative("Operator");
				var conditionProp = prop.FindPropertyRelative("Condition");
				var resultProp = prop.FindPropertyRelative("ExpectedResult");
				
				if (conditionProp != null)
				{
					var label = conditionProp.name ?? "";

					GUI.Label(r, label, EditorStyles.boldLabel);
					r.y += EditorGUIUtility.singleLineHeight;

					EditorGUI.BeginDisabledGroup(!transitionNode.editable);
					{
						if ( conditionProp.objectReferenceValue == null ) {
							GUI.color = Color.red;
						}
						
						var refRect = r;
						EditorGUI.PropertyField(refRect, conditionProp, GUIContent.none);
						// r.x += 42;
						r.y += EditorGUIUtility.singleLineHeight;

						r.width = r.width / 2;
						EditorGUI.PropertyField(r, operatorProp, GUIContent.none);
						r.x += r.width;
						EditorGUI.PropertyField(r, resultProp, GUIContent.none);

						GUI.color = GUI.contentColor;
					}
					EditorGUI.EndDisabledGroup();
				}
				else
					EditorGUI.PropertyField(r, prop, GUIContent.none);
			};
		}
	}
}