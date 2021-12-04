using UnityEngine;

namespace Characters {
	//todo move to Character SC ??
	public class Statistics : MonoBehaviour {
		[SerializeField] private string DisplayName;
		[SerializeField] private StatusValues stats;
		//todo move somewhere else??
		[SerializeField] private Faction faction;	


		public void RefillEnergy() {
			// refill energy etc.
			stats.GetValue(StatusType.Energy).Fill();
		}
	}
}