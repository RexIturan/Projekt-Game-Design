using System.Collections.Generic;
using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI.Commands {
	public class SetInitialStateCommand: ModelCommand<State_NodeModel> {
		const string k_UndoStringSingular = "Set Transition Node Number";
		const string k_UndoStringPlural = "Set Transition Nodes Number";
		
		public SetInitialStateCommand(IReadOnlyList<State_NodeModel> models) : base(k_UndoStringSingular, k_UndoStringPlural, models) { }
		
		public static void DefaultHandler(GraphToolState state, SetInitialStateCommand command) {
			state.PushUndo(command);

			using (var graphUpdater = state.GraphViewState.UpdateScope) {
				foreach (var nodeModel in command.Models) {
					// nodeModel.SetInitialState();
					graphUpdater.MarkChanged(nodeModel);
				}    
			}
		}
	}
}