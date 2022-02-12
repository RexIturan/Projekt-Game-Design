using _Structure._GraphView.LevelGraph.Core.GraphElements.ModelUI;
using _Structure._GraphView.LevelGraph.Core.Model;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class LevelGraph_GraphView : GraphView {
		public LevelGraph_GraphView(
			GraphViewEditorWindow window,
			CommandDispatcher commandDispatcher,
			string graphViewName)
			: base(window, commandDispatcher, graphViewName) {
			
			// increase max zoom
			ContentZoomer.maxScale = 3f;
			
			// //todo save model
			// var model = new SubWindowModel {
			// 	PositionAndSize = new Rect(10, 10, 100, 300)
			// };
			// SubWindow ui = GraphElementFactory.CreateUI<SubWindow>(this, commandDispatcher, model);
			// // ui.SetupBuildAndUpdate(model, commandDispatcher, this);
			// ui.AddToView(this);
			// this.contentContainer.Add(ui);
		}
	}
}