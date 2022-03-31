using System.Collections.Generic;
using Characters;
using Characters.Types;
using Events.ScriptableObjects;
using GDP01;
using UnityEngine;

[CreateAssetMenu(fileName = "New_TacticsGameData", menuName = "GameManager/New TacticsGameData")]
public class TacticsGameDataSO : ScriptableObject {
		[Header("Receiving events on: ")]
		[SerializeField] private GamePhaseEventChannelSO gamePhaseAnnouncementEC;

		// turn indicator and order
		public Faction currentPlayer;
    public int currentPlayerIndex;
    public List<Faction> turnOrder;

		/// <summary>
		/// In contrast to currentPlayer, currentPhase corresponds to the actual Game state in the GameStatemachine. 
		/// After ending a turn, currentPlayer is changed before currentPhase is changed. 
		/// </summary>
		public GamePhase currentPhase; 
    
    // turn data
    public int turnNum;

    public void GoToNextTurn() {
        turnNum++;
        //todo use stack for order
        currentPlayerIndex = ((currentPlayerIndex +1 >= turnOrder.Count) ? 0 : currentPlayerIndex +1);
        currentPlayer = turnOrder[currentPlayerIndex];
    }

    public void SetStartingPlayer(Faction faction) {
        currentPlayerIndex = turnOrder.IndexOf(faction);
        currentPlayer = faction;
    }

    public void Reset() {
        turnNum = 0;
        currentPlayerIndex = 0;
        currentPlayer = turnOrder[currentPlayerIndex];
    }

		private void SaveCurrentPhase(GamePhase phase) {
				currentPhase = phase;
		}

		private void OnEnable() {
				gamePhaseAnnouncementEC.OnEventRaised += SaveCurrentPhase;
		}

		private void OnDisable() {
				gamePhaseAnnouncementEC.OnEventRaised -= SaveCurrentPhase;
		}
}


