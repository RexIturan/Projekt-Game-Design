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

		public void RefillEnergy() {
			// refill energy etc.
			stats.GetValue(StatusType.Energy).Fill();
		}
	}
}