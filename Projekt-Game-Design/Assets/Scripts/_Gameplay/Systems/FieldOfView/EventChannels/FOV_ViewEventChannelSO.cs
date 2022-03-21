using System;
using Events.ScriptableObjects.Core;
using UnityEngine;


namespace Events.ScriptableObjects.FieldOfView {
	[CreateAssetMenu(fileName = "FOV_ViewEventChannelSO", menuName = "Events/FOV/FOV_ViewEventChannelSO", order = 1)]
	public class FOV_ViewEventChannelSO : EventChannelBaseSO{
		// view
		public event Action<bool[,]> OnEventRaised;

		public void RaiseEvent(bool[,] view) {
			OnEventRaised?.Invoke(view);
		}
	}
}
