using System;
using Events.ScriptableObjects.Core;
using Level.Grid;
using UnityEngine;

namespace Events.ScriptableObjects.FieldOfView {
    [CreateAssetMenu(fileName = "FOV_Cross_Query_EventChannel", menuName = "Events/FOV/FOV_Cross_Query_EventChannel", order = 1)]
    public class FOVCrossQueryEventChannelSO : EventChannelBaseSO {
        // grid pos, blocking, callback
        public event Action<Vector3Int, TileProperties, Action<bool[,]>> OnEventRaised;

        /// <param name="startNode">Vector3Int Grid Pos</param>
        /// <param name="blocking">TileFlags</param>
        /// <param name="callback">Action &lt;bool[,]&gt;</param>
        public void RaiseEvent(Vector3Int startNode, TileProperties blocking, Action<bool[,]> callback) {
            OnEventRaised?.Invoke(startNode, blocking, callback);
        }
    }
}