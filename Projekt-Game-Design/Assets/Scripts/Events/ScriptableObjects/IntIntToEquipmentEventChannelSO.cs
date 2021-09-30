using System;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntInt to Equipment Event Channel")]
    public class IntIntToEquipmentEventChannelSO : EventChannelBaseSO {
        
        public event Action<int, int> OnEventRaised;

        public void RaiseEvent(int value, int playerid)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value, playerid);
        }
    }
}