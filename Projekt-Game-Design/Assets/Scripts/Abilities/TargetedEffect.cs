using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
    // effect with information about effective area and proper targets
    //
    [System.Serializable]
    public struct TargetedEffect
    {
        public Effect effect; // specific effect inflicted on target
        public EAbilityTargetFlags targets; // everyone in Area who is a proper target will be affected
        public bool targetIsCenter; // true, if the area has its center on the target. Elsewise, center is self / center is action taker 
        public int area; // 1 for single target, higher values to target every proper target in radius 
                         // TODO: replace range with a proper pattern that can effect more complex areas, 
                         //       e.g. three fields in a row that are targeted by a swung axe 
    }
}