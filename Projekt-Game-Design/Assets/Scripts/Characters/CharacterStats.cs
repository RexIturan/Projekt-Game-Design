using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// data container for stats for both players and enemy
[System.Serializable]
public struct CharacterStats
{
    [SerializeField] public int maxLifePoints;
    // [SerializeField] public int maxMovementPoints;
    // [SerializeField] public int maxManaPoints;
    [SerializeField] public int maxEnergy; // resource for taking an action 

    [SerializeField] public int strength;
    [SerializeField] public int dexterity;
    [SerializeField] public int intelligence;

    [SerializeField] public int viewDistance;
    
}
