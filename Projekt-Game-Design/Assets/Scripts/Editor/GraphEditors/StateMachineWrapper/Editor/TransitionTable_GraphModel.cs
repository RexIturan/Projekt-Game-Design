using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_GraphModel : GraphModel {
		protected override bool IsCompatiblePort(IPortModel startPortModel,
			IPortModel compatiblePortModel) {
			return startPortModel.DataTypeHandle == compatiblePortModel.DataTypeHandle;
		}
	}
}