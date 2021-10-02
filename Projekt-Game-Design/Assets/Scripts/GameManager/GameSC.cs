using System;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using UnityEngine;

namespace GameManager {
    public class GameSC : MonoBehaviour {

        [Header("Recieving Events On")] 
        [SerializeField] private TurnIndicatorEventChannelSO endTurnEC;

        [Header("Sending Events On")] 
        [SerializeField] private BoolEventChannelSO setTurnIndicatorVisibilityEC;

        [Header("SO References")]
        [SerializeField] private TacticsGameDataSO tacticsData;

        private void Awake() {
            endTurnEC.OnEventRaised += HandleEndTurn;
            tacticsData.Reset();
            tacticsData.SetStartingPlayer(EFaction.player);
        }

        private void HandleEndTurn(EFaction faction) {
            // todo needs update if we introduce more factions
            Debug.Log($"{faction} {faction == EFaction.player}");
            setTurnIndicatorVisibilityEC.RaiseEvent(faction == EFaction.player);

            if (tacticsData.currentPlayer == faction) {
                tacticsData.GoToNextTurn();
                Debug.Log($"{tacticsData.currentPlayer}, turn:{tacticsData.turnNum}");    
            }
            else {
                Debug.Log("You can only end the Turn, when its your Turn ");
            }
        }
    }
}
