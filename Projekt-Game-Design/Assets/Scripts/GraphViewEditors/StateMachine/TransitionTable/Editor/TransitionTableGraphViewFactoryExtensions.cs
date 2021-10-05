using GraphViewEditors.StateMachine.TransitionTable.Editor.Nodes;
using GraphViewEditors.StateMachine.TransitionTable.Editor.UI;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor {
    [GraphElementsExtensionMethodsCache(typeof(TransitionTableGraphView))]
    public static class TransitionTableGraphViewFactoryExtensions {
        
        public static IModelUI CreateNode(this ElementBuilder elementBuilder, CommandDispatcher dispatcher, TransitionNodeModel model)
        {
            IModelUI ui = new TransitionNode();
            ui.SetupBuildAndUpdate(model, dispatcher, elementBuilder.View, elementBuilder.Context);
            return ui;
        }
    }
}