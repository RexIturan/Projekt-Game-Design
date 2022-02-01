using System;
using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace _Structure._GraphView.LevelGraph.Nodes {
	[Serializable]
	[SearcherItem(typeof(LevelGraph_Stencil), SearcherContext.Graph, "Level")]
	public class Level_NodeModel : CustomePortNodeModel { 
		[SerializeField] private string m_name;
	
		protected override void OnDefineNode() {
			base.OnDefineNode();
			
			AddInputPort("Input", PortType.Data, LevelGraph_Stencil.Connection,
				options: PortModelOptions.NoEmbeddedConstant);

			AddOutputPort("Output", PortType.Data, LevelGraph_Stencil.Connection,
				options: PortModelOptions.NoEmbeddedConstant);
		}
	}
}