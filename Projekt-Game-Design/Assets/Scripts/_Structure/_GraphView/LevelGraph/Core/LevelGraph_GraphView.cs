using UnityEditor.GraphToolsFoundation.Overdrive;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class LevelGraph_GraphView : GraphView {
		public LevelGraph_GraphView(
			GraphViewEditorWindow window,
			CommandDispatcher commandDispatcher,
			string graphViewName)
			: base(window, commandDispatcher, graphViewName) { }
	}
}