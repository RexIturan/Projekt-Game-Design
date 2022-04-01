using System;
using Characters.Types;
using Events.ScriptableObjects.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/Faction/Faction Event Channel")]
    public class FactionEventChannelSO : EventChannelBaseSO
    {
        public event Action<Faction> OnEventRaised;

        public void RaiseEvent(Faction faction)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(faction);
        }

    }
}
