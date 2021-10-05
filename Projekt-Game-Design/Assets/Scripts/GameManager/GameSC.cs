using System;
using Characters.ScriptableObjects;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using SaveLoad.ScriptableObjects;
using UnityEngine;

namespace GameManager {
    public class GameSC : MonoBehaviour {

        [Header("Recieving Events On")] 
        [SerializeField] private EFactionEventChannelSO endTurnEC;

        [Header("Sending Events On")] 
        [SerializeField] private BoolEventChannelSO setTurnIndicatorVisibilityEC;

        [Header("SO References")]
        [SerializeField] private TacticsGameDataSO tacticsData;
        [SerializeField] private CharacterContainerSO characterContainer;
        
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
        
        [Header("SaveManagerData")] 
        public SaveManagerDataSO saveManagerData;
        
        private void Awake() {
            endTurnEC.OnEventRaised += HandleEndTurn;
            tacticsData.Reset();
            tacticsData.SetStartingPlayer(EFaction.player);
        }

        private void Start() {
            characterContainer.FillContainer();
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
    }
}
