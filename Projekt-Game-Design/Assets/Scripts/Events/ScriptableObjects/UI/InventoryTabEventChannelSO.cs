    using System;
    using Events.ScriptableObjects.Core;
    using UnityEngine;

    namespace Events.ScriptableObjects {
        [CreateAssetMenu(menuName = "Events/InventoryTab Event Channel")]
        public class InventoryTabEventChannelSO : EventChannelBaseSO {
        
            public event Action<InventoryUIController.InventoryTab> OnEventRaised;

            public void RaiseEvent(InventoryUIController.InventoryTab value) {
	            OnEventRaised?.Invoke(value);
            }
        }
    }