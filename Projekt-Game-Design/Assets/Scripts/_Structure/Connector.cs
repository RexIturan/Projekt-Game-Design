using System;
using Events.ScriptableObjects;
using UnityEngine;

namespace GDP01.Structure {
	public class Connector : MonoBehaviour {
		[SerializeField] private LevelDataSO levelData;
		[SerializeField] private LevelConnectorSO connectorData;
		[SerializeField] private IntEventChannelSO MoveTroughConnectorEC;

		public void UseConnector() {
			MoveTroughConnectorEC.RaiseEvent(connectorData.Id);
		}

		private void OnEnable() {
			for ( int i = 0; i < transform.childCount; i++ ) {
				Transform child = transform.GetChild(i);
				if ( Camera.main is {} ) {
					child.GetComponentInChildren<Canvas>().worldCamera = Camera.main;	
				}
			}
		}
	}
}