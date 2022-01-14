using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace UI.Gameplay {

	public enum GameplayScreenStates {
		None,
		Inventory,
		PauseMenu,
	}
	
	public enum GameplayScreen {
		Gameplay,
		Inventory,
	}
	
	public enum MenuScreen {
		None,
		PauseMenu
	} 
	
	public class Gameplay_UIController : MonoBehaviour {
		
		[SerializeField] private BoolEventChannelSO SetMenuVisibilityEC;
		[SerializeField] private BoolEventChannelSO setGameOverlayVisibilityEC;
		[SerializeField] private BoolEventChannelSO setInventoryVisibilityEC;
		
		[Header("Receiving Events On")]
		[SerializeField] private VoidEventChannelSO uiToggleMenuEC;
		[SerializeField] private ScreenEventChannelSO uiToggleScreenEC;
		
///// Private Variables ////////////////////////////////////////////////////////////////////////////

		private MenuScreen menuScreen;
		private GameplayScreen currentScreen;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void ToggleGameplayScreen(GameplayScreen screen, bool visible) {
			switch ( screen ) {
				case GameplayScreen.Gameplay:
					setGameOverlayVisibilityEC.RaiseEvent(visible);
					break;
				
				case GameplayScreen.Inventory:
					setInventoryVisibilityEC.RaiseEvent(visible);
					break;
				default:
					break;
			}
		}

		private void EnableGampleyScreen() {
			ToggleGameplayScreen(currentScreen, true);
		}

		private void DisableGampleyScreen() {
			ToggleGameplayScreen(currentScreen, false);
		}

		private void SwitchGameplayScreen(GameplayScreen screen) {
			DisableGampleyScreen();
			currentScreen = screen;
			EnableGampleyScreen();
		}

		private void ToggleMenu(bool visible) {
			SetMenuVisibilityEC.RaiseEvent(visible);
		}

		private void TryToggleMenu() {
			switch ( menuScreen ) {
				case MenuScreen.None:
					//Activete pause Menu
					ToggleMenu(true);
					DisableGampleyScreen();
					menuScreen = MenuScreen.PauseMenu;
					break;
				
				case MenuScreen.PauseMenu:
					//disable pause Menu
					ToggleMenu(false);
					EnableGampleyScreen();
					menuScreen = MenuScreen.None;
					break;
			}
		}

		private void TryToggleScreen(GameplayScreen screen) {
			
			// do nothing if the menu is open
			if(menuScreen == MenuScreen.PauseMenu)
				return;

			switch ( currentScreen, screen ) {
				case ( GameplayScreen.Inventory, GameplayScreen.Inventory ):
				case ( GameplayScreen.Inventory, GameplayScreen.Gameplay ):
					//close inventory
					SwitchGameplayScreen(GameplayScreen.Gameplay);
					break;
				
				case ( GameplayScreen.Gameplay, GameplayScreen.Inventory ):
					//open inventory
					SwitchGameplayScreen(GameplayScreen.Inventory);
					break;
				
				default:
					//do nothing
					break;
			}
		}
		
///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

		private void Start() {
			currentScreen = GameplayScreen.Gameplay;
			menuScreen = MenuScreen.None;
		}

		private void Awake() {
			uiToggleMenuEC.OnEventRaised += TryToggleMenu;
			uiToggleScreenEC.OnEventRaised += TryToggleScreen;
		}

		private void OnDisable() {
			uiToggleMenuEC.OnEventRaised -= TryToggleMenu;
			uiToggleScreenEC.OnEventRaised -= TryToggleScreen;
		}
	}
}