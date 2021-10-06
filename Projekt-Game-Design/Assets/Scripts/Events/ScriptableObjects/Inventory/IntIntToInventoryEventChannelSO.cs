using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntInt to Inventory Event Channel")]
    public class IntIntToInventoryEventChannelSO : EventChannelBaseSO {
        
        public event Action<int, int> OnEventRaised;

        public void RaiseEvent(int value, int playerid)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value, playerid);
        }
    }
}