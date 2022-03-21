using System.Collections;
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

			//todo move to panel override or so
			var panel = uiDocument.rootVisualElement.hierarchy.parent;
			panel.RegisterCallback<PointerDownEvent>(HandlePanelPointerDownEvent);
		}

		private void HandlePanelPointerDownEvent(PointerDownEvent evt) {
			if ( preventDeselectOnLeftClick ) {
				if ( evt.button == 0 ) {
					//do stuff
					// evt.StopImmediatePropagation();
					// evt.StopPropagation();
					// evt.PreventDefault();
				}
			}
		}
		
		private void OnEnable() {
			//todo player selected
			//todo player deselect
		}

		private void OnDisable() {
			inputReader.SelectAbilityEvent -= _actionBar.ClickActionButton;
		}
	}
}