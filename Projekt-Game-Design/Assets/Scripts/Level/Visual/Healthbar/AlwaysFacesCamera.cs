using UnityEngine;

namespace Visual.Healthbar {
	public class AlwaysFacesCamera : MonoBehaviour {
		private void LateUpdate() {
			if ( Camera.main != null ) transform.rotation = Camera.main.transform.rotation;
		}
	}
}