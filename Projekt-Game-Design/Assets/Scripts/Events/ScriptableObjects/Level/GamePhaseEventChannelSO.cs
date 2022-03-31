using System;
using Events.ScriptableObjects.Core;
using GDP01;
using UnityEngine;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/Level/GamePhaseEventChannel")]
    public class GamePhaseEventChannelSO : EventChannelBaseSO
    {
        public event Action<GamePhase> OnEventRaised;

        public void RaiseEvent(GamePhase phase)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(phase);
        }

    }
}
