﻿using System.Collections;
using GDP01.UI.Components;
using Input;
using UI.Components.ActionButton;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Controller {
	public class ActionBarController : MonoBehaviour {
		[SerializeField] private UIDocument uiDocument;
		[SerializeField] private InputReader inputReader;

		[Range(0, 10)]
		[SerializeField] private int numOfActions;

		[SerializeField] private bool preventDeselectOnLeftClick = true;
///// Privat Variables /////////////////////////////////////////////////////////////////////////////
	
		private ActionBar _actionBar;

///// Properties ///////////////////////////////////////////////////////////////////////////////////


///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private IEnumerator FocusButtonWithDelay(float delay) {
			VisualElement lastButton = null;
			
			foreach ( var actionButton in _actionBar.actionButtons ) {
				var mouseEnterEvt = new MouseEnterEvent();
				mouseEnterEvt.target = actionButton.Button;
				_actionBar.SendEvent(mouseEnterEvt);
				
				var mouseOverEvt = new MouseOverEvent();
				mouseOverEvt.target = actionButton.Button;
				_actionBar.SendEvent(mouseOverEvt);
				
				actionButton.Button.Focus();
				lastButton = actionButton.Button;
				yield return new WaitForSeconds(delay);
				
				var mouseExitEvt = new MouseLeaveEvent();
				mouseExitEvt.target = lastButton;
				_actionBar.SendEvent(mouseExitEvt);
			}
		}
		
		private IEnumerator FocusButtonAndClickWithDelay(float delay) {
			foreach ( var actionButton in _actionBar.actionButtons ) {
				actionButton.Button.Focus();
				yield return new WaitForSeconds(delay);
				var navEvent = new NavigationSubmitEvent();
				navEvent.target = actionButton.Button;
				_actionBar.SendEvent(navEvent);
				yield return new WaitForSeconds(delay);
			}
		}  
///// Public Function //////////////////////////////////////////////////////////////////////////////

		public void FocusThroughAllButtons() {
			StartCoroutine(FocusButtonWithDelay(0.5f));
		}
		
		public void PressAllButtons() {
			StartCoroutine(FocusButtonAndClickWithDelay(0.5f));
		}

///// Unity Functions //////////////////////////////////////////////////////////////////////////////		
		
		private void Start() {

			//todo remove for
			inputReader.EnableGameplayInput();
			
			//get action bar
			_actionBar = uiDocument.rootVisualElement.Q<ActionBar>();
			
			_actionBar.Mappings.Clear();
			for ( int i = 0; i < numOfActions; i++ ) {
				var id = i + 1;
				_actionBar.Mappings.Add(id < 10 ? id.ToString() : "0");
			}
			
			_actionBar.UpdateComponent();
			inputReader.SelectAbilityEvent += _actionBar.ClickActionButton;

			// _actionBar.RegisterCallback<PointerDownEvent>(evt => {
			// 	Debug.Log(evt);
			// });

			//todo move to panel override or so
			var panel = uiDocument.rootVisualElement.hierarchy.parent;
			panel.RegisterCallback<PointerDownEvent>(HandlePanelPointerDownEvent);

			// panel.focusController.
			// var p = uiDocument.rootVisualElement.hierarchy.parent as IPanel;
		}

		private void HandlePanelPointerDownEvent(PointerDownEvent evt) {
			if ( preventDeselectOnLeftClick ) {
				Debug.Log(evt.button);
				if ( evt.button == 0 ) {
					//do stuff
					
					
					// evt.PreventFocusChange();
					evt.StopImmediatePropagation();
					evt.StopPropagation();
					evt.PreventDefault();
					// evt.Dispose();
					// evt.PreventDefault();
					// evt.StopPropagation();
				}
			}
		}
		
		private void OnEnable() {
			//todo player selected
			//todo player deselect
			
			// inputReader.SelectAbilityEvent += _actionBar.ClickActionButton;
		}

		private void OnDisable() {
			inputReader.SelectAbilityEvent -= _actionBar.ClickActionButton;
		}
	}
	
	public static class EventBaseExtension{
		public static void PreventFocusChange(this EventBase evt) {
			// if ( evt.target is VisualElement e ) {
			// 	
			// }
			// evt.target = null;
		}
	}

	public class IgnoreFocusEvents : VisualElement {
		public new class UxmlFactory : UxmlFactory<IgnoreFocusEvents, UxmlTraits> { }
		public new class UxmlTraits : VisualElement.UxmlTraits { }

		protected override void ExecuteDefaultAction(EventBase evt) {
			// Debug.Log("?");			
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