using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

// event calls Pathfinder. Pathfinder calculates Pathnodes and calls 
// callback method with Path as an argument
//
namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/PathfindingQueryEventChannel")]
    public class PathfindingQueryEventChannelSO : EventChannelBaseSO
    {

        public event Action<Vector3Int, int, Action<List<PathNode>>> OnEventRaised;

        public void RaiseEvent(Vector3Int startNode, int distance, Action<List<PathNode>> callback)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(startNode, distance, callback);
        }

    }
}