using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterAction
{
    // restrictions that have to be met for setting a target for an action
    //
    [System.Serializable, System.Flags]
    public enum EActionReachableFlags
    {
        VISIBLE = 1, // target has to be in line of sight
        REACHABLE = 2, // action taker can move to target
        PROJECTILE_REACHABLE = 4, // a projectile has to be able to reach target
    }
}