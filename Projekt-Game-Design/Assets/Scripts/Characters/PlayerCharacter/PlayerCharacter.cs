using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script attached to each playable character 
// contains relevant data such as stats
//
public class PlayerCharacter : MonoBehaviour
{
    // Base stats
    [SerializeField] public PlayerTypeSO playerType;

    // Stats influenced by status effects 
    //
    [SerializeField] public CharacterStats currentStats;

    // Leveling
    [SerializeField] public int experience;
    [SerializeField] public int level; // TODO: maybe a more complex type later on

    // Current values, dynamic
    //
    [SerializeField] public int lifePoints;
    // [SerializeField] public int movementPoints;
    // [SerializeField] public int manaPoints; // TODO: remove comment
    [SerializeField] public int energy;

    [SerializeField] public Vector2Int position;

    [SerializeField] public ScriptableObject item; // TODO: implement items, 
                                                    // the equipped item offers a list of actions to take

    [SerializeField] public List<ScriptableObject> statusEffects; // TODO: implement status effects
                                                                  // stat changing temporary effects

}
