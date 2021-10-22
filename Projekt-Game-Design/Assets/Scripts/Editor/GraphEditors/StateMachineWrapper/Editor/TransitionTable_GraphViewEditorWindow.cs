using System.Collections.Generic;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_GraphViewEditorWindow : GraphViewEditorWindow {
		// [InitializeOnLoadMethod]
		// static void RegisterTool()
		// {
		//     ShortcutHelper.RegisterDefaultShortcuts<TransitionTableGraphEditorWindow>(TransitionTableStencil.toolName);
		// }

		[MenuItem("Tools/GraphEditor/State Machine Wrapper")]
		private static void ShowWindow() {
			FindOrCreateGraphWindow<TransitionTable_GraphViewEditorWindow>();
		}

		protected override void OnEnable() {
			base.OnEnable();
			EditorToolName = "State Machine Wrapper Graph Editor";
			titleContent = new GUIContent("State Machine Wrapper Graph Editor");
		}

		/// <inheritdoc />
		protected override GraphToolState CreateInitialState() {
			var prefs = Preferences.CreatePreferences(EditorToolName);
			return new TransitionTable_State(GUID, prefs);
		}

		protected override GraphView CreateGraphView() {
			return new TransitionTable_GraphView(this, CommandDispatcher, EditorToolName);
		}

		protected override BlankPage CreateBlankPage() {
			var onBoardingProviders = new List<OnboardingProvider>();
			onBoardingProviders.Add(new TransitionTable_OnboardingProvider());

			return new BlankPage(CommandDispatcher, onBoardingProviders);
		}

		protected override bool CanHandleAssetType(IGraphAssetModel asset) {
			return asset is TransitionTable_GraphAssetModel;
		}
	}
}