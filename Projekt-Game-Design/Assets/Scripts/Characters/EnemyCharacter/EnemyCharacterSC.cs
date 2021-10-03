using Grid;
using System.Collections;
using System.Collections.Generic;
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
    public EnemyBehaviorSO behavior;

    // Stats influenced by temporary effects
    //
    public CharacterStats currentStats;

    // Current values
    //
    public int healthPoints;
    // public int movementPoints; // TODO: remove comments
    // public int manaPoints;
    public int energy;
    public int movementPointsPerEnergy;

    public Vector3Int gridPosition;

    public List<ScriptableObject> statusEffects; // TODO: implement status effects
                                                 // stat changing temporary effects

    public CharacterContainerSO characterContainer; // reference to CharacterList

    // Statemachine
    //
    public bool isOnTurn; // it's Enemy's turn
    public bool isDone; // this enemy in particular is done
    public bool abilitySelected;
    public bool abilityExecuted;

    public int abilityID;

    public PathNode movementTarget;

    public void Start()
    {
        transformToPosition();
    }

    // TODO: don't just copy from playerCharacterSC
    //       avoid code that's written twice
    public int GetEnergyUseUpFromMovement()
    {
        return Mathf.CeilToInt((float)movementTarget.gCost / movementPointsPerEnergy);
    }

    // TODO: avoid repetitive code (copied from PlayerCharacter)
    // transforms the gameobject to it's tile position
    public void transformToPosition()
    {
        gameObject.transform.position = new Vector3((0.5f + gridPosition.x) * globalGridData.cellSize,
                                                    gridPosition.y * globalGridData.cellSize,
                                                    (0.5f + gridPosition.z) * globalGridData.cellSize)
                                        + globalGridData.OriginPosition;
    }

    public void Refill() {
        // refill energy etc.
        energy = currentStats.maxEnergy;
        isDone = false;
        isOnTurn = false;
    }
}
