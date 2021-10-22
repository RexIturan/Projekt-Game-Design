using System;
using System.Collections.Generic;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.Nodes {
	[Serializable]
	[SearcherItem(typeof(TransitionTable_Stencil), SearcherContext.Graph, "Transition")]
	public class Transition_NodeModel : NodeModel {
		[SerializeField] private List<int> conditions;
		public int number;
		[SerializeField] bool test;

		public virtual bool Test
		{
			get => test;
			set
			{
				if (!this.IsCollapsible())
					return;

				test = value;
			}
		}
		
		protected override void OnDefineNode() {
			base.OnDefineNode();

			AddInputPort("In", PortType.Data, TransitionTable_Stencil.TransitionIn,
				options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Vertical);
			AddInputPort("Out", PortType.Data, TransitionTable_Stencil.TransitionOut,
				options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Vertical);
		}
	}
}
