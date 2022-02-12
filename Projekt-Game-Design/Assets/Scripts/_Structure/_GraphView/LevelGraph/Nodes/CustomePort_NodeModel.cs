using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace _Structure._GraphView.LevelGraph.Nodes {
	public class CustomePort_NodeModel : NodeModel {
		
		public override IPortModel CreatePort(PortDirection direction, PortOrientation orientation, string portName,
			PortType portType, TypeHandle dataType, string portId, PortModelOptions options) {

			if (dataType.Equals(LevelGraph_Stencil.Connection)) {
				//todo change to arrow type?
				
				
				var portModel = base.CreatePort(direction, orientation, portName, portType, dataType, portId, options);
				// portModel.DataTypeHandle = dataType;
				return portModel;
			}
			else {
				return base.CreatePort(direction, orientation, portName, portType, dataType, portId, options);
			}
		}
	}
}