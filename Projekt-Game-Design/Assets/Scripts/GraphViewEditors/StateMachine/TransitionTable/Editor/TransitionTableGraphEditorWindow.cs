using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor {
    public class TransitionTableGraphEditorWindow : GraphViewEditorWindow {

        // [InitializeOnLoadMethod]
        // static void RegisterTool()
        // {
        //     ShortcutHelper.RegisterDefaultShortcuts<TransitionTableGraphEditorWindow>(TransitionTableStencil.toolName);
        // }
        
        [MenuItem("Tools/GraphEditor/Transition Table Graph Editor")]
        private static void ShowWindow() {
            FindOrCreateGraphWindow<TransitionTableGraphEditorWindow>();
        }

        protected override void OnEnable() {
            
            base.OnEnable();
            EditorToolName = "TransitionTable Graph Editor";
            titleContent = new GUIContent("Recipe Editor");
        }
        
        /// <inheritdoc />
        protected override GraphToolState CreateInitialState()
        {
            var prefs = Preferences.CreatePreferences(EditorToolName);
            return new TransitionTableState(GUID, prefs);
        }
        
        protected override GraphView CreateGraphView() {
            return new TransitionTableGraphView(this, CommandDispatcher, EditorToolName);
        }
        
        protected override BlankPage CreateBlankPage()
        {
            var onboardingProviders = new List<OnboardingProvider>();
            onboardingProviders.Add(new TransitionTableOnboardingProvider());
        
            return new BlankPage(CommandDispatcher, onboardingProviders);
        }
        
        protected override bool CanHandleAssetType(IGraphAssetModel asset) {
            return asset is TransitionTableGraphAssetModel;
        }
    }
}