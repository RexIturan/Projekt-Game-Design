using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Basic/Bool Event Channel")]
    public class BoolEventChannelSO : EventChannelBaseSO {
        
        public event Action<bool> OnEventRaised;
        
        public void RaiseEvent(bool value) {
	        OnEventRaised?.Invoke(value);
        }
    }
}