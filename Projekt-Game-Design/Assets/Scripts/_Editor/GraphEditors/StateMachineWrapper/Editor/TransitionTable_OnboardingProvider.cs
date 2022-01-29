using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine.ScriptableObjects;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_OnboardingProvider : OnboardingProvider {
		public override VisualElement
			CreateOnboardingElements(CommandDispatcher commandDispatcher) {
			var template =
				new TransitionTable_GraphTemplate<TransitionTable_Stencil>(TransitionTable_Stencil.graphName);

			
			
			var container = new VisualElement();

			container.Add(AddNewGraphButton<TransitionTable_GraphAssetModel>(template));
			container.Add(
				AddNewGrapTransitionTableButton<TransitionTable_GraphAssetModel>(template));
			return container;
		}

		VisualElement AddNewGrapTransitionTableButton<T>(
			IGraphTemplate template,
			string promptTitle = null,
			string buttonText = null,
			string prompt = null,
			string assetExtension = k_AssetExtension) where T : ScriptableObject, IGraphAssetModel {
			promptTitle ??= string.Format(k_PromptToCreateTitle, template.GraphTypeName);
			buttonText ??= string.Format(k_ButtonText, template.GraphTypeName);
			prompt ??= string.Format(k_PromptToCreate, template.GraphTypeName);

			var container = new VisualElement();
			container.AddToClassList("onboarding-block");

			var label = new Label(prompt);
			container.Add(label);


			var horizontal = new VisualElement {
				name = "horizontal-wrapper",
				style = { flexDirection = FlexDirection.Row }
			};

			var objectField = new ObjectField() {
				objectType = typeof(TransitionTableSO)
			};

			var button = new Button { text = buttonText };
			button.clicked += () => {
				if ( objectField.value != null ) {
					var transitionTableGraphTemplate = ( TransitionTable_GraphTemplate<TransitionTable_Stencil> )template;
					transitionTableGraphTemplate.TransitionTableLink = ( TransitionTableSO )objectField.value;
					var graphAsset =
						GraphAssetCreationHelpers<T>.PromptToCreate(transitionTableGraphTemplate, promptTitle, prompt,
							assetExtension);
					Selection.activeObject = graphAsset as Object;
				}
			};
			horizontal.Add(button);
			horizontal.Add(objectField);

			container.Add(horizontal);

			return container;
		}
	}
}