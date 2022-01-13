using System.Collections.Generic;
using Characters;

namespace Ability
{
    // possible targets of a character action
    //
    [System.Serializable, System.Flags]
    public enum AbilityTarget
    {
        Self = 1,
        Ally = 2,
        Enemy = 4,
        Ground = 8,
        Neutral = 16
    }

    public static class AbilityTargetExtension {
	    public static List<Faction> GetTargetedFactions(this AbilityTarget abilityTarget, Faction faction) {
		    var targetedFactions = new List<Faction>();

		    if(abilityTarget.HasFlag(AbilityTarget.Ally)) 
			    targetedFactions.AddRange(faction.GetFriendly());
		    if(abilityTarget.HasFlag(AbilityTarget.Enemy)) 
			    targetedFactions.AddRange(faction.GetHostile());
		    if ( abilityTarget.HasFlag(AbilityTarget.Neutral) )
			    targetedFactions.Add(Faction.Neutral);
		    
		    return targetedFactions;
	    }
    }
}