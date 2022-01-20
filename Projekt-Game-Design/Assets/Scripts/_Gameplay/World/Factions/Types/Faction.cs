using System.Collections.Generic;

namespace Characters.Types {
	public enum Faction {
		None,
		Player,
		Enemy,
		Neutral,
		Friendly
	}

	public static class FactionExtension{
		public static List<Faction> GetFriendly(this Faction faction) {
			var friendly = new List<Faction>();
			
			switch ( faction ) {
				case Faction.Player:
				case Faction.Friendly:
					friendly.Add(Faction.Friendly);
					friendly.Add(Faction.Player);
					break;
				case Faction.Enemy:
					friendly.Add(Faction.Enemy);
					break;
				default:
					break;
			}

			return friendly;
		}
		
		public static List<Faction> GetHostile(this Faction faction) {
			var hostile = new List<Faction>();
			
			switch ( faction ) {
				case Faction.Player:
				case Faction.Friendly:
					hostile.Add(Faction.Enemy);
					break;
				case Faction.Enemy:
					hostile.Add(Faction.Player);
					break;
				default:
					break;
			}
			
			return hostile;
		}
	}
}