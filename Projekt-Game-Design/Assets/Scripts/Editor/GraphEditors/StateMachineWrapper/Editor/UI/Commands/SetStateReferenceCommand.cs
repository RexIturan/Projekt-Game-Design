using System.Collections.Generic;
using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI.Commands {
	public class SetStateReferenceCommand : ModelCommand<State_NodeModel, StateSO> {
		const string k_UndoStringSingular = "Set State Reference";
		const string k_UndoStringPlural = "Set State References";

		public SetStateReferenceCommand(StateSO value, IReadOnlyList<State_NodeModel> models) :
			base(k_UndoStringSingular, k_UndoStringPlural, value, models) { }

		public static void DefaultHandler(GraphToolState state, SetStateReferenceCommand command) {
			state.PushUndo(command);

			Debug.Log("set State");
			
			using ( var graphUpdater = state.GraphViewState.UpdateScope ) {
				foreach ( var nodeModel in command.Models ) {
					// nodeModel.SetInitialState();
					nodeModel.SetState(command.Value);
					graphUpdater.MarkChanged(nodeModel);
				}
			}
		}
	}
}