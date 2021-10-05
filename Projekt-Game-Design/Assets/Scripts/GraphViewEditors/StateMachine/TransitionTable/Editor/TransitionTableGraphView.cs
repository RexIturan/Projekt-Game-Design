using UnityEditor.GraphToolsFoundation.Overdrive;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor {
    public class TransitionTableGraphView : GraphView {
        
        public TransitionTableGraphView(
            GraphViewEditorWindow window, 
            CommandDispatcher commandDispatcher, 
            string graphViewName) 
            : base(window, commandDispatcher, graphViewName) { }
    }
}
