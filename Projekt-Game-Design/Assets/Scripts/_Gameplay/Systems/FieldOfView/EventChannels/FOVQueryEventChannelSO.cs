using System;
using Events.ScriptableObjects.Core;
using Level.Grid;
using UnityEngine;

namespace Events.ScriptableObjects.FieldOfView {
    [CreateAssetMenu(fileName = "FOV_Query_EventChannel", menuName = "Events/FOV/FOV_Query_EventChannel", order = 0)]
    public class FOVQueryEventChannelSO : EventChannelBaseSO {
        // grid pos, range, blocking, callback
        public event Action<Vector3Int, int, TileProperties, Action<bool[,]>> OnEventRaised;

        /// <param name="startNode">Vector3Int Grid Pos</param>
        /// <param name="range">int</param>
        /// <param name="blocking">TileFlags</param>
        /// <param name="callback">Action &lt;bool[,]&gt;</param>
        public void RaiseEvent(Vector3Int startNode, int range, TileProperties blocking, Action<bool[,]> callback) {
            OnEventRaised?.Invoke(startNode, range, blocking, callback);
        }
    }
}