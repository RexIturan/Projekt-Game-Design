using System.Collections.Generic;
using GraphViewEditors.StateMachine.TransitionTable.Editor.Nodes;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor.UI.Commands {
    public class SetNumberCommand : ModelCommand<TransitionNodeModel, int> {
        const string k_UndoStringSingular = "Set Transition Node Number";
        const string k_UndoStringPlural = "Set Transition Nodes Number";
        
        // public SetNumberCommand() : base(k_UndoStringSingular) { }
        public SetNumberCommand(int value, params TransitionNodeModel[] nodes) : base(k_UndoStringSingular, k_UndoStringPlural, value, nodes) { }

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