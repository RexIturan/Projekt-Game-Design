using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.Character {
	public enum CharacterIconCornerMode {
		POINTED_CORNERS,
		ROUNDED_CORNERS,
		ROUND_CORNERS,
	}
	
	public class CharacterIcon : VisualElement {
///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string baseUssClassName = "characterIcon";

		private static readonly string containerSuffix = "container";
		
		private static readonly string nameLabelSuffix = "nameLabel";
		
		private static readonly string iconContainerSuffix = "iconContainer";
		private static readonly string iconButtonSuffix = "iconButton";
		
		private static readonly string levelContainerSuffix = "levelContainer";
		private static readonly string levelBackgroundSuffix = "levelBackground";
		private static readonly string levelSuffix = "levelLabel";
		private static readonly string levelChangeSuffix = "levelChangeLabel";
		
		private static readonly string defaultStyleSheet = "UI/characterIcon";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////

		//todo button?
		//todo
		// image
		// name
		// roundness
		// level

///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement container;

		private Label characterNameLabel;
		
		private VisualElement levelContainer;
		//todo also a button?
		private VisualElement levelBackground;
		private Label levelElement;
		private Label levelChangeElement;

		private VisualElement iconContainer;
		private Button iconButton;

///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////		
		
		public bool ShowLevel { get; set; }
		public bool ShowLevelChange { get; set; }
		public bool ShowName { get; set; }

		public int Level { get; set; }
		public int LevelChange { get; set; }
		public string CharacterName { get; set; }
		public Sprite Image { get; set; }
		public CharacterIconCornerMode IconCornerMode { get; set; } 

///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////
		
		private void BuildComponent() {
			this.name = "CharacterIcon";

			//default styleSheet
			this.styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
			this.AddToClassList(baseUssClassName);

			container = new VisualElement {
				name = "CharacterIcon-Container"
			};
			container.AddToClassList(GetClassNameWithSuffix(containerSuffix));

			characterNameLabel = new Label {
				name = "CharacterIcon-NameLabel",
				text = "Name"
			};
			characterNameLabel.AddToClassList(GetClassNameWithSuffix(nameLabelSuffix));

			iconContainer = new VisualElement {
				name = "CharacterIcon-IconContainer"
			};
			iconContainer.AddToClassList(GetClassNameWithSuffix(iconContainerSuffix));

			iconButton = new Button {
				name = "CharacterIcon-IconButton"
			};
			iconButton.AddToClassList(GetClassNameWithSuffix(iconButtonSuffix));

			levelContainer = new VisualElement {
				name = "CharacterIcon-LevelContainer"
			};
			levelContainer.AddToClassList(GetClassNameWithSuffix(levelContainerSuffix));

			levelBackground = new VisualElement {
				name = "CharacterIcon-levelBackground"
			};
			levelBackground.AddToClassList(GetClassNameWithSuffix(levelBackgroundSuffix));

			levelElement = new Label {
				name = "CharacterIcon-LevelLabel",
				text = "1",
			};
			levelElement.AddToClassList(GetClassNameWithSuffix(levelSuffix));

			levelChangeElement = new Label {
				name = "CharacterIcon-LevelLabel",
				text = " 10",
			};
			levelChangeElement.AddToClassList(GetClassNameWithSuffix(levelChangeSuffix));

			levelBackground.Add(levelElement);
			levelBackground.Add(levelChangeElement);
			levelContainer.Add(levelBackground);

			iconContainer.Add(iconButton);
			iconContainer.Add(levelContainer);

			container.Add(characterNameLabel);
			container.Add(iconContainer);

			this.Add(container);
		}

///// Util /////////////////////////////////////////////////////////////////////////////////////////
		
		/// 
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}

///// PUBLIC FUNCTIONS  //////////////////////////////////////////////////////////////////////////// 

		public void UpdateComponent() {
			levelElement.text = Level.ToString();
			levelChangeElement.text = LevelChange > 0 ? "+" : "";
			levelChangeElement.text += LevelChange.ToString();
			characterNameLabel.text = CharacterName;

			if ( Image is { } ) {
				iconButton.style.backgroundImage = new StyleBackground(Image);
			}
			
			if ( ShowLevel ) {
				levelBackground.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
			}
			else {
				levelBackground.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
			}
			
			if ( ShowLevelChange ) {
				levelChangeElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
			}
			else {
				levelChangeElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
			}

			if ( ShowName ) {
				characterNameLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
			}
			else {
				characterNameLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
			}
		}

///// PUBLIC CONSTRUCTORS //////////////////////////////////////////////////////////////////////////		
		public new class UxmlFactory : UxmlFactory<CharacterIcon, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {

			private UxmlBoolAttributeDescription
				showNameAttribute = new UxmlBoolAttributeDescription {
					name = "Show-Name",
					defaultValue = true
				};
			
			private UxmlBoolAttributeDescription
				showLevelAttribute = new UxmlBoolAttributeDescription {
					name = "Show-Level",
					defaultValue = true
				};
			
			private UxmlBoolAttributeDescription
				showLevelChangeAttribute = new UxmlBoolAttributeDescription {
					name = "Show-LevelChange",
					defaultValue = true
				};
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is CharacterIcon element ) {
					element.Clear();

					element.ShowLevel = showLevelAttribute.GetValueFromBag(bag, cc);
					element.ShowName = showNameAttribute.GetValueFromBag(bag, cc);
					element.ShowLevelChange = showLevelChangeAttribute.GetValueFromBag(bag, cc);

					element.name = "CharacterIcon";

					element.BuildComponent();
					element.UpdateComponent();
				}
			}
		}
		
		public CharacterIcon() : this("Character Name", 1, 1, true, true, false){}

		public CharacterIcon(string name, int level, int change, bool showName, bool showLevel, bool showLevelChange) {

			ShowLevel = showLevel;
			ShowLevelChange = showLevelChange;
			ShowName = showName;

			Level = level;
			LevelChange = change;
			CharacterName = name;
			
			BuildComponent();
			UpdateComponent();
		}
	}
}