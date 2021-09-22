using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class TestStatemachine : MonoBehaviour
{

    public InputReader input;

    public bool stateSwitch = true;

    private void Start()
    {
        input.endTurnEvent += handleSwitchState;
    }

    void handleSwitchState()
    {
        stateSwitch = !stateSwitch;
    }
}
