using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntEquipmentPosition Unequip Event Channel")]
    public class UnequipItemEC_SO : EventChannelBaseSO {
        
        public event Action<int, EquipmentPosition> OnEventRaised;

        public void RaiseEvent(int playerID, EquipmentPosition pos) {
	        OnEventRaised?.Invoke(playerID, pos);
        }
    }
}