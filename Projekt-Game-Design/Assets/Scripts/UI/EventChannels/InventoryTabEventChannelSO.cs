using System;
using Events.ScriptableObjects.Core;
using UI.Inventory.Types;
using UnityEngine;

namespace Events.ScriptableObjects {
	[CreateAssetMenu(menuName = "Events/InventoryTab Event Channel")]
	public class InventoryTabEventChannelSO : EventChannelBaseSO {
		public event Action<InventoryTab> OnEventRaised;

		public void RaiseEvent(InventoryTab value) {
			OnEventRaised?.Invoke(value);
		}
	}
}