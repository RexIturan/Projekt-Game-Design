using Characters.Types;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using GDP01;
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
		[SerializeField] private VoidEventChannelSO GameEndScreenEC;
		[SerializeField] private VoidEventChannelSO QuestScreenEC;
		[SerializeField] private GamePhaseEventChannelSO startTurnEC;
		
		[Header("Screen Handeling")] 
		[SerializeField] private ScreenManager screenManager;
		[SerializeField] private ScreenController overlayScreen;
		[SerializeField] private ScreenController inventoryScreen;
		[SerializeField] private ScreenController pauseScreen;
		[SerializeField] private ScreenController questScreen;
		[SerializeField] private ScreenController gameEndScreen;
		[SerializeField] private ScreenController enemyTurnIndicatorScreen;
		
///// Private Variables ////////////////////////////////////////////////////////////////////////////

		private MenuScreen menuScreen;
		private GameplayScreen currentScreen;
		private bool showQuestScreen = true;
		private bool showGameEndScreen = false;
		private bool showEnemyTurnIndicatorScreen = false;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void SetGameplayScreenVisibility(bool value) {
			//old
			// setGameOverlayVisibilityEC.RaiseEvent(value);

			//new
			screenManager.SetScreenVisibility(overlayScreen, value);

			if ( showQuestScreen ) {
				screenManager.SetScreenVisibility(questScreen, true);
			}

			if ( showGameEndScreen ) {
				screenManager.SetScreenVisibility(gameEndScreen, value);
			}
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

		private void SetEnemyTurnIndicatorScreenVisibility(bool value) {
			//new
			screenManager.SetScreenVisibility(enemyTurnIndicatorScreen, value);
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
			Debug.Log("Trying to toggle screen. ");
			
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
		
		//todo(vincent) should work i would like something more general 
		private void HandleQuestScreenToggle() {
			// showQuestScreen = !showQuestScreen;
			
			if(menuScreen == MenuScreen.PauseMenu)
				return;
			
			if(currentScreen == GameplayScreen.Gameplay)
				screenManager.SetScreenVisibility(questScreen, showQuestScreen);
		}

		private void HandleGameEndScreenToggle() {
			showGameEndScreen = !showGameEndScreen;
			
			if(menuScreen == MenuScreen.PauseMenu)
				TryToggleMenu();
			
			SwitchGameplayScreen(GameplayScreen.Gameplay);
		}

		private void HandleStartTurn(GamePhase phase) {
			switch ( phase ) {
				case GamePhase.ENEMY_TURN:
					//enable Enemyturn indicator
					showEnemyTurnIndicatorScreen = true;
					SetEnemyTurnIndicatorScreenVisibility(showEnemyTurnIndicatorScreen);
					break;
				
				case GamePhase.PLAYER_TURN:
				default:
					//disable Enemyturn indicator
					showEnemyTurnIndicatorScreen = false;
					SetEnemyTurnIndicatorScreenVisibility(showEnemyTurnIndicatorScreen);
					break;
			}
		}
		
///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

		private void Start() {
			currentScreen = GameplayScreen.Gameplay;
			menuScreen = MenuScreen.None;
		}

		private void OnEnable() {
			uiToggleMenuEC.OnEventRaised += TryToggleMenu;
			uiToggleScreenEC.OnEventRaised += TryToggleScreen;
			GameEndScreenEC.OnEventRaised += HandleGameEndScreenToggle;
			QuestScreenEC.OnEventRaised += HandleQuestScreenToggle;
			startTurnEC.OnEventRaised += HandleStartTurn;

			HandleStartTurn(GamePhase.PLAYER_TURN);
		}

		private void OnDisable() {
			uiToggleMenuEC.OnEventRaised -= TryToggleMenu;
			uiToggleScreenEC.OnEventRaised -= TryToggleScreen;
			GameEndScreenEC.OnEventRaised -= HandleGameEndScreenToggle;
			QuestScreenEC.OnEventRaised -= HandleQuestScreenToggle;
			startTurnEC.OnEventRaised += HandleStartTurn;
		}
	}
}