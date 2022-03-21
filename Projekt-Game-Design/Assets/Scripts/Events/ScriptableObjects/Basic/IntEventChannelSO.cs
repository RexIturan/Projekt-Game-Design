using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Basic/Int Event Channel")]
    public class IntEventChannelSO : EventChannelBaseSO {

				public event Action<int> BeforeEventRaised;
        public event Action<int> OnEventRaised;

        public void RaiseEvent(int value) {
	        BeforeEventRaised?.Invoke(value);
	        OnEventRaised?.Invoke(value);
        }
    }
}