using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntIntEquipmentPos to Equipment Event Channel")]
    public class EquipItemEC_SO : EventChannelBaseSO {
        
        public event Action<int, int, EquipmentPosition> OnEventRaised;

        public void RaiseEvent(int itemID, int playerID, EquipmentPosition pos) {
	        OnEventRaised?.Invoke(itemID, playerID, pos);
        }
    }
}