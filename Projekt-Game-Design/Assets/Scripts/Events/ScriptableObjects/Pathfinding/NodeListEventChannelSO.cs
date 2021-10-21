using System;
using System.Collections.Generic;
using Events.ScriptableObjects.Core;
using UnityEngine;
using Util;

namespace Events.ScriptableObjects {
	/// <summary>
	/// Event that gives a list of graph-nodes 
	/// </summary>
	[CreateAssetMenu(menuName = "Events/NodeListEventChannel")]
	public class NodeListEventChannelSO : EventChannelBaseSO {
		public event Action<List<PathNode>> OnEventRaised;

		public void RaiseEvent(List<PathNode> nodes) {
			if ( OnEventRaised != null )
				OnEventRaised.Invoke(nodes);
		}
	}
}