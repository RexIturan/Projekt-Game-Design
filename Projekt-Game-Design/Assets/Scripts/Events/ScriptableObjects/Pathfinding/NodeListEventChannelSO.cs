using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

// event that gives a list of graph-nodes
//
namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/NodeListEventChannel")]
    public class NodeListEventChannelSO : EventChannelBaseSO
    {
        public event Action<List<PathNode>> OnEventRaised;

        public void RaiseEvent(List<PathNode> nodes)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(nodes);
        }
    }
}