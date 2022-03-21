using GDP01.Input.Input.Types;
using GDP01.UI.Components;
using Input;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI {
	public class LevelEditor_ActionBar_UIController : MonoBehaviour {
		[SerializeField] private UIDocument uiDocument;
		[SerializeField] private InputReader inputReader;

		[Range(0, 5)]
		[SerializeField] private int numOfActionsLeft;
		
		[Range(0, 5)]
		[SerializeField] private int numOfActionsRight;
		
///// Privat Variables /////////////////////////////////////////////////////////////////////////////
	
		private ActionBar _actionBarLeft;
		private ActionBar _actionBarRight;

///// Properties ///////////////////////////////////////////////////////////////////////////////////


///// Private Functions ////////////////////////////////////////////////////////////////////////////

///// Public Function //////////////////////////////////////////////////////////////////////////////

///// Unity Functions //////////////////////////////////////////////////////////////////////////////		
		
		private void Start() {

			//todo remove for
			// inputReader.EnableGameplayInput();
			
			//get action bar
			_actionBarLeft = uiDocument.rootVisualElement.Q<ActionBar>("ActionBar-Left");
			_actionBarRight = uiDocument.rootVisualElement.Q<ActionBar>("ActionBar-Right");
			
			_actionBarLeft.actionCount = numOfActionsLeft;
			_actionBarRight.actionCount = numOfActionsRight;
			_actionBarLeft.Mappings.Clear();
			_actionBarRight.Mappings.Clear();
			_actionBarLeft.KeyMappings.Clear();
			_actionBarRight.KeyMappings.Clear();
			
			for ( int i = 0; i < numOfActionsLeft + numOfActionsRight; i++ ) {
				var id = i + 1;
				var mappingStr = id < 10 ? id.ToString() : "0";
				var mapping = ( ActionButtonInputId ) i;
				if ( i < numOfActionsLeft ) {
					_actionBarLeft.Mappings.Add(mappingStr);
					_actionBarLeft.KeyMappings[mapping] = i;
				}
				else {
					_actionBarRight.Mappings.Add(mappingStr);
					_actionBarRight.KeyMappings[mapping] = i - numOfActionsLeft;
				}
			}
			
			_actionBarLeft.UpdateComponent();
			_actionBarRight.UpdateComponent();

			inputReader.SelectAbilityEvent += _actionBarLeft.ClickActionButton;
			inputReader.SelectAbilityEvent += _actionBarRight.ClickActionButton;
		}

		private void OnDisable() {
			inputReader.SelectAbilityEvent -= _actionBarLeft.ClickActionButton;
			inputReader.SelectAbilityEvent -= _actionBarRight.ClickActionButton;
		}
	}
}