using System;
using System.Collections.Generic;
using System.Linq;
using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_GraphTemplate<TStencil> : IGraphTemplate where TStencil : Stencil {

		public const float State_Node_X_Spacing = .5f;
		public const float Transition_Node_Spacing = .5f;
		public const float State_Node_Width = 2;
		public const float State_Node_Height = 4;
		public const float Transition_Node_Width = 2;
		public const float Transition_Node_Height = 2;
		
		public TransitionTableSO TransitionTableLink { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GraphTemplate{TStencil}"/> class.
		/// </summary>
		/// <param name="graphTypeName">The name of the type of graph for this template.</param>
		public TransitionTable_GraphTemplate(string graphTypeName = "Graph") {
			GraphTypeName = graphTypeName;
		}

		/// <inheritdoc />
		public virtual Type StencilType => typeof(TStencil);

		/// <inheritdoc />
		public virtual void InitBasicGraph(IGraphModel graphModel) {
			if ( !( graphModel is TransitionTable_GraphModel ttGraphModel ) )
				return;

			ttGraphModel.linkedTransitionTable = TransitionTableLink;
			var table = ttGraphModel.linkedTransitionTable;
			
			Dictionary<StateSO, State_NodeModel> stateSO_stateNodeModel_Dict =
				new Dictionary<StateSO, State_NodeModel>();
			
			Dictionary<State_NodeModel, List<Transition_NodeModel>> toStateNode_Transition_Dict =
				new Dictionary<State_NodeModel, List<Transition_NodeModel>>();
			
			Dictionary<State_NodeModel, List<Transition_NodeModel>> fromStateNode_Transition_Dict =
				new Dictionary<State_NodeModel, List<Transition_NodeModel>>();

			List<State_NodeModel> states = new List<State_NodeModel>();
			var transitions = table._transitions;
			var fromStates = transitions.GroupBy(transition => transition.FromState);
			
			
			// Base Node: Links to Init Node
			var baseNode = ttGraphModel.CreateNode<TransitionTableBase_NodeModel>(table.name,
				Vector2.zero);
			
			// create all from states
			foreach ( var fromState in fromStates ) {
				if ( fromState.Key == null )
					break;

				StateSO state = fromState.Key;
				// create stateNode, save reference in dict
				var fromStateNode = ttGraphModel.CreateNode<State_NodeModel>($"{state.name}");
				fromStateNode.DefineNode();
				fromStateNode.state = state;
				states.Add(fromStateNode);
				stateSO_stateNodeModel_Dict.Add(state, fromStateNode);
			}

			// create edge for init state, from initional to the first state 
			ttGraphModel.CreateEdge(
				states[0].GetInputPorts()
					.First(model => model.DataTypeHandle != TypeHandle.Unknown),
				baseNode.GetOutputPorts()
					.First(model => model.DataTypeHandle != TypeHandle.Unknown));

			// for each transition item in from state create to states
			// create edges between from state and to state
			foreach ( var fromState in fromStates ) {
				if ( fromState.Key == null )
					break;

				foreach ( var transitionItem in fromState ) {
					if ( transitionItem.ToState == null )
						break;

					#region Create To State

					var toState = transitionItem.ToState;

					// create To stateNode if it wasnt created before, save reference in dict
					if ( !stateSO_stateNodeModel_Dict.ContainsKey(toState) ) {
						var stateNode = ttGraphModel.CreateNode<State_NodeModel>($"{toState.name}");
						stateNode.DefineNode();
						stateNode.state = toState;
						stateSO_stateNodeModel_Dict.Add(toState, stateNode);
					}

					#endregion

					#region Create Transition

					var fromStateNode = stateSO_stateNodeModel_Dict[transitionItem.FromState];
					var toStateNode = stateSO_stateNodeModel_Dict[transitionItem.ToState];

					var transitionNode = ttGraphModel.CreateNode<Transition_NodeModel>("Transition");
					transitionNode.DefineNode();
					transitionNode.transitionTable = table;
					transitionNode.transitionID = transitions.ToList().IndexOf(
						transitions.First(
							transition => transition.FromState.Equals(fromState.Key)
						));

					if ( toStateNode_Transition_Dict.ContainsKey(toStateNode) ) {
						bool found = false;
						foreach ( var transitionNodeModel in toStateNode_Transition_Dict[toStateNode] ) {
							if ( transitionNodeModel.Equals(transitionNode) ) {
								found = true;
								transitionNode.Destroy();
								transitionNode = transitionNodeModel;
								break;
							}
						}
						if(!found) {
							toStateNode_Transition_Dict[toStateNode].Add(transitionNode);
						}
					}
					else {
						toStateNode_Transition_Dict.Add(toStateNode, new List<Transition_NodeModel>(){transitionNode});
					}

					if ( fromStateNode_Transition_Dict.ContainsKey(fromStateNode) ) {
						fromStateNode_Transition_Dict[fromStateNode].Add(transitionNode);	
					}
					else {
						fromStateNode_Transition_Dict.Add(fromStateNode,
							new List<Transition_NodeModel>() { transitionNode });	
					}

					#endregion


					#region Craete Edges

					
					//red from transition to state
					var outputTransitionPort = transitionNode.GetInputPorts()
						.First(portModel =>
							portModel.DataTypeHandle == TransitionTable_Stencil.InputState);
					var inputToStatePort = toStateNode.GetOutputPorts()
						.First(port => port.DataTypeHandle == TransitionTable_Stencil.InputState);

					ttGraphModel.CreateEdge(outputTransitionPort, inputToStatePort);
					
					
					//blue from state to transition
					var outputFromStatePort = fromStateNode.GetOutputPorts()
						.First(port => port.DataTypeHandle == TransitionTable_Stencil.OutputState);
					var inputTransitionPort = transitionNode.GetInputPorts()
						.First(portModel =>
							portModel.DataTypeHandle == TransitionTable_Stencil.OutputState);
					
					ttGraphModel.CreateEdge(inputTransitionPort, outputFromStatePort);


					#endregion 
					
				}
			}

			#region positioning of nodes

			// Base Node
			// Do nothing
			
			// State Nodes
			var stateNodes = stateSO_stateNodeModel_Dict.Values.ToList();

			var startOffset = new Vector2(0, 1);
			
			var stateNodeWidth = new Vector2(State_Node_Width, 0);
			var stateNodeHeight = new Vector2(0, State_Node_Height);

			var transitionNodeHeight = new Vector2(0, Transition_Node_Height);
			
			var transitionNodeSpacing = new Vector2(0, Transition_Node_Spacing);
			var stateNodeSpacing = new Vector2(State_Node_X_Spacing, 0);

			Vector2 offset = startOffset;
			for ( int i = 0; i < stateNodes.Count; i++ ) {
				stateNodes[i].Position = ( offset + ( ( stateNodeSpacing + stateNodeWidth ) * i ) ) * 100;

				if ( fromStateNode_Transition_Dict.ContainsKey(stateNodes[i]) ) {
					var stetTransitions = fromStateNode_Transition_Dict[stateNodes[i]];
					for ( int j = 0; j < stetTransitions.Count; j++ ) {
						stetTransitions[j].Position = 
							( offset 
							  + ( ( stateNodeSpacing + stateNodeWidth ) * i ) 
							  + stateNodeHeight 
							  + ( ( transitionNodeHeight + transitionNodeSpacing ) * j ) 
							) * 100;
					}
				}
			}

			#endregion
		}

		/// <inheritdoc />
		public virtual string GraphTypeName { get; }

		/// <inheritdoc />
		public virtual string DefaultAssetName => GraphTypeName;
	}
}