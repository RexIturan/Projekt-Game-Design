using Grid;
using System.Collections;
using System.Collections.Generic;
using Characters.EnemyCharacter.ScriptableObjects;
using Characters.ScriptableObjects;
using UnityEngine;
using Util;

// Script to attached to each enemy
// 
public class EnemyCharacterSC : MonoBehaviour
{

    [Header("SO Reference")] 
    public GridDataSO globalGridData;
    public EnemyTypeSO enemyType;
    public EnemySpawnDataSO enemySpawnData;
    public EnemyBehaviorSO behavior;

    [Header("max stats")]
    // Stats influenced by temporary effects
    public CharacterStats currentStats;
    
    [Header("stats")]
    // Current values
    public int healthPoints;
    public int energy;
    public Vector3Int gridPosition;
    // TODO: implement status effects
    // stat changing temporary effects
    // public List<ScriptableObject> statusEffects; 

    [Header("Target")]
    public CharacterList characterList;
    public PathNode movementTarget;
    public PlayerCharacterSC target;

    [Header("Combat")] 
    public float attackRange;
    public int attackDamage;
    public List<Vector3Int> tileInRangeOfTarget;

    [Header("Movement")]
    public int movementPointsPerEnergy;
    public List<PathNode> reachableNodes;
    public bool movementDone = true;
    
    [Header("Abilities")]
    public int abilityID;
    
    [Header("Statemachine")]
    public bool isOnTurn; // it's Enemy's turn
    public bool isDone; // this enemy in particular is done
    public bool abilitySelected;
    public bool abilityExecuted;
    public bool noTargetFound;
    public bool rangeChecked;

    public void Start()
    {
        transformToPosition();
        characterList = characterList = GameObject.Find("Characters").GetComponent<CharacterList>();
    }

    // TODO: don't just copy from playerCharacterSC
    //       avoid code that's written twice
    public int GetEnergyUseUpFromMovement()
    {
        return Mathf.CeilToInt((float)movementTarget.gCost / movementPointsPerEnergy);
    }

    // TODO: avoid repetitive code (copied from PlayerCharacter)
    // transforms the gameobject to it's tile position
    public void transformToPosition() {
        var pos = gridPosition + globalGridData.getCellCenter();
        pos *= globalGridData.CellSize;
        pos += globalGridData.OriginPosition;

        gameObject.transform.position = pos;
    }

    public void Refill() {
        // refill energy etc.
        energy = currentStats.maxEnergy;
        isDone = false;
        isOnTurn = false;
    }

    public int GetMaxMoveDistance() {
        return energy * movementPointsPerEnergy;
    }

    public void Initialize() {
        healthPoints = enemyType.stats.maxHealthPoints;
        energy = enemyType.stats.maxEnergy; 
        attackRange = enemySpawnData.range;
        attackDamage = enemySpawnData.attack;
        movementPointsPerEnergy = enemySpawnData.movementpointsPerEnergy;
    }
}
