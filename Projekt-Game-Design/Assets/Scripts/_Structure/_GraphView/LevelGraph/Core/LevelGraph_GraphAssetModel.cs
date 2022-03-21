using System;
using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using MenuCommand = UnityEditor.MenuCommand;

namespace _Structure._GraphView.LevelGraph.Core {
	public class LevelGraph_GraphAssetModel : GraphAssetModel {
		public IBlackboardGraphModel BlackboardGraphModel { get; }
    
		public LevelGraph_GraphAssetModel() {
			BlackboardGraphModel = new LevelGraph_BlackboardGraphModel(this);
		}

		[MenuItem("Assets/Create/LevelGraph")]
		public static void CreateGraph(MenuCommand menuCommand) {
			const string path = "Assets";
			var template = new GraphTemplate<LevelGraph_Stencil>(LevelGraph_Stencil.graphName);
			CommandDispatcher commandDispatcher = null;
			if (EditorWindow.HasOpenInstances<LevelGraph_GraphViewEditorWindow>())
			{
				var window = EditorWindow.GetWindow<LevelGraph_GraphViewEditorWindow>();
				if (window != null)
				{
					commandDispatcher = window.CommandDispatcher;
				}
			}

			GraphAssetCreationHelpers<LevelGraph_GraphAssetModel>.CreateInProjectWindow(template, commandDispatcher, path);
		}
        
		[OnOpenAsset(1)]
		public static bool OpenGraphAsset(int instanceId, int line)
		{
			var obj = EditorUtility.InstanceIDToObject(instanceId);
			if (obj is LevelGraph_GraphAssetModel graphAssetModel)
			{
				var window = GraphViewEditorWindow.FindOrCreateGraphWindow<LevelGraph_GraphViewEditorWindow>();
				window.SetCurrentSelection(graphAssetModel, GraphViewEditorWindow.OpenMode.OpenAndFocus);
				return window != null;
			}

			return false;
		}
        
		protected override Type GraphModelType => typeof(LevelGraph_GraphModel);
	}
}

