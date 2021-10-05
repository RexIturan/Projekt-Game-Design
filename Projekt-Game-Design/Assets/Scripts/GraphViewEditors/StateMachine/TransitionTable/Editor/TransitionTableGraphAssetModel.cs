using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor {
    public partial class TransitionTableGraphAssetModel : GraphAssetModel {
        public IBlackboardGraphModel BlackboardGraphModel { get; }
    
        public TransitionTableGraphAssetModel() {
            BlackboardGraphModel = new TransitionTableBlackboardGraphModel(this);
        }

        [MenuItem("Assets/Create/TransitionTableGraph")]
        public static void CreateGraph(MenuCommand menuCommand) {
            const string path = "Assets";
            var template = new GraphTemplate<TransitionTableStencil>(TransitionTableStencil.graphName);
            CommandDispatcher commandDispatcher = null;
            if (EditorWindow.HasOpenInstances<TransitionTableGraphEditorWindow>())
            {
                var window = EditorWindow.GetWindow<TransitionTableGraphEditorWindow>();
                if (window != null)
                {
                    commandDispatcher = window.CommandDispatcher;
                }
            }

            GraphAssetCreationHelpers<TransitionTableGraphAssetModel>.CreateInProjectWindow(template, commandDispatcher, path);
        }
        
        [OnOpenAsset(1)]
        public static bool OpenGraphAsset(int instanceId, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceId);
            if (obj is TransitionTableGraphAssetModel graphAssetModel)
            {
                var window = GraphViewEditorWindow.FindOrCreateGraphWindow<TransitionTableGraphEditorWindow>();
                window.SetCurrentSelection(graphAssetModel, GraphViewEditorWindow.OpenMode.OpenAndFocus);
                return window != null;
            }

            return false;
        }
        
        protected override Type GraphModelType => typeof(TransitionTableGraphModel);
    }
}