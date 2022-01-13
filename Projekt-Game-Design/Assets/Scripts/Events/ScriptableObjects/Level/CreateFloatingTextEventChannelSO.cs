using System;
using Events.ScriptableObjects.Core;
using UnityEngine;


namespace Events.ScriptableObjects
{
	/// <summary>
	/// makes floating text spawner spawn a floating text message
	/// </summary>
    // TODO rename
    [CreateAssetMenu(menuName = "Events/Visual/CreateFloatingTextEventChannel")]
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
