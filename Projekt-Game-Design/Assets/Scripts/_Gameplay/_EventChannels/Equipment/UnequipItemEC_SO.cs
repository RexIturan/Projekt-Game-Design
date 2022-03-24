using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntEquipmentPosition Unequip Event Channel")]
    public class UnequipItemEC_SO : EventChannelBaseSO {
        
        public event Action<int, EquipmentPosition, int> OnEventRaised;

        public void RaiseEvent(int playerID, EquipmentPosition pos, int inventorySlotId) {
	        OnEventRaised?.Invoke(playerID, pos, inventorySlotId);
        }
    }
}