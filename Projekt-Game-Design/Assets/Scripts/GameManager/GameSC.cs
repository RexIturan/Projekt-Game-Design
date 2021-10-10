using System;
using Characters.ScriptableObjects;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using SaveLoad;
using SaveLoad.ScriptableObjects;
using SceneManagement.ScriptableObjects;
using UnityEngine;

namespace GameManager {
    public class GameSC : MonoBehaviour {
        [Header("Recieving Events On")] [SerializeField]
        private EFactionEventChannelSO endTurnEC;

        [Header("Sending Events On")] [SerializeField]
        private BoolEventChannelSO setTurnIndicatorVisibilityEC;

        [Header("SO References")] [SerializeField]
        private TacticsGameDataSO tacticsData;

        [Header("StateMachine")] public bool evaluated;

        // structure
        public bool shouldExit;
        public bool exited;
        public bool initializedGame;

        public bool initializedTactics;

        // todo refactor to enum
        public bool isInTacticsMode;

        // public bool isInMacroMode;
        public bool gameOver;

        [Header("SaveManagerData")] public SaveManagerDataSO saveManagerData;
        [SerializeField] private StringEventChannelSO loadGameFromPath;
        [SerializeField] private VoidEventChannelSO levelLoaded;

        [Header("sceneloading stuff")] [SerializeField]
        private VoidEventChannelSO onSceneReady;

        [SerializeField] private LoadEventChannelSO loadLocationEC;
        public GameSceneSO[] locationsToLoad;
        public bool showLoadScreen;

        private void Awake() {
            endTurnEC.OnEventRaised += HandleEndTurn;
            onSceneReady.OnEventRaised += LoadGameFromPath;
            levelLoaded.OnEventRaised += TriggerRedraw;
            tacticsData.Reset();
            tacticsData.SetStartingPlayer(EFaction.player);
        }

        private void OnDisable() {
            endTurnEC.OnEventRaised -= HandleEndTurn;
            onSceneReady.OnEventRaised -= LoadGameFromPath;
            levelLoaded.OnEventRaised -= TriggerRedraw;
        }

        private void HandleEndTurn(EFaction faction) {
            // todo needs update if we introduce more factions
            setTurnIndicatorVisibilityEC.RaiseEvent(faction == EFaction.player);

            if (tacticsData.currentPlayer == faction) {
                tacticsData.GoToNextTurn();
                // Debug.Log($"{tacticsData.currentPlayer}, turn:{tacticsData.turnNum}");    
            }
            else {
                Debug.Log("You can only end the Turn, when its your Turn");
            }
        }


        public SaveManager saveSystem;
        private bool _hasSaveData;

        public void LoadLocationLevel() {
            _hasSaveData = saveSystem.LoadSaveDataFromDisk(saveSystem.saveManagerData.path);

            if (_hasSaveData) {
                Debug.Log("GameSC: files read");

                Debug.Log("GameSC: load scene");
                loadLocationEC.RaiseEvent(locationsToLoad, showLoadScreen);
            }
        }

        public void LoadGameFromPath() {
            Debug.Log("GameSC: scene loaded");
            if (_hasSaveData) {
                Debug.Log("GameSC: load save data");
                saveSystem.LoadLevel();
                initializedGame = true;
                isInTacticsMode = true;
                initializedTactics = true;
            }
        }

        public void TriggerRedraw() {
            // todo could trigger event, but doesnt currently
        }
    }
}
