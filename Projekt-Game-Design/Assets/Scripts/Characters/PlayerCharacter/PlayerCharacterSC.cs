using System;
using System.Collections;
using System.Collections.Generic;
using Characters.PlayerCharacter.ScriptableObjects;
using Events.ScriptableObjects;
using UnityEngine;
using Input;
using UnityEngine.InputSystem;
using Grid;
using Util;


// script attached to each playable character 
// contains relevant data such as stats
//
[System.Serializable]
public class PlayerCharacterSC : MonoBehaviour {
    [Header("Receiving events on")]
    public PathNodeEventChannelSO targetTileEvent;

    [Header("SO Reference")]
    public InputReader input;
    public GridDataSO globalGridData;

    [Header("Basic Stats")]
    // Base stats
    public PlayerTypeSO playerType;
    public PlayerSpawnDataSO playerSpawnData;

    [Header("Current Max Stats")]
    // Stats influenced by status effects
    [SerializeField]
    private CharacterStats currentStats;

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

    public int movementPointsPerEnergy;

    public Vector3Int gridPosition; // within the grid

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
    public ItemSO item;

    [Header("Abilities")] [SerializeField] private AbilitySO[] abilities;
    [SerializeField] private int abilityID;

    public AbilitySO[] Abilities => abilities;

    public int AbilityID {
        get => abilityID;
        set => abilityID = value;
    }

    // Statemachine
    //
    [Header("State Machine")] 
    public bool isSelected = false;
    public bool abilitySelected = false;
    public bool abilityConfirmed = false;
    public bool abilityExecuted;

    [Header("Movement Chache")]
    // cached target (tile position)
    public PathNode movementTarget;
    public List<PathNode> reachableTiles;

    [Header("Combat Chache")]
    // cached target (player or enemy)
    public PlayerCharacterSC playerTarget;
    public EnemyCharacterSC enemyTarget;
    public List<PathNode> tilesInRange;

    [Header("Timer")]
    public float timeSinceTransition = 0;

    private void Awake() {
        input.mouseClicked += ToggleIsSelected;
        targetTileEvent.OnEventRaised += TargetTile;
    }

    private void OnDisable() {
        input.mouseClicked -= ToggleIsSelected;
        targetTileEvent.OnEventRaised -= TargetTile;
    }

    public void Start() {
        // set Position of gameobject    
        TransformToPosition();
    }

    public void FixedUpdate() {
        timeSinceTransition += Time.fixedDeltaTime;
    }

    void ToggleIsSelected() {
        if (!abilitySelected && !abilityConfirmed)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100.0f))
            {
                if (rayHit.collider.gameObject == gameObject)
                {
                    isSelected = !isSelected;
                }
            }
        }
    }

    // transforms the gameobject to it's tile position
    public void TransformToPosition() {
        var pos = gridPosition + globalGridData.getCellCenter();
        pos *= globalGridData.CellSize;
        pos += globalGridData.OriginPosition;

        gameObject.transform.position = pos;
    }

    // target Tile
    public void TargetTile(PathNode target) {
        movementTarget = target;
        abilityConfirmed = true;
    }

    public int GetEnergyUseUpFromMovement() {
        return Mathf.CeilToInt((float) movementTarget.dist / movementPointsPerEnergy);
    }

    public int GetMaxMoveDistance() {
        return energy * movementPointsPerEnergy;
    }
    
    public void Refill() {
        // refill energy etc.
        energy = currentStats.maxEnergy;
    }

    public void RefreshAbilities()
    {
        List<AbilitySO> currentAbilities = new List<AbilitySO>(playerType.basicAbilities);
        if (item is { }) {
            currentAbilities.AddRange(item.abilities);    
        }

        abilities = currentAbilities.ToArray();
    }

    public void Initialize() {
        level = playerSpawnData.level;
        experience = playerSpawnData.experience;
        healthPoints = playerType.stats.maxHealthPoints;
        energy = playerType.stats.maxEnergy;
        movementPointsPerEnergy = playerSpawnData.movementpointsPerEnergy;
        currentStats = playerType.stats;

        RefreshAbilities();
        RefreshEquipment();
    }

    private void RefreshEquipment() {
        Debug.Log("!!!!!!!!!!!!!! Implement ME !!!!!!!!!!!!!!!!!");
    }
}