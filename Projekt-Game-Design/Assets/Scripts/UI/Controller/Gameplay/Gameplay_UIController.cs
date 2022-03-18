using Events.ScriptableObjects;
using GDP01.UI;
using UI.Gameplay.Types;
using UnityEngine;

namespace UI.Gameplay {
	public class Gameplay_UIController : MonoBehaviour {
		
		[SerializeField] private BoolEventChannelSO SetMenuVisibilityEC;
		[SerializeField] private BoolEventChannelSO setGameOverlayVisibilityEC;
		[SerializeField] private BoolEventChannelSO setInventoryVisibilityEC;
		
		[Header("Receiving Events On")]
		[SerializeField] private VoidEventChannelSO uiToggleMenuEC;
		[SerializeField] private ScreenEventChannelSO uiToggleScreenEC;
		
		[Header("Screen Handeling")] 
		[SerializeField] private ScreenManager screenManager;
		[SerializeField] private ScreenController overlayScreen;
		[SerializeField] private ScreenController inventoryScreen;
		[SerializeField] private ScreenController pauseScreen;
		[SerializeField] private ScreenController questScreen;
		
///// Private Variables ////////////////////////////////////////////////////////////////////////////

		private MenuScreen menuScreen;
		private GameplayScreen currentScreen;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void SetGameplayScreenVisibility(bool value) {
			//old
			// setGameOverlayVisibilityEC.RaiseEvent(value);
			
			//new
			screenManager.SetScreenVisibility(overlayScreen, value);
		}

		private void SetInventoryScreenVisibility(bool value) {
			//old
			// setInventoryVisibilityEC.RaiseEvent(value);
			
			//new
			screenManager.SetScreenVisibility(inventoryScreen, value);
		}

		private void SetMenuScreenVisibility(bool value) {
			//old
			// SetMenuVisibilityEC.RaiseEvent(value);
			
			//new
			screenManager.SetScreenVisibility(pauseScreen, value);
		}

		private void ToggleGameplayScreen(GameplayScreen screen, bool visible) {
			switch ( screen ) {
				case GameplayScreen.Gameplay:
					SetGameplayScreenVisibility(visible);
					break;
				
				case GameplayScreen.Inventory:
					SetInventoryScreenVisibility(visible);
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

		private void ShowMenu(bool visible) {
			SetMenuScreenVisibility(visible);
		}

		private void TryToggleMenu() {
			switch ( menuScreen ) {
				case MenuScreen.None:
					//Activete pause Menu
					ShowMenu(true);
					DisableGampleyScreen();
					menuScreen = MenuScreen.PauseMenu;
					break;
				
				case MenuScreen.PauseMenu:
					//disable pause Menu
					ShowMenu(false);
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