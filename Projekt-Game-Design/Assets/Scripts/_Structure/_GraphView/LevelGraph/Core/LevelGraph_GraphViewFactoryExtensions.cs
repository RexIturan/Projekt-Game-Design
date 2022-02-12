using _Structure._GraphView.LevelGraph.Nodes;
using _Structure._GraphView.LevelGraph.UI.Nodes;
using _Structure._GraphView.LevelGraph.Util;
using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace _Structure._GraphView.LevelGraph.Core {
	[GraphElementsExtensionMethodsCache(typeof(LevelGraph_GraphView))]
	public static class LevelGraph_GraphViewFactoryExtensions {
		
		public static IModelUI CreateLevelNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, Level_NodeModel model) {
			IModelUI ui = new Level_Node();
			ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
			return ui;
		}
		
		//
		// public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, State_NodeModel model)
		// {
		// 	IModelUI ui = new StateNode();
		// 	ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
		// 	return ui;
		// }
		//
		
		// public static IModelUI CreateEdge(this ElementBuilder elementBuilder, CommandDispatcher commandDispatcher, IEdgeModel model)
		// {
		// 	var ui = new Edge();
		// 	Debug.Log("Hi From Edge!");
		// 	ui.SetupBuildAndUpdate(model, commandDispatcher, elementBuilder.View, elementBuilder.Context);
		// 	// ui.EdgeControl.styleSheets.Add();
		// 	ui.EdgeControl.AddStylesheet("Override/Edge.uss");
		// 	return ui;
		// }
		
		public static IModelUI CreatePort(this ElementBuilder elementBuilder,
			CommandDispatcher dispatcher, PortModel model) {
			
			var ui = new Port();
			ui.Setup(model, dispatcher, elementBuilder.View, elementBuilder.Context);
		
			ui.BuildUI();
			
			ui.AddStylesheet("Custom/Port.uss");
			ui.UpdateFromModel();
			return ui;
		}
	}
}
