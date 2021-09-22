using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enemy type containing constant data for individual types of enemies
// such as stats and drops
//
[CreateAssetMenu(fileName = "New EnemyType", menuName = "Enemy/EnemyType")]
public class EnemyTypeSO : ScriptableObject
{
    [SerializeField] public ScriptableObject item; // standard equipped Item 
    [SerializeField] public CharacterStats stats;
    [SerializeField] public LootTable drops;
    [SerializeField] public ActionSO[] basicActions; // actions at all time available
}
