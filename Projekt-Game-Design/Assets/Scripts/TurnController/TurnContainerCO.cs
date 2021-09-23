using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Input;
using UnityEngine;

public class TurnContainerCO : MonoBehaviour
{
    public InputReader input;

    public List<GameObject> PlayersSelected = new List<GameObject>();

    private bool isChoosePhase = true;
    private bool isActionPhase;
    private bool isMoveActionPhase;
    
    [Header("Receiving Events On")]
    [SerializeField] private GameObjEventChannelSO AddPlayerSelectedEventChannel;

    private void Awake()
    {
        AddPlayerSelectedEventChannel.OnEventRaised += AddPlayerSelected;
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
