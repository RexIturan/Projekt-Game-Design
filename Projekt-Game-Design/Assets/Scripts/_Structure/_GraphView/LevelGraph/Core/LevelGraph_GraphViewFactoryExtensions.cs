using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace _Structure._GraphView.LevelGraph.Core {
	[GraphElementsExtensionMethodsCache(typeof(LevelGraph_GraphView))]
	public static class LevelGraph_GraphViewFactoryExtensions {
		
		// public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, Transition_NodeModel model)
		// {
		// 	IModelUI ui = new TransitionNode();
		// 	ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
		// 	return ui;
		// }
		//
		// public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, State_NodeModel model)
		// {
		// 	IModelUI ui = new StateNode();
		// 	ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
		// 	return ui;
		// }
		//
		// public static IModelUI CreatePort(this ElementBuilder elementBuilder,
		// 	CommandDispatcher dispatcher, PortModel model) {
		// 	
		// 	var ui = new Port();
		// 	ui.Setup(model, dispatcher, elementBuilder.View, elementBuilder.Context);
		//
		// 	ui.BuildUI();
		// 	
		// 	ui.AddStylesheet("customPorts.uss");
		// 	ui.UpdateFromModel();
		// 	return ui;
		// }
	}
}
