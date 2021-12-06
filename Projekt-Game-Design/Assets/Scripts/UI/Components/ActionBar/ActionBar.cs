using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;
using Object = System.Object;

namespace UI.Components {
	public class ActionBar : VisualElement {
		public enum Orientation {
			Top,
			Bottom,
			Left,
			Right
		}

		public enum Layout {
			Horizontal,
			Vertical
		}

		public int actionCount { get; set; }
		public Layout actionLayout { get; set; }
		public Orientation orientation { get; set; }
		public bool showNames { get; set; }
		public List<ActionButton.ActionButton> actionButtons;

		// layoutobject??
		// background
		// classes
		// num of actions
		// where??
		// action button

		private static readonly string actionBarClassName = "action-bar";
		private static readonly string containerUssClassName = "action-bar-container";
		private static readonly string buttonContainerUssClassName = "action-bar-button-container";
		private static readonly string horizontalClassName = "horizontal";
		private static readonly string verticalClassName = "vertical";
		private static readonly string topClassName = "top";
		private static readonly string bottomClassName = "bottom";
		private static readonly string leftClassName = "left";
		private static readonly string rightClassName = "right";

		public new class UxmlFactory : UxmlFactory<ActionBar, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {
			private UxmlIntAttributeDescription actionNum =
				new UxmlIntAttributeDescription {
					name = "number-of-actions",
					defaultValue = 5
				};

			private UxmlEnumAttributeDescription<Layout> layout = new UxmlEnumAttributeDescription<Layout> {
				name = "Action-Bar-Layout",
				defaultValue = Layout.Horizontal
			};

			private UxmlEnumAttributeDescription<Orientation> orientation =
				new UxmlEnumAttributeDescription<Orientation> {
					name = "Orientation",
					defaultValue = Orientation.Bottom,
				};

			private UxmlBoolAttributeDescription showNames = new UxmlBoolAttributeDescription() {
				name = "Show-Names",
				defaultValue = true
			};
			
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is ActionBar element ) {
					element.Clear();

					element.name = "ActionBar";
					
					element.actionCount = actionNum.GetValueFromBag(bag, cc);
					element.actionLayout = layout.GetValueFromBag(bag, cc);
					element.orientation = orientation.GetValueFromBag(bag, cc);
					element.showNames = showNames.GetValueFromBag(bag, cc);
					
					element.AddToClassList(actionBarClassName);
					element.RemoveFromClassList(leftClassName);
					element.RemoveFromClassList(rightClassName);
					element.RemoveFromClassList(topClassName);
					element.RemoveFromClassList(bottomClassName);

					if ( element.actionLayout == Layout.Vertical ) {
						if ( element.orientation == Orientation.Left ) {
							element.AddToClassList(leftClassName);
						}
						else if ( element.orientation == Orientation.Right ) {
							element.AddToClassList(rightClassName);
						}
						else {
							Debug.LogWarning(
								$"ActionBar Orientation has to be " +
								$"{Orientation.Left} Or {Orientation.Right} " +
								$"when layout is: {Layout.Vertical}");
						}
					}
					else {
						if ( element.orientation == Orientation.Top ) {
							element.AddToClassList(topClassName);
						}
						else if ( element.orientation == Orientation.Bottom ) {
							element.AddToClassList(bottomClassName);
						}
						else {
							Debug.LogWarning(
								$"ActionBar Orientation has to be " +
								$"{Orientation.Top} Or {Orientation.Bottom} " +
								$"when layout is: {Layout.Horizontal}");
						}
					}

					var container = new VisualElement {
						name = "ActionBar-Container",
					};
					container.AddToClassList(containerUssClassName);

					var buttonContainer = new VisualElement {
						name = "ActionBar-Button-Container",
					};
					buttonContainer.AddToClassList(buttonContainerUssClassName);
					buttonContainer.AddToClassList(
						element.actionLayout == Layout.Horizontal ? horizontalClassName : verticalClassName);
					element.Add(buttonContainer);

					element.actionButtons = new List<ActionButton.ActionButton>();
					for ( int i = 0; i < element.actionCount; i++ ) {
						var actionButton = new ActionButton.ActionButton(
							i.ToString(), i, i.ToString(), "Action Name", element.showNames) {
							mapping = ( i + 1 ).ToString(),
							actionText = "Basic Attack" 
						};
						actionButton.UpdataValues();
						actionButton.BindAction((object[] args) => {
							var str = "";
							foreach ( var obj in args ) {
								str += obj.ToString() + " ";
							}
							Debug.Log(str);
						}, new Object[] { "Wohoo", 2, 5, 4 }, null, "callback");
						element.actionButtons.Add(actionButton);
						buttonContainer.Add(actionButton);
					}
				}
			}
		}

		public void SetVisibility(bool visible) {
			if ( visible ) {
				this.style.display = DisplayStyle.Flex;	
			}
			else {
				this.style.display = DisplayStyle.None;	
			}
		}
	}
}