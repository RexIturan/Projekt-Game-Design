using System.Linq;
using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace _Structure._GraphView.LevelGraph.UI.Parts {
	public class LevelConnection_PortContainer_Part : BaseModelUIPart {
		public static readonly string ussClassName = "ge-level-connection-container-part";
		public static readonly string leftPortsUssName = "left";
		public static readonly string rightPortsUssName = "right";
		public static readonly string mapContainerUssName = "map";
		
		public static LevelConnection_PortContainer_Part Create(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName) {
			if (model is IPortNodeModel) {
				return new LevelConnection_PortContainer_Part(name, model, ownerElement, parentClassName);
			}

			return null;
		}
		
		public LevelConnection_PortContainer_Part(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName) : base(name, model, ownerElement, parentClassName) { }
		
		protected PortContainer m_PortContainer;
		
		protected PortContainer _LeftPortContainer;
		protected PortContainer _RightPortContainer;
		protected VisualElement _MapContainer;
		protected VisualElement m_Root;

		/// <inheritdoc />
		public override VisualElement Root => m_Root;
		
		protected override void BuildPartUI(VisualElement container) {
			if (m_Model is IPortNodeModel) {
				m_Root = new VisualElement { name = PartName };
				m_Root.AddToClassList(ussClassName);
				m_Root.AddToClassList(ussClassName.WithUssElement(PartName));

				_LeftPortContainer = new PortContainer { name = leftPortsUssName };
				_LeftPortContainer.AddToClassList(ussClassName.WithUssElement(leftPortsUssName));
				
				
				_RightPortContainer = new PortContainer { name = rightPortsUssName };
				_RightPortContainer.AddToClassList(ussClassName.WithUssElement(rightPortsUssName));

				
				_MapContainer = new VisualElement();
				_MapContainer.AddToClassList(ussClassName.WithUssElement(mapContainerUssName));
				
				m_Root.Add(_LeftPortContainer);
				m_Root.Add(_MapContainer);
				m_Root.Add(_RightPortContainer);

				container.Add(m_Root);
			}
		}
		
		/// <inheritdoc />
		protected override void UpdatePartFromModel() {
			switch (m_Model) {
				// TODO: Reinstate.
				// case ISingleInputPortNode inputPortHolder:
				//     m_InputPortContainer?.UpdatePorts(new[] { inputPortHolder.InputPort }, m_OwnerElement.GraphView, m_OwnerElement.CommandDispatcher);
				//     break;
				// case ISingleOutputPortNode outputPortHolder:
				//     m_OutputPortContainer?.UpdatePorts(new[] { outputPortHolder.OutputPort }, m_OwnerElement.GraphView, m_OwnerElement.CommandDispatcher);
				//     break;
				case IInputOutputPortsNodeModel portHolder:
					_LeftPortContainer?
						.UpdatePorts(
							portHolder
								.GetInputPorts()
								.Where(p => p.Orientation == PortOrientation.Horizontal),
							m_OwnerElement.View, m_OwnerElement.CommandDispatcher
						);
					_RightPortContainer?.UpdatePorts(portHolder.GetOutputPorts().Where(p => p.Orientation == PortOrientation.Horizontal), m_OwnerElement.View, m_OwnerElement.CommandDispatcher);
					break;
			}
		}
	}
}