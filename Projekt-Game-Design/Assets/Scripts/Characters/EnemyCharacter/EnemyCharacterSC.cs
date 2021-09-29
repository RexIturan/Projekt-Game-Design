using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to attach to each enemy
// 
public class EnemyCharacterSC : MonoBehaviour
{
    [SerializeField] public EnemyTypeSO enemyType;

    // Stats influenced by temporary effects
    //
    [SerializeField] public CharacterStats currentStats;

    // Current values
    //
    [SerializeField] public int healthPoints;
    // [SerializeField] public int movementPoints; // TODO: remove comments
    // [SerializeField] public int manaPoints;
    [SerializeField] public int energy;
    

    [SerializeField] public Vector3Int position;

    [SerializeField] public List<ScriptableObject> statusEffects; // TODO: implement status effects
                                                                  // stat changing temporary effects
    public int attackRange;
    public int preferredEngagementDistance;
    public bool noActionPossible = false;
    public bool moveFinished = false;
    public bool attackFinished = false;
    public bool targetInRange = false;


}
