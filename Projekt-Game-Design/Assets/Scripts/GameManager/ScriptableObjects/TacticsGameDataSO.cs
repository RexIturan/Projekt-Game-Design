using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_TacticsGameData", menuName = "GameManager/New TacticsGameData")]
public class TacticsGameDataSO : ScriptableObject {
    // turn indicator and order
    public ETurnIndicator currentPlayer;
    public int currentPlayerIndex;
    public List<ETurnIndicator> turnOrder;
    
    // turn data
    public int turnNum = 0;

    public void GoToNextTurn() {
        turnNum++;
        currentPlayerIndex = ((currentPlayerIndex +1 >= turnOrder.Count) ? 0 : currentPlayerIndex +1);
        currentPlayer = turnOrder[currentPlayerIndex];
    }

    public void SetStartingPlayer(ETurnIndicator indicator) {
        currentPlayerIndex = turnOrder.IndexOf(indicator);
        currentPlayer = indicator;
    }

    public void Reset() {
        turnNum = 0;
    }
}

public enum ETurnIndicator {
    player,
    enemy
}
