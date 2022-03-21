using System.Collections.Generic;
using Characters.Types;

// possible targets of a character action
[System.Serializable, System.Flags]
public enum TargetRelationship {
	Self = 1,
	Ally = 2,
	Enemy = 4,
	Ground = 8,
	Neutral = 16
}

public static class AbilityTargetExtension {
	public static List<Faction> GetTargetedFactions(this TargetRelationship abilityTarget, Faction faction) {
		var targetedFactions = new List<Faction>();

		if(abilityTarget.HasFlag(TargetRelationship.Ally)) 
			targetedFactions.AddRange(faction.GetFriendly());
		if(abilityTarget.HasFlag(TargetRelationship.Enemy)) 
			targetedFactions.AddRange(faction.GetHostile());
		if ( abilityTarget.HasFlag(TargetRelationship.Neutral) )
			targetedFactions.Add(Faction.Neutral);
		    
		return targetedFactions;
	}
}