using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player type containing constant initial data 
// for individual types of players (e.g. warrior, mage)
//
[CreateAssetMenu(fileName = "New PlayerType", menuName = "Player/PlayerType")]
public class PlayerTypeSO : ScriptableObject
{
    [SerializeField] public ScriptableObject startWeapon; // is not necessarily equipped weapon
    [SerializeField] public CharacterStats stats;
    [SerializeField] public CharacterStats gainPerLevel; // TODO: gain is Linear in this case
    [SerializeField] public ActionSO[] basicActions; // actions at all time available
}
