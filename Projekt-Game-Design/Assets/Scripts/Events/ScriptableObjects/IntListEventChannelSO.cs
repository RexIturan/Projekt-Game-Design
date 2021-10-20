using System;
using System.Collections.Generic;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/IntList Event Channel")]
    public class IntListEventChannelSO : EventChannelBaseSO {
        
        public event Action<List<int>> OnEventRaised;

        public void RaiseEvent(List<int> value) {
	        OnEventRaised?.Invoke(value);
        }
    }
}