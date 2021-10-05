using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine.UIElements;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor {
    public class TransitionTableOnboardingProvider : OnboardingProvider {
        public override VisualElement CreateOnboardingElements(CommandDispatcher commandDispatcher) {
            var template = new GraphTemplate<TransitionTableStencil>(TransitionTableStencil.graphName);
            return AddNewGraphButton<TransitionTableGraphAssetModel>(template);
        }
    }
}