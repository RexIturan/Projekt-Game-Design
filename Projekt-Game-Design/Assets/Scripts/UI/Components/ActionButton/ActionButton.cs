using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.ActionButton {
	
	internal interface IActionButton{
		int id { get; set; }
		Sprite imageData { get; set; }
		string mapping { get; set; }
		string actionText { get; set; }
	}
	
	public class ActionButton : VisualElement, IActionButton  {
		// uss const values
		private static readonly string baseUssClassName = "action-button";
		private static readonly string containerSuffix = "container";
		private static readonly string topContainerSuffix = "top-container";
		private static readonly string nameSuffix = "name";
		private static readonly string mappingSuffix = "mapping";
		private static readonly string buttonSuffix = "button";
		private static readonly string imageSuffix = "image";

		public int id { get; set; }
		public Sprite imageData { get; set; }
		public string mapping { get; set; }
		public string actionText { get; set; }
		private Action callback;
		
		private VisualElement actionImage;
		private Button button;
		private Label nameLabel;
		private Label mappingLabel;
		
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}
		
		private void BuildActionButton(string componentName, bool withName) {
			name = $"ActionButton_{componentName}";
			AddToClassList(baseUssClassName);
			
			var topContainer = new VisualElement() { name = "ActionButton_Top_Container" };
			topContainer.AddToClassList(GetClassNameWithSuffix(topContainerSuffix));

			button = new Button() { name = "ActionButton_Button"};
			button.AddToClassList(GetClassNameWithSuffix(buttonSuffix));

			actionImage = new VisualElement() { name = "ActionButton_Image" };
			actionImage.AddToClassList(GetClassNameWithSuffix(imageSuffix));
			
			mappingLabel = new Label() {
				name = "Mapping_Label",
				// binding = 
				text = mapping,
			};
			mappingLabel.RegisterCallback<ChangeEvent<string>>(evt => {});
			mappingLabel.AddToClassList(GetClassNameWithSuffix(mappingSuffix));
			
			nameLabel = new Label() {
				name = "Name_Label",
				text = actionText,
			};
			nameLabel.AddToClassList(GetClassNameWithSuffix(nameSuffix));
			
			topContainer.Add( mappingLabel );
			actionImage.Add( topContainer );
			button.Add( actionImage );

			//todo make toggleable
			if ( withName ) {
				Add( nameLabel );
			}
			Add( button );
			
			//tooltip
			//todo tooltip window
			tooltip = "this is a tooltip";
			// this.RegisterCallback<MouseOverEvent>();
		}
		
		public new class UxmlFactory : UxmlFactory<ActionButton, UxmlTraits> {}

		public new class UxmlTraits : VisualElement.UxmlTraits {
			//image
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is ActionButton element ) {
					//do stuff
					element.Add(new Label("ActionButton_Text") { text = "Action Button" });
				}
			}
		}

		public ActionButton() : this("default", 0, "NA","no name", true) { }
		
		public ActionButton(string componentName, int id, string mapping, string text, bool withName,  Sprite img = null) {
			this.id = id;
			this.mapping = mapping;
			actionText = text;
			imageData = img;
			
			BuildActionButton(componentName, withName);
		}

		public void UpdataValues() {
			mappingLabel.text = mapping;
			nameLabel.text = actionText;

			if ( imageData != null ) {
				actionImage.style.backgroundImage = Background.FromSprite(imageData);	
			}
			else {
				actionImage.style.backgroundImage = null;
			}
		}

		public void SetMapping(string newMapping) {
			mapping = newMapping;
			UpdataValues();
		}
		
		public void BindAction(Action<object[]> actionCallback, object[] args, Sprite image, string text) {
			imageData = image;
			actionText = text;
			this.callback = () => actionCallback(args);
			
			button.clicked += this.callback;
			
			UpdataValues();
		}

		public void UnbindAction() {
			imageData = null;
			actionText = "";

			button.clicked -= this.callback;
			
			this.callback = null;
			UpdataValues();
		}
	}
}