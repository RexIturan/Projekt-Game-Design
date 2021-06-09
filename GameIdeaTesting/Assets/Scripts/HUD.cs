using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUD : MonoBehaviour {
    public InputReader inputReader;

    public void OnEnable() {
        inputReader.endTurnEvent += onEndTurn;
    }

    public void onEndTurn() {
        Debug.Log("end turn");
    }
}
