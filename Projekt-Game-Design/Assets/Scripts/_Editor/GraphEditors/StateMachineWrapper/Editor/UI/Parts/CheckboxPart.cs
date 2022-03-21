using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI {
	public class CheckboxPart : BaseModelUIPart {
		private SerializedProperty checkboxValue;

		public CheckboxPart(string name, IGraphElementModel model,
			IModelUI ownerElement,
			string parentClassName) : base(name, model, ownerElement, parentClassName) {
		}

		public static CheckboxPart Create(
			string name, IGraphElementModel model, IModelUI modelUI, string parentClassName) {
			if ( model is INodeModel ) {
				return new CheckboxPart(name, model, modelUI, parentClassName);
			}

			return null;
		}

		public override VisualElement Root => CheckboxContainer;
		VisualElement CheckboxContainer { get; set; }
		PropertyField PropertyField { get; set; }

		protected override void BuildPartUI(VisualElement parent) {
			if (!(m_Model is State_NodeModel stateNodeModel))
				return;
			
			CheckboxContainer = new VisualElement();

			if ( checkboxValue != null ) {
				PropertyField = new PropertyField(checkboxValue);
				
				CheckboxContainer.Add(PropertyField);
			}

			var toggle = new Toggle();
			CheckboxContainer.Add(toggle);
			
			parent.Add(CheckboxContainer);
		}

		protected override void UpdatePartFromModel() {
			
		}
	}
}