
using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

// event calls Pathfinder. Pathfinder calculates Pathnodes and calls 
// callback method with Path as an argument
//
namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/FieldOfViewQueryEventChannel")]
    public class FieldOfViewQueryEventChannelSO : EventChannelBaseSO
    {

        public event Action<Vector3Int, int, Action<bool[,]>> OnEventRaised;

        public void RaiseEvent(Vector3Int startNode, int range, Action<bool[,]> callback)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(startNode, range, callback);
        }

    }
}
