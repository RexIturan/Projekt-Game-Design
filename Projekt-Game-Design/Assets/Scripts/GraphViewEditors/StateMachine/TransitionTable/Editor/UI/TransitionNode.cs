using UnityEditor.GraphToolsFoundation.Overdrive;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor.UI {
    class TransitionNode : CollapsibleInOutNode
    {
        public static readonly string paramContainerPartName = "parameter-container";

        protected override void BuildPartList()
        {
            base.BuildPartList();

            PartList.InsertPartAfter(titleIconContainerPartName, NumberFieldPart.Create(paramContainerPartName, Model, this, ussClassName));
        }
    }
}