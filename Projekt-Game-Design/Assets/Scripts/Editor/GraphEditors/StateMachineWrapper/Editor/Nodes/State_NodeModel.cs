using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.Nodes {
	[System.Serializable]
	[SearcherItem(typeof(TransitionTable_Stencil), SearcherContext.Graph, "State")]
	public class State_NodeModel : NodeModel {

		public bool editable = false;
		public string stateName;
		public StateSO state;
		
		protected override void OnDefineNode() {
			base.OnDefineNode();

			// if ( InitialState ) {
			// 	AddInputPort("Initial State", PortType.Data, TransitionTable_Stencil.ToState,
			// 		options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Horizontal);	
			// }
			
			AddInputPort("In", PortType.Execution, TransitionTable_Stencil.InputState,
				options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Vertical);
			
			AddOutputPort("Out", PortType.Execution, TransitionTable_Stencil.OutputState,
				options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Vertical);
			
			AddOutputPort("In", PortType.Execution, TransitionTable_Stencil.InputState,
				options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Vertical);
			
			AddInputPort("Out", PortType.Execution, TransitionTable_Stencil.OutputState,
				options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Vertical);
			
			// hack so the node is collabseable when all other ports are connected
			AddInputPort("", PortType.MissingPort, TypeHandle.Unknown, null,
				PortOrientation.Vertical, PortModelOptions.Hidden);
		}

		public void SetState(StateSO newState) {
			this.state = newState;
			if ( newState != null ) {
				stateName = newState.name;
			}
		}
	}
}
