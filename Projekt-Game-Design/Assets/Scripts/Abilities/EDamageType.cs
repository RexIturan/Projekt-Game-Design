using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
    // types for damage/healing
    //
    [System.Serializable]
    public enum EDamageType
    {
        HEALING, // inverts damage to healing
        NORMAL,
        PIERCING,
        SIEGE,
        MAGIC,
        DIVINE
    }
}