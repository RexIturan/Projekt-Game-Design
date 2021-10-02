using System;
using UnityEngine;

namespace Events.ScriptableObjects.GameState {
    [CreateAssetMenu(menuName = "Events/TurnIndicator Event Channel")]
    public class TurnIndicatorEventChannelSO : EventChannelBaseSO {
        
        public event Action<ETurnIndicator> OnEventRaised;
        
        public void RaiseEvent(ETurnIndicator value) {
            OnEventRaised?.Invoke(value);
        }
    }
}