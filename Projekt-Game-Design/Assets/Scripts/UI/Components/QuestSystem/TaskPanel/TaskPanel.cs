using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.QuestSystem {
	public class TaskPanel : VisualElement {
///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string baseComponentName = "TaskPanel";

		private static readonly string baseUssClassName = "taskPanel";
		private static readonly string containerSuffix = "container";
		private static readonly string containerContentSuffix = "container-content";
		
		// private static readonly string buttonContainerSuffix = "buttonContainer";
		private static readonly string topButtonContainerSuffix = "topButtonContainer";
		private static readonly string navigationButtonContainerSuffix = "navigationButtonContainer";
		private static readonly string buttonSuffix = "button";
		private static readonly string navigationbuttonSuffix = "button-navigation";
		
		
		private static readonly string titleSuffix = "titleLabel";
		
		private static readonly string textContainerSuffix = "textContainer";
		
		private static readonly string descriptionContainerSuffix = "descriptionContainer";
		private static readonly string descriptionSuffix = "descriptionLabel";
		
		private static readonly string instructionContainerSuffix = "instructionContainer";
		private static readonly string instructionSuffix = "instructionLabel";
		
		private static readonly string nextClassName = "next";
		private static readonly string previousClassName = "previous";
		private static readonly string helpClassName = "help";
		private static readonly string skipClassName = "skip";
		
		private static readonly string defaultStyleSheet = "UI/taskPanel";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////



///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement container;
		private VisualElement containerContent;

		private Label titleLabel;
		
		private VisualElement descriptionContainer;
		private Label descriptionLabel;
		
		private VisualElement instructionContainer;
		private Label instructionLabel;

		private List<InstructionField> instructions;

		//todo
		// Task State
		private VisualElement topButtonContainer;
		private VisualElement navigationButtonContainer;
		
		// private VisualElement buttonContainer;
		
		//buttons
		private VisualElement nextContainer;
		private Button nextButton;
		
		private VisualElement previousContainer;
		private Button previousButton;
		
		private VisualElement skipContainer;
		private Button skipButton;
		
		private VisualElement helpContainer;
		private Button helpButton;

///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////

		public List<InstructionWrapper> instructionList;
		public string Title;
		public string Description;
		public Action Next;

		public bool ShowHelp;
		public bool ShowSkip;
		public bool ShowPrevious;
		public bool ShowNext;

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
				new []{containerSuffix});

			InitLabel(
				ref titleLabel, 
				"TitleLabel", 
				"Task Title", 
				titleSuffix);

			{ // Description
				InitContainer(
					ref descriptionContainer,
					"DescriptionContainer",
					new []{textContainerSuffix, descriptionContainerSuffix});

				InitLabel(
					ref descriptionLabel,
					"DescriptionLabel",
					"This is a Test Task, and this is its Description!\n" +
					"Next Line Of the Description.",
					descriptionSuffix);
			}
			
			InitContainer(
				ref instructionContainer,
				"InstructionContainer",
				new []{textContainerSuffix, instructionContainerSuffix});
			
			InitLabel(
				ref instructionLabel, 
				"InstructionLabel", 
				"Instructions:", 
				instructionSuffix);
			
			InitContainer(
				ref topButtonContainer, 
				"TopButtonContainer", 
				new []{topButtonContainerSuffix});

			InitContainer(
				ref containerContent, 
				"ContainerContent", 
				new []{containerContentSuffix});
			
			InitContainer(
				ref skipContainer, 
				"SkipContainer", 
				new []{ConcatClassNames(skipClassName, containerSuffix)});
			
			InitButton(
				ref skipButton, 
				"TaskPanel-SkipButton", 
				"skip", 
				skipClassName,
				new string[]{});
			
			InitContainer(
				ref helpContainer, 
				"HelpContainer", 
				new []{ConcatClassNames(helpClassName, containerSuffix)});
			
			InitButton(
				ref helpButton, 
				"TaskPanel-HelpButton", 
				"help", 
				helpClassName,
				new string[]{});
			
			InitContainer(
				ref navigationButtonContainer, 
				"NavigationButtonContainer", 
				new []{navigationButtonContainerSuffix});
			
			InitContainer(
				ref nextContainer, 
				"NextContainer", 
				new []{ConcatClassNames(nextClassName, containerSuffix)});
			
			InitButton(
				ref nextButton, 
				"TaskPanel-NextButton", 
				"next >>", 
				nextClassName,
				new []{navigationbuttonSuffix});
			
			
			InitContainer(
				ref previousContainer, 
				"PreviousContainer", 
				new []{ConcatClassNames(previousClassName, containerSuffix)});
			
			InitButton(
				ref previousButton, 
				"TaskPanel-PreviousButton", 
				"<< previous", 
				previousClassName,
				new []{navigationbuttonSuffix});

			helpContainer.Add(helpButton);
			skipContainer.Add(skipButton);
			topButtonContainer.Add(helpContainer);
			topButtonContainer.Add(skipContainer);

			previousContainer.Add(previousButton);
			nextContainer.Add(nextButton);
			navigationButtonContainer.Add(previousContainer);
			navigationButtonContainer.Add(nextContainer);
			
			descriptionContainer.Add(descriptionLabel);
			instructionContainer.Add(instructionLabel);
			
			containerContent.Add(topButtonContainer);
			
			container.Add(containerContent);
			container.Add(titleLabel);
			container.Add(descriptionContainer);
			container.Add(instructionContainer);
			container.Add(navigationButtonContainer);

			this.Add(container);
		}
		
