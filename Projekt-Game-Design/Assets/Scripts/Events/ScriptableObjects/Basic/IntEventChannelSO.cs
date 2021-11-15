using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Basic/Int Event Channel")]
    public class IntEventChannelSO : EventChannelBaseSO {
        
        public event Action<int> OnEventRaised;

        public void RaiseEvent(int value) {
	        OnEventRaised?.Invoke(value);
        }
    }
}