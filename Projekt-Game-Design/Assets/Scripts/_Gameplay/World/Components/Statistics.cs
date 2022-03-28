using System;
using Ability;
using Characters.Types;
using UnityEngine;
using Visual.Healthbar;

namespace Characters {
	//todo move to Character SC ??
	public class Statistics : MonoBehaviour {
		public string DisplayName;
		public Sprite DisplayImage;
		[SerializeField] private StatusValues stats;
		[SerializeField] private ArmorType armorType;
		//todo move somewhere else??
		[SerializeField] private Faction faction;

		//property -> getter
		public StatusValues StatusValues => stats;

		public ArmorType ArmorType => armorType;

		public Faction Faction => faction;
		// getter und setter faction
		public Faction GetFaction() { return faction; }
		public void SetFaction(Faction faction) { this.faction = faction; }

		public void RefillEnergy() {
			// refill energy etc.
			stats.Energy.Fill();
		}

    public void RefillAll() {
			foreach(StatusValue stat in stats.GetStatusValues()) {
				stat.Fill();
			}
		}
    
///// Unity Function ///////////////////////////////////////////////////////////////////////////////

		//todo is used when status values are changed in the editor, refactor with is dirty flag or so
		private void OnValidate() {
			//update healthbar
			if ( StatusValues.GetStatusValues() is { Length: > 0 } ) {
				var hitPoints = StatusValues?.HitPoints;
				if ( hitPoints != null ) {
					GetComponentInChildren<HealthbarController>().UpdateVisuals(hitPoints);	
				}	
			}
		}
	}
}