using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to attach to each enemy
// 
public class EnemyCharacter : MonoBehaviour
{
    [SerializeField] public EnemyTypeSO enemyType;

    // Stats influenced by temporary effects
    //
    [SerializeField] public CharacterStats currentStats;

    // Current values
    //
    [SerializeField] public int lifePoints;
    // [SerializeField] public int movementPoints; // TODO: remove comments
    // [SerializeField] public int manaPoints;
    [SerializeField] public int energy;

    [SerializeField] public Vector2Int position;

    [SerializeField] public List<ScriptableObject> statusEffects; // TODO: implement status effects
                                                                  // stat changing temporary effects
}
