using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestStatemachine : MonoBehaviour
{

    public InputReader input;

    public bool stateSwitch = true;

    private void Start()
    {
        input.mouseClicked += handleSwitchState;
    }

    void handleSwitchState()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100.0f)){
            if(rayHit.collider.gameObject == gameObject)
            {
                stateSwitch = !stateSwitch;
            }
        }
        
    }


}
