using System;
using UnityEngine;

namespace Characters {
	public class Timer : MonoBehaviour {
		public float timeSinceTransition;

		private void Start() {
			timeSinceTransition = 0;
		}

		private void FixedUpdate() {
			timeSinceTransition += Time.fixedDeltaTime;
		}
	}
}