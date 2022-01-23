using System;
using System.Collections.Generic;
using UI.Components.ActionButton;
using UnityEngine;
using UnityEngine.UIElements;
using Util.Extensions;

namespace GDP01.UI.Components {
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

///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string actionBarClassName = "action-bar";
		private static readonly string containerUssClassName = "action-bar-container";
		private static readonly string buttonContainerUssClassName = "action-bar-button-container";
		private static readonly string horizontalClassName = "horizontal";
		private static readonly string verticalClassName = "vertical";
		private static readonly string topClassName = "top";
		private static readonly string bottomClassName = "bottom";
		private static readonly string leftClassName = "left";
		private static readonly string rightClassName = "right";

		private static readonly string defaultStyleSheet = "UI/actionBar";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////
		// layoutobject??
		// background
		// classes
		// num of actions
		// where??
		// action button
		private Layout actionLayout;
		private Orientation orientation;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		// logic settings
		public int actionCount { get; set; }
		public List<string> Mappings { get; set; }
		public List<Action<Action>> ButtonCLickActions { get; set; }

		
		// visual settings
		public bool showNames { get; set; }

///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		public readonly List<ActionButton> actionButtons;
		private VisualElement container;
		private VisualElement buttonContainer;

///// Private Functions ////////////////////////////////////////////////////////////////////////////		

		private void BuildComponent() {
			this.name = "ActionBar";

			StyleSheet style = Resources.Load<StyleSheet>(defaultStyleSheet);
			this.styleSheets.Add(style);

			this.AddToClassList(actionBarClassName);
			this.RemoveFromClassList(leftClassName);
			this.RemoveFromClassList(rightClassName);
			this.RemoveFromClassList(topClassName);
			this.RemoveFromClassList(bottomClassName);

			container = new VisualElement {
				name = "ActionBar-Container",
			};
			container.AddToClassList(containerUssClassName);

			buttonContainer = new VisualElement {
				name = "ActionBar-Button-Container",
			};
			buttonContainer.AddToClassList(buttonContainerUssClassName);
			
			container.Add(buttonContainer);
			
			this.Add(container);
		}
		
		private void HandleActionSelected(int selection, ActionButton actionButton) {
			if ( selection == actionButton.Id ) {
				// Debug.Log(index);
				

				var controller = actionButton.Button.focusController; 
				if ( controller is {} && controller.focusedElement == actionButton.Button) {
					// actionButton.Button.Blur();
				}
				else {
					actionButton.Button.Focus();
				}
			}
		}

		private void HandleActionClicked(int selection, ActionButton actionButton) {
			if ( selection == actionButton.Id ) {
				// var navEvent = new NavigationSubmitEvent();
				// navEvent.target = actionButton.Button;
				// this.SendEvent(navEvent);

				//todo maybe change the behaviour so the ability would be repeatable?
				HandleActionSelected(selection, actionButton);
			}
		}

///// PUBLIC FUNCTIONS  ////////////////////////////////////////////////////////////////////////////

		public void SelectActionButton(int index) {
			if ( actionButtons.IsValidIndex(index) ) {
				HandleActionSelected(index, actionButtons[index]);
			}
		}

		public void ClickActionButton(int index) {
			if ( actionButtons.IsValidIndex(index) ) {
				HandleActionClicked(index, actionButtons[index]);
			}
		}

		//todo refactor
		public void UpdateComponent() {

			if ( Mappings.Count < actionCount ) {
				var missingMappings = actionCount - Mappings.Count;
				for ( int i = 0; i < missingMappings; i++ ) {
					//todo better placeholder
					Mappings.Add("-");
				}
			}
			
			//todo dont do it like this, check if change is needed
			buttonContainer.Clear();
			actionButtons.Clear();
			
			for ( int i = 0; i < this.actionCount; i++ ) {
				
				var actionButton = new ActionButton(
					i.ToString(), i, Mappings[i], "Action Name", this.showNames) {
					Mapping = Mappings[i],
					ActionText = "Basic Attack"
				};
				actionButton.UpdateComponent();
				actionButton.SetupActionButton(null, "callback");
				actionButton.BindOnClickedAction(
					(args) => {
					
						var str = "";
						foreach ( var obj in args ) {
							str += obj + " ";
						}

						Debug.Log(str);
					}, 
					Debug.Log,
				new System.Object[] { "Button Pressed", actionButton.Mapping });
			
				this.actionButtons.Add(actionButton);
				buttonContainer.Add(actionButton);
				
				// if ( ButtonCLickActions.IsValidIndex(i) ) {
				// 	var navEvent = new NavigationSubmitEvent();
				// 	navEvent.target = actionButton;
				//
				// 	ButtonCLickActions[i].Invoke((() => this.SendEvent(navEvent)));
				// 	
				// 	//test
				// 	this.SendEvent(navEvent);
				// 	
				// }
			}
		
			//todo private methode v
			buttonContainer.AddToClassList(
				this.actionLayout == Layout.Horizontal ? horizontalClassName : verticalClassName);
			
			if ( actionLayout == Layout.Vertical ) {
				if ( orientation == Orientation.Left ) {
					AddToClassList(leftClassName);
				}
				else if ( orientation == Orientation.Right ) {
					AddToClassList(rightClassName);
				}
				else {
					Debug.LogWarning(
						$"ActionBar Orientation has to be " +
						$"{Orientation.Left} Or {Orientation.Right} " +
						$"when layout is: {Layout.Vertical}");
				}
			}
			else {
				if ( orientation == Orientation.Top ) {
					AddToClassList(topClassName);
				}
				else if ( orientation == Orientation.Bottom ) {
					AddToClassList(bottomClassName);
				}
				else {
					Debug.LogWarning(
						$"ActionBar Orientation has to be " +
						$"{Orientation.Top} Or {Orientation.Bottom} " +
						$"when layout is: {Layout.Horizontal}");
				}
			}
		}

		public void ResetActionButtons() {
			foreach ( var button in actionButtons ) {
				button.ResetActionButton();
			}	
		}
		
		public void SetVisibility(bool visibility) {
			this.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
		}
		
		public void SetVisibility(VisualElement element, bool visibility) {
			element.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
		}

///// PUBLIC CONSTRUCTORS //////////////////////////////////////////////////////////////////////////		

		public new class UxmlFactory : UxmlFactory<GDP01.UI.Components.ActionBar, UxmlTraits> { }

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

					element.actionCount = actionNum.GetValueFromBag(bag, cc);
					element.actionLayout = layout.GetValueFromBag(bag, cc);
					element.orientation = orientation.GetValueFromBag(bag, cc);
					element.showNames = showNames.GetValueFromBag(bag, cc);

					
					
					element.BuildComponent();
					element.UpdateComponent();
				}
			}
		}

		//todo init through code
		public ActionBar() {
			this.actionButtons = new List<ActionButton>();
			this.ButtonCLickActions = new List<Action<Action>>();
			Mappings = new List<string>();
			
			BuildComponent();
			UpdateComponent();
			
			
		}

		protected override void ExecuteDefaultAction(EventBase evt) {
			if ( evt is PointerDownEvent pEvt ) {
				if ( pEvt.button == 0 && focusController?.focusedElement is GroupedButton ) {
					Debug.Log("action bar, is this the right position?");					
				}
				else {
					base.ExecuteDefaultAction(evt);	
				}
			}
			else {
				base.ExecuteDefaultAction(evt);	
			}
		}
	}
}