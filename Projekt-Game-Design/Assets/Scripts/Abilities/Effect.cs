using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
    // effect with information about damage/heal, attribute boni, 
    // applied status effects and evniromental effects
    //
    [System.Serializable]
    public struct Effect
    {
        public EDamageType type;
        public int baseDamage; // healing if type is HEALING
        public float strengthBonus, dexterityBonus, intelligenceBonus; // Percentage of attributes that is added
        // public List<ScriptableObject> statusEffects; // status effects added to target
        // public ScriptableObject environmentalEffect; // if environment is changed, e.g. tiles types are changed
    }
}