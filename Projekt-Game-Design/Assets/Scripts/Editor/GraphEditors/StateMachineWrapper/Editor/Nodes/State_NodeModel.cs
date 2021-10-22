using System;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.Nodes {
	[Serializable]
	[SearcherItem(typeof(TransitionTable_Stencil), SearcherContext.Graph, "State")]
	public class State_NodeModel : NodeModel {
		
		
		
		protected override void OnDefineNode() {
			base.OnDefineNode();

			AddInputPort("In", PortType.Data, TransitionTable_Stencil.TransitionIn,
				options: PortModelOptions.NoEmbeddedConstant);
			AddInputPort("Out", PortType.Data, TransitionTable_Stencil.TransitionOut,
				options: PortModelOptions.Default);
		}
	}
}
