using System;
using System.Collections.Generic;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor.Nodes {
    [Serializable]
    [SearcherItem(typeof(TransitionTableStencil), SearcherContext.Graph, "Transition")]
    public class TransitionNodeModel : NodeModel {
        [SerializeField] private List<int> conditions;
        public int number;

        protected override void OnDefineNode() {
            base.OnDefineNode();

            AddInputPort("In", PortType.Data, TransitionTableStencil.TransitionIn,
                options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Vertical);
            AddInputPort("Out", PortType.Data, TransitionTableStencil.TransitionOut,
                options: PortModelOptions.NoEmbeddedConstant, orientation: PortOrientation.Vertical);
        }
    }
}