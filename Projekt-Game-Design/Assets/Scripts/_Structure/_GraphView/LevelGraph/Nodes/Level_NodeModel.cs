using System;
using _Structure._GraphView.LevelGraph.Util;
using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace _Structure._GraphView.LevelGraph.Nodes {
	[Serializable]
	[SearcherItem(typeof(LevelGraph_Stencil), SearcherContext.Graph, "Level")]
	public class Level_NodeModel : CustomePort_NodeModel { 
		[SerializeField] private string m_name;
	
		protected override void OnDefineNode() {
			base.OnDefineNode();
			
			AddInputPort("Input", LevelGraph_PortType.Connection, LevelGraph_Stencil.Connection,
				options: PortModelOptions.NoEmbeddedConstant);

			AddOutputPort("Output", LevelGraph_PortType.Connection, LevelGraph_Stencil.Connection,
				options: PortModelOptions.NoEmbeddedConstant);
			
		}
	}
}