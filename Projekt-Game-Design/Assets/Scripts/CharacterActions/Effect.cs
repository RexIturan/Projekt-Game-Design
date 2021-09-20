using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// effect with information about damage/heal, attribute boni, 
// applied status effects and evniromental effects
//
[System.Serializable]
public struct Effect
{
    [SerializeField] public int baseDamage; // negative if it's healing
    [SerializeField] public float strengthBonus, dexterityBonus, intelligenceBonus; // Percentage of attributes that is added, negative if healing
    [SerializeField] public List<ScriptableObject> statusEffects; // status effects added to target
    [SerializeField] public ScriptableObject environmentalEffect; // if environment is changed, e.g. tiles types are changed
}
