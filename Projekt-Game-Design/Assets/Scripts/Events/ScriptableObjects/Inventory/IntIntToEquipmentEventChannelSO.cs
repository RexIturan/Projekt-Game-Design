using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntInt to Equipment Event Channel")]
    public class IntIntToEquipmentEventChannelSO : EventChannelBaseSO {
        
        public event Action<int, int> OnEventRaised;

        public void RaiseEvent(int value, int playerID) {
	        OnEventRaised?.Invoke(value, playerID);
        }
    }
}