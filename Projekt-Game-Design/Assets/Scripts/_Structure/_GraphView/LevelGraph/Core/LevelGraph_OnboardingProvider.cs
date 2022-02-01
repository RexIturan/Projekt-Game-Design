using Editor.GraphEditors.StateMachineWrapper.Editor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.UIElements;

namespace _Structure._GraphView.LevelGraph.Core {
	public class LevelGraph_OnboardingProvider : OnboardingProvider {
		
		public override VisualElement
			CreateOnboardingElements(CommandDispatcher commandDispatcher) {
			var template = new GraphTemplate<LevelGraph_Stencil>(LevelGraph_Stencil.graphName);
			return AddNewGraphButton<LevelGraph_GraphAssetModel>(template);
		}
	}
}