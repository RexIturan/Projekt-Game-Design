using System;
using System.Collections.Generic;
using Events.ScriptableObjects.Core;
using UnityEngine;
using Util;

namespace Events.ScriptableObjects.Pathfinding {
    [CreateAssetMenu(fileName = "FindPathBatchEC", menuName = "Events/Pathfinding/Find Path Batch", order = 0)]
    public class FindPathBatch_EventChannel_SO : EventChannelBaseSO
    {
        public event Action<List<Tuple<Vector3Int, Vector3Int>>, Action<List<List<PathNode>>>> OnEventRaised;

        public void RaiseEvent(List<Tuple<Vector3Int, Vector3Int>> input, Action<List<List<PathNode>>> callback) {
            OnEventRaised?.Invoke(input, callback);
        }
    }
}