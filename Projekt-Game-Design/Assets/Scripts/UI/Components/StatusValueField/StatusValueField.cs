using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components {

	public enum StatusValueField_ModifierType {
		None,		
		Add,
		Subtract,
		Multiply,
	}
	
	public enum StatusValueField_ValueType {
		Percent,
		Flat,
		Recource,
	}
	
	public class StatusValueField : VisualElement {
///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string baseUssClassName = "statusValueField";
		
		private static readonly string containerSuffix = "container";
		private static readonly string titleLabelSuffix = "titleLabel";
		private static readonly string iconSuffix = "icon";
		private static readonly string valueContainerSuffix = "valueContainer";
		private static readonly string valueLabelSuffix = "valueLabel";
		private static readonly string valueChangeLabelSuffix = "valueChangeLabel";
		private static readonly string valueChangeContainerSuffix = "valueChangeLabel-container";
		
		private static readonly string defaultStyleSheet = "UI/statusValueField";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////

///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement container;
		private Label titleLabel;
		private VisualElement iconElement;
		private VisualElement valueContainer;
		private Label valueLabel;
		private VisualElement valueChangeContainer;
		private Label valueChangeLabel;

///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////

		public string Title { get; set; }
		public int Min { get; set; }
		public int Max { get; set; }
		public int Value { get; set; }
		public int ChangeValue { get; set; }
		public StatusValueField_ValueType ValueType { get; set; }
		public StatusValueField_ModifierType ModifierType { get; set; }
		
		public Sprite Image { get; set; }

///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////

		private void BuildComponents() {
			
			//init base Component
			this.name = "StatusValueField";

			//default styleSheet
			this.styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
			this.AddToClassList(baseUssClassName);
			
			//Build Sub Components
			container = new VisualElement {
				name = "StatusValueField-Container"
			};
			container.AddToClassList(GetClassNameWithSuffix(containerSuffix));
			
			titleLabel = new Label {
				name = "StatusValueField-TitleLabel"
			};
			titleLabel.AddToClassList(GetClassNameWithSuffix(titleLabelSuffix));
			
			iconElement = new VisualElement {
				name = "StatusValueField-Icon"
			};
			iconElement.AddToClassList(GetClassNameWithSuffix(iconSuffix));

			valueContainer = new VisualElement {
				name = "StatusValueField-ValueConatiner"
			};
			valueContainer.AddToClassList(GetClassNameWithSuffix(valueContainerSuffix));
			
			valueLabel = new Label {
				name = "StatusValueField-ValueLabel"
			};
			valueLabel.AddToClassList(GetClassNameWithSuffix(valueLabelSuffix));
			
			valueChangeContainer = new VisualElement {
				name = "StatusValueField-ValueConatiner"
			};
			valueChangeContainer.AddToClassList(GetClassNameWithSuffix(valueChangeContainerSuffix));
			
			valueChangeLabel = new Label {
				name = "StatusValueField-ValueChangeLabel"
			};
			valueChangeLabel.AddToClassList(GetClassNameWithSuffix(valueChangeLabelSuffix));
			
			container.Add(titleLabel);
			// container.Add(iconElement);
			valueContainer.Add(valueLabel);
			valueChangeContainer.Add(valueChangeLabel);
			valueContainer.Add(valueChangeContainer);
			container.Add(valueContainer);
			
			this.Add(container);
		}

		private void UpdateValueLabel() {
			string newStr;
			
			switch ( ValueType ) {
				case StatusValueField_ValueType.Recource:
					newStr = $"{Value}/{Max}";
					break;
				case StatusValueField_ValueType.Percent:
					newStr = $"{Value}%";
					break;
				case StatusValueField_ValueType.Flat:
				default:
					newStr = $"{Value}";
					break;
			}

			if ( ValueType != StatusValueField_ValueType.Recource ) {
				var op = "";
				switch ( ModifierType ) {
					case StatusValueField_ModifierType.Add:
						op = "+";
						break;
					case StatusValueField_ModifierType.Subtract:
						op = "-";
						break;
					case StatusValueField_ModifierType.Multiply:
						op = "*";
						break;
					case StatusValueField_ModifierType.None:
					default:
						break;
				}
				newStr = $"{op}{newStr}";
			}

			valueLabel.text = newStr;
		}

///// Util /////////////////////////////////////////////////////////////////////////////////////////
 
		/// 
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}

///// PUBLIC FUNCTIONS  ////////////////////////////////////////////////////////////////////////////

		public void UpdateComponents() {

			titleLabel.text = Title;
			
			if ( Image != null ) {
				iconElement.style.backgroundImage = new StyleBackground(Image);	
			}

			if ( ChangeValue != 0 ) {
				valueChangeLabel.text = ChangeValue > 0 ? $" +{ChangeValue}" : $" {ChangeValue}";
			}
			
			UpdateValueLabel();
		}

///// PUBLIC CONSTRUCTORS //////////////////////////////////////////////////////////////////////////
		public new class UxmlFactory : UxmlFactory<StatusValueField, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {

			private UxmlStringAttributeDescription
				titleAttribute = new UxmlStringAttributeDescription { name = "Title"};

			private UxmlIntAttributeDescription maxValueAttribute = new UxmlIntAttributeDescription {
				name = "Max"
			};

			private UxmlIntAttributeDescription currentValueAttribute = new UxmlIntAttributeDescription {
				name = "Value"
			};
			
			private UxmlIntAttributeDescription changeValueAttribute = new UxmlIntAttributeDescription {
				name = "Value-Change"
			};
			
			private UxmlEnumAttributeDescription<StatusValueField_ValueType> valueTypeAttribute =
				new UxmlEnumAttributeDescription<StatusValueField_ValueType> {
					name = "Value-Type",
					defaultValue = StatusValueField_ValueType.Flat
				};

			private UxmlEnumAttributeDescription<StatusValueField_ModifierType> modifierTypeAttribute =
				new UxmlEnumAttributeDescription<StatusValueField_ModifierType> {
					name = "Modifier-Type",
					defaultValue = StatusValueField_ModifierType.None
				};
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is StatusValueField element ) {
					
					element.Clear();

					element.Title = titleAttribute.GetValueFromBag(bag, cc);
					element.Value = currentValueAttribute.GetValueFromBag(bag, cc);
					element.Max = maxValueAttribute.GetValueFromBag(bag, cc);
					element.ChangeValue = changeValueAttribute.GetValueFromBag(bag, cc);
					element.ValueType = valueTypeAttribute.GetValueFromBag(bag, cc);
					element.ModifierType = modifierTypeAttribute.GetValueFromBag(bag, cc);
					
					element.BuildComponents();
					element.UpdateComponents();
				}
			}
		}
		
		public StatusValueField() : this("Test Value", 0, 100, 10, 1, StatusValueField_ValueType.Flat, StatusValueField_ModifierType.None, null){}
		public StatusValueField(string title, int value, int change) : this(title, 0, 100, value, change, StatusValueField_ValueType.Flat, StatusValueField_ModifierType.None, null){}

		public StatusValueField(string title, int min, int max, int value, int change, StatusValueField_ValueType valueType, StatusValueField_ModifierType modifierType, Sprite image) {
			Title = title;
			Min = min;
			Max = max;
			Value = value;
			ChangeValue = change;
			ValueType = valueType;
			ModifierType = modifierType;
			Image = image;

			BuildComponents();
			UpdateComponents();
		}
	}
}