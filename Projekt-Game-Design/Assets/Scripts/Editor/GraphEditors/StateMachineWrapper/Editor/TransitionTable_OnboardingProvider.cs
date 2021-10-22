using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.UIElements;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_OnboardingProvider : OnboardingProvider {
		public override VisualElement CreateOnboardingElements(CommandDispatcher commandDispatcher) {
			var template =
				new GraphTemplate<TransitionTable_Stencil>(TransitionTable_Stencil.graphName);
			return AddNewGraphButton<TransitionTable_GraphAssetModel>(template);
		}
	}
}