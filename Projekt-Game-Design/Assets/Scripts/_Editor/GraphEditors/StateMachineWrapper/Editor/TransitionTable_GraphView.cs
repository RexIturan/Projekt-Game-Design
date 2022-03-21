using UnityEditor.GraphToolsFoundation.Overdrive;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_GraphView : GraphView {
		public TransitionTable_GraphView(
			GraphViewEditorWindow window,
			CommandDispatcher commandDispatcher,
			string graphViewName)
			: base(window, commandDispatcher, graphViewName) { }
	}
}