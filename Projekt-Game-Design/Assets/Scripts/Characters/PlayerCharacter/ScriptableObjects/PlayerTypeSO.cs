using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player type containing constant initial data 
// for individual types of players (e.g. warrior, mage)
//
[CreateAssetMenu(fileName = "New PlayerType", menuName = "Character/PlayerType")]
public class PlayerTypeSO : ScriptableObject
{
    public int id;
    public GameObject prefab;
    [SerializeField] public CharacterStats stats;
    [SerializeField] public CharacterStats gainPerLevel; // TODO: gain is Linear in this case
    [SerializeField] public ScriptableObject startWeapon; // is not necessarily equipped weapon
    [SerializeField] public AbilitySO[] basicAbilities; // actions at all time available
}
