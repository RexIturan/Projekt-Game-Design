using System;
using Events.ScriptableObjects.Core;
using UnityEngine;
using Util;

namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/PathNodeEventChannel")]
    public class PathNodeEventChannelSO : EventChannelBaseSO
    {

        public event Action<PathNode> OnEventRaised;

        public void RaiseEvent(PathNode value)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(value);
        }

    }
}