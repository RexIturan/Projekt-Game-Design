using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntEquipmentPosition to Inventory Event Channel")]
    public class IntEquipmentPositionToInventoryEventChannelSO : EventChannelBaseSO {
        
        public event Action<int, EquipmentPosition> OnEventRaised;

        public void RaiseEvent(int playerID, EquipmentPosition pos) {
	        OnEventRaised?.Invoke(playerID, pos);
        }
    }
}