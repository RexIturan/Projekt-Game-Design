using System;
using Events.ScriptableObjects.Core;
using Level.Grid;
using UnityEngine;

namespace Events.ScriptableObjects.FieldOfView {
    [CreateAssetMenu(fileName = "FOV_Query_EventChannel", menuName = "Events/FOV/FOV_Query_EventChannel", order = 0)]
    public class FOV_Query_EventChannelSO : EventChannelBaseSO {
        // grid pos, range, blocking, callback
        public event Action<Vector3Int, int, ETileFlags, Action<bool[,]>> OnEventRaised;

        public void RaiseEvent(Vector3Int startNode, int range, ETileFlags blocking, Action<bool[,]> callback) {
            OnEventRaised?.Invoke(startNode, range, blocking, callback);
        }

    }
}