///// Util /////////////////////////////////////////////////////////////////////////////////////////
		
		private string GetComponentName(string component) {
			return $"{baseComponentName}-{component}";
		}
		
		private string GetClassNameWithSuffix(string suffix) {
			return ConcatClassNames(baseUssClassName, suffix);
		}

		private string ConcatClassNames(string firstSuffix, string secondSuffix) {
			return $"{firstSuffix}-{secondSuffix}";	
		}
		
		private string GetClassNameWithHover(string className) {
			return $"{className}:hover";
		}
		
		private string GetClassNameWithFocus(string className) {
			return $"{className}:focus";
		}

///// PUBLIC FUNCTIONS  ////////////////////////////////////////////////////////////////////////////
	
		public void UpdateComponent() {

			SetVisibility(helpButton, ShowHelp);
			SetVisibility(skipButton, ShowSkip);
			SetVisibility(previousButton, ShowPrevious);
			SetVisibility(nextButton, ShowNext);

			descriptionLabel.text = Description;
			titleLabel.text = Title;
			
			instructions.ForEach(field => {
				if ( instructionContainer.Contains(field) ) {
					instructionContainer.Remove(field);
				}
			});
			instructions.Clear();
			
			foreach ( var i in instructionList ) {
				var instruction = new InstructionField(i.text, i.state);
				instructions.Add(instruction);
				instructionContainer.Add(instruction);
			}
		}
		
		public void SetVisibility(bool visibility) {
			SetVisibility(this, visibility);
		}
		
		public void SetVisibility(VisualElement element, bool visibility) {
			if ( visibility ) {
				element.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
			} else {
				element.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
			}
		}

		public void SetNextCallback(Action callback) {
			nextButton.clicked -= Next;
			Next = callback;
			nextButton.clicked += Next;
		}
		
		public void RemoveNextCallback(Action callback) {
			nextButton.clicked -= callback;
		}
		
		public void SetPreviousCallback(Action callback) {
			previousButton.clicked += callback;
		}
		
		public void RemovePreviousCallback(Action callback) {
			previousButton.clicked -= callback;
		}
		
		public void SetSkipCallback(Action callback) {
			skipButton.clicked += callback;
		}
		
		public void RemoveSkipCallback(Action callback) {
			skipButton.clicked -= callback;
		}
		
		public void SetHelpCallback(Action callback) {
			helpButton.clicked += callback;
		}
		
		public void RemoveHelpCallback(Action callback) {
			helpButton.clicked -= callback;
		}
		
///// PUBLIC CONSTRUCTORS //////////////////////////////////////////////////////////////////////////
		public new class UxmlFactory : UxmlFactory<TaskPanel, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is TaskPanel element ) {
					element.Clear();

					element.BuildComponent();
					element.UpdateComponent();
				}
			}
		}
		
		public TaskPanel() : this("TaskPanel", "No Description"){}

		public TaskPanel(string title, string description) {
			instructions = new List<InstructionField>();
			instructionList = new List<InstructionWrapper>();

			Title = title;
			Description = description;

			ShowHelp = false;
			ShowSkip = false;
			ShowPrevious = false;
			ShowNext = true;
			
			// instructionList.Add(new InstructionWrapper {
			// 	state = InstructionState.Done,
			// 	text = "Press [W][ArrowUp]"
			// });
			//
			// instructionList.Add(new InstructionWrapper {
			// 	state = InstructionState.Done,
			// 	text = "Press [A][ArrowLeft]"
			// });
			//
			// instructionList.Add(new InstructionWrapper {
			// 	state = InstructionState.Failed,
			// 	text = "Press [S][ArrowDown]"
			// });
			//
			// instructionList.Add(new InstructionWrapper {
			// 	state = InstructionState.Active,
			// 	text = "Press [D] 1/5"
			// });
			
			BuildComponent();
			UpdateComponent();
		}
	}
}