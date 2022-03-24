using System.Collections.Generic;
using Characters;
using Characters.Types;
using UnityEngine;

[CreateAssetMenu(fileName = "New_TacticsGameData", menuName = "GameManager/New TacticsGameData")]
public class TacticsGameDataSO : ScriptableObject {
    // turn indicator and order
    public Faction currentPlayer;
    public int currentPlayerIndex;
    public List<Faction> turnOrder;
    
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
}


