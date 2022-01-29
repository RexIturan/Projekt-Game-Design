using Editor.GraphEditors.StateMachineWrapper.Editor.UI.Helper;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI {
	class TransitionNode : CollapsibleInOutNode
	{
		public static readonly string contitionListPartName = "condition-list-container";

		protected override void BuildPartList()
		{
			base.BuildPartList();

			PartList.InsertPartAfter(titleIconContainerPartName, new ConditionListPart(contitionListPartName, Model, this, ussClassName));
		}

		/// <inheritdoc />
		protected override void PostBuildUI() {
			base.PostBuildUI();
		}
	}
}
