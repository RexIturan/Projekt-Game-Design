using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
	[CreateAssetMenu(menuName = "Events/Basic/Vector3Int Event Channel")]
	public class Vector3IntEventChannelSO : EventChannelBaseSO {
		public event Action<Vector3Int> OnEventRaised;

		public void RaiseEvent(Vector3Int value) {
			OnEventRaised?.Invoke(value);
		}
	}
}