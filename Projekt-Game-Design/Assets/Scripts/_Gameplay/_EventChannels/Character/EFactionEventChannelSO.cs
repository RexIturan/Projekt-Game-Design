using System;
using Characters.Types;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects.GameState {
    [CreateAssetMenu(menuName = "Events/TurnIndicator Event Channel")]
    public class EFactionEventChannelSO : EventChannelBaseSO {
        
        public event Action<Faction> OnEventRaised;
        
        public void RaiseEvent(Faction value) {
            OnEventRaised?.Invoke(value);
        }
    }
}