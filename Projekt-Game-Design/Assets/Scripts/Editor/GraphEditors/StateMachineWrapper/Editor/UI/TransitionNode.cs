using UnityEditor.GraphToolsFoundation.Overdrive;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI {
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
