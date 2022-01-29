using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_GraphAssetModel : GraphAssetModel {
		public IBlackboardGraphModel BlackboardGraphModel { get; }
    
		public TransitionTable_GraphAssetModel() {
			BlackboardGraphModel = new TransitionTable_BlackboardGraphModel(this);
		}

		[MenuItem("Assets/Create/StateMachineWrapper")]
		public static void CreateGraph(MenuCommand menuCommand) {
			const string path = "Assets";
			var template = new GraphTemplate<TransitionTable_Stencil>(TransitionTable_Stencil.graphName);
			CommandDispatcher commandDispatcher = null;
			if (EditorWindow.HasOpenInstances<TransitionTable_GraphViewEditorWindow>())
			{
				var window = EditorWindow.GetWindow<TransitionTable_GraphViewEditorWindow>();
				if (window != null)
				{
					commandDispatcher = window.CommandDispatcher;
				}
			}

			GraphAssetCreationHelpers<TransitionTable_GraphAssetModel>.CreateInProjectWindow(template, commandDispatcher, path);
		}
        
		[OnOpenAsset(1)]
		public static bool OpenGraphAsset(int instanceId, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceId);
			if (obj is TransitionTable_GraphAssetModel graphAssetModel)
			{
				var window = GraphViewEditorWindow.FindOrCreateGraphWindow<TransitionTable_GraphViewEditorWindow>();
				window.SetCurrentSelection(graphAssetModel, GraphViewEditorWindow.OpenMode.OpenAndFocus);
				return window != null;
			}

			return false;
		}
        
		protected override Type GraphModelType => typeof(TransitionTable_GraphModel);
	}
}

