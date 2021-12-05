using Characters;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using SaveSystem;
using SaveSystem.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;

namespace GameManager {
    public class GameSC : MonoBehaviour {
        [Header("Receiving Events On")] [SerializeField]
        private EFactionEventChannelSO endTurnEC;

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
        
        [Header("StateMachine")] 
        public bool evaluated;
        // structure
        public bool shouldExit;
        public bool exited;
        public bool initializedGame;
        public bool initializedTactics;
        // todo refactor to enum
        public bool isInTacticsMode;
        // public bool isInMacroMode;
        public bool gameOver;
        
        private void Awake() {
            endTurnEC.OnEventRaised += HandleEndTurn;
            onSceneReady.OnEventRaised += LoadGameFromPath;
            levelLoaded.OnEventRaised += HandlelocationLoaded;
            
            saveManagerData.Reset();
            tacticsData.Reset();
            tacticsData.SetStartingPlayer(Faction.Player);
        }

        private void OnDisable() {
            endTurnEC.OnEventRaised -= HandleEndTurn;
            onSceneReady.OnEventRaised -= LoadGameFromPath;
            levelLoaded.OnEventRaised -= HandlelocationLoaded;
            saveManagerData.Reset();
        }

        private void HandleEndTurn(Faction faction) {
            // todo needs update if we introduce more factions
            setTurnIndicatorVisibilityEC.RaiseEvent(faction == Faction.Player);

            Debug.Log($"End Turn.\n Turn: {tacticsData.turnNum} Current Faction: {tacticsData.currentPlayer}");
            
            if (tacticsData.currentPlayer == faction) {
                tacticsData.GoToNextTurn();
                // Debug.Log($"{tacticsData.currentPlayer}, turn:{tacticsData.turnNum}");    
            }
            else {
                Debug.Log($"You can only end the Turn, when its your Turn.\nTurn: {tacticsData.turnNum}");
            }
        }
        
        public void LoadLocationLevel() {
	        
            // _hasSaveData = saveSystem.LoadSaveDataFromDisk(saveSystem.saveManagerData.path);

            if (_hasSaveData) {
                Debug.Log("GameSC: files read");

                Debug.Log("GameSC: load scene");
                // loadLocationEC.RaiseEvent(locationsToLoad, showLoadScreen);
            }
        }

        public void LoadGameFromPath() {
            Debug.Log("GameSC > LoadGameFromPath:\n scene loaded");
            if (_hasSaveData) {
                Debug.Log("GameSC: load save data");
                // saveSystem.InitializeLevel();
                initializedGame = true;
                isInTacticsMode = true;
                initializedTactics = true;
            }
        }

        void HandlelocationLoaded() {
            initializedGame = true;
            isInTacticsMode = true;
            initializedTactics = true;
        }
        
        public void TriggerRedraw() {
            // todo could trigger event, but doesnt currently
        }
    }
}
