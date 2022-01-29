using System;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.Nodes {
	[System.Serializable]
	[SearcherItem(typeof(TransitionTable_Stencil), SearcherContext.Graph, "Transition Table Base")]
	public class TransitionTableBase_NodeModel : NodeModel{
		protected override void OnDefineNode() {
			base.OnDefineNode();
			
			AddOutputPort("Initial State", PortType.Execution, TransitionTable_Stencil.InputState,
				options: PortModelOptions.NoEmbeddedConstant,
				orientation: PortOrientation.Horizontal);
		}
		
		public override PortCapacity GetPortCapacity(IPortModel portModel) {
			return portModel.Direction == PortDirection.Output ? PortCapacity.Single : PortCapacity.Multi; 
		}
	}
}