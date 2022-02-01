using System.Collections.Generic;
using _Structure._GraphView.LevelGraph.Core;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class LevelGraph_GraphViewEditorWindow : GraphViewEditorWindow {
		// [InitializeOnLoadMethod]
		// static void RegisterTool()
		// {
		//     ShortcutHelper.RegisterDefaultShortcuts<TransitionTableGraphEditorWindow>(TransitionTableStencil.toolName);
		// }

		[MenuItem("Tools/GraphEditor/Level Graph")]
		private static void ShowWindow() {
			FindOrCreateGraphWindow<LevelGraph_GraphViewEditorWindow>();
		}

		protected override void OnEnable() {
			base.OnEnable();
			EditorToolName = "Level Graph Editor";
			titleContent = new GUIContent("Level Graph Editor");
			
			// adds a button to hide the node inspector
			// todo(vincent) move to extension or so
			var toolbar = rootVisualElement.Q<VisualElement>(className : "ge-main-toolbar");
			var optionsButton = rootVisualElement.Q<Button>("optionsButton");
			var inspector = rootVisualElement.Q<VisualElement>(className: "model-inspector");
			var button = new Button();
			button.tooltip = "Toggle Node Inspector";
			button.clicked += () => {
				if (inspector.style.display == DisplayStyle.None) {
					inspector.style.display = DisplayStyle.Flex;    
				}
				else {
					inspector.style.display = DisplayStyle.None;    
				}
			};
			toolbar.Add(button);
			button.PlaceBehind(optionsButton);
		}

		/// <inheritdoc />
		protected override GraphToolState CreateInitialState() {
			var prefs = Preferences.CreatePreferences(EditorToolName);
			return new LevelGraph_State(GUID, prefs);
		}

		protected override GraphView CreateGraphView() {
			return new LevelGraph_GraphView(this, CommandDispatcher, EditorToolName);
		}

		protected override BlankPage CreateBlankPage() {
			var onBoardingProviders = new List<OnboardingProvider>();
			onBoardingProviders.Add(new LevelGraph_OnboardingProvider());

			return new BlankPage(CommandDispatcher, onBoardingProviders);
		}

		protected override bool CanHandleAssetType(IGraphAssetModel asset) {
			return asset is LevelGraph_GraphAssetModel;
		}
	}
}