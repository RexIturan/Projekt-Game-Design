using System;
using UnityEngine;

namespace Events.ScriptableObjects.GameState {
    [CreateAssetMenu(menuName = "Events/TurnIndicator Event Channel")]
    public class TurnIndicatorEventChannelSO : EventChannelBaseSO {
        
        public event Action<EFaction> OnEventRaised;
        
        public void RaiseEvent(EFaction value) {
            OnEventRaised?.Invoke(value);
        }
    }
}