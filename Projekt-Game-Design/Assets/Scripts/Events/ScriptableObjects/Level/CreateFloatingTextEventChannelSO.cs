using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

// makes floating text spawner spawn a floating text message
//
namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/CreateFloatingTextEventChannel")]
    public class CreateFloatingTextEventChannelSO : EventChannelBaseSO
    {

        public event Action<String, Vector3, Color> OnEventRaised;

        public void RaiseEvent(String text, Vector3 position, Color color)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(text, position, color);
        }

    }
}