using System;
using Events.ScriptableObjects;
using SaveSystem.V2.Data;
using UnityEngine;

namespace SaveSystem.V2.TestComponents {
	public struct CollectableData {
		public Vector3 Position { get; set; }
		public int Amount { get; set; }
		public bool Active { get; set; }
	} 
	
	public class Collectable : MonoBehaviour, ISaveState<CollectableData> {

		[SerializeField] private int amount = 1;
		[SerializeField] private IntEventChannelSO collectEC;

		private void OnEnable() {
			GetComponentInChildren<Canvas>().worldCamera = Camera.main;
		}

		public void Collect() {
			collectEC.RaiseEvent(amount);
			gameObject.SetActive(false);
		}

		public CollectableData Save() {
			return new CollectableData {
				Position = transform.position,
				Amount = amount,
				Active = gameObject.activeInHierarchy
			};
		}

		public void Load(CollectableData data) {
			transform.position = data.Position;
			amount = data.Amount;
			gameObject.SetActive(data.Active);
		}
	}
}