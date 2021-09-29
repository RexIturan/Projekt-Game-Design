using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

// event calls Pathfinder. Pathfinder calculates Pathnodes and calls 
// callback method with Path as an argument
//
namespace Events.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Events/PathFindingPathQueryEventChannel")]
    public class PathFindingPathQueryEventChannelSO : EventChannelBaseSO
    {

        public event Action<Vector3Int, Vector3Int, Action<List<PathNode>>> OnEventRaised;

        public void RaiseEvent(Vector3Int startNode, Vector3Int endtNode, Action<List<PathNode>> callback)
        {
            if (OnEventRaised != null)
                OnEventRaised.Invoke(startNode, endtNode, callback);
        }

    }
}