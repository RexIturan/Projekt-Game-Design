using System;
using UnityEngine;

namespace Events.ScriptableObjects {
    [CreateAssetMenu(menuName = "Events/Basic/String Event Channel")]
    public class StringEventChannelSO : ScriptableObject {
        public event Action<string> OnEventRaised;

        public void RaiseEvent(string value) {
	        OnEventRaised?.Invoke(value);
        }
    }
}