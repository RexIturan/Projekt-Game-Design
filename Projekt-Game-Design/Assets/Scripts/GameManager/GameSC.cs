using System.Collections.Generic;
using Characters.Types;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using GameManager.ScriptableObjects;
using SaveSystem;
using SaveSystem.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;

namespace GameManager {
	public class GameSC : MonoBehaviour {
		[Header("Receiving Events On")] [SerializeField]
		private VoidEventChannelSO startGameEC;

		[SerializeField] private EFactionEventChannelSO endTurnEC;
		[SerializeField] private EFactionEventChannelSO newTurnEC;
		[SerializeField] private VoidEventChannelSO triggerDefeatEC;
		[SerializeField] private VoidEventChannelSO triggerVictoryEC;

		[Header("Sending Events On")] [SerializeField]
		private BoolEventChannelSO setTurnIndicatorVisibilityEC;

		[Header("SO References")] [SerializeField]
		private TacticsGameDataSO tacticsData;

		[Header("SaveManagerData")] public SaveManagerDataSO saveManagerData;

		// [SerializeField] private StringEventChannelSO loadGameFromPath;
		[SerializeField] private VoidEventChannelSO levelLoaded;

		[Header("sceneloading Event Channel")] [SerializeField]
		private VoidEventChannelSO onSceneReady;

		// [SerializeField] private LoadEventChannelSO loadLocationEC;
		public GameSceneSO[] locationsToLoad;
		public bool showLoadScreen;

		//todo isnt used, remove or use
		public SaveManager saveSystem;
		private bool _hasSaveData = false;

		[Header("StateMachine")] public bool start;
		public bool backToMainMenu;

		public bool evaluated;

		// structure
		public bool initializedGame;

		// public bool isInMacroMode;
		public bool gameOver;
		public bool victory;

		[Header("Victory Conditions"), SerializeField] private List<GameEndConditionSO> victoryConditions;
		[Header("GameOver Conditions"), SerializeField] private List<GameEndConditionSO> gameOverConditions;

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		public Faction CurrentPlayer => tacticsData.currentPlayer;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		

/////	Callbacks

		private void HandleStartGame() {
			start = true;
			gameOver = false;
			victory = false;
			// evaluated = false;
			backToMainMenu = false;

			initializedGame = false;
		}

		private void HandleDefeat() {
			gameOver = true;
		}

		private void HandleVictory() {
			victory = true;
		}

		void HandlelocationLoaded() {
			initializedGame = true;
			// isInTacticsMode = true;
		}

		private void HandleEndTurn(Faction faction) {
			// todo needs update if we introduce more factions
			setTurnIndicatorVisibilityEC.RaiseEvent(faction == Faction.Player);

			Debug.Log(
				$"End Turn.\n Turn: {tacticsData.turnNum} Current Faction: {tacticsData.currentPlayer}");

			if ( tacticsData.currentPlayer == faction ) {
				tacticsData.GoToNextTurn();
				// Debug.Log($"{tacticsData.currentPlayer}, turn:{tacticsData.turnNum}");    
			}
			else {
				Debug.Log($"You can only end the Turn, when its your Turn.\nTurn: {tacticsData.turnNum}");
			}

			// newTurnEC.RaiseEvent(faction);
		}

///// Public Functions /////////////////////////////////////////////////////////////////////////////		
		
		public void UpdateOverlay(Faction faction) {
			newTurnEC.RaiseEvent(faction);
		}
		//     
		// public void LoadLocationLevel() {
		//      
		// 	// _hasSaveData = saveSystem.LoadSaveDataFromDisk(saveSystem.saveManagerData.path);
		//
		// 	if (_hasSaveData) {
		// 		Debug.Log("GameSC: files read");
		//
		// 		Debug.Log("GameSC: load scene");
		// 		// loadLocationEC.RaiseEvent(locationsToLoad, showLoadScreen);
		// 	}
		// }

		// public void LoadGameFromPath() {
		// 	// Debug.Log("GameSC > LoadGameFromPath:\n Scene Loaded");
		// 	if (_hasSaveData) {
		// 		Debug.Log("GameSC: load save data");
		// 		// saveSystem.InitializeLevel();
		// 		initializedGame = true;
		// 		isInTacticsMode = true;
		// 	}
		// }

		public void ResetGameState() {
			gameOver = false;
			victory = false;

			start = false;
			backToMainMenu = false;
			initializedGame = false;

			//todo set when evaluating ???
			evaluated = true;
			
			tacticsData.Reset();
		}
		
		public void CheckGameEndingConditions() {
			//for each gameover condition -> check ->
			// -> set gameOver = true
			foreach ( var condition in gameOverConditions ) {
				if ( condition.CheckCondition() ) {
					gameOver = true;
				}
			}
			
			// for each victory condition -> check
			// -> set victory = true
			foreach ( var condition in victoryConditions ) {
				if ( condition.CheckCondition() ) {
					victory = true;
				}
			}
		}
		
/////	Unity Functions
		private void Awake() {
			startGameEC.OnEventRaised += HandleStartGame;
			endTurnEC.OnEventRaised += HandleEndTurn;
			// onSceneReady.OnEventRaised += LoadGameFromPath;
			levelLoaded.OnEventRaised += HandlelocationLoaded;
			triggerDefeatEC.OnEventRaised += HandleDefeat;
			triggerVictoryEC.OnEventRaised += HandleVictory;

			saveManagerData.Reset();
			tacticsData.Reset();
			tacticsData.SetStartingPlayer(Faction.Player);
		}

		private void OnDisable() {
			startGameEC.OnEventRaised -= HandleStartGame;
			endTurnEC.OnEventRaised -= HandleEndTurn;
			// onSceneReady.OnEventRaised -= LoadGameFromPath;
			levelLoaded.OnEventRaised -= HandlelocationLoaded;
			triggerDefeatEC.OnEventRaised -= HandleDefeat;
			triggerVictoryEC.OnEventRaised -= HandleVictory;
			saveManagerData.Reset();
		}
	}
}