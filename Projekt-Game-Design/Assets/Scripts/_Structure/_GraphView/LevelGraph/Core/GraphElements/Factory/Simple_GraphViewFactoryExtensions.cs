using _Structure._GraphView.LevelGraph.Core.GraphElements.ModelUI;
using _Structure._GraphView.LevelGraph.Core.Model;
using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace _Structure._GraphView.LevelGraph.Core.GraphElements.Factory {
	[GraphElementsExtensionMethodsCache(typeof(GraphView))]
	public static class Simple_GraphViewFactoryExtensions {
		
		public static IModelUI CreateSubWindow(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, SubWindowModel model) {
			IModelUI ui = new SubWindow();
			ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
			return ui;
		}
	}
}