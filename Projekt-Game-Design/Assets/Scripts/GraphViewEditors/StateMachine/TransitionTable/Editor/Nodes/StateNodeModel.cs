using System;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor.Nodes {
    [Serializable]
    [SearcherItem(typeof(TransitionTableStencil), SearcherContext.Graph, "State")]
    public class StateNodeModel : NodeModel {
        protected override void OnDefineNode() {
            base.OnDefineNode();

            AddInputPort("In", PortType.Data, TransitionTableStencil.TransitionIn,
                options: PortModelOptions.NoEmbeddedConstant);
            AddInputPort("Out", PortType.Data, TransitionTableStencil.TransitionOut,
                options: PortModelOptions.NoEmbeddedConstant);
        }
    }
}