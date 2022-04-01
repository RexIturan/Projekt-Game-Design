using UnityEngine;

/// <summary>
/// Effects an ability has. Are evalutated on ability execution. 
/// </summary>
namespace Ability {
    [System.Serializable]
    public struct TargetedEffect {
        public Effect effect; // specific effect inflicted on target
        public TargetRelationship targets; // everyone in Area who is a proper target will be affected
        public bool targetIsCenter; // true, if the area has its center on the target. Elsewise, center is self / center is action taker 
        public TargetPattern area;
				/// <summary>
				/// If set, tile effects are spawned in every tile within the area. 
				/// </summary>
				public GameObject tileEffect;
    }
}