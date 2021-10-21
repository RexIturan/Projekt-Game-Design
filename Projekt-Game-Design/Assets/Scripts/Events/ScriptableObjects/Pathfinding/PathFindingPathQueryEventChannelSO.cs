using System;
using System.Collections.Generic;
using Events.ScriptableObjects.Core;
using UnityEngine;
using Util;

namespace Events.ScriptableObjects {
	/// <summary>
	/// event calls Pathfinder. Pathfinder calculates Pathnodes and calls
	/// callback method with Path as an argument
	/// </summary>
	[CreateAssetMenu(menuName = "Events/PathFindingPathQueryEventChannel")]
	public class PathFindingPathQueryEventChannelSO : EventChannelBaseSO {
		
		public event Action<Vector3Int, Vector3Int, Action<List<PathNode>>> OnEventRaised;

		public void RaiseEvent(Vector3Int startNode, Vector3Int endNode,
			Action<List<PathNode>> callback) {
			OnEventRaised?.Invoke(startNode, endNode, callback);
		}
	}
}