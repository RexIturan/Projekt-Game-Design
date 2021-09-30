using System;
using UnityEngine;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/Vector3Int Event Channel")]
    public class Vector3IntEventChannelSO : EventChannelBaseSO
    {

        public event Action<Vector3Int> OnEventRaised;

        public void RaiseEvent(Vector3Int value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }

    }
}