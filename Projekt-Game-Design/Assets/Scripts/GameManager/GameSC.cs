using System;
using Events.ScriptableObjects.GameState;
using UnityEngine;

namespace GameManager {
    public class GameSC : MonoBehaviour {

        [Header("Recieving Events On")]
        [SerializeField] private TurnIndicatorEventChannelSO endTurnEC;
        
        [Header("SO References")]
        [SerializeField] private TacticsGameDataSO tacticsData;

        private void Awake() {
            endTurnEC.OnEventRaised += HandleEndTurn;
            tacticsData.Reset();
            tacticsData.SetStartingPlayer(ETurnIndicator.player);
        }

        private void HandleEndTurn(ETurnIndicator indicator) {
            if (tacticsData.currentPlayer == indicator) {
                tacticsData.GoToNextTurn();
                Debug.Log($"{tacticsData.currentPlayer}, turn:{tacticsData.turnNum}");    
            }
            else {
                Debug.Log("You can only end the Turn, when its your Turn ");
            }
        }
    }
}
