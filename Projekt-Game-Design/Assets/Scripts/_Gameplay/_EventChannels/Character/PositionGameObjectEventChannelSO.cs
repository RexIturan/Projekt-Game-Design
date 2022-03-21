using System;
using Characters.Types;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects.GameState {
	[CreateAssetMenu(fileName = "PositionGameObjectEventChannelSO", menuName = "Events/Character/Position GameObject Event Channel")]
	public class PositionGameObjectEventChannelSO : EventChannelBaseSO {
		public event Action<Vector3Int, GameObject> OnEventRaised;
        
		public void RaiseEvent(Vector3Int gridPos, GameObject gameObject) {
			OnEventRaised?.Invoke(gridPos, gameObject);
		}
	}
}
