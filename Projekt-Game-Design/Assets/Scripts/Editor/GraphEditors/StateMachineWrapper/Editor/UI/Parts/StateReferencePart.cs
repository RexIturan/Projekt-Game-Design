using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using Editor.GraphEditors.StateMachineWrapper.Editor.UI.Commands;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI {
	public class StateReferencePart : BaseModelUIPart {
		public static readonly string ussClassName = "ge-sample-state-reference-node-part";
		// public static readonly string collapsedUssClassName = "ge-node--collapsed";
		// public static readonly string portNotConnectedUssClassName = "ge-port--not-connected";
		
		public StateReferencePart(string name, IGraphElementModel model, IModelUI ownerElement,
			string parentClassName) : base(name, model, ownerElement, parentClassName) { }

		public static StateReferencePart Create(
			string name, IGraphElementModel model, IModelUI modelUI, string parentClassName) {
			if ( model is INodeModel ) {
				return new StateReferencePart(name, model, modelUI, parentClassName);
			}

			return null;
		}

		public override VisualElement Root => Container;
		VisualElement Container { get; set; }
		EditableLabel StateLabel { get; set; }
		ObjectField StateReference { get; set; }

		void OnStateReferenceChange(ChangeEvent<Object> evt) {
			if ( !( m_Model is State_NodeModel stateNodeModel ) )
				return;

			Debug.Log("state ref Callback!");
			
			m_OwnerElement.CommandDispatcher.Dispatch(
				new SetStateReferenceCommand(( StateSO )evt.newValue, new[] { stateNodeModel }));
		}

		protected override void BuildPartUI(VisualElement parent) {
			if ( !( m_Model is State_NodeModel stateNodeModel ) )
				return;

			Container = new VisualElement();
			Container.AddToClassList(ussClassName);
			Container.AddToClassList(m_ParentClassName.WithUssElement(PartName));


			StateLabel = new EditableLabel();
			StateReference = new ObjectField() {
				name = "State Reference Field",
				allowSceneObjects = false,
				objectType = typeof(StateSO),
			};
			
			StateLabel.SetValueWithoutNotify(stateNodeModel.stateName);
			StateReference.RegisterCallback<ChangeEvent<Object>>(OnStateReferenceChange);
			
			if ( stateNodeModel.state != null ) {
				StateReference.value = stateNodeModel.state;
			}

			Container.Add(StateLabel);
			Container.Add(StateReference);
			parent.Add(Container);
		}

		protected override void UpdatePartFromModel() {
			if ( !( m_Model is State_NodeModel stateNodeModel ) )
				return;
			
			// bool collapsed = (m_Model as ICollapsible)?.Collapsed ?? false;
			// StateReference.EnableInClassList(collapsedUssClassName, collapsed);
			// StateReference.EnableInClassList(portNotConnectedUssClassName, collapsed);
			
			StateLabel.SetValueWithoutNotify(stateNodeModel.stateName);
			
			if ( stateNodeModel.state != null ) {
				StateReference.value = stateNodeModel.state;
			}
		}
	}
}