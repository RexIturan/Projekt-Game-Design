using System;
using System.Linq;
using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {

	public static class IPortModel_Extensions {
		public static bool IsStateNode(this IPortModel port){
			return port.NodeModel is State_NodeModel;
		}
		
		public static bool IsTransitionNode(this IPortModel port) {
			return port.NodeModel is Transition_NodeModel;
		}

		public static bool IsInput(this IPortModel port) {
			return port.Direction == PortDirection.Input;
		}
		
		public static bool IsOutput(this IPortModel port) {
			return port.Direction == PortDirection.Output;
		}
	}
	
	public class TransitionTable_GraphModel : GraphModel {

		public TransitionTableSO linkedTransitionTable;
		
		protected override bool IsCompatiblePort(IPortModel startPortModel,
			IPortModel compatiblePortModel) 
		{

			bool compatible = false;
			if ( ( startPortModel.IsInput() && compatiblePortModel.IsOutput() ) ||
			     ( startPortModel.IsOutput() && compatiblePortModel.IsInput() )  ) {

				if ( startPortModel.DataTypeHandle == compatiblePortModel.DataTypeHandle ) {
					if ( startPortModel.NodeModel != compatiblePortModel.NodeModel ) {
						if ( startPortModel.IsStateNode()) {

							compatible = !compatiblePortModel.IsStateNode();
				
						} else if (startPortModel.IsTransitionNode()) {

							compatible = !compatiblePortModel.IsTransitionNode();
						}
						else {
							compatible = startPortModel.DataTypeHandle == compatiblePortModel.DataTypeHandle;	
						}	
					}	
				}
			}
			
			return compatible;
		}
	}
}