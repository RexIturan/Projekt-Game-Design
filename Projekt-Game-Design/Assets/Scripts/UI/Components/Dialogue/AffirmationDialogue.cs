using System;
using UnityEngine;
using UnityEngine.UIElements;

public class AffirmationDialogue : VisualElement
{
		private static readonly string defaultStyleSheet = "affirmationDialogue";
		private static readonly string className = "affirmationDialogue";
		private static readonly string classNameHeader = "header";
		private static readonly string classNameButtonContainer = "buttonContainer";

		private TextElement header;
		private TextElement description;
		private Button affirmationButton;
		private Button cancelButton;

		private Action callbackAffirmation;
		private Action callbackCancel;

		public AffirmationDialogue(string headerText, string descriptionText, 
				Action callbackAffirmation, string affirmationText, 
				Action callbackCancel, string cancelText) {

				// Setting up style
				styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
				AddToClassList(className);

				// header
				header = new TextElement();
				header.AddToClassList(classNameHeader);
				header.text = headerText;
				Add(header);

				// description
				if(!descriptionText.Equals("")) {
						description = new TextElement();
						description.text = descriptionText;
						Add(description);
				}

				// buttons
				VisualElement buttonContainer = new VisualElement();
				buttonContainer.AddToClassList(classNameButtonContainer);

				this.callbackAffirmation = callbackAffirmation;
				this.callbackCancel = callbackCancel;

				affirmationButton = new Button();
				affirmationButton.clicked += callbackAffirmation;
				affirmationButton.text = affirmationText;
				buttonContainer.Add(affirmationButton);

				if ( callbackCancel != null ) {
						cancelButton = new Button();
						cancelButton.clicked += callbackCancel;
						cancelButton.text = cancelText;
						buttonContainer.Add(cancelButton);
				}

				Add(buttonContainer);
		}

		public void UnbindActions() {
				affirmationButton.clicked -= callbackAffirmation;
				if(cancelButton != null)
						cancelButton.clicked -= callbackCancel;
		}
}
