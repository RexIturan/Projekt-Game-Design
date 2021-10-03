    using System;
    using UnityEngine;

    namespace Events.ScriptableObjects {
        [CreateAssetMenu(menuName = "Events/InventoryTab Event Channel")]
        public class InventoryTabEventChannelSO : EventChannelBaseSO {
        
            public event Action<InventoryUIController.inventoryTab> OnEventRaised;

            public void RaiseEvent(InventoryUIController.inventoryTab value)
            {
                if (OnEventRaised != null)
                    OnEventRaised.Invoke(value);
            }
        }
    }