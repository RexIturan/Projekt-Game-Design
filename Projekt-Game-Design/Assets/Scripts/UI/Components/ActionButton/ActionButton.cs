using System;
using System.Collections.Generic;
using GDP01.Util.Util.UI;
using UI.Components.Tooltip;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.ActionButton {
	
	internal interface IActionButton{
		int Id { get; set; }
		Sprite ImageData { get; set; }
		string Mapping { get; set; }
		string ActionText { get; set; }
	}
	
	public class ActionButton : VisualElement, IActionButton  {
		///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string baseUssClassName = "action-button";
		// private static readonly string containerSuffix = "container";
		private static readonly string topContainerSuffix = "top-container";
		private static readonly string nameSuffix = "name";
		private static readonly string mappingSuffix = "mapping";
		private static readonly string buttonSuffix = "button";
		private static readonly string imageSuffix = "image";
		
		private static readonly string defaultStyleSheet = "UI/actionButton";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////

		private Action onFocusCallback;
		private Action onBlurCallback;
		
///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement actionImage;
		private GroupedButton button;
		private Label nameLabel;
		private Label mappingLabel;
		private AbilityTooltip actionTooltip;
		
///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////

		public int Id { get; set; }
		public Sprite ImageData { get; set; }
		public string Mapping { get; set; }
		public string ActionText { get; set; }
		public Button Button => button;
		
		public bool ShowName { get; set; }
		
///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////		
		
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}
		
		private void BuildActionButton(string componentName) {
			name = $"ActionButton_{componentName}";
			AddToClassList(baseUssClassName);
			
			StyleSheet style = Resources.Load<StyleSheet>(defaultStyleSheet);
			this.styleSheets.Add(style);
			
			var topContainer = new VisualElement() { name = "ActionButton_Top_Container" };
			topContainer.AddToClassList(GetClassNameWithSuffix(topContainerSuffix));

			button = new GroupedButton() { name = "ActionButton_Button"};
			button.AddToClassList(GetClassNameWithSuffix(buttonSuffix));

			actionImage = new VisualElement() { name = "ActionButton_Image" };
			actionImage.AddToClassList(GetClassNameWithSuffix(imageSuffix));
			
			mappingLabel = new Label() {
				name = "Mapping_Label",
				text = Mapping,
			};
			mappingLabel.RegisterCallback<ChangeEvent<string>>(evt => {});
			mappingLabel.AddToClassList(GetClassNameWithSuffix(mappingSuffix));
			
			nameLabel = new Label() {
				name = "Name_Label",
				text = ActionText,
			};
			nameLabel.AddToClassList(GetClassNameWithSuffix(nameSuffix));
			
			topContainer.Add( mappingLabel );
			actionImage.Add( topContainer );
			button.Add( actionImage );

			Add( nameLabel );
			Add( button );
			
			//tooltip
			actionTooltip = new AbilityTooltip(this);
		}

		private void HandleFocus(EventBase evt) {
			onFocusCallback();
		}
		
		private void HandleBlur(EventBase evt) {
			onBlurCallback();
		}
		
///// PUBLIC FUNCTIONS /////////////////////////////////////////////////////////////////////////////

		public void UpdateComponent() {
			mappingLabel.text = Mapping;
			nameLabel.text = ActionText;

			if ( ImageData is {} ) {
				actionImage.style.backgroundImage = Background.FromSprite(ImageData);	
			}
			else {
				actionImage.style.backgroundImage = null;
			}
			
			nameLabel.SetStyleDisplayVisibility(ShowName);

			actionTooltip.UpdateValues(nameLabel.text, "This is a placeholder. You can see it's a placeholder. This means, that this code needs to be reworked. Yes. " + 
					"The deal is, this function should also require a description, but I don't want to touch too many things. ");
			actionTooltip.Activate();
		}
		
		public void BindOnClickedAction(Action<object[]> onFocus, Action<object[]> onBlur, object[] args) {
			onFocusCallback = () => onFocus(args);
			onBlurCallback = () => onBlur(args);
			
			// button.clicked += this.callback;
			button.RegisterCallback<FocusEvent>(HandleFocus);
			button.RegisterCallback<BlurEvent>(HandleBlur);
		}

		public void UnbindOnClickedAction() {
			// button.clicked -= this.callback;
			button.UnregisterCallback<FocusEvent>(HandleFocus);
			button.UnregisterCallback<BlurEvent>(HandleBlur);
			onFocusCallback = null;
			onBlurCallback = null;
			actionTooltip.Deactivate();
		}

		public void SetupActionButton(Sprite image, string text) {
			ImageData = image;
			ActionText = text;
			UpdateComponent();
		}
		
		public void ResetActionButton() {
			ImageData = null;
			ActionText = "";
			UnbindOnClickedAction();
			UpdateComponent();
		}
		
///// PUBLIC Constructors ///////////////////////////////////////////////////////////////////////////
		public new class UxmlFactory : UxmlFactory<ActionButton, UxmlTraits> {}

		public new class UxmlTraits : VisualElement.UxmlTraits {
			//image
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				//todo add attributes
				// - name
				// - showName
				
				if ( ve is ActionButton element ) {
					element.Clear();
					//do stuff
					
					element.BuildActionButton(default);
					element.UpdateComponent();
				}
			}
		}

		public ActionButton() : this("default", 0, "NA","no name", true) { }
		
		public ActionButton(string componentName, int id, string mapping, string text, bool showName,  Sprite img = null) {
			this.Id = id;
			this.Mapping = mapping;
			ActionText = text;
			ImageData = img;
			ShowName = showName;
			
			BuildActionButton(componentName);
			UpdateComponent();
		}
	}
}