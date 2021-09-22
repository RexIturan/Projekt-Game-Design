    using UnityEngine;
    using UnityEngine.Events;

    namespace Events.ScriptableObjects {
        [CreateAssetMenu(menuName = "Events/InventoryTab Event Channel")]
        public class InventoryTabEventChannelSO : EventChannelBaseSO {
        
            public UnityAction<InventoryUIController.inventoryTab> OnEventRaised;

            public void RaiseEvent(InventoryUIController.inventoryTab value)
            {
                if (OnEventRaised != null)
                    OnEventRaised.Invoke(value);
            }
        }
    }