using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAction
{
    // effect with information about damage/heal, attribute boni, 
    // applied status effects and evniromental effects
    //
    [System.Serializable]
    public struct Effect
    {
        [SerializeField] public EDamageType type;
        [SerializeField] public int baseDamage; // healing if type is HEALING
        [SerializeField] public float strengthBonus, dexterityBonus, intelligenceBonus; // Percentage of attributes that is added
        [SerializeField] public List<ScriptableObject> statusEffects; // status effects added to target
        [SerializeField] public ScriptableObject environmentalEffect; // if environment is changed, e.g. tiles types are changed
    }
}