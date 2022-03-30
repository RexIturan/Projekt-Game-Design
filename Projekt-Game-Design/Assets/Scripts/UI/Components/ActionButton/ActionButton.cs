using System;
using System.Collections.Generic;
using UI.Components.Tooltip;
using UnityEngine;
using UnityEngine.UIElements;
using static GDP01.Util.UI.CustomVisualElementExtensions;

namespace UI.Components.ActionButton {
	
	internal interface IActionButton{
		int Id { get; set; }
		Sprite ImageData { get; set; }
		string Mapping { get; set; }
		string ActionText { get; set; }
	}
	
	public class ActionButton : VisualElement, IActionButton  {
///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private const string componentName = "ActionButton";
		
		private static readonly string baseUssClassName = "action-button";
		// private static readonly string containerSuffix = "container";
		private static readonly string topContainerSuffix = "top-container";
		private static readonly string nameSuffix = "name";
		private static readonly string mappingSuffix = "mapping";
		private static readonly string buttonSuffix = "button";
		private static readonly string buttonContainerSuffix = "button-container";
		private static readonly string imageSuffix = "image";
		private static readonly string cooldownSuffix = "cooldown";
		private static readonly string cooldownContainerSuffix = "cooldown-container";
		
		private static readonly string selectedClassName = "selected";
		
		private static readonly string defaultStyleSheet = "UI/actionButton";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////

		private Action onFocusCallback;
		private Action onBlurCallback;
		private Action onClickCallback;
		
///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement actionImage;
		private VisualElement buttonContainer;
		private GroupedButton button;
		private Label nameLabel;
		private Label mappingLabel;
		private VisualElement cooldownContainer;
		private Label cooldownLabel;
		private AbilityTooltip actionTooltip;
		
///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////

		public int Id { get; set; }
		public Sprite ImageData { get; set; }
		public string Mapping { get; set; }
		public string ActionText { get; set; }
		public int Cooldown { get; set; } 
		public Button Button => button;
		
		public bool ShowName { get; set; }
		public bool Selected { get; set; }
		
///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////		
		
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}
		
		private void BuildActionButton(string componentNameSuffix) {
			name = $"{componentName}_{componentNameSuffix}";
			AddToClassList(baseUssClassName);
			
			StyleSheet style = Resources.Load<StyleSheet>(defaultStyleSheet);
			this.styleSheets.Add(style);
			
			var topContainer = new VisualElement() { name = "ActionButton_Top_Container" };
			topContainer.AddToClassList(GetClassNameWithSuffix(topContainerSuffix));

			InitContainer(ref buttonContainer,  componentName, 
				"Button_Container",
				baseUssClassName, new []{buttonContainerSuffix});
			
			
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
			
			//cooldown
			InitContainer(ref cooldownContainer, componentName, 
				"Cooldown_Container",
				baseUssClassName, new []{cooldownContainerSuffix});
			
			InitLabel(ref cooldownLabel, componentName, "Cooldown_Label", 
				"0", baseUssClassName, cooldownSuffix);

			cooldownLabel.pickingMode = PickingMode.Ignore;
			cooldownContainer.pickingMode = PickingMode.Ignore;
			cooldownContainer.Add(cooldownLabel);
			
			topContainer.Add( mappingLabel );
			actionImage.Add( topContainer );
			button.Add( actionImage );

			buttonContainer.pickingMode = PickingMode.Ignore;
			buttonContainer.Add(button);
			buttonContainer.Add(cooldownContainer);
			
			Add( nameLabel );
			Add( buttonContainer );
			
			//tooltip
			actionTooltip = new AbilityTooltip(this);
		}

		private void HandleFocus(EventBase evt) {
			onFocusCallback();
		}
		
		private void HandleBlur(EventBase evt) {
			onBlurCallback();
		}

		private void HandleClick() {
			onClickCallback?.Invoke();
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

			if ( Cooldown > 0 ) {
				cooldownLabel.text = Cooldown.ToString();
				cooldownLabel.SetStyleDisplayVisibility(true);
			}
			else {
				cooldownLabel.SetStyleDisplayVisibility(false);
			}

			if ( Selected ) {
				AddToClassList(GetClassNameWithSuffix(selectedClassName));
			}
			else {
				RemoveFromClassList(GetClassNameWithSuffix(selectedClassName));
			}
			
			nameLabel.SetStyleDisplayVisibility(ShowName);
		}
		
		public void BindOnClickedAction(Action<object[]> onClick, object[] args) {
			onClickCallback = () => onClick(args);
			
			// button.clicked += this.callback;
			button.RegisterCallback<FocusEvent>(HandleFocus);
			button.RegisterCallback<BlurEvent>(HandleBlur);
			button.clicked += HandleClick;
		}
		
		public void BindOnClickedAction(Action<object[]> onFocus, Action<object[]> onBlur, object[] args) {
			onFocusCallback = () => onFocus(args);
			onBlurCallback = () => onBlur(args);
			
			// button.clicked += this.callback;
			button.RegisterCallback<FocusEvent>(HandleFocus);
			button.RegisterCallback<BlurEvent>(HandleBlur);
			button.clicked += HandleClick;
		}

		public void UnbindOnClickedAction() {
			// button.clicked -= this.callback;
			button.UnregisterCallback<FocusEvent>(HandleFocus);
			button.UnregisterCallback<BlurEvent>(HandleBlur);
			button.clicked -= HandleClick;
			onFocusCallback = null;
			onBlurCallback = null;
		}

		public void SetupActionButton(Sprite image, string text) {
			ImageData = image;
			ActionText = text;
			UpdateComponent();
		}
		
		public void SetupActionButton(AbilitySO abilitySO, int cooldown) {
			ImageData = abilitySO.icon;
			ActionText = abilitySO.name;
			Cooldown = cooldown;
			SetupTooltip(abilitySO);
			UpdateComponent();
		}

		public void SetupTooltip(AbilitySO ability) {
			actionTooltip.UpdateValues(ability);
			actionTooltip.Activate();
		}
		
		public void ResetActionButton() {
			ImageData = null;
			ActionText = "";
			Cooldown = 0;
			UnbindOnClickedAction();
			UpdateComponent();
		}

		public void ResetTooltip() {
			actionTooltip.Deactivate();
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
		
		public ActionButton(string componentName, int id, string mapping, string text, bool showName,  Sprite img = null, int cooldown = 0) {
			this.Id = id;
			this.Mapping = mapping;
			ActionText = text;
			ImageData = img;
			ShowName = showName;
			Cooldown = cooldown;
			Selected = false;
			
			BuildActionButton(componentName);
			UpdateComponent();
		}
		
		/// <summary>
		/// Cleanup tooltip on deletion. 
		/// </summary>
		~ActionButton() {
			actionTooltip.Remove();
		}
	}
}