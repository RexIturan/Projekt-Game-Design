using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using Input;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
    public CharacterStats CurrentStats => currentStats;
    // TODO: implement status effects
    // stat changing temporary effects
    [SerializeField] private List<ScriptableObject> statusEffects;
    
    // Leveling
    [SerializeField] private int experience;
    // TODO: maybe a more complex type later on
    [SerializeField] private int level; 
    // Current values, dynamic
    [SerializeField] private int lifePoints;
    [SerializeField] private int energy;

    [SerializeField] private Vector2Int position;

    public int LifePoints {
        get => lifePoints;
        set => lifePoints = value;
    }
    
    public int EnergyPoints {
        get => energy;
        set => energy = value;
    }
    
    [Header("Equipment")]
    // TODO: implement items,
    // the equipped item offers a list of actions to take
    [SerializeField] private ScriptableObject item;

    
    [Header("Abilities")] 
    [SerializeField] private AbilitySO[] abilitys;
    [SerializeField] private int abilityID;
    public int AbilityID => abilityID;
    public AbilitySO[] Abilitys => abilitys;
    
    [Header("State Machine")]
    public bool isSelected = false;
    public bool abilitySelected;
    public bool abilityConfirmed;
    public bool abilityExecuted;
    
    // public bool IsSelected {
    //     get => isSelected;
    //     set => isSelected = value;
    // }

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
