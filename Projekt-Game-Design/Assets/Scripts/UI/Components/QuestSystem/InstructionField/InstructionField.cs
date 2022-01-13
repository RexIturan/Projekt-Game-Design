using System.Collections.Generic;
using QuestSystem.ScriptabelObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.QuestSystem {
	public struct InstructionWrapper {
		public InstructionState state;
		public string text;
	}
	
	public enum InstructionState {
		Active,
		Done,
		Failed
	}
	
	public class InstructionField : VisualElement {
///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string baseComponentName = "InstructionField";
		private static readonly string baseUssClassName = "instructionField";
		
		private static readonly string containerSuffix = "container";
		private static readonly string buttonSuffix = "button";
		private static readonly string doneSuffix = "done";
		private static readonly string activeSuffix = "active";
		private static readonly string failedSuffix = "failed";
		private static readonly string doneLabelSuffix = "done-label";
		private static readonly string activeLabelSuffix = "active-label";
		private static readonly string failedLabelSuffix = "failed-label";
		private static readonly string labelSuffix = "label";
		private static readonly string stateLabelSuffix = "stateLabel";
		
		private static readonly string defaultStyleSheet = "UI/instructionField";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////



///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement container;
		private Label instructionLabel;
		private Label stateLabel;

///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////

		public string InstructionName { get; set; }
		public InstructionState State { get; set; }

///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////

		private void InitContainer( ref VisualElement container, string ComponentName, string[] suffix) {
			container = new VisualElement {
				name = GetComponentName(ComponentName),
			};
			foreach ( var s in suffix ) {
				container.AddToClassList(GetClassNameWithSuffix(s));
			}
		}

		private void InitLabel( ref Label label, string ComponentName, string labelText, string suffix) {
			label = new Label {
				name = GetComponentName(ComponentName),
				text = labelText
			};
			label.AddToClassList(GetClassNameWithSuffix(suffix));
		}

		private void InitButton( ref Button button, string ComponentName, string buttonText, string baseClass, string[] suffix) {
			button = new Button {
				name = GetComponentName(ComponentName),
				text = buttonText
			};

			button.AddToClassList(GetClassNameWithSuffix(buttonSuffix));
			
			foreach ( var s in suffix ) {
				button.AddToClassList(GetClassNameWithSuffix(s));
			}

			button.AddToClassList(baseClass);
			button.AddToClassList(GetClassNameWithHover(baseClass));
			button.AddToClassList(GetClassNameWithFocus(baseClass));
		}
		
		private void BuildComponent() {
			this.name = "TaskPanel";

			//default styleSheet
			this.styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
			this.AddToClassList(baseUssClassName);

			InitContainer(
				ref container, 
				"Container",
				new string[]{containerSuffix, activeSuffix});
			
			InitLabel(
				ref instructionLabel,
				"InstructionLabel",
				"[W] Test Instruction",
				labelSuffix);
			
			InitLabel(
				ref stateLabel,
				"StateLabel",
				"active",
				stateLabelSuffix);
			
			container.Add(instructionLabel);
			container.Add(stateLabel);
			this.Add(container);
		}

		private void SetInstructionState(InstructionState state) {
			
			container.RemoveFromClassList(GetClassNameWithSuffix(doneSuffix));
			container.RemoveFromClassList(GetClassNameWithSuffix(failedSuffix));
			container.RemoveFromClassList(GetClassNameWithSuffix(activeSuffix));
			
			stateLabel.RemoveFromClassList(GetClassNameWithSuffix(doneLabelSuffix));
			stateLabel.RemoveFromClassList(GetClassNameWithSuffix(failedLabelSuffix));
			stateLabel.RemoveFromClassList(GetClassNameWithSuffix(activeLabelSuffix));
			
			switch ( state ) {
				case InstructionState.Done:
					container.AddToClassList(GetClassNameWithSuffix(doneSuffix));
					stateLabel.AddToClassList(GetClassNameWithSuffix(doneLabelSuffix));
					stateLabel.text = "Done";
					break;
				case InstructionState.Failed:
					container.AddToClassList(GetClassNameWithSuffix(failedSuffix));
					stateLabel.AddToClassList(GetClassNameWithSuffix(failedLabelSuffix));
					stateLabel.text = "Failed";
					break;
				case InstructionState.Active:
				default:
					container.AddToClassList(GetClassNameWithSuffix(activeSuffix));
					stateLabel.AddToClassList(GetClassNameWithSuffix(activeLabelSuffix));
					stateLabel.text = "";
					break;
			}
		}
		
///// Util /////////////////////////////////////////////////////////////////////////////////////////
		
		private string GetComponentName(string component) {
			return $"{baseComponentName}-{component}";
		}
		
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}
		
		private string GetClassNameWithHover(string className) {
			return $"{className}:hover";
		}
		
		private string GetClassNameWithFocus(string className) {
			return $"{className}:focus";
		}

///// PUBLIC FUNCTIONS  ////////////////////////////////////////////////////////////////////////////
	
		public void UpdateComponent() {

			instructionLabel.text = InstructionName;

			SetInstructionState(State);
		}

///// PUBLIC CONSTRUCTORS //////////////////////////////////////////////////////////////////////////
		public new class UxmlFactory : UxmlFactory<InstructionField, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is InstructionField element ) {
					element.Clear();

					element.BuildComponent();
					element.UpdateComponent();
				}
			}
		}
		
		public InstructionField() : this("InstructionField", InstructionState.Active){}

		public InstructionField(string name, InstructionState state) {

			InstructionName = name;
			State = state;
			
			BuildComponent();
			UpdateComponent();
		}
		
		public InstructionField(TaskInfo taskInfo) {

			InstructionName = taskInfo.text;
			State = taskInfo.failed ? InstructionState.Failed : InstructionState.Active;
			State = taskInfo.done ? InstructionState.Done : InstructionState.Active;
			
			//todo add range, show range
			
			BuildComponent();
			UpdateComponent();
		}
	}
}