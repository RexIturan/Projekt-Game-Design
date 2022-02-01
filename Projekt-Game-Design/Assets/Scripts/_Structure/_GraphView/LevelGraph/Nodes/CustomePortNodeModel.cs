using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace _Structure._GraphView.LevelGraph.Nodes {
	public class CustomePortNodeModel : NodeModel {
		
		public override IPortModel CreatePort(PortDirection direction, PortOrientation orientation, string portName,
			PortType portType, TypeHandle dataType, string portId, PortModelOptions options) {

			if (dataType.Equals(LevelGraph_Stencil.Connection)) {
				var portModel = base.CreatePort(direction, orientation, portName, portType, dataType, portId, options);
				portModel.ToolTip = "This Works";
				portModel.DataTypeHandle = TypeHandle.Bool;
				return portModel;
			}
			else {
				return base.CreatePort(direction, orientation, portName, portType, dataType, portId, options);
			}
		}
	}
}