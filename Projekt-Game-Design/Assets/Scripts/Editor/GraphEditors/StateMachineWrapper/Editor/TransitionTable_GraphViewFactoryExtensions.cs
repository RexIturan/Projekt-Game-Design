using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using Editor.GraphEditors.StateMachineWrapper.Editor.UI;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public static class TransitionTable_GraphViewFactoryExtensions {
		public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, Transition_NodeModel model)
		{
			IModelUI ui = new TransitionNode();
			ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
			return ui;
		}
	}
}
