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
public class PlayerCharacterSC : MonoBehaviour
{
    [Header("SO Reference")]
    [SerializeField] private InputReader input;
    
    
    [Header("Basic Stats")]
    // Base stats
    [SerializeField] private PlayerTypeSO playerType;
    
    
    [Header("Current Stats")]
    // Stats influenced by status effects
    [SerializeField] private CharacterStats currentStats;
    // TODO: implement status effects
    // stat changing temporary effects
    [SerializeField] private List<ScriptableObject> statusEffects;
    
    // Leveling
    [SerializeField] private int experience;
    // TODO: maybe a more complex type later on
    [SerializeField] private int level; 
    // Current values, dynamic
    [SerializeField] private int hitPoints;
    [SerializeField] private int energy;

    [SerializeField] private Vector2Int position;
    
    
    [Header("Equipment")]
    // TODO: implement items,
    // the equipped item offers a list of actions to take
    [SerializeField] private ScriptableObject item;

    
    [Header("Abilities")] 
    [SerializeField] private int abilityID;
    [SerializeField] private AbilitySO[] abilitys; 
    
    
    [Header("State Machine")]
    [SerializeField] private bool isSelected = false;
    
    
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
