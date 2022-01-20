using Characters.Types;
using UnityEngine;

namespace Characters {
	//todo move to Character SC ??
	public class Statistics : MonoBehaviour {
		public string DisplayName;
		public Sprite DisplayImage;
		[SerializeField] private StatusValues stats;
		//todo move somewhere else??
		[SerializeField] private Faction faction;

		//property -> getter
		public StatusValues StatusValues => stats;

		public Faction Faction => faction;
		// getter und setter faction
		public Faction GetFaction() { return faction; }
		public void SetFaction(Faction faction) { this.faction = faction; }

		public void RefillEnergy() {
			// refill energy etc.
			stats.GetValue(StatusType.Energy).Fill();
		}

    public void RefillAll()
		{
			foreach(StatusValue val in stats.GetStatusValues())
			{
				val.Fill();
			}
		}
	}
}