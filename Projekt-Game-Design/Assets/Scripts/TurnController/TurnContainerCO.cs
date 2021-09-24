using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Input;
using UnityEngine;

public class TurnContainerCO : MonoBehaviour
{
    public InputReader input;
    public GameObject selectedPlayer = null;

    public List<GameObject> PlayersSelected = new List<GameObject>();

    private bool isChoosePhase = true;
    private bool isActionPhase;
    private bool isMoveActionPhase;
    
    [Header("Receiving Events On")]
    [SerializeField] private GameObjEventChannelSO PlayerSelectedEvent;
    [SerializeField] private GameObjEventChannelSO PlayerUnselectedEvent;

    private void Awake()
    {
        PlayerSelectedEvent.OnEventRaised += SelectPlayer;
        PlayerUnselectedEvent.OnEventRaised += UnselectPlayer;
    }

    private void SelectPlayer(GameObject player)
    {
        selectedPlayer = player;
    }

    private void UnselectPlayer(GameObject player)
    {
        if(player.Equals(selectedPlayer))
            selectedPlayer = null;
    }

    private void AddPlayerSelected(GameObject obj)
    {
        if (!PlayersSelected.Contains(obj))
        {
            PlayersSelected.Add(obj);
            Debug.Log("Spieler wurde zur Liste hinzugefügt");
        }
    }

    public bool getIsChoosePhase()
    {
        return isChoosePhase;
    }
}
