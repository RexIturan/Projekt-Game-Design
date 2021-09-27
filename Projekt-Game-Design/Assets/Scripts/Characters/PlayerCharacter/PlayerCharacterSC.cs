using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using Input;
using UnityEngine.InputSystem;
using Grid;

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
    
    
    [Header("Current Stats")]
    // Stats influenced by status effects
    public CharacterStats currentStats;
    // TODO: implement status effects
    // stat changing temporary effects
    public List<ScriptableObject> statusEffects;

    // Leveling
    public int experience;
    // TODO: maybe a more complex type later on
    public int level;
    // Current values, dynamic
    public int hitPoints;
    public int energy;

    public Vector3Int position; // within the grid
    
    
    [Header("Equipment")]
    // the equipped item offers a list of actions to take
    public ScriptableObject item;

    
    [Header("Abilities")] 
    [SerializeField] private int abilityID;
    public int AbilityID => abilityID;
    [SerializeField] private AbilitySO[] abilitys;
    // cached target (tile position)
    public Vector3Int movementTarget; 
    
    
    [Header("State Machine")]
    [SerializeField] private bool isSelected = false;

    public bool IsSelected {
        get => isSelected;
        set => isSelected = value;
    }

    // set Position of gameobject
    public void Start()
    {
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
        this.gameObject.transform.position = new Vector3(position.x * globalGridData.cellSize, 
                                                         position.y * globalGridData.cellSize, 
                                                         position.z * globalGridData.cellSize)
                                             + globalGridData.OriginPosition;
    }
}
