using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using Editor.GraphEditors.StateMachineWrapper.Editor.UI.Commands;
using Editor.GraphEditors.StateMachineWrapper.Editor.UI.Helper;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI {
	public class StateNode : CollapsibleInOutNode
	{
		public static readonly string paramContainerPartName = "parameter-container";
		public static readonly string checkboxContainerPartName = "checkbox-container";
		public static readonly string stateReferenceContainerPartName = "state-reference-container";
		public static readonly string actionListContainer = "action-list-container";
		
		protected override void BuildPartList()
		{
			base.BuildPartList();
			
			if (!(Model is State_NodeModel stateNodeModel))
				return;
			
			PartList.InsertPartAfter(titleIconContainerPartName, new StateReferencePart(stateReferenceContainerPartName, Model, this, ussClassName));
			PartList.InsertPartAfter(stateReferenceContainerPartName, new ActionListPart(actionListContainer, Model, this, ussClassName));
		}

		// protected override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
		// {
		// 	base.BuildContextualMenu(evt);
		//
		// 	if (!(Model is State_NodeModel stateNodeModel))
		// 	{
		// 		return;
		// 	}
		//
		// 	if (evt.menu.MenuItems().Count > 0)
		// 		evt.menu.AppendSeparator();
		//
		// 	evt.menu.AppendAction($"Set Initial Node", action: action =>
		// 	{
		// 		CommandDispatcher.Dispatch(new SetInitialStateCommand(new []{ stateNodeModel }));
		// 		// CommandDispatcher.Dispatch(new AddPortCommand(new[] { mixNodeModel }));
		// 	});
		// }
		
		// /// <inheritdoc />
		// protected override void PostBuildUI() {
		// 	base.PostBuildUI();
		// }
	}
}