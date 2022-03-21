using SaveSystem.V2.Data;
using UnityEngine;

namespace SaveSystem.V2.TestComponents {
	public class Gold : MonoBehaviour, ISaveState<Gold> {

		[SerializeField] private int Amount = 0;

		public Gold Save() {
			return this;
		}

		public void Load(Gold gold) {
			// ?? 
		}
	}
}