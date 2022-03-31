using System;
using Audio;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects
{
	/// <summary>
	/// Event containing a SoundSO reference. 
	/// </summary>
    [CreateAssetMenu(menuName = "Events/Audio/SoundEventChannelSO")]
    public class SoundEventChannelSO : EventChannelBaseSO
    {
        public event Action<SoundSO> OnEventRaised;

        public void RaiseEvent(SoundSO sound)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(sound);
        }

    }
}
