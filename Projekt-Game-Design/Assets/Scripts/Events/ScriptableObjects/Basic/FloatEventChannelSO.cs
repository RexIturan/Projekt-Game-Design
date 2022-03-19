using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Basic/Float Event Channel")]
    public class FloatEventChannelSO : EventChannelBaseSO {

				public event Action<float> BeforeEventRaised;
        public event Action<float> OnEventRaised;

        public void RaiseEvent(float value) {
	        BeforeEventRaised?.Invoke(value);
	        OnEventRaised?.Invoke(value);
        }
    }
}