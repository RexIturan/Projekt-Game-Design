using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAction
{
    // possible targets of a character action
    //
    [System.Serializable, System.Flags]
    public enum EActionTargetFlags
    {
        SELF = 1,
        ALLY = 2,
        ENEMY = 4,
        GROUND = 8,
        WORLD_OBJECT = 16
    }
}