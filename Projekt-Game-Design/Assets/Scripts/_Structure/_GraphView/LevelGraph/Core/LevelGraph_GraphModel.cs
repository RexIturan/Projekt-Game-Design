using System;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {

	public static class LevelGraph_IPortModel_Extensions {
		public static bool IsInput(this IPortModel port) {
			return port.Direction == PortDirection.Input;
		}
		
		public static bool IsOutput(this IPortModel port) {
			return port.Direction == PortDirection.Output;
		}
	}
	
	public partial class LevelGraph_GraphModel : GraphModel {
		
		protected override bool IsCompatiblePort(IPortModel startPortModel,
			IPortModel compatiblePortModel) {
			
			//TODO Implement
			
			return true;
		}
		
		
	}

	//change edge types
	public partial class LevelGraph_GraphModel : GraphModel {
		protected override Type GetEdgeType(IPortModel toPort, IPortModel fromPort) {
			return base.GetEdgeType(toPort, fromPort);
		}
	}
}