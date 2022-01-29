using System;
using System.Collections.Generic;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.Nodes {
	[System.Serializable]
	[SearcherItem(typeof(TransitionTable_Stencil), SearcherContext.Graph, "Transition Item")]
	public class Transition_NodeModel : NodeModel {

		public bool editable = false;
		public int transitionID = -1;
		public TransitionTableSO transitionTable;
		
		protected override void OnDefineNode() {
			base.OnDefineNode();

			AddInputPort("From State", PortType.Execution, TransitionTable_Stencil.OutputState,
				options: PortModelOptions.NoEmbeddedConstant,
				orientation: PortOrientation.Vertical);
			
			AddOutputPort("To State", PortType.Execution, TransitionTable_Stencil.InputState,
				options: PortModelOptions.NoEmbeddedConstant,
				orientation: PortOrientation.Vertical);
			
			AddOutputPort("From State", PortType.Execution, TransitionTable_Stencil.OutputState,
				options: PortModelOptions.NoEmbeddedConstant,
				orientation: PortOrientation.Vertical);
			
			AddInputPort("To State", PortType.Execution, TransitionTable_Stencil.InputState,
				options: PortModelOptions.NoEmbeddedConstant,
				orientation: PortOrientation.Vertical);

			// hack so the node is collabseable when all other ports are connected
			AddInputPort("", PortType.MissingPort, TypeHandle.Unknown, null,
				PortOrientation.Vertical, PortModelOptions.Hidden);
		}

		public override PortCapacity GetPortCapacity(IPortModel portModel) {
			return portModel.Direction == PortDirection.Output ? PortCapacity.Single : PortCapacity.Multi; 
		}

		public bool hasValidValues() {
			return transitionTable != null && transitionID > -1;
		}
	}
}