/*
 * effect with information about effective area and proper targets
 */
namespace Ability {
    [System.Serializable]
    public struct TargetedEffect {
        public Effect effect; // specific effect inflicted on target
        public TargetRelationship targets; // everyone in Area who is a proper target will be affected
        public bool targetIsCenter; // true, if the area has its center on the target. Elsewise, center is self / center is action taker 
        public TargetPattern area; 
    }
}