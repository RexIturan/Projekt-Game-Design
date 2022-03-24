using System;
using GameManager.Provider;
using GDP01._Gameplay.Provider;
using SceneManagement.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Controller.GameEnd {
	public class GameEndScreen_UIController : MonoBehaviour {

		private const string victoryText = "Victory";
		private const string gameOverText = "Game Over";
		
		
		[Serializable]
		struct GameEndScreenComponentNames {
			public string gameEndLabel;
			public string backToMenuButton;
			public string exitGameButton;
		}

		[SerializeField] private Color GameOverColor = Color.red;
		[SerializeField] private Color VictoryColor = Color.green;
		
		[SerializeField] private GameEndScreenComponentNames componentNames;
		[SerializeField] private VoidEventChannelSO ExitGameEC;
		
		//todo replace with new scene loading
		[SerializeField] private LoadEventChannelSO loadMenuEC;
		[SerializeField] private GameSceneSO[] menuToLoad;
		
		private Label gameEndLabel;
		private Button backToMenuButton;
		private Button exitButton;
		
///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private void SetGameEndLabelText() {
			// if(GameplayProvider.Current.GameSC)
			// get game state (gameOver / victory)
			var gameState = GameStateProvider.Current.GameSC;
			if ( gameState.gameOver ) {
				gameEndLabel.text = gameOverText;
				gameEndLabel.style.color = GameOverColor;
			} else if (gameState.victory) {
				gameEndLabel.text = victoryText;
				gameEndLabel.style.color = VictoryColor;
			}
			else {
				gameEndLabel.text = "Error";
				gameEndLabel.style.color = Color.magenta;
			}
		}

		private void BindElements() {
			var root = GetComponent<UIDocument>().rootVisualElement;
			gameEndLabel = root.Q<Label>(componentNames.gameEndLabel);
			backToMenuButton = root.Q<Button>(componentNames.backToMenuButton);
			exitButton = root.Q<Button>(componentNames.exitGameButton);

			backToMenuButton.clicked += HandleMainMenuButton;
			exitButton.clicked += HandleExitGame;

			SetGameEndLabelText();
		}

		private void UnbindElements() {
			backToMenuButton.clicked -= HandleMainMenuButton;
			exitButton.clicked -= HandleExitGame;
			
			gameEndLabel = null;
			backToMenuButton = null;
			exitButton = null;
		}
		
///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

		private void HandleMainMenuButton() {
			GameStateProvider.Current.GameSC.backToMainMenu = true;
			
			// load Scene
			loadMenuEC.RaiseEvent(menuToLoad, true);
		
			//todo new scene loading
		}

		private void HandleExitGame() {
			ExitGameEC.RaiseEvent();
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void OnEnable() {
			BindElements();
		}

		private void OnDisable() {
			UnbindElements();
		}
	}
}