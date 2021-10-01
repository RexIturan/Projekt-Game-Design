using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

// Script to attached to each enemy
// 
public class EnemyCharacterSC : MonoBehaviour
{
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
    

    public Vector3Int position;

    public List<ScriptableObject> statusEffects; // TODO: implement status effects
                                                 // stat changing temporary effects



    public bool isOnTurn; // it's Enemy's turn
    public bool isDone; // this enemy in particular is done
    public bool abilitySelected;
    public bool abilityExecuted;

    public PathNode movementTarget; 
}
