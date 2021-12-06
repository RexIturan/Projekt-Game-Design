using UnityEngine;

namespace Player {
	public class Selectable : MonoBehaviour {
		public bool isSelected;

		public void Select() {
			isSelected = true;
		}

		public void Deselect() {
			isSelected = false;
		}
	}
}