using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace UI.Components {
	internal interface IProgressBar {
		int Min { get; set; }
		int Max { get; set; }
		int Value { get; set; }
		int ChangeValue { get; set; }
		string Title { get; set; }
	}
	
	public class ProgressBar : VisualElement, IProgressBar {
		private static readonly string baseUssClassName = "progressBar";
		private static readonly string containerWrapperSuffix = "container-wrapper";
		private static readonly string containerSuffix = "container";
		private static readonly string barContainerSuffix = "barContainer";
		private static readonly string barSuffix = "bar";
		private static readonly string changeBarSuffix = "changeBar";
		private static readonly string textSuffix = "text";
		private static readonly string titleSuffix = "title";
		private static readonly string changeTextSuffix = "changeText";
		private static readonly string textContainerSuffix = "textContainer";
		private static readonly string increaseUssClassName = "increase";
		private static readonly string decreaseUssClassName = "decrease";

		//--- PROGRESS BAR ---//
		// current  value
		// min
		// max 
		// base color
		// add color
		// remove color
		// background color

		private bool showText = false;
		
		private int min;
		private int max;
		private int value;
		private int changeValue;
		
		public bool ShowChange { get; set; }
		
		public int Min {
			get => this.min;
			set { this.min = value > max ? max : value; }
		}
		
		public int Max {
			get { return max; } 
			set { this.max = value < min ? min : value; }
		}
		
		public int Value {
			get { return value; }
			set {
				if ( value > max ) {
					this.value = max;
				} else if ( value < min ) {
					this.value = min;
				}
				else {
					this.value = value;
				}
			}
		}
		
		public int ChangeValue {
			get { return changeValue; }
			set {
				if ( this.value + value > max ) {
					//todo full or just relevant range
					this.changeValue = max - this.value;
				} else if ( this.value + value < min ) {
					//todo full or just relevant range
					this.changeValue = this.value;
				}
				else {
					this.changeValue = value;
				}
			}
		}
		
		public string Title { get; set; }

		private VisualElement container;
		
		private VisualElement barContainer;
		private VisualElement bar;
		private VisualElement changeBar;
		//todo rename
		private VisualElement valueTextContainer;
		private Label titleLabel;
		private Label valueTextLabel;
		private Label changeTextLabel;

///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////

		private void ChangeProgressBarWidth(float valueBar, float changeValueBar) {
			bar.style.width = new StyleLength(Length.Percent(valueBar));
			changeBar.style.width = new StyleLength(Length.Percent(changeValueBar));
		}

		private void UpdateValueLabel() {
			var op = changeValue > 0 ? "+" : "-";
			var change = ShowChange ? $"   {op} {Mathf.Abs(changeValue)}" : "";

			valueTextLabel.text = $"{value} / {max}";
			changeTextLabel.text = change;
		}

		private void SetChangeBarStyle(bool increase) {
			if ( increase ) {
				changeBar.RemoveFromClassList(decreaseUssClassName);
				changeBar.AddToClassList(increaseUssClassName);	
			}
			else {
				changeBar.RemoveFromClassList(increaseUssClassName);
				changeBar.AddToClassList(decreaseUssClassName);
			}
		} 
		
		private void BuildComponents() {
			this.name = "ProgressBar";

			//default styleSheet
			this.styleSheets.Add(Resources.Load<StyleSheet>("UI/progressBar"));
			this.AddToClassList(baseUssClassName);

			container = new VisualElement {
				name = "ProgressBar-Container",
			};
			container.AddToClassList(GetClassNameWithSuffix(containerWrapperSuffix));

			barContainer = new VisualElement {
				name = "ProgressBar-BarContainer",
			};
			barContainer.AddToClassList(GetClassNameWithSuffix(barContainerSuffix));
			barContainer.AddToClassList(GetClassNameWithSuffix(containerSuffix));

			bar = new VisualElement {
				name = "ProgressBar-Bar",
			};
			bar.AddToClassList(GetClassNameWithSuffix(barSuffix));

			changeBar = new VisualElement {
				name = "ProgressBar-ChangeBar",
			};
			changeBar.AddToClassList(GetClassNameWithSuffix(changeBarSuffix));
			changeBar.AddToClassList(increaseUssClassName);

			valueTextContainer = new VisualElement {
				name = "ProgressBar-TextContainer"
			};
			valueTextContainer.AddToClassList(GetClassNameWithSuffix(containerSuffix));
			valueTextContainer.AddToClassList(GetClassNameWithSuffix(textContainerSuffix));

			valueTextLabel = new Label {
				name = "ProgressBar-Text"
			};
			valueTextLabel.AddToClassList(GetClassNameWithSuffix(textSuffix));

			changeTextLabel = new Label {
				name = "ProgressBar-ChangeText"
			};
			changeTextLabel.AddToClassList(GetClassNameWithSuffix(changeTextSuffix));

			titleLabel = new Label {
				name = "ProgressBar-Title",
				text = Title
			};
			titleLabel.AddToClassList(GetClassNameWithSuffix(titleSuffix));

			container.Add(titleLabel);

			barContainer.Add(bar);
			barContainer.Add(changeBar);
			container.Add(barContainer);

			valueTextContainer.Add(valueTextLabel);
			valueTextContainer.Add(changeTextLabel);
			container.Add(valueTextContainer);

			this.Add(container);
		}
		
///// Util ///////////////////////////////////////////////////////////////////////////////////////////
		
		/// 
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}
		
///// PUBLIC FUNCTION //////////////////////////////////////////////////////////////////////////////

		public void SetChange(int newChangeValue) {
			ChangeValue = newChangeValue;
			SetChangeBarStyle(ChangeValue > 0);
			UpdateComponent();
		}

		public void SetChangeVisiblility(bool visibility) {
			ShowChange = visibility;
		}

		public void SetTitleVisibility(bool visibility) {
			titleLabel.visible = visibility;
			if ( visibility ) {
				titleLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
			}
			else {
				titleLabel.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
			}
		}
		
		public void SetColors(Color backgroundColor) {
			barContainer.style.backgroundColor = new StyleColor(backgroundColor);
		}
		
		public void UpdateComponent() {
			UpdateValueLabel();
			SetChangeBarStyle(ChangeValue > 0);
			
			// get width for value bar
			
			var currentValue = Value;
			var changeValueInRange = 0;
			
			if ( ShowChange ) {
				changeValueInRange = changeValue;
				if ( ChangeValue < 0 ) {
					changeValueInRange = Mathf.Abs(changeValue);
					currentValue -= changeValueInRange;
				}
			}

			float changeValueWidth = 100 * Mathf.InverseLerp(min, max, changeValueInRange + min);
			float currentValueWidth = 100 * Mathf.InverseLerp(min, max, currentValue);
			
			ChangeProgressBarWidth(currentValueWidth, changeValueWidth);
		}

///// PUBLIC CONSTRUCTORS //////////////////////////////////////////////////////////////////////////

		public new class UxmlFactory : UxmlFactory<ProgressBar, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {

			private UxmlIntAttributeDescription maxAttribute = new UxmlIntAttributeDescription {
				name = "max",
				defaultValue = 100,
			};
			
			private UxmlIntAttributeDescription minAttribute = new UxmlIntAttributeDescription {
				name = "min",
				defaultValue = 0,
			};
			
			private UxmlIntAttributeDescription currentValue = new UxmlIntAttributeDescription {
				name = "Current-Value",
				defaultValue = 100,
			};
			
			private UxmlIntAttributeDescription currentChangeValue = new UxmlIntAttributeDescription {
				name = "Change-Value",
				defaultValue = 10,
			};
			
			private UxmlBoolAttributeDescription showChnageAttribute = new UxmlBoolAttributeDescription {
				name = "Show-Change",
				defaultValue = false
			};
			
			private UxmlBoolAttributeDescription showTitleAttribute = new UxmlBoolAttributeDescription {
				name = "Show-Title",
				defaultValue = false
			};
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);
				
				if ( ve is ProgressBar element ) {
					element.Max = maxAttribute.GetValueFromBag(bag, cc);
					element.Min = minAttribute.GetValueFromBag(bag, cc);
					element.Value = currentValue.GetValueFromBag(bag, cc);
					element.SetChange(currentChangeValue.GetValueFromBag(bag, cc));
					element.SetChangeVisiblility(showChnageAttribute.GetValueFromBag(bag, cc));
					element.SetTitleVisibility(showTitleAttribute.GetValueFromBag(bag, cc));
					
					element.UpdateComponent();
				}
			}
		}

		public ProgressBar() : this("ProgressBar Title", 0, 100, 100, 0){}

		public ProgressBar(string title, int value, int max, int change) : this(title, 0, max, value,
			change) {
			
		}

		//todo refactor: make init methodes for components		
		public ProgressBar(string title, int min, int max, int value, int change) {
			Min = min;
			Max = max;
			Value = value;
			ChangeValue = change;
			Title = title;
			if ( ChangeValue != 0 ) {
				ShowChange = true;
			}
			
			BuildComponents();
			UpdateComponent();
		}
	}
}