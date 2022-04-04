using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
	[CreateAssetMenu(menuName = "Events/Basic/Vector3 Event Channel")]
	public class Vector3EventChannelSO : EventChannelBaseSO {
		public event Action<Vector3> OnEventRaised;

		public void RaiseEvent(Vector3 value) {
			OnEventRaised?.Invoke(value);
		}
	}
}