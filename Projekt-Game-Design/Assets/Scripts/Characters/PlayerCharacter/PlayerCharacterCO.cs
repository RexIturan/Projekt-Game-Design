using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using Input;
using UnityEngine.InputSystem;

// script attached to each playable character 
// contains relevant data such as stats
//
public class PlayerCharacterCO : MonoBehaviour
{
    public InputReader input;
    
    // Base stats
    [SerializeField] public PlayerTypeSO playerType;

    // Stats influenced by status effects 
    //
    [SerializeField] public CharacterStats currentStats;

    // Leveling
    [SerializeField] public int experience;
    [SerializeField] public int level; // TODO: maybe a more complex type later on

    // Current values, dynamic
    //
    [SerializeField] public int lifePoints;
    // [SerializeField] public int movementPoints;
    // [SerializeField] public int manaPoints; // TODO: remove comment
    [SerializeField] public int energy;

    [SerializeField] public Vector2Int position;

    [SerializeField] public ScriptableObject item; // TODO: implement items, 
                                                    // the equipped item offers a list of actions to take

    [SerializeField] public List<ScriptableObject> statusEffects; // TODO: implement status effects
                                                                  // stat changing temporary effects
                                                                  
    // Statemachine
    public bool isSelected = false;
    private void Awake()
    {
        input.mouseClicked += toggleIsSelected;
    }
    
    void toggleIsSelected()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 100.0f)){
            if(rayHit.collider.gameObject == gameObject)
            {
                isSelected = !isSelected;
            }
        }
        
    }
}
