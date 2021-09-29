using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using Input;
using UnityEngine.InputSystem;
using Grid;
using Util;


// script attached to each playable character 
// contains relevant data such as stats
//
public class PlayerCharacterSC : MonoBehaviour
{
    [Header("SO Reference")]
    public InputReader input;
    public GridDataSO globalGridData;
    
    [Header("Basic Stats")]
    // Base stats
    public PlayerTypeSO playerType;
    
    
    [Header("Current Max Stats")]
    // Stats influenced by status effects
    [SerializeField] private CharacterStats currentStats;
    public CharacterStats CurrentStats => currentStats;
    // TODO: implement status effects
    // stat changing temporary effects
    [SerializeField] private List<ScriptableObject> statusEffects;
    
    [Header("Current Stats")]
    // Leveling
    // TODO: maybe a more complex type later on
    public int level;
    public int experience;
    
    
    // Current values, dynamic
    public int healthPoints;
    public int energy;
    // TODO could be just energy???
    public int movementPointsPerEnergy;

    
    
    

    public Vector3Int position; // within the grid

    public int HealthPoints {
        get => healthPoints;
        set => healthPoints = value;
    }
    
    public int EnergyPoints {
        get => energy;
        set => energy = value;
    }
    
    [Header("Equipment")]
    // the equipped item offers a list of actions to take
    public ScriptableObject item;

    
    [Header("Abilities")] 
    [SerializeField] private AbilitySO[] abilitys;
    [SerializeField] private int abilityID;
    
    public AbilitySO[] Abilitys => abilitys;
    public int AbilityID => abilityID;
    
    // cached target (tile position)
    public Vector3Int movementTarget; 
    
    // Statemachine
    public List<PathNode> reachableTiles;

    
    [Header("State Machine")]
    public bool isSelected = false;
    public bool abilitySelected;
    public bool abilityConfirmed;
    public bool abilityExecuted;
    
    // public bool IsSelected {
    //     get => isSelected;
    //     set => isSelected = value;
    // }
    
    public int GetMaxMoveDistance() {
        return energy * movementPointsPerEnergy;
    }
    
    public void Start()
    {
        // set Position of gameobject    
        transformToPosition();
    }

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
    
    // transforms the gameobject to it's tile position
    public void transformToPosition()
    {
        gameObject.transform.position = new Vector3(position.x * globalGridData.cellSize, 
                                                         position.y * globalGridData.cellSize, 
                                                         position.z * globalGridData.cellSize)
                                             + globalGridData.OriginPosition;
    }
}
