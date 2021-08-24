using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public GameObject selectedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPlayerClicked += setSelectedPlayer;
    }

    private void setSelectedPlayer(GameObject obj)
    {
        selectedPlayer = obj;
    }

}
