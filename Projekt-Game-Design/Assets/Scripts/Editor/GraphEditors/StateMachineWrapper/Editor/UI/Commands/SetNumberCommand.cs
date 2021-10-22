using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI.Commands {
	public class SetNumberCommand : ModelCommand<Transition_NodeModel, int> {
		const string k_UndoStringSingular = "Set Transition Node Number";
		const string k_UndoStringPlural = "Set Transition Nodes Number";
        
		// public SetNumberCommand() : base(k_UndoStringSingular) { }
		public SetNumberCommand(int value, params Transition_NodeModel[] nodes) : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes) { }

		public static void DefaultHandler(GraphToolState state, SetNumberCommand command) {
			state.PushUndo(command);

			using (var graphUpdater = state.GraphViewState.UpdateScope) {
				foreach (var nodeModel in command.Models) {
					nodeModel.number = command.Value;
					graphUpdater.MarkChanged(nodeModel);
				}    
			}
		}
	}
}
