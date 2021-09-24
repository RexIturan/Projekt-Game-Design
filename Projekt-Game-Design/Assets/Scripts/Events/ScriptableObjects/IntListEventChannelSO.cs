using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntList Event Channel")]
    public class IntListEventChannelSO : EventChannelBaseSO {
        
        public UnityAction<List<int>> OnEventRaised;

        public void RaiseEvent(List<int> value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }
    }
}